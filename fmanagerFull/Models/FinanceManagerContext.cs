using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace fmanagerFull.Models
{
    public class FinanceManagerContext : DbContext
    {
        public FinanceManagerContext(DbContextOptions<FinanceManagerContext> options) : base(options)
        {
            
        }

        public DbSet<TransactionRecord> Transaction { get; set; }
        public DbSet<Account> Account { get; set; }

        public async Task AddTransaction(TransactionRecord transaction)
        {
            await Transaction.AddAsync(transaction);
        }

        public void DeleteTransaction(TransactionRecord transactionRecord)
        {
            Entry(transactionRecord).State = EntityState.Deleted;
           // Transaction.Remove(transactionRecord);

        }

        public TransactionRecord GetById(int id)
        {
            return Transaction.SingleOrDefault(t => t.Id == id);
        }

		public IEnumerable<TransactionRecord> GetTransactions()
        {
            return Transaction.AsNoTracking().ToList();
        }

        public void UpdateTransaction(TransactionRecord transaction)
        {
            Transaction.Update(transaction);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return Account.AsNoTracking().ToList();
        }

        public Account GetAccountById(int accountId)
        {
            return Account.SingleOrDefault(a => a.Id == accountId);
        }

        public void UpdateAccount(Account account)
        {
            Account.Update(account);
        }
    }
}