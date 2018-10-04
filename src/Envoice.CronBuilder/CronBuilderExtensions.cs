using System;
using System.Collections.Generic;
using System.Linq;

namespace Envoice.CronBuilder
{
    public static class CronBuilderExtensions
    {
        public static CronBuilder WithDaily(this CronBuilder builder, int interval)
        {
            builder.DayOfWeek.SetInterval(interval);
            return builder;
        }

        public static CronBuilder WithDaysOfMonth(this CronBuilder builder, int[] daysOfMonth)
        {
            builder.DayOfMonth.SetValues(daysOfMonth);
            return builder;
        }

        public static CronBuilder WithDaysOfMonth(this CronBuilder builder, IList<int> daysOfMonth)
        {
            builder.DayOfMonth.SetValues(daysOfMonth.ToArray());
            return builder;
        }

        public static CronBuilder WithDaysOfWeek(this CronBuilder builder, int[] weekdays)
        {
            builder.DayOfWeek.SetValues(weekdays);
            return builder;
        }

        public static CronBuilder WithDaysOfWeek(this CronBuilder builder, IList<int> weekdays)
        {
            builder.DayOfWeek.SetValues(weekdays.ToArray());
            return builder;
        }

        public static CronBuilder WithHourly(this CronBuilder builder, int interval)
        {
            builder.Hours.SetInterval(interval);
            return builder;
        }

        public static CronBuilder WithHours(this CronBuilder builder, IList<int> hours)
        {
            builder.Hours.SetValues(hours.ToArray());
            return builder;
        }

        public static CronBuilder WithMinutes(this CronBuilder builder, IList<int> minutes)
        {
            builder.Minutes.SetValues(minutes.ToArray());
            return builder;
        }

        public static CronBuilder WithMinutely(this CronBuilder builder, int interval)
        {
            builder.Hours.SetInterval(interval);
            return builder;
        }

        public static CronBuilder WithMonthly(this CronBuilder builder, int interval)
        {
            builder.Hours.SetInterval(interval);
            return builder;
        }

        public static CronBuilder WithWeekly(this CronBuilder builder, int interval)
        {
            throw new NotSupportedException(); // TODO
        }
    }
}
