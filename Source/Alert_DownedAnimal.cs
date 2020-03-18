using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace Biogeneration.Druidlike.Alerts
{
    public class Alert_DownedAnimal : Alert
    {
        private const int AnimalSkillToAlert = 8;
        private List<Pawn> animalsNeedingRescueResult = new List<Pawn>();
        private List<Pawn> colonistWithAnimalRescueResult = new List<Pawn>();

        private List<Pawn> ColonistsWithAnimalRescue
        {
            get
            {
                colonistWithAnimalRescueResult.Clear();
                List<Pawn> allMaps_FreeColonists = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists;
                foreach (Pawn ableColonist in allMaps_FreeColonists)
                {
                    if (ableColonist.skills.skills.Count <= 0 || ableColonist.Downed || ableColonist.Dead )
                    {
                        continue;
                    }

                    if (ableColonist.story.traits.DegreeOfTrait(DruidlikeDefOf.AnimalLover) > 0 || ableColonist.skills.GetSkill(SkillDefOf.Animals).levelInt >= AnimalSkillToAlert)
                    {
                        colonistWithAnimalRescueResult.Add(ableColonist);
                    }
                }
                return colonistWithAnimalRescueResult;
            }
        }

        private List<Pawn> AnimalsNeedingRescue
        {
            get
            {
                animalsNeedingRescueResult.Clear();
                List<Pawn> allMaps_Spawned = PawnsFinder.AllMaps_Spawned;
                foreach (Pawn pawn in allMaps_Spawned)
                {
                    if (pawn.RaceProps.Animal && pawn.Faction == null && pawn.health.Downed)
                    {
                        animalsNeedingRescueResult.Add(pawn);
                    }
                }
                return animalsNeedingRescueResult;
            }
        }

        public override string GetLabel()
        {
            if (AnimalsNeedingRescue.Count == 1)
            {
                return "AlertDownedAnimal".Translate();
            }
            return "AlertDownedAnimals".Translate();
        }

        public override TaggedString GetExplanation()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Pawn dyingAnimal in AnimalsNeedingRescue.OrderByDescending(a => a.health.hediffSet.BleedRateTotal))
            {
                int bloodLoss = HealthUtility.TicksUntilDeathDueToBloodLoss(dyingAnimal);
                stringBuilder.Append("    " + dyingAnimal.LabelShortCap);
                stringBuilder.Append(" (" + "TimeToDeath".Translate(bloodLoss.ToStringTicksToPeriod(true, false, true, true).Colorize(ColoredText.WarningColor)) + ")");
                stringBuilder.AppendLine();
            }

            var firstColonistWithAnimalRescue = ColonistsWithAnimalRescue.FirstOrFallback();
            Log.Message($"Druidlike: colonist: {firstColonistWithAnimalRescue?.NameShortColored}");

            if (AnimalsNeedingRescue.Count == 1)
            {
                return "AlertDownedAnimalDesc".Translate(firstColonistWithAnimalRescue?.NameShortColored ?? "", stringBuilder.ToString());
            }
            return "AlertDownedAnimalsDesc".Translate(firstColonistWithAnimalRescue?.NameShortColored ?? "", stringBuilder.ToString());
        }

        public override AlertReport GetReport()
        {
            if (!DruidlikeSettings.DownedAnimalAwareness)
            {
                return AlertReport.Inactive;
            }
            if (ColonistsWithAnimalRescue.Count <= 0)
            {
                return AlertReport.Inactive;
            }

            return AlertReport.CulpritsAre(AnimalsNeedingRescue);
        }
    }
}
