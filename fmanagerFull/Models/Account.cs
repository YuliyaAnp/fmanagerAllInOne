using System;
namespace fmanagerFull.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public AccountType Type { get; set; }
    }
}
