using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using fmanagerFull.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using fmanagerFull.Controllers;
using Newtonsoft.Json;

namespace fmanagerFull.UnitTests
{
    public class TransactionsControllerTests : IDisposable
    {
        FinanceManagerContext testTransactionContext;
        TransactionsController controller;

        public TransactionsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<FinanceManagerContext>().UseInMemoryDatabase("TestDatabase");
            testTransactionContext = new FinanceManagerContext(builder.Options);
            controller = new TransactionsController(testTransactionContext);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfTransactions()
        {
            SetupTestDatabase();

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Transaction>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void GetById_ReturnsObject_WithTransaction()
        {
            SetupTestDatabase();

            var expectedTransaction = new Transaction()
            {
                Sum = 100,
                Id = 1,
                Description = "Test One"
            };

            var result = controller.Get(1).Result as ObjectResult;

            var expectedString = JsonConvert.SerializeObject(expectedTransaction);
            var actualString = JsonConvert.SerializeObject(result.Value);

            Assert.Equal(expectedString, actualString);
        }

        [Fact]
        public void Get_ReturnsJsonList_WithAllTransactions()
        {
            SetupTestDatabase();

            var expectedListOfTransactions = new List<Transaction>{
            new Transaction {
                Sum = 100,
                Id = 1,
                Description = "Test One"
                },
            new Transaction {
                Sum = 200,
                Id = 2,
                Description = "Test Two"
                }
            };

            AssertThatTransactionsInDbAsExpected(expectedListOfTransactions);
        }

        [Fact]
        public void Create_AddsNewTransaction_ToTheListOfTransactions()
        {
            SetupTestDatabase();

            var expectedListOfTransactions = new List<Transaction>{
            new Transaction {
                Sum = 100,
                Id = 1,
                Description = "Test One"
                },
            new Transaction {
                Sum = 200,
                Id = 2,
                Description = "Test Two"
                },
           new Transaction {
                Sum = 300,
                Id = 3,
                Description = "Test Three"
                }
            };

            var newTransaction = new Transaction
            {
                Sum = 300,
                Id = 3,
                Description = "Test Three"
            };

            var createResult = controller.Create(newTransaction).Result;

            AssertThatTransactionsInDbAsExpected(expectedListOfTransactions);
        }

        [Fact]
        public void Delete_DeletesTransaction_FromTheListOfTransactions()
        {
            SetupTestDatabase();

            var expectedListOfTransactions = new List<Transaction>{
            new Transaction {
                Sum = 100,
                Id = 1,
                Description = "Test One"
                }
            };

            var deleteResult = controller.Delete(2).Result;

            AssertThatTransactionsInDbAsExpected(expectedListOfTransactions);
        }

        public void Dispose()
        {
            testTransactionContext.Database.EnsureDeleted();
            testTransactionContext.Dispose();
            controller.Dispose();
        }

        private void SetupTestDatabase()
        {
            var transactions = GetTestTransactions();

            testTransactionContext.Transaction.AddRange(transactions);
            int changed = testTransactionContext.SaveChanges();
        }

        private List<Transaction> GetTestTransactions()
        {
            var transactions = new List<Transaction>();
            transactions.Add(new Transaction()
            {
                Sum = 100,
                Id = 1,
                Description = "Test One"
            });
            transactions.Add(new Transaction()
            {
                Sum = 200,
                Id = 2,
                Description = "Test Two"
            });
            return transactions;
        }

        private void AssertThatTransactionsInDbAsExpected(List<Transaction> expectedListOfTransactions)
        {
            JsonResult actualJsonResult = controller.Get();

            var expectedJson = JsonConvert.SerializeObject(expectedListOfTransactions);
            var actualJson = JsonConvert.SerializeObject(actualJsonResult.Value);

            Assert.Equal(expectedJson, actualJson);
        }
    }
}
