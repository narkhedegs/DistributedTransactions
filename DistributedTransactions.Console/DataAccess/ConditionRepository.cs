using System.Configuration;
using System.Data.SqlClient;
using DistributedTransactions.Console.Models;

namespace DistributedTransactions.Console.DataAccess
{
    public interface IConditionRepository
    {
        void Insert(Condition condition);
    }

    public class ConditionRepository : IConditionRepository
    {
        private readonly string _connectionStringName;

        public ConditionRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public void Insert(Condition condition)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
            var connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                var insertConditionCommand = new SqlCommand("INSERT INTO Conditions (CaseId, ConditionName) VALUES(@CaseId, @ConditionName)", connection);
                insertConditionCommand.Parameters.AddRange(new[]
                        {
                            new SqlParameter("@CaseId", condition.CaseId),
                            new SqlParameter("@ConditionName", condition.ConditionName)
                        });
                insertConditionCommand.ExecuteNonQuery(); 

                insertConditionCommand.Dispose();
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
