using System;
using DistributedTransactions.Console.Common;

namespace DistributedTransactions.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitRepl = false;
            while (!exitRepl)
            {
                System.Console.WriteLine(Environment.NewLine + "Please select one option and press ENTER. " + Environment.NewLine);
                System.Console.WriteLine("1. Local Transaction with one durable resource");
                System.Console.WriteLine("2. Distributed Transaction with two durable resources on same server");
                System.Console.WriteLine("3. Distributed Transaction with two durable resources on different servers");
                System.Console.WriteLine("4. Reset Databases");

                System.Console.Write(Environment.NewLine + "Your selected ");
                var selection = Convert.ToInt32(System.Console.ReadLine());

                var causeException = false;
                if (selection != 4)
                {
                    System.Console.Write(Environment.NewLine + "Cause exception inside transaction? (Y/N) ");
                    causeException = Convert.ToString(System.Console.ReadLine()).ToLower() == "Y".ToLower();
                }

                var transactionExample = new TransactionExample();
                switch (selection)
                {
                    case 1:
                        transactionExample.RunWithOneResource(causeException);
                        break;
                    case 2:
                        transactionExample.RunWithTwoResources(causeException);
                        break;
                    case 3:
                        transactionExample.RunWithTwoResourcesOnDifferentServers(causeException);
                        break;
                    case 4:
                        DatabaseHelper.ResetDatabases();
                        break;
                }

                System.Console.Write(Environment.NewLine + "Exit Repl? (Y/N) ");
                exitRepl = Convert.ToString(System.Console.ReadLine()).ToLower() == "Y".ToLower();
            }
        }
    }
}
