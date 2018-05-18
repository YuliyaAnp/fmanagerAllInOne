using System.Threading.Tasks;
using fmanagerFull.Models;
using Microsoft.AspNetCore.Mvc;

namespace fmanagerFull.Controllers
{
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly TransactionsService transactionsService;

        public TransactionsController(TransactionsService transactionsService)
        {
            this.transactionsService = transactionsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = transactionsService.GetTransactions(); 
            return View(list);
        }

        [HttpGet("Get")]
        public JsonResult Get()
        {
            var list = transactionsService.GetTransactions();
            return Json(list);
        }

        [HttpGet("Get/{id}", Name = "GetTransaction")]
        public IActionResult Get(int id)
        {
            var transaction = transactionsService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return new ObjectResult(transaction);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody]Transaction transaction)
        {
            if (transaction == null)
                return BadRequest();

            transactionsService.AddTransaction(transaction);

            return Ok();
        }

        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody]Transaction transaction)
        //{
        //    if (transaction == null || transaction.Id != id)
        //        return BadRequest();

        //    var tran = await context.GetById(id);
        //    if (tran == null)
        //        return NotFound();

        //    tran.Sum = transaction.Sum;
        //    tran.Description = transaction.Description;

        //    context.Update(tran);
        //    context.SaveChanges();

        //    return new NoContentResult();
        //}

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var trans = transactionsService.GetTransactionById(id);
            if (trans == null)
                return NotFound();

        //    transactionsService.DeleteTransaction(trans);

            return new NoContentResult();

        }
    }
}
