// using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Zephry
{
    /// <summary>
    /// A POCO class passed as the first argument to web service calls indicating who the calling user is.
    /// </summary>
    [Serializable]
    public class LogonToken : Zephob
    {

        #region Fields
        private int _entity;
        private string _userId = string.Empty;
        private string _token = string.Empty;
        private bool _admin;
        #endregion

        #region Properties
        [JsonProperty("entity")]
        public int Entity { get => _entity; set => _entity = value; }
        [JsonProperty("userid")]
        public string UserId { get => _userId; set => _userId = value; }
        [JsonProperty("token")]
        public string Token { get => _token; set => _token = value; }
        [JsonProperty("admin")]
        public bool Admin { get => _admin; set => _admin = value; }
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

            _entity = ((LogonToken)aSource)._entity;
            _userId = ((LogonToken) aSource)._userId;
            _token = ((LogonToken) aSource)._token;
            _admin = ((LogonToken)aSource)._admin;
        }
        #endregion
        
    }
}
