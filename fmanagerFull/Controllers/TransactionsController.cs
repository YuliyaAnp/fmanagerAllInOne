﻿using System.Threading.Tasks;
using fmanagerFull.Models;
using Microsoft.AspNetCore.Mvc;

namespace fmanagerFull.Controllers
{
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsService transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
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
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await transactionsService.GetById(id);
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

            await transactionsService.AddTransaction(transaction);

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
        public async Task<IActionResult> Delete(int id)
        {
            var trans = await transactionsService.GetById(id);
            if (trans == null)
                return NotFound();

            transactionsService.DeleteTransaction(trans);

            return new NoContentResult();

        }
    }
}
