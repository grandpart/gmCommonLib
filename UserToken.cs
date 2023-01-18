using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UserToken : Zephob
    {
        
        #region Static Constructor

        /// <summary>
        ///   Initializes the <see cref="UserToken"/> class.
        /// </summary>
        /// <remarks>
        ///   Constructor overload is static for instant construction (not lazy)
        /// </remarks>
        static UserToken()
        {

        }

        #endregion

        #region Instance Singleton

        /// <summary>
        ///   Instance.
        /// </summary>
        private static UserToken _instance = new UserToken();

        /// <summary>
        ///   Gets the <see cref="UserToken"/> instance.
        /// </summary>
        public static UserToken Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }
        #endregion

        #region Fields
        /// <summary>
        /// UserID.
        /// </summary>
        private string _userId;
        /// <summary>
        ///   Password.
        /// </summary>
        [NonSerialized]
        private string _password;
        /// <summary>
        ///   The URL.
        /// </summary>
        private string _url;
        /// <summary>
        ///   The method of the current service call.
        /// </summary>
        [NonSerialized]
        private string _method;
        /// <summary>
        ///   Context.
        /// </summary>
        private ConnectionContext _context = ConnectionContext.Integrated;
        /// <summary>
        ///   Version.
        /// </summary>
        private string _version;
        #endregion

        #region Properties

        /// <summary>
        ///   Gets the user ID.
        /// </summary>
        /// <value>
        ///   The user ID.
        /// </value>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        /// <summary>
        ///   Gets the password.
        /// </summary>
        /// <value>
        ///   The password.
        /// </value>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        ///   Gets the context.
        /// </summary>
        /// <value>
        ///   The context.
        /// </value>
        public ConnectionContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        /// <summary>
        ///   Gets the version.
        /// </summary>
        /// <value>
        ///   The version.
        /// </value>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        /// <summary>
        ///   Gets or sets the URL.
        /// </summary>
        /// <value>
        ///   The URL.
        /// </value>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        ///   Gets or sets the Method.
        /// </summary>
        /// <value>
        ///   The method.
        /// </value>
        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }

        #endregion

        #region AssignFromSource
        /// <summary>
        /// Assigns aSource to this instance of <see cref="UserToken"/>.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is UserToken))
            {
                throw new ArgumentException("Invalid Assignment Source for UserToken");
            }

            _userId = (aSource as UserToken)._userId;
            _password = (aSource as UserToken)._password;
            _url = (aSource as UserToken)._url;
            _method = (aSource as UserToken)._method;
            _context = (aSource as UserToken)._context;
            _version = (aSource as UserToken)._version;
        }
        #endregion

    }
}
