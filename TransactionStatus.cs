
namespace Zephry
{
    public class TransactionStatus
    {
        #region Fields
        private TransactionResult _transactionResult;
        private string _message;
        #endregion

        #region Properties
        public TransactionResult TransactionResult
        {
            get { return _transactionResult; }
            set { _transactionResult = value; }
        }
        public string Result
        {
            get { return CommonUtils.ParseEnum(_transactionResult); }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
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
