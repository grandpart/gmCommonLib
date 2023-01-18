﻿using System;
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
        public TransactionResult TransactionResult
        {
            get { return _transactionResult; }
            set { _transactionResult = value; }
        }
        #endregion

        public TransactionStatusException(TransactionResult aTransactionResult, string aMessage) : base(aMessage)
        {
            _transactionResult = aTransactionResult;
        }
    }
}