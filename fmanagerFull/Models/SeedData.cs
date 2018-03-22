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
            using (var context = new TransactionContext(
                serviceProvider.GetRequiredService<DbContextOptions<TransactionContext>>()))
            {
                if (!context.Transaction.Any())
                {
                    context.Transaction.AddRange(
                     new Transaction
                     {
                         Sum = 10,
                         Description = "Food",
                         DateTime = DateTime.Today
                     },
                     new Transaction
                     {
                         Sum = -30,
                         Description = "Cinema",
                         DateTime = DateTime.Today
                     },
                     new Transaction
                     {
                         Sum = 10,
                         Description = "Income",
                         DateTime = DateTime.Today
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
                             Name = "Asos salary",
                             Type = AccountType.Income
                         }
                        );

                    context.SaveChanges();
                }
            }

        }
    }
}