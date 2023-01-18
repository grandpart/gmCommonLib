using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    public class EmailAddress
    {
        #region Fields
        private string _address;
        private string _displayName;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        public EmailAddress() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="aAddress">An address.</param>
        /// <param name="aDisplayName">A display name.</param>
        public EmailAddress(string aAddress, string aDisplayName)
        {
            _address = aAddress;
            _displayName = aDisplayName;
        }
        #endregion
    }
}
