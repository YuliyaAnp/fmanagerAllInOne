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
                    if (context.Transaction.Any())
                    {
                        return;  
                    }

                    context.Transaction.AddRange(
                         new Transaction
                         {
                             Sum = 10,
                             Description = "Food",
                         },
                         new Transaction
                         {
                             Sum = -30,
                             Description = "Cinema",
                         },
                         new Transaction
                         {
                             Sum = 10,
                             Description = "Income",
                         }
                             
                        );

                    context.SaveChanges();
                }
            }

        }
}