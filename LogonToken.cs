using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    /// A POCO class passed as the first argument to web service calls indicating who the calling user is.
    /// </summary>
    [Serializable]
    public class LogonToken : Zephob
    {

        #region Fields
        private bool _admin;
        private string _userId;
        private string _token;
        private string _name;
        private string _surname;
        private int _entity;
        private int? _organization;
        #endregion

        #region Properties
        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }
        public int Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }
        public int? Organization
        {
            get { return _organization; }
            set { _organization = value; }
        }

        #endregion

        #region AssignFromSource
        /// <summary>
        /// Assigns aSource to this instance of <see cref="LogonToken"/>.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not LogonToken)
            {
                throw new ArgumentException("Invalid Assignment Source for LogonToken");
            }

            _admin = ((LogonToken) aSource)._admin;
            _userId = ((LogonToken) aSource)._userId;
            _token = ((LogonToken) aSource)._token;
            _name = ((LogonToken) aSource)._name;
            _surname = ((LogonToken) aSource)._surname;
            _entity = ((LogonToken) aSource)._entity;
            _organization = ((LogonToken)aSource)._organization;
        }
        #endregion
        
    }
}
