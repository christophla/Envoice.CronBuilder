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
            if (interval <= 0)
                throw new CronException("Hours cannot be zero or negative");

            if (interval > 24)
                throw new CronException("Hours can not be greater than 24");

            builder.Hours.Value = $"0/{interval}";
            return builder;
        }

        public static CronBuilder WithHours(this CronBuilder builder, IList<int> hours)
        {
            if (hours == null || hours.Count == 0) return builder;

            if (hours.Any(o => o < 0))
                throw new CronException("Hours cannot be negative");

            if (hours.Any(o => o > 23))
                throw new CronException("Hours cannot be greater than 23");

            builder.Hours.Value = string.Join(",", hours.OrderBy(o => o).Distinct());

            return builder;
        }

        public static CronBuilder WithMinutes(this CronBuilder builder, IList<int> minutes)
        {
            if (minutes == null || minutes.Count == 0) return builder;

            if (minutes.Any(o => o < 0))
                throw new CronException("Minutes cannot be negative");

            if (minutes.Any(o => o > 59))
                throw new CronException("Minutes cannot be greater than 59");

            builder.Minutes.Value = string.Join(",", minutes.OrderBy(o => o).Distinct());

            return builder;
        }

        public static CronBuilder WithMinutely(this CronBuilder builder, int interval)
        {
            if (interval <= 0)
                throw new CronException("Minutes cannot be zero or negative");

            if (interval > 60)
                throw new CronException("Minutes cannot be greater than 60");

            builder.Minutes.Value = $"0/{interval}";
            return builder;
        }

        public static CronBuilder WithMonthly(this CronBuilder builder, int interval)
        {
            if (interval <= 0)
                throw new CronException("Months cannot be zero or negative");

            if (interval > 12)
                throw new CronException("Months cannot be greater than 12");

            builder.Month.Value = $"1/{interval}";
            builder.DayOfMonth.Type = CronTabType.Startup;
            return builder;
        }

        public static CronBuilder WithWeekly(this CronBuilder builder, int interval)
        {
            throw new NotSupportedException(); // TODO
        }
    }
}
