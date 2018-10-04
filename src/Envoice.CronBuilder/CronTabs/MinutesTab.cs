using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class MinutesTab : CronTab
    {
        public MinutesTab(CronBuilder builder) : base(builder)
        {
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Minutes cannot be less than 1");

            if (interval > 24)
                throw new CronException("Minutes can not be greater than 24");

            Value = $"0/{interval}";
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o < 0))
                throw new CronException("Minutes cannot be negative");

            if (values.Any(o => o > 23))
                throw new CronException("Minutes cannot be greater than 23");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
        }
    }
}
