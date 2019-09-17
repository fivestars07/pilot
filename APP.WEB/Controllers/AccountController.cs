using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APP.BLL;
using APP.MODEL;
using APP.VIEWMODEL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APP.WEB.Controllers
{
    public class AccountController : Controller
    {

        private readonly AccountBll _accountBll;
        private UserAll _userAll;
        private ClaimInfo _claimInfo;
        private readonly ILogger _logger;

        public AccountController(AccountBll accountBll, UserAll userAll, ClaimInfo claimInfo, ILogger<AccountController> logger)
        {
            _accountBll = accountBll;
            _userAll = userAll;
            _logger = logger;
            _claimInfo = claimInfo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Join(string rtnUrl = null)
        {
            ViewData["rtnUrl"] = rtnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Join(JoinView model, string rtnUrl = null)
        {
            ViewData["rtnUrl"] = rtnUrl;

            _accountBll.SetJoin(model);
            return View();
        }

        [HttpGet]
        public IActionResult Login(string rtnUrl = null)
        {
            ViewData["rtnUrl"] = rtnUrl;
            return View();
        }

        private bool ValidateLogin(string id, string password)
        {
            _userAll = _accountBll.GetSelectUser(id, password);
            if (_userAll.id.Equals(id))
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginView model, string rtnUrl = null)
        {
            ViewData["rtnUrl"] = rtnUrl;

            if (ValidateLogin(model.id, model.password))
            {
                // 추후 비동기 방식으로 작업 할것.
                _accountBll.SetLoginDate(_userAll);

                var claims = new List<Claim>
                {
                    new Claim("userNo", _userAll.userNo.ToString()),
                    new Claim("id", _userAll.id),
                    new Claim("ip", _userAll.ip),
                    new Claim("role", _userAll.role)
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "id", "role")),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(5)
                    });

                if (Url.IsLocalUrl(rtnUrl))
                {
                    return Redirect(rtnUrl);
                }
                else
                {
                    return Redirect("/");
                }

            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Claims()
        {
            return View();
        }

        private bool LoginCheck()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var claim in User.Claims)
                {
                    if (claim.Type.Equals("userNo"))
                    {
                        _claimInfo.userNo = Int32.Parse(claim.Value);
                    }

                    if (claim.Type.Equals("id"))
                    {
                        _claimInfo.id = claim.Value;
                    }

                    if (claim.Type.Equals("ip"))
                    {
                        _claimInfo.ip = claim.Value;
                    }

                    if (claim.Type.Equals("role"))
                    {
                        _claimInfo.role = claim.Value;
                    }
                }

                return true;
            }

            return false;
        }

        [HttpGet]
        public IActionResult EditView()
        {
            if (LoginCheck())
            {
                _userAll = _accountBll.GetSelectUser(_claimInfo.userNo);

                JoinView model = new JoinView();
                model.userNo = _userAll.userNo;
                model.id = _userAll.id;
                model.pwd = "";
                model.userName = _userAll.userName;
                model.email = _userAll.email;
                model.phone = _userAll.phone;
                model.role = _userAll.role;

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(JoinView model)
        {
            _accountBll.SetUpdateUser(model);

            return View();
        }

    }
}
