using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class MonthTab : CronTab
    {
        public MonthTab(CronBuilder builder) : base(builder)
        {
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Months cannot be less than 1");

            if (interval > 12)
                throw new CronException("Months cannot be greater than 12");

            var offset = (Builder.StartTime.HasValue) ? (int)Builder.StartTime.Value.Month : 1;

            Value = $"{offset}/{interval}";
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o <= 0))
                throw new CronException("Months cannot be zero or negative");

            if (values.Any(o => o > 23))
                throw new CronException("Months cannot be greater than 12");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
        }
    }
}
