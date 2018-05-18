using System;
using Xunit;
using fmanagerFull.Models;
using System.Collections.Generic;
using fmanagerFull.Controllers;
using FluentAssertions;

namespace fmanagerFull.UnitTests
{
    public class TransactionsServiceShould : IDisposable
    {
        TransactionsService transactionsService;
        TestSqlDbClient testSqlClient;
        SqlDbClient sqlClient;

        public TransactionsServiceShould()
        {
            testSqlClient = new TestSqlDbClient("TestDatabase", "root", "515BuisWaW");
            sqlClient = new SqlDbClient("TestDatabase", "root", "515BuisWaW");

            transactionsService = new TransactionsService(sqlClient);

            testSqlClient.CreateTestDatabase();
            testSqlClient.CreateTestTransactionTable();
            testSqlClient.CreateTestAccountsTable();

            testSqlClient.InsertTestTransactions();
            testSqlClient.InsertTestAccounts();
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
                            DateTime = "2018-04-28",
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Salary"
                        }
            };

            var actualResult = transactionsService.GetTransactions();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ReturnCorrectTransaction_WhenGetById()
        {
            var expectedResult = new Transaction()
                {
                    Sum = -10,
                    Description = "McDonalds",
                    DateTime = "2018-04-28",
                    AccountToIncreaseName = "Cafe",
                    AccountToSubstractName = "Salary"
                };

            var actualResult = transactionsService.GetTransactionById(1);

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ReturnUpdatedTransactions_WhenAddTransaction()
        {
            var expectedResult = new List<Transaction>()
            {
                new Transaction()
                        {
                            Sum = -10,
                            Description = "McDonalds",
                            DateTime = "2018-04-28",
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Salary"
                        },
                new Transaction()
                        {
                            Sum = -20,
                            Description = "Mumu",
                            DateTime = "2018-05-01",
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Debit card"
                        }
            };
            
            var newTransaction = new Transaction()
            {
                Sum = -20,
                Description = "Mumu",
                DateTime = "2018-05-01",
                AccountToIncreaseName = "Cafe",
                AccountToSubstractName = "Debit card"
            };

            transactionsService.AddTransaction(newTransaction);

            var actualResult = transactionsService.GetTransactions();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        //[Fact]
        //public void ReturnUpdatedTransactions_WhenDeleteTransaction()
        //{
        // //   SetupTestDatabase();

        //    var expectedResult = new List<Transaction>()
        //    {
        //        new Transaction()
        //                {
        //                    Id = 1,
        //                    Sum = -10,
        //                    Description = "McDonalds",
        //                    DateTime = DateTime.Today,
        //                    AccountToIncreaseName = "Cafe",
        //                    AccountToSubstractName = "Santander debit card",
        //   //                 Currency = Currency.Pound
        //                }
        //    };

        //    var transactionToDelete = new Transaction()
        //    {
        //        Id = 2,
        //        Sum = -20,
        //        Description = "Mumu",
        //        DateTime = DateTime.Today,
        //        AccountToIncreaseName = "Cafe",
        //        AccountToSubstractName = "Rubles cash",
        //   //     Currency = Currency.Pound
        //    };

        //    transactionsService.DeleteTransaction(transactionToDelete);

        //    var actualResult = transactionsService.GetTransactions();

        //    actualResult.Should().BeEquivalentTo(expectedResult);
        //}

        public void Dispose()
        {
            testSqlClient.DropTestDatabase();
        }

    }
}
