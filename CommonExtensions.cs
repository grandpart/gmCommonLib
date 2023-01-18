using System;
using System.Globalization;

namespace Zephry
{
    /// <summary>
    /// CommonExtensions static class. Zephry's standard extension set, applied mainly to sealed classes and primitive types
    /// </summary>
    /// <remarks>
    /// namespace Zephry.
    /// </remarks>
    public static class CommonExtensions
    {
        #region Month names long
        public static string[] MonthNamesLong =
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        public static string MonthNameLong(int aMonthIndex)
        {
            if (aMonthIndex < 1 || aMonthIndex > 12)
            {
                throw new ArgumentException("MonthIndex must be in the range 1 to 12");
            }
            return MonthNamesLong[aMonthIndex-1];
        }
        #endregion

        #region SortFormatFull

        /// <summary>
        /// Returns the string representation of a DateTime in "yyyy-MM-dd hh:mm:ss" format
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns>Formatted DateTime</returns>
        public static string SortFormatFull(this DateTime aDateTime)
        {
            return aDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        #region SortFormatStartOfday

        /// <summary>
        /// Appends zero time to the truncated value of a date
        /// </summary>
        /// <param name="aDateTime">A date time.</param>
        /// <returns>A string representation of DateTime in "yyyy-MM-dd 00:00:00" format</returns>
        public static string SortFormatStartOfDay(this DateTime aDateTime)
        {
            return SortFormatFull(aDateTime.Date);
        }

        #endregion

        #region SortFormatEndOfDay

        /// <summary>
        /// Appends full time to the truncated value of a date
        /// </summary>
        /// <param name="aDateTime">A date time.</param>
        /// <returns>A string representation of DateTime in "yyyy-MM-dd 23:59:59" format</returns>
        public static string SortFormatEndOfDay(this DateTime aDateTime)
        {
            return SortFormatFull(aDateTime.Date.Add(new TimeSpan(23, 59, 59)));
        }

        #endregion

        #region SortFormatDate

        /// <summary>
        /// Returns the string representation of a DateTime in "yyyy-MM-dd" format
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static string SortFormatDate(this DateTime aDateTime)
        {
            return aDateTime.ToString("yyyy-MM-dd");
        }

        #endregion

        #region FinPeriod from DateTime

        /// <summary>
        /// Returns the integer representation of a DateTime in yyyyMM format (the Financial Period)
        /// </summary>
        /// <param name="aDateTime"></param>
        /// <returns></returns>
        public static int FinPeriod(this DateTime aDateTime)
        {
            return (aDateTime.Year*100) + (aDateTime.Month);
        }

        #endregion

        #region FinPeriod Add

        /// <summary>
        /// Returns an input integer in Period format (yyyyMM) + 1 month
        /// </summary>
        /// <param name="aPeriod">A period.</param>
        /// <returns>Incremented Period</returns>
        public static int AddPeriod(this int aPeriod)
        {
            int vYear = aPeriod/100;
            int vMonth = aPeriod%100;
            if (vMonth < 1 || vMonth > 12)
            {
                throw new Exception(
                    String.Format(
                        "Integer \"{0}\" is not a valid Year/Month period in the format yyyyMM for AddPeriod operations",
                        vMonth));
            }
            vMonth++;
            if (vMonth > 12)
            {
                vYear++;
                vMonth = 1;
            }
            return (vYear*100) + vMonth;
        }

        /// <summary>
        /// Returns an input integer in Period format (yyyyMM) + n months
        /// </summary>
        /// <param name="aPeriod">A start period.</param>
        /// <param name="aPeriodsToAdd">Number of periods to add.</param>
        /// <returns>
        /// Incremented Period
        /// </returns>
        public static int AddPeriod(this int aPeriod, int aPeriodsToAdd)
        {
            int vPeriod = aPeriod;
            for (int i = 0; i < aPeriodsToAdd; i++)
            {
                vPeriod = vPeriod.AddPeriod();
            }
            return vPeriod;
        }

        #endregion

        #region FinPeriod Subtract

        /// <summary>
        /// Returns an input integer in Period format (yyyyMM) - 1 month
        /// </summary>
        /// <param name="aPeriod">A period.</param>
        /// <returns>Decremented Period</returns>
        public static int SubtractPeriod(this int aPeriod)
        {
            var vYear = aPeriod/100;
            var vMonth = aPeriod%100;
            if (vMonth < 1 || vMonth > 12)
            {
                throw new Exception(
                    String.Format(
                        "Integer \"{0}\" is not a valid Year/Month period in the format yyyyMM for SubtractPeriod operations",
                        vMonth));
            }
            vMonth--;
            if (vMonth < 1)
            {
                vYear--;
                vMonth = 12;
            }
            return (vYear*100) + vMonth;
        }

        /// <summary>
        /// Returns an input integer in Period format (yyyyMM) - n months
        /// </summary>
        /// <param name="aPeriod">A start period.</param>
        /// <param name="aPeriodsToSubtract"></param>
        /// <returns>
        /// Decremented Period
        /// </returns>
        public static int SubtractPeriod(this int aPeriod, int aPeriodsToSubtract)
        {
            var vPeriod = aPeriod;
            for (var i = 0; i < aPeriodsToSubtract; i++)
            {
                vPeriod = vPeriod.SubtractPeriod();
            }
            return vPeriod;
        }

        #endregion

        #region AddWeek

        /// <summary>
        /// Returns an input integer in week format (yyyyWW) + 1 week
        /// </summary>
        /// <param name="aWeek"></param>
        /// <returns>Incremented Week Period</returns>
        public static int AddWeek(this int aWeek)
        {
            int vYear = aWeek/100;
            int vWeek = aWeek%100;
            if (vWeek < 1 || vWeek > 53)
            {
                throw new Exception(
                    String.Format(
                        "Integer \"{0}\" is not a valid Year/Week period in the format yyyyWW for AddWeek operations",
                        vWeek));
            }
            vWeek++;
            if (vWeek > 53)
            {
                vYear++;
                vWeek = 1;
            }
            return (vYear*100) + vWeek;
        }

        #endregion

        #region WeekStart given a Week number in format yyyyww
        public static DateTime WeekStart(this int aWeekNumber)
        {
            return WeekStart(WeekThursday(aWeekNumber));
        }
        #endregion

        #region WeekEnd given a Week number in format yyyyww
        public static DateTime WeekEnd(this int aWeekNumber)
        {
            return WeekEnd(WeekThursday(aWeekNumber));
        }
        #endregion

        #region WeekThursday given a Week number in format yyyyww
        private static DateTime WeekThursday(int aWeekNumber)
        {
            var vJanOne = new DateTime(aWeekNumber / 100, 1, 1);
            var vDaysOffset = DayOfWeek.Thursday - vJanOne.DayOfWeek;
            var vFirstThursday = vJanOne.AddDays(vDaysOffset);
            var vCalendar = CultureInfo.CurrentCulture.Calendar;
            var vFirstWeek = vCalendar.GetWeekOfYear(vFirstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var vWeekNum = aWeekNumber % 100;
            if (vFirstWeek <= 1) { vWeekNum -= 1; }
            return vFirstThursday.AddDays(vWeekNum*7);
        }
        #endregion

        #region ToHoursMinutes

        /// <summary>
        /// Returns s string in the format hh:mm
        /// </summary>
        /// <param name="aMinutes"></param>
        /// <returns>Incremented Week Period</returns>
        public static string ToHoursMinutes(this int aMinutes)
        {
            int vHours = aMinutes/60;
            int vMinutes = aMinutes%60;

            return $"{vHours:0}:{vMinutes:00}";
        }

        #endregion

        #region ToHoursMinutesDecimal

        /// <summary>
        /// Returns decimal in the format hhh,mm
        /// </summary>
        /// <param name="aMinutes"></param>
        /// <returns>Incremented Week Period</returns>
        public static decimal ToHoursMinutesDecimal(this int aMinutes)
        {
            int vHours = aMinutes / 60;
            decimal vRemainingMinutes = (aMinutes % 60);
            decimal vMinuteFraction = vRemainingMinutes / 60;
            return decimal.Round((decimal)vHours + vMinuteFraction, 2);
        }

        #endregion

        public static DateTime DayStart(this DateTime aDateTime)
        {
            return new DateTime(aDateTime.Year, aDateTime.Month, aDateTime.Day, 0, 0, 0);
        }

        public static DateTime DayEnd(this DateTime aDateTime)
        {
            return new DateTime(aDateTime.Year, aDateTime.Month, aDateTime.Day, 23, 59, 59);
        }

        public static DateTime WeekStart(this DateTime aDateTime)
        {
            DateTime vDateTime = aDateTime.Subtract(TimeSpan.FromDays((int) aDateTime.DayOfWeek));
            return new DateTime(vDateTime.Year, vDateTime.Month, vDateTime.Day, 0, 0, 0);
        }

        public static DateTime WeekEnd(this DateTime aDateTime)
        {
            DateTime vDateTime = WeekStart(aDateTime).AddDays(6);
            return new DateTime(vDateTime.Year, vDateTime.Month, vDateTime.Day, 23, 59, 59);
        }

        public static DateTime MonthStart(this DateTime aDateTime)
        {
            return new DateTime(aDateTime.Year, aDateTime.Month, 1, 0, 0, 0);
        }

        public static DateTime MonthEnd(this DateTime aDateTime)
        {
            return new DateTime(aDateTime.Year, aDateTime.Month, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
        }

        public static DateTime PlusDays(this DateTime aDateTime, int aDays)
        {
            return new DateTime(aDateTime.Year, aDateTime.Month, aDateTime.Day, 23, 59, 59).AddDays(aDays);
        }

        public static DateTime MinusDays(this DateTime aDateTime, int aDays)
        {
            return new DateTime(aDateTime.Year, aDateTime.Month, aDateTime.Day, 23, 59, 59).AddDays(-aDays);
        }

        public static string IntToHex(this int aInt, int aLength, char aPad)
        {
            return aInt.ToString("X").PadLeft(aLength, aPad);
            //byte[] vKey = System.Text.Encoding.Unicode.GetBytes("to be agile is @not% always *y3y");
            //string vIvString = string.Format("{0}{1}", aLogonToken)
            //byte[] vIv = 
            //using (RijndaelManaged myRijndael = new RijndaelManaged())
            //{

            //    myRijndael.GenerateKey();
            //    myRijndael.GenerateIV();
            //    // Encrypt the string to an array of bytes. 
            //    byte[] mixed = Zephry.Scrabble.Mixit(aLogonToken.Token, myRijndael.Key, myRijndael.IV);

            //    // Decrypt the bytes to a string. 
            //    string vFixed = Zephry.Scrabble.Fixit(mixed, myRijndael.Key, myRijndael.IV);
            //}
        }

    }
}
