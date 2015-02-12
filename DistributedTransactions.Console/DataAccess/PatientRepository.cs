using System;
using System.Configuration;
using System.Data.SqlClient;
using DistributedTransactions.Console.Models;

namespace DistributedTransactions.Console.DataAccess
{
    public interface IPatientRepository
    {
        int Insert(Patient patient);
    }

    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionStringName;

        public PatientRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public int Insert(Patient patient)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
            var connection = new SqlConnection(connectionString);

            var patientId = -1;
            using (connection)
            {
                connection.Open();

                var insertPatientCommand =
                    new SqlCommand(
                        "INSERT INTO Patients (FirstName, LastName, City, State, ZipCode) VALUES(@FirstName, @LastName, @City, @State, @ZipCode);SELECT SCOPE_IDENTITY() AS PatientId;",
                        connection);
                insertPatientCommand.Parameters.AddRange(new[]
                {
                    new SqlParameter("@FirstName", patient.FirstName),
                    new SqlParameter("@LastName", patient.LastName),
                    new SqlParameter("@City", patient.City),
                    new SqlParameter("@State", patient.State),
                    new SqlParameter("@ZipCode", patient.ZipCode)
                });
                patientId = Convert.ToInt32(insertPatientCommand.ExecuteScalar());

                insertPatientCommand.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return patientId;
        }
    }
}
