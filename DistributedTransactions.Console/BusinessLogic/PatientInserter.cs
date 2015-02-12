using DistributedTransactions.Console.Common;
using DistributedTransactions.Console.DataAccess;
using DistributedTransactions.Console.Models;

namespace DistributedTransactions.Console.BusinessLogic
{
    public interface IPatientInserter
    {
        int Insert(Patient patient);
    }

    public class PatientInserter : IPatientInserter
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ICaseRepository _caseRepository;
        private readonly IConditionRepository _conditionRepository;
        private readonly ITransactionManager _transactionManager;

        public PatientInserter(
            IPatientRepository patientRepository, 
            ICaseRepository caseRepository,
            IConditionRepository conditionRepository,
            ITransactionManager transactionManager)
        {
            _patientRepository = patientRepository;
            _caseRepository = caseRepository;
            _conditionRepository = conditionRepository;
            _transactionManager = transactionManager;
        }

        public int Insert(Patient patient)
        {
            var patientId = 0;

            using (var transaction = _transactionManager.CreateTransaction())
            {
                patientId = _patientRepository.Insert(patient);
                foreach (var @case in patient.Cases)
                {
                    @case.PatientId = patientId;
                    var caseId = _caseRepository.Insert(@case);
                    foreach (var condition in @case.Conditions)
                    {
                        condition.CaseId = caseId;
                        _conditionRepository.Insert(condition);
                    }
                }

                transaction.Complete();
            }

            return patientId;
        }
    }
}
