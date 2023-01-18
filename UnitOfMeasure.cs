using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    #region UnitOfMeasureAttribute Class
    /// <summary>
    /// An Enumeration Attribute class to persist base equality of Units of Measurement.
    /// </summary>
    public class UnitOfMeasureAttribute : Attribute
    {
        private UnitBase _base;
        private double _isEqualTo;

        /// <summary>
        /// Gets or sets the base unit.
        /// </summary>
        /// <value>
        /// The base unit.
        /// </value>
        public UnitBase Base
        {
            get { return _base; }
            set { _base = value; }
        }
        /// <summary>
        /// Gets or sets the equals.
        /// </summary>
        /// <value>
        /// The equals.
        /// </value>
        public double IsEqualTo
        {
            get { return _isEqualTo; }
            set { _isEqualTo = value; }
        }

    }
    #endregion

    public enum UnitBase
    {
        Weight,
        Volume,
        Area,
        Distance,
        Unit
    }

    #region UnitOfMeasure Enumeration
    /// <summary>
    /// A Units of Measurement enumeration decorated with conversion information
    /// </summary>
    public enum UnitOfMeasure
    {
        [UnitOfMeasure(Base = UnitBase.Unit, IsEqualTo = 1)]
        Unit=0,
        [UnitOfMeasure(Base = UnitBase.Weight, IsEqualTo = 1)]
        Kilogram=5,
        [UnitOfMeasure(Base = UnitBase.Weight, IsEqualTo = 2.2046226218)]
        Pound=10,
        [UnitOfMeasure(Base = UnitBase.Weight, IsEqualTo = 35.27396195)]
        Ounce=15,
        [UnitOfMeasure(Base = UnitBase.Weight, IsEqualTo = 0.15747304442)]
        Stone=20,
        [UnitOfMeasure(Base = UnitBase.Weight, IsEqualTo = 5000)]
        Carat=25,
        [UnitOfMeasure(Base = UnitBase.Volume, IsEqualTo = 1)]
        Liter=30,
        [UnitOfMeasure(Base = UnitBase.Volume, IsEqualTo = 1.7597539864)]
        Pint=35,
        [UnitOfMeasure(Base = UnitBase.Volume, IsEqualTo = 0.8798769932)]
        Quart=40,
        [UnitOfMeasure(Base = UnitBase.Volume, IsEqualTo = 0.2199692483)]
        Gallon=45,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 1)]
        Hectare = 50,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 2.4710538147)]
        Acre = 55,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 100)]
        Are = 60,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 1.167269756)]
        Morgen = 65,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 107639.10417)]
        SquareFoot = 70,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 10000)]
        SquareMeter = 75,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 0.0038610215855)]
        SquareMile = 80,
        [UnitOfMeasure(Base = UnitBase.Area, IsEqualTo = 0.0038610215855)]
        SquareYard = 85,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 1)]
        Meter = 90,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 100)]
        Centimeter = 95,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 0.001)]
        Kilometer = 100,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 3.280839895)]
        Feet = 105,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 39.37007874)]
        Inch = 110,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 1.0936132983)]
        Yard = 115,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 0.00062137119224)]
        Mile = 120,
        [UnitOfMeasure(Base = UnitBase.Distance, IsEqualTo = 0.00020712331461)]
        League = 125
    }
    #endregion

    #region UnitConverter Class
    /// <summary>
    /// A Unit of Measure conversion class
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// Converts the number of source units to target units.
        /// </summary>
        /// <param name="aUnits">A number of units.</param>
        /// <param name="aFromUnitOfMeasure">A from unit of measure.</param>
        /// <param name="aToUnitOfMeasure">A to unit of measure.</param>
        /// <returns></returns>
        public static double Convert(double aUnits, UnitOfMeasure aFromUnitOfMeasure, UnitOfMeasure aToUnitOfMeasure)
        {
            var vEnumType = typeof(UnitOfMeasure);
            //
            var vFromEnumInfo = vEnumType.GetMember(aFromUnitOfMeasure.ToString());
            var vFromEnumAttributes = vFromEnumInfo[0].GetCustomAttributes(typeof(UnitOfMeasureAttribute), false);
            var vFromBaseUnit = ((UnitOfMeasureAttribute)vFromEnumAttributes[0]).Base;
            var vFromEquals = ((UnitOfMeasureAttribute)vFromEnumAttributes[0]).IsEqualTo;
            //
            var vToEnumInfo = vEnumType.GetMember(aToUnitOfMeasure.ToString());
            var vToEnumAttributes = vToEnumInfo[0].GetCustomAttributes(typeof(UnitOfMeasureAttribute), false);
            var vToBaseUnit = ((UnitOfMeasureAttribute)vToEnumAttributes[0]).Base;
            var vToEquals = ((UnitOfMeasureAttribute)vToEnumAttributes[0]).IsEqualTo;
            //
            if (vToBaseUnit != vFromBaseUnit)
            {
                throw new ArgumentException(String.Format("Cannot convert incompatible UnitsOfMeasure {0} and {1}", aFromUnitOfMeasure, aToUnitOfMeasure));
            }
            //
            return (aUnits / vFromEquals) * vToEquals;
        }

        /// <summary>
        /// Returns a list of enumarable values that are compatible with the argument Base.
        /// </summary>
        /// <param name="aUnitOfMeasure">A unit of measure.</param>
        /// <returns></returns>
        public static IEnumerable<UnitOfMeasure> CompatibleUnits(UnitBase aUnitBase)
        {
            var vEnumType = typeof(UnitOfMeasure);
            //
            foreach (UnitOfMeasure vValue in Enum.GetValues(typeof(UnitOfMeasure)))
            {
                var vEnumInfo = vEnumType.GetMember(vValue.ToString());
                var vEnumAttributes = vEnumInfo[0].GetCustomAttributes(typeof(UnitOfMeasureAttribute), false);
                var vBaseUnit = ((UnitOfMeasureAttribute)vEnumAttributes[0]).Base;
                if (vBaseUnit == aUnitBase)
                {
                    yield return vValue;
                }
            }
        }
    }
    #endregion
}
