using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Zephry
{
    public class DateFilter : Zephob
    {
        #region Fields
        /// <summary>
        /// The start date of a date range.
        /// </summary>
        private DateTime _date1;
        /// <summary>
        /// The end date of a date range.
        /// </summary>
        private DateTime _date2;
        /// <summary>
        /// The date range oparator (equal, less than, between, etc.)
        /// </summary>
        private DateOperator _dateOperator;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the date1.
        /// </summary>
        /// <value>
        /// The date1.
        /// </value>
        [DataType(DataType.Date)]
        public DateTime Date1
        {
            get { return _date1; }
            set { _date1 = value; }
        }
        /// <summary>
        /// Gets or sets the date2.
        /// </summary>
        /// <value>
        /// The date2.
        /// </value>
        [DataType(DataType.Date)]
        public DateTime Date2
        {
            get { return _date2; }
            set { _date2 = value; }
        }
        /// <summary>
        /// Gets or sets the date operator.
        /// </summary>
        /// <value>
        /// The date operator.
        /// </value>
        public DateOperator DateOperator
        {
            get { return _dateOperator; }
            set { _dateOperator = value; }
        }
        #endregion

        #region Constructors

        public DateFilter()
        {

        }
        public DateFilter(DateOperator aDateOperator, DateTime aDate1, DateTime aDate2)
        {
            _dateOperator = aDateOperator;
            _date1 = aDate1;
            _date2 = aDate2;
        }
        #endregion

        #region AssignFromSource
        /// <summary>
        /// Assigns source properties to this instance.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is DateFilter))
            {
                throw new ArgumentException("Invalid assignment source", "DateFilter");
            }

            _date1 = ((DateFilter) aSource)._date1;
            _date2 = ((DateFilter) aSource)._date2;
            _dateOperator = ((DateFilter) aSource)._dateOperator;
        }
        #endregion
    }
}
