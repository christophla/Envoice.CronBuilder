using System;
using Envoice.CronBuilder.CronTabs;

namespace Envoice.CronBuilder
{
    /// <summary>
    /// Utility for building cron expressions.
    /// </summary>
    public class CronBuilder : IFormattable, IEquatable<CronBuilder>
    {
        internal CronTab DayOfMonth;
        internal CronTab DayOfWeek;
        internal CronTab Hours;
        internal CronTab Minutes;
        internal CronTab Month;
        internal CronTab Seconds;
        internal DateTime? StartTime;
        internal CronTab Year;

        /// <summary>
        ///     Creates a new cron builder instance.
        /// </summary>
        public CronBuilder()
        {
            DayOfMonth = new DayOfMonthTab(this);
            DayOfWeek = new DayOfWeekTab(this);
            Hours = new HoursTab(this);
            Minutes = new MinutesTab(this);
            Month = new MonthTab(this);
            Seconds = new SecondsTab(this);
            Year = new YearTab(this);
        }

        /// <summary>
        ///     Creates a new cron builder with given start time offset.
        /// </summary>
        /// <param name="startTime">The start time offset</param>
        public CronBuilder(DateTime startTime) : this()
        {
            StartTime = startTime;
        }

        public override bool Equals(object other)
        {
            if (other is CronBuilder)
            {
                return this.Equals((CronBuilder)other);
            }
            return false;
        }

        public bool Equals(CronBuilder other)
        {
            if (other == null || GetType() != other.GetType())
                return false;

            return
                this.DayOfMonth.Equals(other.DayOfMonth) &
                this.DayOfWeek.Equals(other.DayOfWeek) &
                this.Hours.Equals(other.Hours) &
                this.Minutes.Equals(other.Minutes) &
                this.Month.Equals(other.Month) &
                this.Seconds.Equals(other.Seconds) &
                this.Year.Equals(other.Year);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) ^ this.DayOfMonth.GetHashCode();
                hash = (hash * 7) ^ this.DayOfWeek.GetHashCode();
                hash = (hash * 7) ^ this.Hours.GetHashCode();
                hash = (hash * 7) ^ this.Minutes.GetHashCode();
                hash = (hash * 7) ^ this.Month.GetHashCode();
                hash = (hash * 7) ^ this.Seconds.GetHashCode();
                hash = (hash * 7) ^ this.Year.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        ///     Returns the cron expression
        /// </summary>
        public override string ToString()
        {
            //minutes, hours, days, months, and days of week
            return $"{Seconds} {Minutes} {Hours} {DayOfMonth} {Month} {DayOfWeek} {Year}";
        }

        /// <summary>
        ///     Returns the cron expression with formatting.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            if (format == null || format.Length == 0)
                format = "A";

            if (format.Length != 1)
            {
                // all acceptable format strings are of length 1
                throw new FormatException($"The format {format} can only be one character");
            }

            char formatCh = format[0];
            if (formatCh == 'A' || formatCh == 'a')
            {
                return $"{Seconds} {Minutes} {Hours} {DayOfMonth} {Month} {DayOfWeek} {Year}";
            }
            if (formatCh == 'S' || formatCh == 's')
            {
                return $"{Minutes} {Hours} {DayOfMonth} {Month} {DayOfWeek} {Year}";
            }
            if (formatCh == 'Y' || formatCh == 's')
            {
                return $"{Minutes} {Hours} {DayOfMonth} {Month} {DayOfWeek}";
            }
            else
            {
                throw new FormatException($"The format {format} is not valid");
            }
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

    }
}
