using System;

namespace fmanagerFull.Models
{
    public class TransactionRecord
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int AccountToIncreaseId { get; set; }
        public int AccountToSubstractId { get; set; }
    }
}
