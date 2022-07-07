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
        public static Admin signin_user = null;
        private readonly CoffeeContext _context;
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
                    signin_user = JsonSerializer.Deserialize<Admin>(JsonUser);
                    return RedirectToAction("Index");
                }
            }
            return View();
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
