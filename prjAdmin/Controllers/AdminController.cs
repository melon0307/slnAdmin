using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{    
    public class AdminController : Controller
    {
        private static Admin signIn_User;
        private readonly CoffeeContext db;

        public AdminController(CoffeeContext context)
        {
            db = context;
        }
        public IActionResult List()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.AdminOk)
                {
                    var q = db.Admins;
                    return View(q);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Admin a)
        {
            db.Admins.Add(a);
            db.SaveChanges();

            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.AdminOk)
                {
                    var q = db.Admins.FirstOrDefault(a => a.AdminId == id);
                    return View(q);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public ActionResult Edit(Admin a)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.AdminOk)
                {
                    var q = db.Admins.FirstOrDefault(p => p.AdminId == a.AdminId);
                    if (q == null)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        q.Email = a.Email;
                        q.Password = a.Password;
                        q.ProductOk = a.ProductOk;
                        q.MemberOk = a.MemberOk;
                        q.ArticleOk = a.ArticleOk;
                        q.AdminOk = a.AdminOk;
                    }
                    db.SaveChanges();
                    return RedirectToAction("List");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
