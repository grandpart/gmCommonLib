using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Zephry
{
    [Serializable]
    public class TransactionStatusException : Exception
    {
        #region Fields
        private readonly int _httpcode = 500;
        private readonly string _result = string.Empty;
        #endregion

        #region Properties
        public int HttpCode { get => _httpcode; }
        public string Result { get => _result; }
        #endregion

        public TransactionStatusException(TransactionResult aTransactionResult, string aMessage) : base(aMessage)
        {
            _httpcode = (int)aTransactionResult;
            _result = CommonUtils.ParseEnum(aTransactionResult);
        }
    }
}
