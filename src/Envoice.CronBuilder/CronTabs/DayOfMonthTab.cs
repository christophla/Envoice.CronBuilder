using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class DayOfMonthTab : CronTab
    {
        public DayOfMonthTab(CronBuilder builder) : base(builder, CronTabMode.Startup)
        {
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Days cannot be less than 1");

            if (interval > 7)
                throw new CronException("Days can not be greater than 7");

            Value = $"1/{interval}";
            Builder.DayOfWeek.Mode = CronTabMode.Startup;
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o <= 0))
                throw new CronException("Days of month can not be zero or negative");

            if (values.Any(o => o > 31))
                throw new CronException("Days of month can not be greater than 31");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
            Builder.DayOfWeek.Mode = CronTabMode.Startup;
        }
    }
}
