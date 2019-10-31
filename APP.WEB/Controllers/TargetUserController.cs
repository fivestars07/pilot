using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.BLL.TargetUserBll;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APP.WEB.Controllers
{
    public class TargetUserController : Controller
    {

        private readonly TargetUserBll _targetUserBll;

        public TargetUserController(TargetUserBll targetUserBll)
        {
            _targetUserBll = targetUserBll;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            _targetUserBll.GetList_TargetUser(1);
            _targetUserBll.Get_TargetUser(1);
            return View();
        }

    }
}
