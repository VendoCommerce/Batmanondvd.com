using System;


namespace CSCore
{
    /// <summary>
    /// A utility class that aids in manipulation of date and time values
    /// For example given a specific month and year, whats the last date of the month.  
    /// </summary>
    /// 
    public enum Quarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public class DateTimeUtil
    {
        public DateTimeUtil() { }

        public static string GetDateFormatLabel(string culture)
        {
            string label = "";
            if (culture == "en-US")
                label = "mm/dd/yyyy";
            else if (culture == "en-GB")
                label = "dd/mm/yyyy";
            return label;
        }

        public static DateTime? GetEastCoastStartDate(DateTime? input)
        {
            DateTime? val = input;
            if (val.HasValue)
                val = val.Value.AddDays(-1).AddHours(21).AddMinutes(00).AddSeconds(00);

            return val;
        }

        public static DateTime? GetEastCoastDate(DateTime? input)
        {
            DateTime? val = input;
            if (val.HasValue)
                val = val.Value.AddHours(21).AddMinutes(00).AddSeconds(00);

            return val;
        }

        public static DateTime? GetEndDate(DateTime? input)
        {
            DateTime? val = input;
            if (val.HasValue)
                val = val.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

            return val;
        }



        public static DateTime AddWeekDay(DateTime date, int days)
        {
            // add days which are considered holidays
            DayOfWeek[] weekEnd = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };

            // a working day denotes -1
            int workingDay = -1;
            DateTime buffer = date;
            do
            {
                int day = Array.IndexOf(weekEnd, buffer.DayOfWeek);
                if (day == workingDay)
                {
                    days--;
                }

                if (days == 0)
                {
                    break;
                }

                buffer = buffer.AddDays(1);

            } while (true);

            return buffer;
        }

        // Returns the number of days in a specific month
        private static int DaysInMonth(int year, int month)
        {
            return System.DateTime.DaysInMonth(year, month);

        }

        public static int DiffWeekdays(DateTime day1, DateTime day2)
        {
            // add days which are considered holidays
            DayOfWeek[] weekEnd = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };

            // a working day denotes -1
            int workingDay = -1;
            DateTime buffer = day1;
            int days = 0;
            while (buffer <= day2)
            {
                int day = Array.IndexOf(weekEnd, buffer.DayOfWeek);
                if (day == workingDay)
                {
                    days++;
                }

                buffer = buffer.AddDays(1);
            }

