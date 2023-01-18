using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zephry
{
    public class MethodToken : Zephob
    {
        private string _serviceMethod;
        private bool _pooled = true;
        private LogonToken _logonToken = new LogonToken();

        #region Properties
        public string ServiceMethod
        {
            get { return _serviceMethod; }
            set { _serviceMethod = value; }
        }
        public bool Pooled
        {
            get { return _pooled; }
            set { _pooled = value; }
        }    
        public LogonToken LogonToken
        {
            get { return _logonToken; }
            set { _logonToken = value; }
        }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is MethodToken))
            {
                throw new ArgumentException("Invalid Assignment Source for ServiceToken");
            }

            _serviceMethod = (aSource as MethodToken)._serviceMethod;
            _pooled = (aSource as MethodToken)._pooled;
            _logonToken.AssignFromSource((aSource as MethodToken)._logonToken);
        }
        #endregion
    }
}
