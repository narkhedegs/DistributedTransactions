using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DistributedTransactions.Console.Helpers
{
    public static class DatabaseHelper
    {
        public static void ResetDatabases()
        {
            var connectionStrings = new[]
            {
                ConfigurationManager.ConnectionStrings["twinbrook-medical-center"].ConnectionString,
                ConfigurationManager.ConnectionStrings["northwest-medical-center"].ConnectionString,
                ConfigurationManager.ConnectionStrings["remote-twinbrook-medical-center"].ConnectionString
            };

            try
            {
                foreach (var connectionString in connectionStrings)
                {
                    var connection = new SqlConnection(connectionString);

                    using (connection)
                    {
                        connection.Open();

                        var commands = new List<string>
                        {
                            "DELETE FROM Conditions;DELETE FROM Cases;DELETE FROM Patients;",
                            "DBCC CHECKIDENT ('Patients', RESEED, 0);DBCC CHECKIDENT ('Cases', RESEED, 0);"
                        };

                        commands.ForEach(command =>
                        {
                            var resetDatabaseCommand = new SqlCommand(command, connection);
                            resetDatabaseCommand.ExecuteNonQuery();
                            resetDatabaseCommand.Dispose();
                        });

                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception: {0}", exception.Message);
            }
        }
    }
}
