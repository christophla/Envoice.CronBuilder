using System;
using System.Linq;

namespace Envoice.CronBuilder.CronTabs
{
    internal class SecondsTab : CronTab, IComparable, IComparable<SecondsTab>
    {
        public SecondsTab(CronBuilder builder) : base(builder)
        {
        }

        public int CompareTo(object value)
        {
            if (value == null) return 1;
            if (!(value is SecondsTab))
            {
                throw new ArgumentException("Argument value must be type of SecondsTab.");
            }

            return ((SecondsTab)value).Value == Value ? 1 : 0;
        }

        public int CompareTo(SecondsTab value)
        {
            return ((SecondsTab)value).Value == Value ? 1 : 0;
        }

        protected override void SetIntervalInternal(int interval)
        {
            if (interval < 1)
                throw new CronException("Seconds cannot be less than 1");

            if (interval > 60)
                throw new CronException("Seconds can not be greater than 24");

            var offset = (Builder.StartTime.HasValue) ? (int)Builder.StartTime.Value.Second : 0;

            Value = $"{offset}/{interval}";
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
