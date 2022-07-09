using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class DashboardController : Controller
    {
        public static Admin signIn_user = null;
        private readonly CoffeeContext _context;
        public static string btnSignInText = "登入";

        public DashboardController(CoffeeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return PartialView();
        }

        [HttpPost]
        public IActionResult Signin(CLoginViewModel vModel)
        {
            var user = _context.Admins.FirstOrDefault(a => a.Email == vModel.txtAccount);
            if(user != null)
            {
                if (user.Password.Equals(vModel.txtPassword))
                {
                    string JsonUser = JsonSerializer.Serialize(user); //user物件轉json
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, JsonUser); //json放到session
                    signIn_user = JsonSerializer.Deserialize<Admin>(JsonUser);
                    btnSignInText = "登出";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Signout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
                signIn_user = null;
                btnSignInText = "登入";                
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Signup(Admin user)
        {
            
            return View();
        }
    }
}