            return days;
        }

        public static int DiffDates(string unit, DateTime day1, DateTime day2)
        {
            int intDiff = 0;
            TimeSpan ts = day1 - day2;

            switch (unit)
            {
                case "d":
                    intDiff = ts.Days;
                    break;
                case "h":
                    intDiff = ts.Hours;
                    break;
                case "m":
                    intDiff = ts.Minutes;
                    break;
                case "s":
                    intDiff = ts.Seconds;
                    break;
                case "ms":
                    intDiff = ts.Milliseconds;
                    break;
            }

            return intDiff;
        }

        // Returns the number of whole weeks between two dates
        // The week is rounded down-it takes 7 whole days to make a week.
        private static double DiffWeeks(DateTime day1, DateTime day2)
        {
            return System.Math.Floor
                ((day2.ToOADate() - day1.ToOADate()) / 7); ;

        }

        // Return boolean value if year is a leap year or not 
        private static bool LeapYear(int year)
        {
            // Use 4 digit exact year for accuracy
            return System.DateTime.IsLeapYear(year);
        }


        public static DateTime NextMonthFirstDay(DateTime day)
        {
            DateTime ret = new DateTime(day.Year, day.Month, 1);
            return ret.AddMonths(1);
        }

        public static DateTime NextMonthLastDay(DateTime day)
        {
            // get first day of the month
            DateTime ret = new DateTime(day.Year, day.Month, 1);
            // get the next month
            ret = ret.AddMonths(2);
            // subtract one day from this (next) month
            ret = ret.Subtract(new TimeSpan(1, 0, 0, 0, 0));
            return ret;
        }

        public static DateTime PriorMonthFirstDay(DateTime day)
        {
            DateTime ret = new DateTime(day.Year, day.Month, 1);
            return ret.AddMonths(-1);
        }

        public static DateTime PriorMonthLastDay(DateTime day)
        {
            // get first day of the month
            DateTime ret = new DateTime(day.Year, day.Month, 1);
            // subtract one day from this (next) month
            ret = ret.Subtract(new TimeSpan(1, 0, 0, 0, 0));
            return ret;
        }

        public static DateTime MonthFirstDay(int month, int year)
        {
            return new DateTime(year, month, 1);
        }

        public static DateTime MonthLastDay(int month, int year)
        {
            // get first day of the month
            DateTime ret = new DateTime(year, month, 1);
            // get the next month
            ret = ret.AddMonths(1);
            // subtract one day from this (next) month
            ret = ret.Subtract(new TimeSpan(1, 0, 0, 0, 0));
            return ret;
        }

        public static DateTime MonthFirstWeekday(int month, int year)
        {
            DateTime ret = new DateTime(year, month, 1);
            while (
                ret.DayOfWeek == DayOfWeek.Saturday ||
                ret.DayOfWeek == DayOfWeek.Sunday)
            {
                ret = ret.AddDays(1);
            }
            return ret;
        }

        public static DateTime MonthLastWeekday(int month, int year)
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0, 0);
            // get first day of month
            DateTime ret = new DateTime(year, month, 1);
            // get the next month
            ret = ret.AddMonths(1);
            // subtract one day 
            ret = ret.Subtract(oneDay);
            while (
                ret.DayOfWeek == DayOfWeek.Saturday ||
                ret.DayOfWeek == DayOfWeek.Sunday)
            {
                ret = ret.Subtract(oneDay);
            }
            return ret;
        }
        // Date incremented by one, if no time argument is specified
        // Default is 12:00:00 AM
        public static DateTime NextDate(DateTime day)
        {
            return day.AddDays(1);
        }

        /// <summary>
        /// eg. NextDayOfWeek (#03/28/64#, 6) returns the date of the next friday after 03/28/64
        /// </summary>
        /// <param name="day">date to check</param>
        /// <param name="num">day of week to calculate (1=Sunday, 7=Saturday)</param>
        /// <returns>the specified day of the week after the given date</returns>
        public static DateTime NextDayOfWeek(DateTime day, int num)
        {
            return day.AddDays(num - ((int)day.DayOfWeek + 1) + (((int)day.DayOfWeek + 1) < num ? 0 : 7));
        }

        public static DateTime NextWeekday(DateTime day)
        {
            DateTime ret = day.AddDays(1);
            while (
                ret.DayOfWeek == DayOfWeek.Saturday ||
                ret.DayOfWeek == DayOfWeek.Sunday)
            {
                ret = ret.AddDays(1);
            }
            return ret;
        }

        // Decrements the specified date by 1, if no time argument is 
        // specified than Default time of 12:00:00 AM is passed
        public static DateTime PriorDate(DateTime day)
        {
            return day.AddDays(-1);
        }

        /// <summary>
        /// eg. PriorDayOfWeek (#03/28/64#, 6) returns the date of the Friday before 03/28/64 
        /// </summary>
        /// <param name="day">Date to check</param>
        /// <param name="num">Day of week to look for (1=Sunday, 7=Saturday)</param>
        /// <returns>the specified day of the week after the given date</returns>
        public static DateTime PriorDayOfWeek(DateTime day, int num)
        {
            // Parameters: day - 
            // num - 
            return day.AddDays(num - ((int)day.DayOfWeek + 1) - (((int)day.DayOfWeek + 1) > num ? 0 : 7));
        }

        public static DateTime PriorWeekday(DateTime day)
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0, 0);
            DateTime ret = day.Subtract(oneDay);

            while (
                ret.DayOfWeek == DayOfWeek.Saturday ||
                ret.DayOfWeek == DayOfWeek.Sunday)
            {
                ret = ret.Subtract(oneDay);
            }

            return ret;
        }


        public static DateTime PriorQuarterFirstDay(DateTime date)
        {
            int quarter = (date.Month - 1) / 3 + 1;
            int firstMonthInQuarter = (3 * quarter) - 2;
            DateTime ret = new DateTime(date.Year, firstMonthInQuarter, 1);
            return ret.AddMonths(-3);
        }

        public static DateTime PriorQuarterLastDay(DateTime date)
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0, 0);
            // get the quarter the date lies in
            int quarter = (date.Month - 1) / 3 + 1;
            //    quarter : 1  2  3  4
            // Last month : 3  6  9  12
            int lastMonthInQuarter = quarter * 3;
            DateTime ret = new DateTime(date.Year, lastMonthInQuarter, 1);
            return ret.AddMonths(-2).Subtract(oneDay);
        }

        //Calculate Next quarter first date:Sridhar Sambangi
        public static DateTime NextQuarterFirstDay(DateTime date)
        {
            int quarter = (date.Month - 1) / 3 + 1;
            int firstMonthInQuarter = (3 * quarter) - 2;
            DateTime ret = new DateTime(date.Year, firstMonthInQuarter, 1);
            return ret.AddMonths(3);
        }

        public static DateTime NextQuarterLastDay(DateTime date)
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0, 0);
            // get the quarter the date lies in
            int quarter = (date.Month - 1) / 3 + 1;
            //    quarter : 1  2  3  4
            // Last month : 3  6  9  12
            int lastMonthInQuarter = quarter * 3;
            DateTime ret = new DateTime(date.Year, lastMonthInQuarter, 1);
            return ret.AddMonths(4).Subtract(oneDay);
        }

        //Calculate each quarter last date:Sridhar Sambangi
        public static DateTime QuarterLastDay(DateTime date, int quarter)
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0, 0);
            int lastMonthInQuarter = quarter * 3;
            DateTime ret = new DateTime(date.Year, lastMonthInQuarter, 1);
            // get last day of quarter
            ret = ret.AddMonths(1).Subtract(oneDay);
            return ret;
        }

        public static DateTime YearFirstDay(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime LastDayOfYear(DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        public static DateTime LastDays(DateTime day, int number)
        {
            return day.AddDays(-number);
        }

        public static DateTime NextDays(DateTime day, int number)
        {
            return day.AddDays(number);
        }

        public static DateTime PriorWeekFirstDay(DateTime day)
        {
            return day.AddDays(-((int)day.DayOfWeek + 7));
        }

        public static DateTime PriorWeekLastDay(DateTime day)
        {
            return day.AddDays(-(((int)day.DayOfWeek) + 1));
        }

        public static DateTime NextWeekFirstDay(DateTime day)
        {
            return day.AddDays((7 - (((int)day.DayOfWeek) + 1)) + 1);
        }

        public static DateTime NextWeekLastDay(DateTime day)
        {
            return day.AddDays((7 - (((int)day.DayOfWeek) + 1)) + 7);
        }

        public static DateTime WeekFirstDay(DateTime day)
        {

            return day.AddDays(-((int)day.DayOfWeek));
        }

        public static DateTime WeekLastDay(DateTime day)
        {
            return day.AddDays((7 - (((int)day.DayOfWeek) + 1)));
        }

        public static DateTime FirstDayOfLastYear()
        {
            return new DateTime(DateTime.UtcNow.Year - 1, 1, 1, 0, 0, 0);
        }

        public static DateTime LastDayOfLastYear()
        {
            return new DateTime(DateTime.UtcNow.Year - 1, 12, 31);
        }

        #region Quarters
        // Calculates the first day of the quarter for a given date.
        // (quarters start at the beginning of January, April,
        // July, October)
        public static DateTime QuarterFirstDay(DateTime date)
        {
            // get the quarter the date lies in
            int quarter = (date.Month - 1) / 3 + 1;
            //   quarter : 1  2  3  4
            // 1st month : 1  4  7  10
            int firstMonthInQuarter = (3 * quarter) - 2;
            return new DateTime(date.Year, firstMonthInQuarter, 1);
        }

        // For a given date, calculate the last day of the quarter
        // (quarters end at the end of March, June, September,
        // December)
        public static DateTime QuarterLastDay(DateTime date)
        {
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0, 0);
            // get the quarter the date lies in
            int quarter = (date.Month - 1) / 3 + 1;
            //    quarter : 1  2  3  4
            // Last month : 3  6  9  12
            int lastMonthInQuarter = quarter * 3;
            DateTime ret = new DateTime(date.Year, lastMonthInQuarter, 1);
            // get last day of quarter
            ret = ret.AddMonths(1).Subtract(oneDay);
            return ret;
        }

        public static DateTime GetStartOfQuarter(int Year, Quarter Qtr)
        {
            if (Qtr == Quarter.First)    // 1st Quarter = January 1 to March 31
                return new DateTime(Year, 1, 1, 0, 0, 0, 0);
            else if (Qtr == Quarter.Second) // 2nd Quarter = April 1 to June 30
                return new DateTime(Year, 4, 1, 0, 0, 0, 0);
            else if (Qtr == Quarter.Third) // 3rd Quarter = July 1 to September 30
                return new DateTime(Year, 7, 1, 0, 0, 0, 0);
            else // 4th Quarter = October 1 to December 31
                return new DateTime(Year, 10, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfQuarter(int Year, Quarter Qtr)
        {
            if (Qtr == Quarter.First)    // 1st Quarter = January 1 to March 31
                return new DateTime(Year, 3, DateTime.DaysInMonth(Year, 3));
            else if (Qtr == Quarter.Second) // 2nd Quarter = April 1 to June 30
                return new DateTime(Year, 6, DateTime.DaysInMonth(Year, 6));
            else if (Qtr == Quarter.Third) // 3rd Quarter = July 1 to September 30
                return new DateTime(Year, 9, DateTime.DaysInMonth(Year, 9));
            else // 4th Quarter = October 1 to December 31
                return new DateTime(Year, 12, DateTime.DaysInMonth(Year, 12));
        }

        public static Quarter GetQuarter(Month Month)
        {
            if (Month <= Month.March)
                // 1st Quarter = January 1 to March 31
                return Quarter.First;
            else if ((Month >= Month.April) && (Month <= Month.June))
                // 2nd Quarter = April 1 to June 30
                return Quarter.Second;
            else if ((Month >= Month.July) && (Month <= Month.September))
                // 3rd Quarter = July 1 to September 30
                return Quarter.Third;
            else // 4th Quarter = October 1 to December 31
                return Quarter.Fourth;
        }

        public static Quarter GetPreviousQuarter(Month Month)
        {
            if (Month <= Month.March)
                // 1st Quarter = January 1 to March 31
                return Quarter.Fourth;
            else if ((Month >= Month.April) && (Month <= Month.June))
                // 2nd Quarter = April 1 to June 30
                return Quarter.First;
            else if ((Month >= Month.July) && (Month <= Month.September))
                // 3rd Quarter = July 1 to September 30
                return Quarter.Second;
            else // 4th Quarter = October 1 to December 31
                return Quarter.Third;
        }

        public static DateTime GetEndOfLastQuarter(DateTime now)
        {
            if ((Month)now.Month <= Month.March)
                //go to last quarter of previous year
                return GetEndOfQuarter(now.Year - 1, Quarter.Fourth);
            else //return last quarter of current year
                return GetEndOfQuarter(now.Year,
                  GetQuarter((Month)now.Month));
        }

        public static DateTime GetStartOfLastQuarter(DateTime now)
        {
            if ((Month)now.Month <= Month.March)
                //go to last quarter of previous year
                return GetStartOfQuarter(now.Year - 1, Quarter.Fourth);
            else //return last quarter of current year
                return GetStartOfQuarter(now.Year,
                  GetQuarter((Month)now.Month));
        }

        public static DateTime GetStartOfCurrentQuarter(DateTime now)
        {
            return GetStartOfQuarter(now.Year,
                   GetQuarter((Month)now.Month));
        }

        public static DateTime GetEndOfCurrentQuarter(DateTime now)
        {
            return GetEndOfQuarter(now.Year,
                   GetQuarter((Month)now.Month));
        }


        public static DateTime GetStartOfPreviousQuarter(DateTime now)
        {
            if ((Month)now.Month <= Month.March)
                //go to last quarter of previous year
                return GetStartOfQuarter(now.Year - 1, Quarter.Fourth);
            else //return last quarter of current year
            {

                return GetStartOfQuarter(now.Year,
                  GetPreviousQuarter((Month)now.Month));
            }
        }

        public static DateTime GetEndOfPreviousQuarter(DateTime now)
        {
            if ((Month)now.Month <= Month.March)
                //go to last quarter of previous year
                return GetEndOfQuarter(now.Year - 1, Quarter.Fourth);
            else //return last quarter of current year
                return GetEndOfQuarter(now.Year,
                  GetPreviousQuarter((Month)now.Month));
        }

        #endregion
    }
}
