using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CoffeeContext _context;
        private static Admin signIn_User;

        public ArticleController(CoffeeContext context, IWebHostEnvironment host)
        {
            _environment = host;
            _context = context;
        }
        //List
        public IActionResult List(CKeywordViewModel vModel)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ArticleOk)
                {
                    IEnumerable<CArticleViewModel> datas = null;
                    var list = _context.Articles.Select(a => new CArticleViewModel()
                    {
                        ArticleId = a.ArticleId,
                        ArticleName = a.ArticleName,
                        ArticleDescription = a.ArticleDescription,
                        ArticleDate = a.ArticleDate,
                        ArticleImage = a.ArticleImage
                    }).OrderByDescending(b => b.ArticleDate);

                    if (string.IsNullOrEmpty(vModel.txtKeyword))
                    {
                        datas = list;
                    }
                    else
                    {
                        datas = list.Where(a => a.ArticleName.Contains(vModel.txtKeyword));
                    }
                    return View(datas);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
        //Creat
        public IActionResult CreatArticle()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ArticleOk)
                {
                    return View();
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpPost]
        public IActionResult CreatArticle(CArticleViewModel a)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ArticleOk)
                {
                    Article art = new Article();
                    art.ArticleName = a.ArticleName;
                    art.ArticleDescription = a.ArticleDescription;
                    art.ArticleDate = a.ArticleDate;
                    if (a.photo != null)
                    {
                        string aName = Guid.NewGuid().ToString() + ".jpg";
                        a.photo.CopyTo(new FileStream(_environment.WebRootPath + "/articleImages/" + aName, FileMode.Create));
                        art.ArticleImage = aName;
                    }

                    _context.Articles.Add(art);
                    _context.SaveChanges();
                    return RedirectToAction("List");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
        //Delete
        public IActionResult DeleteArticle(int? id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ArticleOk)
                {
                    Article art = _context.Articles.FirstOrDefault(a => a.ArticleId == id);
                    if (art != null)
                    {
                        _context.Articles.Remove(art);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("List");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
        //Edit
        public IActionResult EditArticle(int? id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ArticleOk)
                {
                    CoffeeContext db = new CoffeeContext();
                    Article art = _context.Articles.FirstOrDefault(a => a.ArticleId == id);
                    if (art == null)
                    {
                        return RedirectToAction("List");
                    }
                    return View(art);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpPost]
        public IActionResult EditArticle(CArticleViewModel a)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ArticleOk)
                {
                    Article art = _context.Articles.FirstOrDefault(article => article.ArticleId == a.ArticleId);
                    if (art != null)
                    {
                        if (a.photo != null)
                        {
                            string aName = Guid.NewGuid().ToString() + ".jpg";
                            a.photo.CopyTo(new FileStream(_environment.WebRootPath + "/articleImages/" + aName, FileMode.Create));
                            art.ArticleImage = aName;
                        }
                        art.ArticleName = a.ArticleName;
                        art.ArticleDescription = a.ArticleDescription;
                        art.ArticleDate = a.ArticleDate;
                    }
                    _context.SaveChanges();
                    return RedirectToAction("List");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
