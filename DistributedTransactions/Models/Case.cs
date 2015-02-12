using System.Collections.ObjectModel;

namespace DistributedTransactions.Models
{
    public class Case
    {
        public Case()
        {
            Conditions = new Collection<Condition>();
        }

        public int CaseId { get; set; }
        public int PatientId { get; set; }
        public string CaseDescription { get; set; }
        public Collection<Condition> Conditions { get; set; }
    }
}
