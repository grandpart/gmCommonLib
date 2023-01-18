using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace Zephry
{
    /// <summary>
    ///   Session class. [Serializable].
    /// </summary>
    /// <remarks>
    ///   namespace Zephry
    /// </remarks>
    [Serializable]
    public sealed class Connection
    {
    //    #region Static Constructor

    //    /// <summary>
    //    ///   Initializes the <see cref="Connection"/> class.
    //    /// </summary>
    //    /// <remarks>
    //    ///   Constructor overload is static for instant construction (not lazy)
    //    /// </remarks>
    //    static Connection()
    //    {

    //    }

    //    #endregion

    //    #region Instance Singleton

    //    /// <summary>
    //    ///   Instance.
    //    /// </summary>
    //    private static Connection _instance = new Connection();

    //    /// <summary>
    //    ///   Gets the <see cref="Connection"/> instance.
    //    /// </summary>
    //    public static Connection Instance
    //    {
    //        get { return _instance; }
    //        set { _instance = value; }
    //    }

    //    #endregion

        #region Fields

        /// <summary>
        ///   Database UserID.
        /// </summary>
        private string _dBUser;
        /// <summary>
        ///   Database Password.
        /// </summary>
        private string _dBPassword;
        /// <summary>
        ///   Database Server
        /// </summary>
        private string _dBServer;
        /// <summary>
        ///   Database Name. 
        /// </summary>
        private string _dBName;

        #endregion

        #region Properties


        /// <summary>
        ///   Gets or sets the database userID.
        /// </summary>
        public string DbUser
        {
            get { return _dBUser; }
            set { _dBUser = value; }
        }

        /// <summary>
        ///   Gets or sets the database password.
        /// </summary>
        public string DbPassword
        {
            get { return _dBPassword; }
            set { _dBPassword = value; }
        }

        /// <summary>
        ///   Gets or sets the database server.
        /// </summary>
        public string DbServer
        {
            get { return _dBServer; }
            set { _dBServer = value; }
        }

        /// <summary>
        ///   Gets or sets the name of the database.
        /// </summary>the DB.
        public string DbName
        {
            get { return _dBName; }
            set { _dBName = value; }
        }

        /// <summary>
        ///   Gets the SQL Connection <see cref="String"/>.
        /// </summary>
        public string SqlConnectionString
        {
            get
            {
                return (new SqlConnectionStringBuilder()
                {
                    DataSource = _dBServer,
                    InitialCatalog = _dBName,
                    UserID = _dBUser,
                    Password = _dBPassword,
                    PersistSecurityInfo = true,
                    Pooling = false
                }).ToString();
            }
        }
        #endregion
    }
}