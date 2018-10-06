using System;
using Shouldly;
using Xbehave;
using Xunit;

namespace Envoice.CronBuilder.Tests
{
    public class CronBuilderTests
    {
        [Scenario]
        [Example(1, "* * * ? * 1/1 *")]
        [Example(7, "* * * ? * 1/7 *")]
        public void Cron_Build_With_Daily_Week(int interval, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a daily recurrence is set"
                .x(() => builder.WithDaily(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(1, "* * * ? * 6/1 *")]
        [Example(7, "* * * ? * 6/7 *")]
        public void Cron_Build_With_Daily_StartDate(int interval, string crontab, DateTime startDate, CronBuilder builder)
        {
            "Given a cron builder and a start date"
                .x(() =>
                {
                    startDate = new DateTime(2000, 1, 15);
                    builder = new CronBuilder(startDate);
                });

            "When a daily recurrence is set"
                .x(() => builder.WithDaily(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(-1)]
        [Example(0)]
        [Example(8)]
        public void Cron_Build_With_Daily_Exceptions(int interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a daily recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithDaily(interval)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(new[] { 1 }, "* * * 1 * ? *")]
        [Example(new[] { 1, 2 }, "* * * 1,2 * ? *")]
        [Example(new[] { 2, 1 }, "* * * 1,2 * ? *")]
        [Example(new[] { 1, 2, 2 }, "* * * 1,2 * ? *")]
        public void Cron_Build_With_Days_Of_Month(int[] daysOfMonth, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a days of month recurrence is set"
                .x(() => builder.WithDaysOfMonth(daysOfMonth));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(new[] { 0 })]
        [Example(new[] { -1 })]
        [Example(new[] { 32 })]
        [Example(new[] { 1, 2, -1 })]
        public void Cron_Build_With_Days_Of_Month_Exceptions(int[] daysOfMonth, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a days of month recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithDaysOfMonth(daysOfMonth)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(new[] { 1 }, "* * * ? * 1 *")]
        [Example(new[] { 1, 2 }, "* * * ? * 1,2 *")]
        [Example(new[] { 2, 1 }, "* * * ? * 1,2 *")]
        [Example(new[] { 1, 2, 2 }, "* * * ? * 1,2 *")]
        public void Cron_Build_With_Days_Of_Week(int[] daysOfWeek, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a days of week recurrence is set"
                .x(() => builder.WithDaysOfWeek(daysOfWeek));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(new[] { 0 })]
        [Example(new[] { -1 })]
        [Example(new[] { 8 })]
        [Example(new[] { 1, 2, -1 })]
        public void Cron_Build_With_Days_Of_Week_Exceptions(int[] daysOfWeek, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a days of week recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithDaysOfWeek(daysOfWeek)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(1, "* * 0/1 ? * * *")]
        [Example(2, "* * 0/2 ? * * *")]
        [Example(24, "* * 0/24 ? * * *")]
        public void Cron_Build_With_Hourly(int interval, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When an hourly recurrence is set"
                .x(() => builder.WithHourly(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(1, "* * 5/1 ? * * *")]
        [Example(2, "* * 5/2 ? * * *")]
        [Example(24, "* * 5/24 ? * * *")]
        public void Cron_Build_With_Hourly_StartDate(int interval, string crontab, DateTime startDate, CronBuilder builder)
        {
            "Given a cron builder and a start date"
                .x(() =>
                {
                    startDate = new DateTime(2000, 1, 1, 5, 1, 1);
                    builder = new CronBuilder(startDate);
                });

            "When an hourly recurrence is set"
                .x(() => builder.WithHourly(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(0)]
        [Example(-1)]
        [Example(25)]
        public void Cron_Build_With_Hourly_Exceptions(int interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When an hourly recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithHourly(interval)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(new[] { 0 }, "* * 0 ? * * *")]
        [Example(new[] { 0, 1 }, "* * 0,1 ? * * *")]
        [Example(new[] { 1, 0 }, "* * 0,1 ? * * *")]
        [Example(new[] { 1, 1, 0 }, "* * 0,1 ? * * *")]
        public void Cron_Build_With_Hours(int[] interval, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When an hours recurrence is set"
                .x(() => builder.WithHours(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(new[] { -1 })]
        [Example(new[] { 24 })]
        [Example(new[] { 0, 24 })]
        [Example(new[] { -1, 0, 1 })]
        [Example(new[] { 1, 0, -1 })]
        public void Cron_Build_With_Hours_Exceptions(int[] interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When an hours recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithHours(interval)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(1, "* 0/1 * ? * * *")]
        [Example(2, "* 0/2 * ? * * *")]
        [Example(60, "* 0/60 * ? * * *")]
        public void Cron_Build_With_Minutely(int interval, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a minutely recurrence is set"
                .x(() => builder.WithMinutely(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(1, "* 5/1 * ? * * *")]
        [Example(2, "* 5/2 * ? * * *")]
        [Example(60, "* 5/60 * ? * * *")]
        public void Cron_Build_With_Minutely_StartDate(int interval, string crontab, DateTime startDate, CronBuilder builder)
        {
            "Given a cron builder and a start date"
                .x(() =>
                {
                    startDate = new DateTime(2000, 1, 1, 1, 5, 1);
                    builder = new CronBuilder(startDate);
                });

            "When a minutely recurrence is set"
                .x(() => builder.WithMinutely(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(0)]
        [Example(-1)]
        [Example(61)]
        public void Cron_Build_With_Minutely_Exceptions(int interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a minutely recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithMinutely(interval)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(new[] { 0 }, "* 0 * ? * * *")]
        [Example(new[] { 0, 1 }, "* 0,1 * ? * * *")]
        [Example(new[] { 1, 0 }, "* 0,1 * ? * * *")]
        [Example(new[] { 1, 1, 0 }, "* 0,1 * ? * * *")]
        public void Cron_Build_With_Minutes(int[] interval, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a minutes recurrence is set"
                .x(() => builder.WithMinutes(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(new[] { -1 })]
        [Example(new[] { 60 })]
        [Example(new[] { 0, 60 })]
        [Example(new[] { -1, 0, 1 })]
        [Example(new[] { 1, 0, -1 })]
        public void Cron_Build_With_Minutes_Exceptions(int[] interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a minutes recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithMinutes(interval)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(1, "* * * ? 1/1 * *")]
        [Example(2, "* * * ? 1/2 * *")]
        [Example(12, "* * * ? 1/12 * *")]
        public void Cron_Build_With_Monthly(int interval, string crontab, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a minutely recurrence is set"
                .x(() => builder.WithMonthly(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(1, "* * * ? 5/1 * *")]
        [Example(2, "* * * ? 5/2 * *")]
        [Example(12, "* * * ? 5/12 * *")]
        public void Cron_Build_With_Monthly_StartDate(int interval, string crontab, DateTime startDate, CronBuilder builder)
        {
            "Given a cron builder and a start date"
                .x(() =>
                {
                    startDate = new DateTime(2000, 5, 1);
                    builder = new CronBuilder(startDate);
                });

            "When a minutely recurrence is set"
                .x(() => builder.WithMonthly(interval));

            "The cron statement should be"
                .x(() => builder.ToString().ShouldBe(crontab));
        }

        [Scenario]
        [Example(0)]
        [Example(-1)]
        [Example(13)]
        public void Cron_Build_With_Monthly_Exceptions(int interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a minutely recurrence is set"
                .x(() => ex = Record.Exception(() => builder.WithMonthly(interval)));

            "Then an exception is thrown"
                .x(() => Assert.IsType<CronException>(ex));
        }

        [Scenario]
        [Example(1, "* * * ? * 1/1 *", null)]
        [Example(1, "* * * ? * 1/1 *", "D")]
        [Example(1, "* * ? * 1/1 *", "S")]
        public void Cron_Build_With_Output_Formats(int interval, string crontab, string format, CronBuilder builder)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When a daily recurrence is set"
                .x(() => builder.WithDaily(interval));

            "The cron statement should be"
                .x(() => builder.ToString(format).ShouldBe(crontab));
        }

        [Scenario]
        public void Cron_Build_With_Invalid_Output_Format(int interval, CronBuilder builder, Exception ex)
        {
            "Given a cron builder"
                .x(() => builder = new CronBuilder());

            "When an invalid format is used"
                .x(() => ex = Record.Exception(() => builder.ToString("W")));

            "Then a format exception is thrown"
                .x(() => Assert.IsType<FormatException>(ex));
        }
    }
}
