using System;
using System.Transactions;

namespace DistributedTransactions.Console.Common
{
    public interface ITransaction : IDisposable
    {
        bool IsComplete { get; }
        bool IsDisposed { get; }
        void Complete();
    }

    public class Transaction : ITransaction
    {
        public bool IsComplete { get; private set; }
        public bool IsDisposed { get; private set; }
        private readonly TransactionScope _transactionScope;

        public Transaction(TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required)
        {
            _transactionScope = new TransactionScope(transactionScopeOption);
        }

        public void Complete()
        {
            _transactionScope.Complete();
            IsComplete = true;
        }

        public void Dispose()
        {
            _transactionScope.Dispose();
            IsDisposed = true;
        }
    }
}
