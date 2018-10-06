using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class HoursTab : CronTab
    {
        public HoursTab(CronBuilder builder) : base(builder)
        {
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Hours cannot be less than 1");

            if (interval > 24)
                throw new CronException("Hours can not be greater than 24");

            var offset = (Builder.StartTime.HasValue) ? (int)Builder.StartTime.Value.Hour : 0;

            Value = $"{offset}/{interval}";
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o < 0))
                throw new CronException("Hours cannot be negative");

            if (values.Any(o => o > 23))
                throw new CronException("Hours cannot be greater than 23");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
        }
    }
}
