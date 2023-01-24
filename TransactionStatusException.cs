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
        private TransactionResult _transactionResult;
        #endregion

        #region Properties
        [JsonProperty("txresult")]
        public TransactionResult TransactionResult { get => _transactionResult; set => _transactionResult = value; }
        #endregion

        public TransactionStatusException(TransactionResult aTransactionResult, string aMessage) : base(aMessage)
        {
            _transactionResult = aTransactionResult;
        }
    }
}
