using System;
using System.Configuration;
using System.Data.SqlClient;
using DistributedTransactions.Console.Models;

namespace DistributedTransactions.Console.DataAccess
{
    public class CaseRepository
    {
        private readonly string _connectionStringName;

        public CaseRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public int Insert(Case @case)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
            var connection = new SqlConnection(connectionString);

            var caseId = -1;
            using (connection)
            {
                connection.Open();

                var insertCaseCommand = new SqlCommand("INSERT INTO Cases (PatientId, CaseDescription) VALUES(@PatientId, @CaseDescription); SELECT SCOPE_IDENTITY() AS CaseId;", connection);
                insertCaseCommand.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@PatientId", @case.PatientId), 
                        new SqlParameter("@CaseDescription", @case.CaseDescription) 
                    });
                caseId = Convert.ToInt32(insertCaseCommand.ExecuteScalar());

                insertCaseCommand.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return caseId;
        }
    }
}
