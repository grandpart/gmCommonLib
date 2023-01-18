using System;
using System.Transactions;
using System.Data.SqlClient;

namespace Zephry
{
    /// <summary>
    /// A static utility class with commonly used data, exchange, string, date and math methods
    /// </summary>
    public static class CommonUtils
    {
        #region ServerDate
        /// <summary>
        /// Returns the current DateTime of the database server identified by aSqlCommand
        /// </summary>
        /// <param name="aSqlCommand">A SQL command.</param>
        /// <returns></returns>
        public static DateTime ServerDate(SqlCommand aSqlCommand)
        {
            aSqlCommand.CommandType = System.Data.CommandType.Text;
            aSqlCommand.CommandText = "select CURRENT_TIMESTAMP";
            return (DateTime)aSqlCommand.ExecuteScalar();
        }
        #endregion

        #region WorkingDaysBetween
        public static int WorkingDaysBetween(DateTime aDateStart, DateTime aDateEnd)
        {
            var vDays = 0;
            for (var vDate = aDateStart; vDate <= aDateEnd; vDate = vDate.AddDays(1))
            {
                if (vDate.DayOfWeek != DayOfWeek.Saturday && vDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++vDays;
                }
            }
            //while (aDateStart <= aDateEnd)
            //{
            //    if (aDateStart.DayOfWeek != DayOfWeek.Saturday && aDateStart.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        ++vDays;
            //    }
            //    aDateStart = aDateStart.AddDays(1);
            //}
            return vDays;
        }
        #endregion

        #region int MinutesBetween
        public static int MinutesBetween(DateTime aDateStart, DateTime aDateEnd)
        {
            return Convert.ToInt32(aDateEnd.Subtract(aDateStart).TotalMinutes);
        }
        #endregion

        #region String TimeDifference
        public static string TimeDifference(DateTime aDate1, DateTime aDate2)
        {
            var vTimeSpan = aDate2.Subtract(aDate1);
            if (vTimeSpan.Days > 0)
                return $"{vTimeSpan.Days}d:{vTimeSpan.Hours}h:{vTimeSpan.Minutes}m:{vTimeSpan.Seconds}.{vTimeSpan.Milliseconds}s";
            if (vTimeSpan.Hours > 0)
                return $"{vTimeSpan.Hours}h:{vTimeSpan.Minutes}m:{vTimeSpan.Seconds}.{vTimeSpan.Milliseconds}s";
            return $"{vTimeSpan.Minutes}m:{vTimeSpan.Seconds}.{vTimeSpan.Milliseconds}s";
        }
        #endregion

        #region String ByteDisplay
        /// <summary>
        /// Display the byte size as a formatted string
        /// </summary>
        /// <param name="aBytes"></param>
        /// <returns></returns>
        public static string ByteDisplay(long aBytes)
        {
            if (aBytes > CommonConstants.Petrabyte)
                return string.Format("{0:#,##0.##} Petrabytes ({1:#,##0.##} bytes)", aBytes/CommonConstants.Petrabyte, Convert.ToDouble(aBytes));
            if (aBytes > CommonConstants.Terabyte)
                return string.Format("{0:#,##0.##} Terabytes ({1:#,##0.##} bytes)", aBytes/CommonConstants.Terabyte, Convert.ToDouble(aBytes));
            if (aBytes > CommonConstants.Gigabyte)
                return string.Format("{0:#,##0.##} Gigabytes ({1:#,##0.##} bytes)", aBytes/CommonConstants.Gigabyte, Convert.ToDouble(aBytes));
            if (aBytes > CommonConstants.Megabyte)
                return string.Format("{0:#,##0.##} Megabytes ({1:#,##0.##} bytes)", aBytes/CommonConstants.Megabyte, Convert.ToDouble(aBytes));
            return string.Format("{0:#,##0.##} Kilobytes ({1:#,##0.##} bytes)", aBytes/CommonConstants.Kilobyte, Convert.ToDouble(aBytes));
        }

        #endregion

        #region Decimal ConvertByBase

