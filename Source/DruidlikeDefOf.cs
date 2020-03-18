using RimWorld;

namespace Biogeneration.Druidlike
{
    [DefOf]
    public static class DruidlikeDefOf
    {
        public static TraitDef AnimalLover;
        public static TraitDef Loner;

        public static ThoughtDef LonerInRelationship;
        public static ThoughtDef IsolatedLoner;

        static DruidlikeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DruidlikeDefOf));
        }
    }
}
