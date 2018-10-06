using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class YearTab : CronTab
    {
        public YearTab(CronBuilder builder) : base(builder)
        {
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1979)
                throw new CronException("Years cannot be less than 1979");

            if (interval > 2099)
                throw new CronException("Seconds can not be greater than 2099");

            var offset = (Builder.StartTime.HasValue) ? (int)Builder.StartTime.Value.Year : 0;

            Value = $"{offset}/{interval}";
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o < 1979))
                throw new CronException("Seconds cannot be less than 1979");

            if (values.Any(o => o > 2099))
                throw new CronException("Seconds cannot be greater than 2099");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
        }
    }
}