        /// <summary>
        /// Returns a Target Exchange value based on the Exchange Rates of Source and Target values as related to a Base value
        /// </summary>
        /// <param name="aVolume">The number of Target values to use in the calculation</param>
        /// <param name="aValue">The Source value</param>
        /// <param name="aSourceIsOneBase">The Exchange rate of a Source value to a Base value (where Base value = 1)</param>
        /// <param name="aTargetIsOneBase">The Exchange rate of a Target value to a Base value (where Base value = 1)</param>
        /// <returns></returns>
        public static decimal ConvertByBase(int aVolume, decimal aValue, double aSourceIsOneBase, double aTargetIsOneBase)
        {
            double vWork = aVolume * (double)aValue * (aTargetIsOneBase / aSourceIsOneBase);
            return Convert.ToDecimal(Math.Round(vWork, 2));
        }

        #endregion

        #region <T> DbValueTo

        /// <summary>
        /// Return a C# object of type T from a DB object, or a default C# object of type T if the DB object is DBNull
        /// </summary>
        /// <typeparam name="T">The C# Type to test and return.</typeparam>
        /// <param name="aObject">A DB object to convert.</param>
        /// <param name="aValueIfNull">A C# object of type T if aObject is DBNull.</param>
        /// <returns>A C# object of type T.</returns>
        public static T DbValueTo<T>(object aObject, T aValueIfNull)
        {
            return (aObject != DBNull.Value) ? (T)aObject : aValueIfNull;
        }

        #endregion

        #region <T> CsValueTo

        /// <summary>
        /// Return a DB object of type T from a C# object, or a default DB object of type T if the C# object is null
        /// </summary>
        /// <typeparam name="T">The DB Type to test and return.</typeparam>
        /// <param name="aObject">A C# object to convert.</param>
        /// <param name="aValueIfNull">A DB object of type T if aObject is null.</param>
        /// <returns>A DB object of type T.</returns>
        public static T CsValueTo<T>(object aObject, T aValueIfNull)
        {
            return (aObject != null) ? (T)aObject : aValueIfNull;
        }

        #endregion

        #region String DateClause take DateFilter
        /// <summary>
        /// DateClause with Filtert
        /// </summary>
        /// <param name="aDateFilter"></param>
        /// <returns></returns>
        public static string DateClause(DateFilter aDateFilter)
        {
            return DateClause(aDateFilter.DateOperator, aDateFilter.Date1, aDateFilter.Date2);
        }
        #endregion

        #region String DateClause takes arguments
        /// <summary>
        /// Returns an SQL Date clause given an operator and two dates.
        /// Probably belongs in common.
        /// </summary>
        /// <param name="aDateOperator">A date operator.</param>
        /// <param name="aDate1">A date1.</param>
        /// <param name="aDate2">A date2.</param>
        /// <returns></returns>
        public static string DateClause(DateOperator aDateOperator, DateTime aDate1, DateTime aDate2)
        {
            string vWork;
            switch (aDateOperator)
            {
                case DateOperator.LessThan:
                    vWork = String.Format("< '{0}'", aDate1.SortFormatStartOfDay());
                    break;
                case DateOperator.LessEqual:
                    vWork = String.Format("<= '{0}'", aDate1.SortFormatEndOfDay());
                    break;
                case DateOperator.GreaterThan:
                    vWork = String.Format("> '{0}'", aDate1.SortFormatEndOfDay());
                    break;
                case DateOperator.GreaterEqual:
                    vWork = String.Format(">= '{0}'", aDate1.SortFormatStartOfDay());
                    break;
                case DateOperator.Equal:
                    vWork = String.Format("between '{0}' and '{1}'", aDate1.SortFormatStartOfDay(), aDate1.SortFormatEndOfDay());
                    break;
                case DateOperator.Between:
                    vWork = String.Format("between '{0}' and '{1}'", aDate1.SortFormatStartOfDay(), aDate2.SortFormatEndOfDay());
                    break;
                default:
                    vWork = String.Format("between '{0}' and '{1}'", aDate1.SortFormatStartOfDay(), aDate1.SortFormatEndOfDay());
                    break;
            }
            return vWork;
        }
        #endregion

        #region AmountClause take AmountFilter
        /// <summary>
        /// AmountClause with Filtert
        /// </summary>
        /// <param name="aAmountFilter"></param>
        /// <returns></returns>
        public static string AmountClause(AmountFilter aAmountFilter)
        {
            return AmountClause(aAmountFilter.AmountOperator, aAmountFilter.Amount1, aAmountFilter.Amount2);
        }
        #endregion

