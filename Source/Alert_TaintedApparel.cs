using RimWorld;
using Verse;

namespace Biogeneration.Druidlike.Alerts
{
    public class Alert_TaintedApparel : Alert_Thought
    {
        protected override ThoughtDef Thought
        {
            get
            {
                return ThoughtDefOf.DeadMansApparel;
            }
        }

        public Alert_TaintedApparel()
        {
            this.defaultLabel = "AlertTaintedApparel".Translate();
            this.explanationKey = "AlertTaintedApparelDesc";
        }

        public override AlertReport GetReport()
        {
            if (!DruidlikeSettings.TaintedApparel)
            {
                return AlertReport.Inactive;
            }
            return base.GetReport();
        }
    }
}
