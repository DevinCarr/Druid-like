using HarmonyLib;
using RimWorld;
using Verse;

namespace Biogeneration.Druidlike.Harmony
{
    [HarmonyPatch(typeof(InteractionUtility))]
    public static class InteractionUtilityPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("CanInitiateRandomInteraction")]
        public static bool CanInitiateRandomInteractionPatch(ref bool __result, Pawn p)
        {
            if (p.IsColonist && p.story.traits.DegreeOfTrait(DruidlikeDefOf.Loner) > 0)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
