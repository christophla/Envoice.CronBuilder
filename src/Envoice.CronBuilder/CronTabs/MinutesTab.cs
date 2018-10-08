using System;
using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class MinutesTab : CronTab, IComparable, IComparable<MinutesTab>
    {
        public MinutesTab(CronBuilder builder) : base(builder)
        {
        }

        public int CompareTo(object value)
        {
            if (value == null) return 1;
            if (!(value is MinutesTab))
            {
                throw new ArgumentException("Argument value must be type of MinutesTab.");
            }

            return ((MinutesTab)value).Value == Value ? 1 : 0;
        }

        public int CompareTo(MinutesTab value)
        {
            return ((MinutesTab)value).Value == Value ? 1 : 0;
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Minutes cannot be less than 1");

            if (interval > 60)
                throw new CronException("Minutes can not be greater than 60");

            var offset = (Builder.StartTime.HasValue) ? (int)Builder.StartTime.Value.Minute : 0;

            Value = $"{offset}/{interval}";
        }

        protected override void SetValuesInternal(int[] values)
        {
            if (values == null || values.Length == 0) return;

            if (values.Any(o => o < 0))
                throw new CronException("Minutes cannot be negative");

            if (values.Any(o => o > 59))
                throw new CronException("Minutes cannot be greater than 59");

            Value = string.Join(",", values.OrderBy(o => o).Distinct());
        }
    }
}
