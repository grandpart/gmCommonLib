using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    /// Either set a total to the sum of a list of integers, or correct a list of integers that don't add up to a total so that they do.
    /// </summary>
    public class DevianceEditor
    {
        #region Fields
        private int _amount;
        private List<DevianceItem> _amountParts = new List<DevianceItem>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// Gets or sets the amount parts.
        /// </summary>
        /// <value>
        /// The amount parts.
        /// </value>
        public List<DevianceItem> AmountParts
        {
            get { return _amountParts; }
            set { _amountParts = value; }
        }
        #endregion

        #region Add a DevianceItem
        /// <summary>
        /// Adds a DevianceItem to the Parts List.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="aValue">A value.</param>
        public void Add(object tag, int aValue)
        {
            var vDevianceItem = new DevianceItem() { Tag = tag, Value = aValue };
            _amountParts.Add(vDevianceItem);
        }
        #endregion

        #region Get a Value
        /// <summary>
        /// Gets Value of a DevianceItem based on it's tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public int GetValue(object tag)
        {
            var vDevianceItem = _amountParts.FirstOrDefault(c => c.Tag == tag);
            return vDevianceItem == null ? 0 : vDevianceItem.Value;
        }
        #endregion

        #region Apply
        /// <summary>
        /// Correct the values in a list for a deviation from a total
        /// </summary>
        /// <param name="aDevianceCorrection">A deviance correction.</param>
        public void Apply(DevianceCorrection aDevianceCorrection)
        {
            switch (aDevianceCorrection)
            {
                case DevianceCorrection.None:
                    break;
                case DevianceCorrection.CorrectAmount:
                    _amount = _amountParts.Sum(c => c.Value);
                    break;
                case DevianceCorrection.CorrectList:
                    CorrectList();
                    break;
            }
        }
        #endregion

        private void CorrectList()
        {
            // If the ListCount is Zero, get out.
            if (!_amountParts.Any())
            {
                return;
            }
            // If the amount is Zero, make all parts Zero, get out.
            if (_amount <= 0)
            {
                _amount = 0;
                _amountParts.ForEach(c => c.Value = 0);
                return;
            }
            // If the PartsTotal is Zero, do a simple split, get out.
            var vPartsTotal = _amountParts.Sum(c => c.Value);
            if (vPartsTotal <= 0)
            {
                _amountParts.ForEach(c => c.Value = (int)Math.Round((double)(vPartsTotal / _amountParts.Count())));
                _amount = _amountParts.Sum(c => c.Value);
                return;
            }
            // If there is no difference between the amount and the sum of the parts, all is well, get out.
            var vDeviance = _amount - vPartsTotal;
            if (vDeviance == 0) 
            { 
                return; 
            }
            //**************************************************************
            // Apply either an increase or a decrease to all non-zero parts.
            //**************************************************************
            bool vIncrease = vDeviance > 0;
            if (!vIncrease)
            {
                vDeviance = vDeviance * -1;
            }
            double vPercOfTotal;
            _amountParts.ForEach(c => 
            {
                vPercOfTotal = (double)c.Value / (double)vPartsTotal;
                double vChangeAmount = vPercOfTotal * (double)vDeviance;
                c.Value = vIncrease ? c.Value + (int)Math.Round((double)vChangeAmount) : c.Value - (int)Math.Round((double)vChangeAmount);
            });
            _amount = _amountParts.Sum(c => c.Value);
        }


    }
}
