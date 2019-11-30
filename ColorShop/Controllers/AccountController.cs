using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        public IActionResult Login()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }
    }
}
