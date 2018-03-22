using System;

namespace fmanagerFull.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public Account AccountToAdd { get; set; }
        public Account AccountToSubstract { get; set; }
    }
}
