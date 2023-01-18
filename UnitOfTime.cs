using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    #region UnitOfTimeAttribute Class
    /// <summary>
    /// An Enumeration Attribute class to persist base equality of Units of Timement.
    /// </summary>
    public class UnitOfTimeAttribute : Attribute
    {
        private double _ofBase;

        /// <summary>
        /// Gets or sets the equals.
        /// </summary>
        /// <value>
        /// The equals.
        /// </value>
        public double OfBase
        {
            get { return _ofBase; }
            set { _ofBase = value; }
        }

    }
    #endregion

    #region UnitOfTime Enumeration
    /// <summary>
    /// A Units of Timement enumeration decorated with conversion information
    /// </summary>
    public enum UnitOfTime
    {
        [UnitOfTime(OfBase = 1)]
        Day = 0,
        [UnitOfTime(OfBase = 0.142857143)]
        Week = 1,
        [UnitOfTime(OfBase = 0.032876712)]
        Month = 2,
        [UnitOfTime(OfBase = 0.002739726)]
        Year = 3
    }
    #endregion

    #region UnitConverter Class
    /// <summary>
    /// A Unit of Time conversion class
    /// </summary>
    public static class TimeConverter
    {
        /// <summary>
        /// Converts the number of source units to target units.
        /// </summary>
        /// <param name="aUnits">A number of units.</param>
        /// <param name="aFromUnitOfTime">A from unit of time.</param>
        /// <param name="aToUnitOfTime">A to unit of time.</param>
        /// <returns></returns>
        public static double Convert(double aUnits, UnitOfTime aFromUnitOfTime, UnitOfTime aToUnitOfTime)
        {
            var vEnumType = typeof(UnitOfTime);
            //
            var vFromEnumInfo = vEnumType.GetMember(aFromUnitOfTime.ToString());
            var vFromEnumAttributes = vFromEnumInfo[0].GetCustomAttributes(typeof(UnitOfTimeAttribute), false);
            var vFromOfBase = ((UnitOfTimeAttribute)vFromEnumAttributes[0]).OfBase;
            //
            var vToEnumInfo = vEnumType.GetMember(aToUnitOfTime.ToString());
            var vToEnumAttributes = vToEnumInfo[0].GetCustomAttributes(typeof(UnitOfTimeAttribute), false);
            var vToOfBase = ((UnitOfTimeAttribute)vToEnumAttributes[0]).OfBase;
            //
            return (aUnits / vFromOfBase) * vToOfBase;
        }

    }
    #endregion
}
