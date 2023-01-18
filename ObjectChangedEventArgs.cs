using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    ///   ObjectChangedEventArgs class. Inherit from <see cref="EventArgs"/>.
    /// </summary>
    /// <remarks>
    ///   namespace Zephry.
    /// </remarks>
    public class ObjectChangedEventArgs : EventArgs
    {
        #region Fields

        private object _objectInstance;
        private ChangeAction _changeAction;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ChangeAction"/>.
        /// </summary>
        /// <value>
        /// The ChangeAction.
        /// </value>
        public ChangeAction ChangeAction
        {
            get { return _changeAction; }
            set { _changeAction = value; }
        }

        /// <summary>
        ///   Gets or sets the object instance.
        /// </summary>
        /// <value>
        ///   The object instance.
        /// </value>
        public object ObjectInstance
        {
            get { return _objectInstance; }
            set { _objectInstance = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        ///   Initializes a new instance of the <see cref="ObjectChangedEventArgs"/> class.
        /// </summary>
        /// <param name="aObject">An object.</param>
        /// <param name="aChangeAction">A <see cref="ChangeAction"/>.</param>
        public ObjectChangedEventArgs(object aObject, ChangeAction aChangeAction)
        {
            _objectInstance = aObject;
            _changeAction = aChangeAction;
        }

        #endregion
    }
}
