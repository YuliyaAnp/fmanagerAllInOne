using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fmanagerFull.Models;

namespace fmanagerFull.Controllers
{
    public class TransactionsService
    {
        private readonly FinanceManagerContext context;
        public SqlDbClient client;

        public TransactionsService(SqlDbClient sqlDbClient)
        {
            // this.context = context;

            //client = new SqlDbClient("TestDatabase", "root", "515BuisWaW");

            client = sqlDbClient;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var transactionRecords = client.GetTransactions();
            //var transactionRecords = context.GetTransactions();
            //var accounts = context.GetAccounts();

            var transactions = new List<Transaction>();

            foreach (var transactionRecord in transactionRecords)
            {
                transactions.Add(new Transaction()
                {
                    Sum = transactionRecord.Sum,
                    Description = transactionRecord.Description,
                    DateTime = transactionRecord.DateTime,
                    AccountToIncreaseName = client.GetAccountById(transactionRecord.AccountToIncreaseId).Name, 
                    AccountToSubstractName = client.GetAccountById(transactionRecord.AccountToSubstractId).Name
                    //Currency = accounts.Where(a => a.Id == transactionRecord.AccountToIncreaseId).FirstOrDefault().Currency
                });
            }

            return transactions;
        }

        public Transaction GetById(int id)
        {
            var transactionRecord = context.GetById(id);
            var accounts = context.GetAccounts();

            Transaction transaction = new Transaction
            {
                Sum = transactionRecord.Sum,
                Description = transactionRecord.Description,
                DateTime = transactionRecord.DateTime,
                AccountToIncreaseName = accounts.Where(a => a.Id == transactionRecord.AccountToIncreaseId).FirstOrDefault().Name,
                AccountToSubstractName = accounts.Where(a => a.Id == transactionRecord.AccountToSubstractId).FirstOrDefault().Name,
                //Currency = accounts.Where(a => a.Id == transactionRecord.AccountToIncreaseId).FirstOrDefault().Currency
            };

            return transaction;
        }

        public void AddTransaction(Transaction transaction)
        {
            var accounts = context.GetAccounts();

            TransactionRecord transactionRecord = new TransactionRecord
            {
                Sum = transaction.Sum,
                Description = transaction.Description,
                DateTime = transaction.DateTime,
                AccountToIncreaseId = accounts.Where(a => a.Name == transaction.AccountToIncreaseName).FirstOrDefault().Id,
                AccountToSubstractId = accounts.Where(a => a.Name == transaction.AccountToSubstractName).FirstOrDefault().Id
            };

            context.AddTransaction(transactionRecord).Wait();
            context.SaveChanges();

            Account accountToSubstruct = context.GetAccountById(transactionRecord.AccountToSubstractId);
            accountToSubstruct.Balance -= transaction.Sum;
            context.UpdateAccount(accountToSubstruct);
            context.SaveChanges();

            Account accountToIncrease = context.GetAccountById(transactionRecord.AccountToIncreaseId);
            accountToIncrease.Balance += transaction.Sum;
            context.UpdateAccount(accountToIncrease);
            context.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            
            var accounts = context.GetAccounts().ToList();

            TransactionRecord transactionRecord = new TransactionRecord
            {
                Id = transaction.Id,
                Sum = transaction.Sum,
                Description = transaction.Description,
                DateTime = transaction.DateTime,
                AccountToIncreaseId = accounts.Where(a => a.Name == transaction.AccountToIncreaseName).FirstOrDefault().Id,
                AccountToSubstractId = accounts.Where(a => a.Name == transaction.AccountToSubstractName).FirstOrDefault().Id
            };

            context.DeleteTransaction(transactionRecord);
            context.SaveChanges();

            //Account accountToSubstruct = context.GetAccountById(transactionRecord.AccountToSubstractId);
            //accountToSubstruct.Balance += transaction.Sum;
            //context.UpdateAccount(accountToSubstruct);
            //context.SaveChanges();

            //Account accountToIncrease = context.GetAccountById(transactionRecord.AccountToIncreaseId);
            //accountToIncrease.Balance -= transaction.Sum;
            //context.UpdateAccount(accountToIncrease);
            //context.SaveChanges();

        }
    }
}
