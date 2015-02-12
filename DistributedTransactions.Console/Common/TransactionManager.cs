using System.Transactions;

namespace DistributedTransactions.Console.Common
{
    public interface ITransactionManager
    {
        ITransaction CreateTransaction();
        ITransaction CreateTransaction(TransactionScopeOption transactionScopeOption);
    }

    public class TransactionManager : ITransactionManager
    {
        public ITransaction CreateTransaction()
        {
            return CreateTransaction(TransactionScopeOption.Required);
        }

        public ITransaction CreateTransaction(TransactionScopeOption transactionScopeOption)
        {
            return new Transaction(transactionScopeOption);
        }
    }
}
