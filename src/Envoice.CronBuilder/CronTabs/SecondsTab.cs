using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class SecondsTab : CronTab
    {
        public SecondsTab(CronBuilder builder) : base(builder)
        {
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Seconds cannot be less than 1");

            if (interval > 60)
                throw new CronException("Seconds can not be greater than 24");

            Value = $"0/{interval}";
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o < 0))
                throw new CronException("Seconds cannot be negative");

            if (values.Any(o => o > 59))
                throw new CronException("Seconds cannot be greater than 59");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
        }
    }
}
