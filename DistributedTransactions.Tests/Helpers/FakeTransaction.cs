using DistributedTransactions.Console.Common;

namespace DistributedTransactions.Tests.Helpers
{
    public class FakeTransaction : ITransaction
    {
        public bool IsComplete { get; private set; }
        public bool IsDisposed { get; private set; }

        public void Complete()
        {
            IsComplete = true;
        }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}