        #region String AmountClause takes arguments

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aAmountOperator"></param>
        /// <param name="aAmount1"></param>
        /// <param name="aAmount2"></param>
        /// <returns></returns>
        public static string AmountClause(AmountOperator aAmountOperator, decimal aAmount1, decimal aAmount2)
        {
            string vWork;
            switch (aAmountOperator)
            {
                case AmountOperator.LessThan:
                    vWork = string.Format("< {0}", aAmount1);
                    break;
                case AmountOperator.LessEqual:
                    vWork = string.Format("<= {0}", aAmount1);
                    break;
                case AmountOperator.GreaterThan:
                    vWork = string.Format("> {0}", aAmount1);
                    break;
                case AmountOperator.GreaterEqual:
                    vWork = string.Format(">= {0}", aAmount1);
                    break;
                case AmountOperator.Equal:
                    vWork = string.Format("= {0}", aAmount1);
                    break;
                case AmountOperator.Between:
                    vWork = string.Format("between {0} and {1}", aAmount1, aAmount2);
                    break;
                default:
                    vWork = string.Format("= {0}", aAmount1);
                    break;
            }
            return vWork;
        }
        #endregion
        
        #region String FinPeriodClause takes FinPeriodFilter
        /// <summary>
        /// FinPeriodClause with Filter
        /// </summary>
        /// <param name="aFinPeriodFilter"></param>
        /// <returns></returns>
        public static string FinPeriodClause(FinPeriodFilter aFinPeriodFilter)
        {
            return FinPeriodClause(aFinPeriodFilter.FinPeriodOperator, aFinPeriodFilter.FinPeriod1, aFinPeriodFilter.FinPeriod2);
        }
        #endregion

        #region String FinPeriodClause
        /// <summary>
        /// Returns an SQL FinPeriod clause given an operator and two dates.
        /// Probably belongs in common.
        /// </summary>
        /// <param name="aFinPeriodOperator">A date operator.</param>
        /// <param name="aFinPeriod1">A date1.</param>
        /// <param name="aFinPeriod2">A date2.</param>
        /// <returns></returns>
        public static string FinPeriodClause(FinPeriodOperator aFinPeriodOperator, int aFinPeriod1, int aFinPeriod2)
        {
            string vWork;
            switch (aFinPeriodOperator)
            {
                case FinPeriodOperator.LessThan:
                    vWork = String.Format("< {0}", aFinPeriod1);
                    break;
                case FinPeriodOperator.LessEqual:
                    vWork = String.Format("<= {0}", aFinPeriod1);
                    break;
                case FinPeriodOperator.GreaterThan:
                    vWork = String.Format("> {0}", aFinPeriod1);
                    break;
                case FinPeriodOperator.GreaterEqual:
                    vWork = String.Format(">= {0}", aFinPeriod1);
                    break;
                case FinPeriodOperator.Equal:
                    vWork = String.Format("= {0}", aFinPeriod1);
                    break;
                case FinPeriodOperator.Between:
                    vWork = String.Format("between {0} and {1}", aFinPeriod1, aFinPeriod2);
                    break;
                default:
                    vWork = String.Format("= {0}", aFinPeriod1);
                    break;
            }
            return vWork;
        }
        #endregion
        
        #region Enum from String
        /// <summary>
        /// Parses a string value to an Enumerated ordinal of type T
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        #endregion

        #region Enum to String
        /// <summary>
        /// Parses an Enumerated ordinal of type T to its string representation
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns></returns>
        public static string ParseEnum<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }
        #endregion

        #region TransactionScope GetTransactionScope with Options
        /// <summary>
        /// Return a TransactionScope with the IsolationLevel option of aIsolationLevel and TransactionScopeOption of aTransactionScopeOption
        /// </summary>
        /// <param name="aIsolationLevel">An isolation level.</param>
        /// <param name="aTransactionScopeOption">A transaction scope option.</param>
        /// <returns>
        /// a TransactionScope
        /// </returns>
        public static TransactionScope GetTransactionScope(IsolationLevel aIsolationLevel, TransactionScopeOption aTransactionScopeOption)
        {
            var vTransactionOptions = new TransactionOptions() { IsolationLevel = aIsolationLevel, Timeout = TransactionManager.MaximumTimeout };
            return new TransactionScope(aTransactionScopeOption, vTransactionOptions);
        }
        #endregion

    }
}