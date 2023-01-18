
namespace Zephry
{
    /// <summary>
    /// An old-school bitmap integer representing functional access, where each bit starting at the low order represents a boolean true if on(1), false if off(0).
    /// </summary>
    public class Access
    {
        #region Fields
        /// <summary>
        /// A 32-bit signed integer used to carry access information in its bit patterns
        /// </summary>
        private int _accessMap;

        //This is a test
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the access map in full.
        /// </summary>
        /// <value>
        /// The access map.
        /// </value>
        public int AccessMap
        {
            get { return _accessMap; }
            set { _accessMap = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> is loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if loaded; otherwise, <c>false</c>.
        /// </value>
        public  bool Loaded
        {
            get { return GetBit(0); }
            set { SetBit(0, value); }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> allows List.
        /// </summary>
        /// <value>
        ///   <c>true</c> if list; otherwise, <c>false</c>.
        /// </value>
        public  bool List
        {
            get { return GetBit(1); }
            set { SetBit(1, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> allows Create.
        /// </summary>
        /// <value>
        ///   <c>true</c> if create; otherwise, <c>false</c>.
        /// </value>
        public  bool Create
        {
            get { return GetBit(2); }
            set { SetBit(2, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> allows Read.
        /// </summary>
        /// <value>
        ///   <c>true</c> if read; otherwise, <c>false</c>.
        /// </value>
        public  bool Read
        {
            get { return GetBit(3); }
            set { SetBit(3, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> allows Update.
        /// </summary>
        /// <value>
        ///   <c>true</c> if update; otherwise, <c>false</c>.
        /// </value>
        public  bool Update
        {
            get { return GetBit(4); }
            set { SetBit(4, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Access"/> allows Delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if delete; otherwise, <c>false</c>.
        /// </value>
        public  bool Delete
        {
            get { return GetBit(5); }
            set { SetBit(5, value); }
        }
        #endregion

        #region GetBit

        /// <summary>
        /// Returns true if aBit bit of this instance's _accessMap is on, else false
        /// </summary>
        /// <param name="aBit">A bit.</param>
        /// <returns></returns>
        private bool GetBit(int aBit)
        {
            return ((_accessMap >> aBit) & 1) == 1;
        }
        #endregion

        #region SetBit
        /// <summary>
        /// Sets the aBit bit of this instance's _accessMap to 1 if aOn is true else 0
        /// </summary>
        /// <param name="aBit">A bit.</param>
        /// <param name="aOn">if set to <c>true</c> [a on].</param>
        private void SetBit(int aBit, bool aOn)
        {
            if (aOn)
            {
                _accessMap = _accessMap | (1 << aBit);
            }
            else
            {
                _accessMap = _accessMap & ~(1 << aBit);
            }
        }
        #endregion

        #region Merge
        /// <summary>
        /// Merges the Argument AccessMap with this instance's AccessMap
        /// </summary>
        /// <param name="aAccess">A access.</param>
        public void Merge(Access aAccess)
        {
            _accessMap |= aAccess._accessMap;
        }
        #endregion

        #region AssignFromSource
        /// <summary>
        /// Assigns the source AccessMap to the the AccessMap of this instance.
        /// </summary>
        /// <param name="aSourceAccess">A source access map.</param>
        public void AssignFromSource(Access aSourceAccess)
        {
            _accessMap = aSourceAccess.AccessMap;
        }
        #endregion
    }
}
