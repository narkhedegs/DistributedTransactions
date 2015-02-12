using System.Transactions;
using DistributedTransactions.Console.DataAccess;
using DistributedTransactions.Console.Models;

namespace DistributedTransactions.Console.BusinessLogic
{
    public class PatientInserter
    {
        private readonly PatientRepository _patientRepository;
        private readonly CaseRepository _caseRepository;
        private readonly ConditionRepository _conditionRepository;

        public PatientInserter(
            PatientRepository patientRepository, 
            CaseRepository caseRepository,
            ConditionRepository conditionRepository)
        {
            _patientRepository = patientRepository;
            _caseRepository = caseRepository;
            _conditionRepository = conditionRepository;
        }

        public int Insert(Patient patient)
        {
            var patientId = 0;

            using (var scope = new TransactionScope())
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

                scope.Complete();
            }

            return patientId;
        }
    }
}
