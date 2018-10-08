using System;

namespace Envoice.CronBuilder.CronTabs
{
    internal abstract class CronTab : IEquatable<CronTab>
    {
        protected CronBuilder Builder;

        protected string Value { get; set; }

        protected CronTab(CronBuilder builder, CronTabMode mode = CronTabMode.All)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            Mode = mode;
        }

        protected abstract void SetIntervalInternal(int interval);
        protected abstract void SetValuesInternal(int[] values);

        public virtual void SetInterval(int interval)
        {
            Mode = CronTabMode.Value;
            SetIntervalInternal(interval);
        }

        public virtual void SetValues(int[] values)
        {
            Mode = CronTabMode.Value;
            SetValuesInternal(values);
        }

        public CronTabMode Mode { get; set; }

        public override string ToString()
        {
            switch (Mode)
            {
                case (CronTabMode.All):
                    return "*";
                case (CronTabMode.Startup):
                    return "?";
                case (CronTabMode.Value):
                    return Value;
                default:
                    return "*";
            }
        }

        public bool Equals(CronTab other)
        {
            return other.Value == Value;
        }
    }
}
