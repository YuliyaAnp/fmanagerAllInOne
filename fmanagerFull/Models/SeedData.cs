//using System;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace fmanagerFull.Models
//{
//    public static class SeedData
//    {
//        public static void Initialize(IServiceProvider serviceProvider)
//        {
//            using (var context = new FinanceManagerContext(
//                serviceProvider.GetRequiredService<DbContextOptions<FinanceManagerContext>>()))
//            {
//                if (!context.Transaction.Any())
//                {
//                    context.Transaction.AddRange(
//                     new TransactionRecord
//                     {
//                         Sum = -10,
//                         Description = "McDonalds",
//                         DateTime = DateTime.Today,
//                         AccountToIncreaseId = 4,
//                         AccountToSubstractId = 2
//                     },
//                     new TransactionRecord
//                     {
//                         Sum = 300,
//                         Description = "Salary",
//                         DateTime = DateTime.Today,
//                         AccountToIncreaseId = 3
//                     },
//                     new TransactionRecord
//                     {
//                         Sum = -10,
//                         Description = "Coffee",
//                         DateTime = DateTime.Today,
//                         AccountToIncreaseId = 4,
//                         AccountToSubstractId = 1
//                     }

//                    );

//                    context.SaveChanges();
//                }
//                if (!context.Account.Any())
//                {
//                    context.Account.AddRange(
//                         new Account
//                         {
//                             Balance = 0,
//                             Currency = Currency.Ruble,
//                             Name = "Rubles cash",
//                             Type = AccountType.Assets
//                         },
//                        new Account
//                         {
//                             Balance = 0,
//                             Currency = Currency.Pound,
//                             Name = "Santander debit card",
//                             Type = AccountType.Assets
//                         },
//                        new Account
//                        {
//                            Balance = 0,
//                            Currency = Currency.Pound,
//                            Name = "Salary",
//                            Type = AccountType.Income
//                        },
//                        new Account
//                        {
//                            Balance = 0,
//                            Currency = Currency.Pound,
//                            Name = "Cafe",
//                            Type = AccountType.Expenses
//                        },
//                        new Account
//                        {
//                            Balance = 0,
//                            Currency = Currency.Pound,
//                            Name = "Food",
//                            Type = AccountType.Expenses
//                        }                        
//                        );

//                    context.SaveChanges();
//                }
//            }

//        }
//    }
//}