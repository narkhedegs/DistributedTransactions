using System;
using System.Transactions;
using DistributedTransactions.Console.BusinessLogic;
using DistributedTransactions.Console.DataAccess;
using DistributedTransactions.Console.Helpers;

namespace DistributedTransactions.Console
{
    /// <summary>
    /// This class contains methods to execute transactions.
    /// </summary>
    public class TransactionExample
    {
        /// <summary>
        /// Example of a local transaction that involves only one durable resource (for ex. SQL Server Database). 
        /// This transaction will not be escalated to MSDTC.
        /// </summary>
        /// <param name="failTransaction">Boolean parameter when set to true causes exception inside a transaction scope.</param>
        public void RunWithOneResource(bool failTransaction = false)
        {
            var patient = PatientGenerator.Generate();

            try
            {
                using (var scope = new TransactionScope())
                {
                    var patientInserter = new PatientInserter(
                        new PatientRepository("twinbrook-medical-center"),
                        new CaseRepository("twinbrook-medical-center"),
                        new ConditionRepository("twinbrook-medical-center"));

                    var patientId = patientInserter.Insert(patient);
                    System.Console.WriteLine("Inserted a patient with PatientId: {0}", patientId);

                    if (failTransaction) throw new Exception("Transaction Failed.");

                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Example of a distributed transaction that involves two durable resources. This transaction will escalate 
        /// to MSDTC as it involves two SQL Server Databases even if they are on the same server.
        /// </summary>
        /// <param name="failTransaction">Boolean parameter when set to true causes exception inside a transaction scope.</param>
        public void RunWithTwoResources(bool failTransaction = false)
        {
            var patient = PatientGenerator.Generate();

            try
            {
                using (var scope = new TransactionScope())
                {
                    var twinbrookPatientInserter = new PatientInserter(
                        new PatientRepository("twinbrook-medical-center"),
                        new CaseRepository("twinbrook-medical-center"),
                        new ConditionRepository("twinbrook-medical-center"));

                    var twinbrookPatientId = twinbrookPatientInserter.Insert(patient);
                    System.Console.WriteLine("Inserted a patient with PatientId: {0} into Twinbrook Medical Center", twinbrookPatientId);

                    var northwestPatientInserter = new PatientInserter(
                        new PatientRepository("northwest-medical-center"),
                        new CaseRepository("northwest-medical-center"),
                        new ConditionRepository("northwest-medical-center"));

                    var northwestPatientId = northwestPatientInserter.Insert(patient);
                    System.Console.WriteLine("Inserted a patient with PatientId: {0} into Northwest Medical Center", northwestPatientId);

                    if (failTransaction) throw new Exception("Transaction Failed.");

                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Example of a distributed transaction that involves two durable resources on two different servers. This transaction
        ///  will escalate to MSDTC as it involves two SQL Server Databases on two different servers.
        /// </summary>
        /// <param name="failTransaction">Boolean parameter when set to true causes exception inside a transaction scope.</param>
        public void RunWithTwoResourcesOnDifferentServers(bool failTransaction = false)
        {
            var patient = PatientGenerator.Generate();

            try
            {
                using (var scope = new TransactionScope())
                {
                    var twinbrookPatientInserter = new PatientInserter(
                        new PatientRepository("twinbrook-medical-center"),
                        new CaseRepository("twinbrook-medical-center"),
                        new ConditionRepository("twinbrook-medical-center"));

                    var twinbrookPatientId = twinbrookPatientInserter.Insert(patient);
                    System.Console.WriteLine("Inserted a patient with PatientId: {0} into Twinbrook Medical Center", twinbrookPatientId);

                    var remoteTwinbrookPatientInserter = new PatientInserter(
                        new PatientRepository("remote-twinbrook-medical-center"),
                        new CaseRepository("remote-twinbrook-medical-center"),
                        new ConditionRepository("remote-twinbrook-medical-center"));

                    var remoteTwinbrookPatientId = remoteTwinbrookPatientInserter.Insert(patient);
                    System.Console.WriteLine("Inserted a patient with PatientId: {0} into Remote Twinbrook Medical Center", remoteTwinbrookPatientId);

                    if (failTransaction) throw new Exception("Transaction Failed.");

                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception: {0}", exception.Message);
            }
        }
    }
}
