using System;

namespace Zephry
{
    public class AmountFilter : Zephob
    {
        #region Fields
        private decimal _amount1;
        private decimal _amount2;
        private AmountOperator _amountOperator;
        #endregion

        #region Properties
        public Decimal Amount1
        {
            get { return _amount1; }
            set { _amount1 = value; }
        }
        public Decimal Amount2
        {
            get { return _amount2; }
            set { _amount2 = value; }
        }
        public AmountOperator AmountOperator
        {
            get { return _amountOperator; }
            set { _amountOperator = value; }
        }
        #endregion

        #region AssignFromSource
        /// <summary>
        /// Assigns source properties to this instance.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is AmountFilter))
            {
                throw new ArgumentException("Invalid assignment source", "AmountFilter");
            }

            _amount1 = ((AmountFilter) aSource)._amount1;
            _amount2 = ((AmountFilter) aSource)._amount2;
            _amountOperator = ((AmountFilter) aSource)._amountOperator;
        }
        #endregion
    }
}
