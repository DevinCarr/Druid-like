using RimWorld;

namespace Biogeneration.Druidlike
{
    [DefOf]
    public static class DruidlikeDefOf
    {
        public static TraitDef AnimalLover;

        static DruidlikeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DruidlikeDefOf));
        }
    }
}
