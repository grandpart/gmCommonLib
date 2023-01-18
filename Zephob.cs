using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    ///   Zephob abstract class.
    /// </summary>
    /// <remarks>
    ///   namespace Zephry.
    /// </remarks>
    [Serializable]
    public abstract class Zephob
    {
        #region Fields
        
        //private int _hashValue;
        //private ObjectState _objectState = ObjectState.None;
        #endregion

        #region Properties
        /// <summary>
        ///   Gets or Sets the _hashValue field
        /// </summary>
        /// <value>
        ///   The HashValue.
        /// </value>
        //public int HashValue
        //{
        //    get { return _hashValue; }
        //    set { _hashValue = value; }
        //}
        /// <summary>
        ///   Gets or sets the _objectState field.
        /// </summary>
        /// <value>
        ///  An <see cref="ObjectState"/>.
        /// </value>
        //public ObjectState ObjectState
        //{
        //    get { return _objectState; }
        //    set { _objectState = value; }
        //}
        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns from source.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public abstract void AssignFromSource(object aSource);

        #endregion
    }
}
