using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using fmanagerFull.Models;

namespace fmanagerFull.Controllers
{
    public class TransactionsService : ITransactionsService
    {
        private readonly FinanceManagerContext context;

        public TransactionsService(FinanceManagerContext context)
        {
            this.context = context;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return context.GetTransactions();
        }

        public Task<Transaction> GetById(int id)
        {
            return context.GetById(id);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await context.AddTransaction(transaction);
            context.SaveChanges();

            Account accountToSubstruct = context.GetAccountByName(transaction.AccountToSubstractAmount);
            accountToSubstruct.Balance -= transaction.Sum;
            context.UpdateAccount(accountToSubstruct);
            context.SaveChanges();

            Account accountToIncrease = context.GetAccountByName(transaction.AccountToIncreaseAmount);
            accountToIncrease.Balance += transaction.Sum;
            context.UpdateAccount(accountToIncrease);
            context.SaveChanges();
        }

        public void DeleteTransaction(Transaction trans)
        {
            context.DeleteTransaction(trans);
            context.SaveChanges();
        }
    }
}
