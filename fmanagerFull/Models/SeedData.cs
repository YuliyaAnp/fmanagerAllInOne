using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace fmanagerFull.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FinanceManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<FinanceManagerContext>>()))
            {
                if (!context.Transaction.Any())
                {
                    context.Transaction.AddRange(
                     new Transaction
                     {
                         Sum = -10,
                         Description = "McDonalds",
                         DateTime = DateTime.Today,
                         AccountToIncreaseAmount = "Cafe",
                         AccountToSubstractAmount = "Santander debit card"
                     },
                     new Transaction
                     {
                         Sum = 300,
                         Description = "Salary",
                         DateTime = DateTime.Today,
                         AccountToIncreaseAmount = "Salary"
                     },
                     new Transaction
                     {
                         Sum = -10,
                         Description = "Coffee",
                         DateTime = DateTime.Today,
                         AccountToIncreaseAmount = "Cafe",
                         AccountToSubstractAmount = "Roubles cash"
                     }

                    );

                    context.SaveChanges();
                }
                if (!context.Account.Any())
                {
                    context.Account.AddRange(
                         new Account
                         {
                             Balance = 0,
                             Currency = Currency.Ruble,
                             Name = "Rubles cash",
                             Type = AccountType.Assets
                         },
                        new Account
                         {
                             Balance = 0,
                             Currency = Currency.Pound,
                             Name = "Santander debit card",
                             Type = AccountType.Assets
                         },
                        new Account
                        {
                            Balance = 0,
                            Currency = Currency.Pound,
                            Name = "Salary",
                            Type = AccountType.Income
                        },
                        new Account
                        {
                            Balance = 0,
                            Currency = Currency.Pound,
                            Name = "Cafe",
                            Type = AccountType.Expenses
                        }
                        );

                    context.SaveChanges();
                }
            }

        }
    }
}