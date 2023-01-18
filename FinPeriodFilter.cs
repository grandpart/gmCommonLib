using System;

namespace Zephry
{
    public class FinPeriodFilter : Zephob
    {
        #region Fields
        private int _finPeriod1;
        private int _finPeriod2;
        private FinPeriodOperator _finPeriodOperator;
        #endregion

        #region Properties

        public int FinPeriod1
        {
            get { return _finPeriod1; }
            set { _finPeriod1 = value; }
        }

        public int FinPeriod2
        {
            get { return _finPeriod2; }
            set { _finPeriod2 = value; }
        }

        public FinPeriodOperator FinPeriodOperator
        {
            get { return _finPeriodOperator; }
            set { _finPeriodOperator = value; }
        }

        #endregion

        #region AssignFromSource
        /// <summary>
        /// Assigns source properties to this instance.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is FinPeriodFilter))
            {
                throw new ArgumentException("Invalid assignment source", "FinPeriodFilter");
            }

            _finPeriod1 = ((FinPeriodFilter)aSource)._finPeriod1;
            _finPeriod2 = ((FinPeriodFilter)aSource)._finPeriod2;
            _finPeriodOperator = ((FinPeriodFilter)aSource)._finPeriodOperator;
        }
        #endregion
    }
}
