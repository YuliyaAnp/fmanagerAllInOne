using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace fmanagerFull.Models
{
    public class FinanceManagerContext : DbContext
    {
        public FinanceManagerContext(DbContextOptions<FinanceManagerContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Account> Account { get; set; }

        public async Task AddTransaction(Transaction transaction)
        {
            await Transaction.AddAsync(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            Transaction.Remove(transaction);
        }

        public async Task<Transaction> GetById(int id)
        {
            return await Transaction.SingleOrDefaultAsync(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return Transaction.ToList();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            Transaction.Update(transaction);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return Account.ToList();
        }
    }
}