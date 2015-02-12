using System;
using DistributedTransactions.Console.BusinessLogic;
using DistributedTransactions.Console.Common;
using DistributedTransactions.Console.DataAccess;
using DistributedTransactions.Console.Models;
using DistributedTransactions.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DistributedTransactions.Tests
{
    [TestFixture]
    public class when_inserting_a_patient
    {
        private IPatientInserter _patientInserter;
        private Mock<IPatientRepository> _patientRepository;
        private Mock<ICaseRepository> _caseRepository;
        private Mock<IConditionRepository> _conditionRepository;
        private Mock<ITransactionManager> _transactionManager;
        private ITransaction _transaction;

        [SetUp]
        public void SetUp()
        {
            _patientRepository = new Mock<IPatientRepository>();
            _caseRepository = new Mock<ICaseRepository>();
            _conditionRepository = new Mock<IConditionRepository>();

            _transaction = new FakeTransaction();
            _transactionManager = new Mock<ITransactionManager>();
            _transactionManager.Setup(manager => manager.CreateTransaction()).Returns(_transaction);

            _patientInserter = new PatientInserter(
                _patientRepository.Object, 
                _caseRepository.Object,
                _conditionRepository.Object, 
                _transactionManager.Object);
        }

        [Test]
        public void should_rollback_transaction_when_an_exception_occurs()
        {
            //Cause exception inside PatientInserter.Insert method.
            _conditionRepository.Setup(repository => repository.Insert(It.IsAny<Condition>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _patientInserter.Insert(PatientGenerator.Generate()));
            Assert.That(!_transaction.IsComplete);
        }
    }
}
