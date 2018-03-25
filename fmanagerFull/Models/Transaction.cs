using System;

namespace fmanagerFull.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string AccountToIncreaseAmount { get; set; }
        public string AccountToSubstractAmount { get; set; }
    }
}
