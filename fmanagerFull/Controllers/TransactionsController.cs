using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fmanagerFull.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fmanagerFull.Controllers
{
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly TransactionContext context;

        public TransactionsController(TransactionContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = context.Transaction.ToList();
            return View(list);
        }

        [HttpGet("Get")]
        public JsonResult Get()
        {
            var list = context.Transaction.ToList();
            return Json(list);
        }

        [HttpGet("Get/{id}", Name = "GetTransaction")]
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await context.Transaction.SingleOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return new ObjectResult(transaction);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]Transaction transaction)
        {
            if (transaction == null)
                return BadRequest();

            await context.Transaction.AddAsync(transaction);
            context.SaveChanges();

            return CreatedAtRoute("GetTransaction", new { id = transaction.Id }, transaction);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Transaction transaction)
        {
            if (transaction == null || transaction.Id != id)
                return BadRequest();

            var tran = await context.Transaction.SingleOrDefaultAsync(t => t.Id == id);
            if (tran == null)
                return NotFound();

            tran.Sum = transaction.Sum;
            tran.Description = transaction.Description;

            context.Transaction.Update(tran);
            context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trans = await context.Transaction.SingleOrDefaultAsync(t => t.Id == id);
            if (trans == null)
                return NotFound();

            context.Transaction.Remove(trans);
            context.SaveChanges();

            return new NoContentResult();

        }
    }
}
