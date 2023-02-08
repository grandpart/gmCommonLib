
namespace Zephry
{
    public class TransactionStatus
    {
        private int _status;
        private string _result = string.Empty;
        private string _message = string.Empty;

        public int Status { get => _status; set => _status = value; }
        public string Result { get => _result; set => _result = value; }
        public string Message { get => _message; set => _message = value; }

        public TransactionStatus(int aStatus, string aResult, string aMessage)
        {
            _status = aStatus;
            _result = aResult;
            _message = aMessage;
        }
    }
}
