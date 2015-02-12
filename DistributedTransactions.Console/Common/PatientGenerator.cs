using System;
using System.Collections.ObjectModel;
using DistributedTransactions.Console.Models;

namespace DistributedTransactions.Console.Common
{
    public static class PatientGenerator
    {
        public static Patient Generate()
        {
            var random = new Random();
            var randomNumber = random.Next(1, 9999);

            var patient = new Patient
            {
                FirstName = "Gaurav-" + randomNumber,
                LastName = "Narkhede-" + randomNumber,
                City = "Columbus",
                State = "Ohio",
                ZipCode = "Zip-" + randomNumber,
                Cases = new Collection<Case>
                {
                    new Case
                    {
                        CaseDescription = "Case-1-" + randomNumber,
                        Conditions = new Collection<Condition>
                        {
                            new Condition {ConditionName = "Asthma-" + randomNumber},
                            new Condition {ConditionName = "High Blood Presure-" + randomNumber}
                        }
                    },
                    new Case
                    {
                        CaseDescription = "Case-2-" + randomNumber,
                        Conditions = new Collection<Condition>
                        {
                            new Condition {ConditionName = "Migrane-" + randomNumber},
                            new Condition {ConditionName = "Low Blood Presure-" + randomNumber}
                        }
                    },
                }
            };

            return patient;
        }
    }
}
