using RimWorld;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Verse;

namespace Biogeneration.Druidlike.ThoughtWorkers
{
    public class ThoughtWorker_IsSocialForLoner : ThoughtWorker
    {
        private static ConcurrentDictionary<string, int> lonerPawnsDebounce = new ConcurrentDictionary<string, int>();

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            // Make sure the pawn is human
            if (!p.RaceProps.Humanlike && p.interactions != null)
            {
                return ThoughtState.Inactive;
            }

            List<DirectPawnRelation> directRelations = p.relations.DirectRelations;
            foreach (var relations in directRelations)
            {
                if (relations.def == PawnRelationDefOf.Lover
                    || relations.def == PawnRelationDefOf.Fiance
                    || relations.def == PawnRelationDefOf.Spouse)
                {
                    return ThoughtState.ActiveAtStage(0);
                }
            }

            if (lonerPawnsDebounce.TryGetValue(p.ThingID, out int ticks))
            {
                if (Find.TickManager.TicksAbs > ticks + 4 * GenDate.TicksPerHour) // Wait 4 hours before checking IsolatedLoner status
                {
                    lonerPawnsDebounce.TryRemove(p.ThingID, out int _);
                }
                return ThoughtState.Inactive;
            }

            // This means that they interacted with someone recently
            if (p.interactions.InteractedTooRecentlyToInteract())
            {
                p.needs.mood.thoughts.memories.RemoveMemoriesOfDef(DruidlikeDefOf.IsolatedLoner);
                lonerPawnsDebounce.TryAdd(p.ThingID, Find.TickManager.TicksAbs);
            }
            else if (p.needs.mood.thoughts.memories.GetFirstMemoryOfDef(DruidlikeDefOf.IsolatedLoner) == null)
            {
                p.needs.mood.thoughts.memories.TryGainMemory(DruidlikeDefOf.IsolatedLoner, null);
            }

            return ThoughtState.Inactive;
        }
    }
}
