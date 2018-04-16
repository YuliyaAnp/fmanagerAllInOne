using System.Collections.Generic;
using System.Threading.Tasks;
using fmanagerFull.Models;

namespace fmanagerFull.Controllers
{
    public interface ITransactionsService
    {
        IEnumerable<Transaction> GetTransactions();

        Task<Transaction> GetById(int id);

        Task AddTransaction(Transaction transaction);

        void DeleteTransaction(Transaction trans);
    }
}