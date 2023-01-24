using Newtonsoft.Json;

namespace Zephry
{
    public class TransactionStatus
    {
        #region Fields
        private TransactionResult _transactionResult;
        private string _message;
        #endregion

        #region Properties
        [JsonProperty("txresult")]
        public TransactionResult TransactionResult { get => _transactionResult; set => _transactionResult = value; }
        [JsonProperty("result")]
        public string Result => CommonUtils.ParseEnum(_transactionResult);
        [JsonProperty("message")]
        public string Message { get => _message; set => _message = value; }
        #endregion

        #region Constructors

        public TransactionStatus()
        {
        }

        public TransactionStatus(TransactionResult aTransactionResult, string aMessage)
        {
            _transactionResult = aTransactionResult;
            _message = aMessage;
        }
        #endregion

        #region AssignFromSource
        public void AssignFromSource(TransactionStatus aSource)
        {
            if (aSource == null) { return; }

            _transactionResult = aSource._transactionResult;
            _message = aSource._message;
        }
        #endregion

    }
}
