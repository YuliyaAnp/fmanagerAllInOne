using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fmanagerFull.Models;

namespace fmanagerFull.Controllers
{
    public class TransactionsService
    {
        public SqlDbClient client;

        public TransactionsService(SqlDbClient sqlDbClient)
        {
            client = sqlDbClient;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var transactionRecords = client.GetTransactions();

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
                });
            }

            return transactions;
        }

        public Transaction GetById(int id)
        {
            var transactionRecord = client.GetTransactionById(id);

            Transaction transaction = new Transaction
            {
                Sum = transactionRecord.Sum,
                Description = transactionRecord.Description,
                DateTime = transactionRecord.DateTime,
                AccountToIncreaseName = client.GetAccountById(transactionRecord.AccountToIncreaseId).Name,
                AccountToSubstractName = client.GetAccountById(transactionRecord.AccountToSubstractId).Name
            };

            return transaction;
        }

        public void AddTransaction(Transaction transaction)
        {
            var accounts = client.GetAllAccounts();

            TransactionRecord transactionRecord = new TransactionRecord
            {
                Sum = transaction.Sum,
                Description = transaction.Description,
                DateTime = transaction.DateTime,
                AccountToIncreaseId = accounts.FirstOrDefault(a => a.Name == transaction.AccountToIncreaseName).Id,
                AccountToSubstractId = accounts.FirstOrDefault(a => a.Name == transaction.AccountToSubstractName).Id
            };

            client.InsertTransactionRecord(transactionRecord);

            //Account accountToSubstruct = context.GetAccountById(transactionRecord.AccountToSubstractId);
            //accountToSubstruct.Balance -= transaction.Sum;
            //context.UpdateAccount(accountToSubstruct);
            //context.SaveChanges();

            //Account accountToIncrease = context.GetAccountById(transactionRecord.AccountToIncreaseId);
            //accountToIncrease.Balance += transaction.Sum;
            //context.UpdateAccount(accountToIncrease);
            //context.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            
            //var accounts = context.GetAccounts().ToList();

            //TransactionRecord transactionRecord = new TransactionRecord
            //{
            //    Id = transaction.Id,
            //    Sum = transaction.Sum,
            //    Description = transaction.Description,
            //    DateTime = transaction.DateTime,
            //    AccountToIncreaseId = accounts.Where(a => a.Name == transaction.AccountToIncreaseName).FirstOrDefault().Id,
            //    AccountToSubstractId = accounts.Where(a => a.Name == transaction.AccountToSubstractName).FirstOrDefault().Id
            //};

            //context.DeleteTransaction(transactionRecord);
            //context.SaveChanges();

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
