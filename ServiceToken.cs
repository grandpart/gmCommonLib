using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zephry
{
    /// <summary>
    /// A POCO class used as the first argument to all SOAP Consumer calls. The consumer calls the web method defined by ServiceAction
    /// of the web service located at ServiceUrl, and passes the LogonToken as the first argument.
    /// </summary>
    public class ServiceToken: Zephob
    {
        #region Fields
        private string _serviceUrl;
        private MethodToken _methodToken = new MethodToken();
        #endregion

        #region Properties
        public string ServiceUrl
        {
            get { return _serviceUrl; }
            set { _serviceUrl = value; }
        }
        public MethodToken MethodToken
        {
            get { return _methodToken; }
            set { _methodToken = value; }
        }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is ServiceToken))
            {
                throw new ArgumentException("Invalid Assignment Source for ServiceToken");
            }

            _serviceUrl = (aSource as ServiceToken)._serviceUrl;
            _methodToken.AssignFromSource((aSource as ServiceToken)._methodToken);
        }
        #endregion
    }
}
