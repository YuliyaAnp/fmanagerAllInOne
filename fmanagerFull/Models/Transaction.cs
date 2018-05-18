using System;

namespace fmanagerFull.Models
{
    public class Transaction
    {
        public int Sum { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
        public string AccountToIncreaseName { get; set; }
        public string AccountToSubstractName { get; set; }
        //public Currency Currency { get; set; }
        public int Id { get; set; }
    }
}