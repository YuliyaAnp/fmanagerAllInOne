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
        readonly TransactionsService transactionsService;
        readonly TestSqlDbClient testSqlClient;
        readonly SqlDbClient sqlClient;

        public TransactionsServiceShould()
        {
            testSqlClient = new TestSqlDbClient("TestDatabase", "admin", "1234");
            sqlClient = new SqlDbClient("TestDatabase", "admin", "1234");

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
                            Id = 1,
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
                    Id = 1,
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
                            Id = 1,
                            Sum = -10,
                            Description = "McDonalds",
                            DateTime = "2018-04-28",
                            AccountToIncreaseName = "Cafe",
                            AccountToSubstractName = "Salary"
                        },
                new Transaction()
                        {
                            Id = 2,
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

        [Fact]
        public void ReturnUpdatedTransactions_WhenDeleteTransaction()
        {
            var expectedResult = new List<Transaction>();

            var transactionToDelete = new Transaction()
            {
                Id = 1,
                Sum = -10,
                Description = "McDonalds",
                DateTime = "2018-04-28",
                AccountToIncreaseName = "Cafe",
                AccountToSubstractName = "Salary"
            };

            transactionsService.DeleteTransaction(transactionToDelete);

            var actualResult = transactionsService.GetTransactions();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        public void Dispose()
        {
            testSqlClient.DropTestDatabase();
        }

    }
}
