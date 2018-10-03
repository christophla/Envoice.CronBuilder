using System;

namespace Envoice.CronBuilder.CronTabs
{
    internal abstract class CronTab
    {
        protected CronBuilder Builder;

        protected abstract string Value { get; set; }

        protected CronTab(CronBuilder builder, CronTabMode mode = CronTabMode.All)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public abstract void SetInterval(int interval);

        public abstract void SetValues(int[] values);

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
    }
}
