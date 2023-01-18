using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    /// An item that participates in the list of DevianceItem's of a Deviance Editor.
    /// </summary>
    public class DevianceItem
    {
        #region Fields
        private object _tag = new object();
        private int _value;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion
    }
}
