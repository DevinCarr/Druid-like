using UnityEngine;
using Verse;

namespace Biogeneration.Druidlike.Alerts
{
    public class DruidlikeSettings : ModSettings
    {
        public static bool TaintedApparel = true;
        public static bool DownedAnimalAwareness = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref TaintedApparel, "bd_taintedApparel", true);
            Scribe_Values.Look(ref DownedAnimalAwareness, "bd_downedAnimalAwareness", true);
        }
    }

    public class DruidlikeMod : Mod
    {
        DruidlikeSettings settings;

        public DruidlikeMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DruidlikeSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            inRect.width = 450f;
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("AlertTaintedApparel".Translate(), ref DruidlikeSettings.TaintedApparel, "AlertTaintedApparelDesc".Translate());
            listingStandard.CheckboxLabeled("AlertDownedAnimalsSettingLabel".Translate(), ref DruidlikeSettings.DownedAnimalAwareness, "AlertDownedAnimalsSettingDesc".Translate());
            listingStandard.End();

            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Druidlike_ModName".Translate();
        }
    }
}
