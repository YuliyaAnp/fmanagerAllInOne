using fmanagerFull.Models;
using Microsoft.AspNetCore.Mvc;

namespace fmanagerFull.Controllers
{
    [Route("[controller]")]
    public class AccountsController : Controller
    {

        //public AccountsController(FinanceManagerContext context)
        //{
        //    this.context = context;
        //}

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var list = context.GetAccounts();
        //    return View(list);
        //}

        //[HttpGet("Get")]
        //public JsonResult Get()
        //{
        //    var list = context.GetAccounts();
        //    return Json(list);
        //}
    }
}
