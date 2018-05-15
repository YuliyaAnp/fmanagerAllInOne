using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using fmanagerFull.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using fmanagerFull.Controllers;
using Newtonsoft.Json;
using FluentAssertions;

namespace fmanagerFull.UnitTests
{
    public class TransactionsServiceShould : IDisposable
    {
        TransactionsService transactionsService;
        SqlDbClient sqlClient;

        public TransactionsServiceShould()
        {
            sqlClient = new SqlDbClient("TestDatabase", "root", "515BuisWaW");
            transactionsService = new TransactionsService(sqlClient);

            sqlClient.CreateTestDatabase();
            sqlClient.CreateTestTransactionTable();
            sqlClient.CreateTestAccountsTable();

        }

        [Fact]
        public void ReturnAllTransactions_WhenGetTransactions()
        {
            var expectedResult = new List<Transaction>()
            {
                new Transaction()
                        {
                            Sum = -10,
                            Description = "McDonalds",
                            DateTime = DateTime.Parse("2018-04-28"),
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Salary",
                           // Currency = Currency.Pound
                        },
                //new Transaction()
                        //{
                        //    Sum = -20,
                        //    Description = "Mumu",
                        //    DateTime = DateTime.Today,
                        //    AccountToIncreaseName = "Cafe",
                        //    AccountToSubstractName = "Rubles cash",
                        //    Currency = Currency.Pound
                        //},
            };

            sqlClient.InsertTestTransactions();
            sqlClient.InsertTestAccounts();

            var actualResult = transactionsService.GetTransactions();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ReturnCorrectTransaction_WhenGetById()
        {
           // SetupTestDatabase();

            var expectedResult = new Transaction()
                {
                    Sum = -10,
                    Description = "McDonalds",
                    DateTime = DateTime.Today,
                    AccountToIncreaseName = "Cafe",
                    AccountToSubstractName = "Santander debit card",
                   // Currency = Currency.Pound
                };

            var actualResult = transactionsService.GetById(1);

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ReturnUpdatedTransactions_WhenAddTransaction()
        {
          //  SetupTestDatabase();

            var expectedResult = new List<Transaction>()
            {
                new Transaction()
                        {
                            Sum = -10,
                            Description = "McDonalds",
                            DateTime = DateTime.Today,
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Santander debit card",
                         //   Currency = Currency.Pound
                        },
                new Transaction()
                        {
                            Sum = -20,
                            Description = "Mumu",
                            DateTime = DateTime.Today,
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Rubles cash",
                          //  Currency = Currency.Pound
                        },
                new Transaction()
                        {
                            Sum = 300,
                            Description = "Waitrose",
                            DateTime = DateTime.Today,
                            AccountToIncreaseName = "Food",
                            AccountToSubstractName = "Rubles cash",
                         //   Currency = Currency.Pound
                        }
            };

            var newTransaction = new Transaction()
            {
                Sum = 300,
                Description = "Waitrose",
                DateTime = DateTime.Today,
                AccountToIncreaseName = "Food",
                AccountToSubstractName = "Rubles cash",
             //   Currency = Currency.Pound
            };

            transactionsService.AddTransaction(newTransaction);

            var actualResult = transactionsService.GetTransactions();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ReturnUpdatedTransactions_WhenDeleteTransaction()
        {
         //   SetupTestDatabase();

            var expectedResult = new List<Transaction>()
            {
                new Transaction()
                        {
                            Id = 1,
                            Sum = -10,
                            Description = "McDonalds",
                            DateTime = DateTime.Today,
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Santander debit card",
           //                 Currency = Currency.Pound
                        }
            };

            var transactionToDelete = new Transaction()
            {
                Id = 2,
                Sum = -20,
                Description = "Mumu",
                DateTime = DateTime.Today,
                AccountToIncreaseName = "Cafe",
                AccountToSubstractName = "Rubles cash",
           //     Currency = Currency.Pound
            };

            transactionsService.DeleteTransaction(transactionToDelete);

            var actualResult = transactionsService.GetTransactions();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        public void Dispose()
        {
            sqlClient.DropTestDatabase();
            //TODO: drop database

        }

        //private void SetupTestDatabase()
        //{
        //    var transactions = GetTestTransactions();
        //    var accounts = GetTestAccounts();

        //    testTransactionContext.Transaction.AddRange(transactions);
        //    testTransactionContext.Account.AddRange(accounts);
        //    testTransactionContext.SaveChanges();
        //}

        //private List<Account> GetTestAccounts()
        //{
        //    var accounts = new List<Account>();

        //    accounts.Add(
        //                new Account
        //                {
        //                    Id = 1,
        //                    Balance = 0,
        //                    Currency = Currency.Ruble,
        //                    Name = "Rubles cash",
        //                    Type = AccountType.Assets
        //                });
        //    accounts.Add(
        //                new Account
        //                {
        //                    Id = 2,
        //                    Balance = 0,
        //                    Currency = Currency.Pound,
        //                    Name = "Santander debit card",
        //                    Type = AccountType.Assets
        //                });
        //    accounts.Add(
        //                new Account
        //                {
        //                    Id = 3,
        //                    Balance = 0,
        //                    Currency = Currency.Pound,
        //                    Name = "Salary",
        //                    Type = AccountType.Income
        //                });
        //    accounts.Add(
        //                new Account
        //                {
        //                    Id = 4,
        //                    Balance = 0,
        //                    Currency = Currency.Pound,
        //                    Name = "Cafe",
        //                    Type = AccountType.Expenses
        //                });
        //    accounts.Add(
        //                new Account
        //                {
        //                    Id = 5,
        //                    Balance = 0,
        //                    Currency = Currency.Pound,
        //                    Name = "Food",
        //                    Type = AccountType.Expenses
        //                });


        //    return accounts;
        //}

        //private List<TransactionRecord> GetTestTransactions()
        //{
        //    var transactions = new List<TransactionRecord>();
        //    transactions.Add(new TransactionRecord()
        //             {
        //                 Sum = -10,
        //                 Description = "McDonalds",
        //                 DateTime = DateTime.Today,
        //                 AccountToIncreaseId = 4,
        //                 AccountToSubstractId = 2
        //             });
        //    transactions.Add(new TransactionRecord()
        //             {
        //                 Sum = -20,
        //                 Description = "Mumu",
        //                 DateTime = DateTime.Today,
        //                 AccountToIncreaseId = 4,
        //                 AccountToSubstractId = 1
        //             });
        //    return transactions;
        //}
    }
}
