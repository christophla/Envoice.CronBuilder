using System;
using Envoice.CronBuilder.CronTabs;

namespace Envoice.CronBuilder
{
    /// <summary>
    /// Utility for building cron expressions.
    /// </summary>
    public class CronBuilder
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

        /// <summary>
        /// Returns the cron expression
        /// </summary>
        public override string ToString()
        {
            //minutes, hours, days, months, and days of week
            return $"{Minutes} {Hours} {DayOfMonth} {Month} {DayOfWeek} {Year}";
        }
    }
}
