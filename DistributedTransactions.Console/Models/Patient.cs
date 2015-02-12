using System.Collections.ObjectModel;

namespace DistributedTransactions.Console.Models
{
    public class Patient
    {
        public Patient()
        {
            Cases = new Collection<Case>();
        }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Collection<Case> Cases { get; set; }
    }
}
