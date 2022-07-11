using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class ApiController : Controller
    {
        private readonly CoffeeContext _context;
        private readonly IWebHostEnvironment _host;
        public static string randomCode;

        public ApiController(CoffeeContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            var category = _context.Categories;
            return Json(category);
        }

        public IActionResult Country()
        {
            var country = _context.Countries;
            return Json(country);
        }

        public IActionResult Roasting()
        {
            var roasting = _context.Roastings;
            return Json(roasting);
        }

        public IActionResult Package()
        {
            var package = _context.Packages;
            return Json(package);
        }
        public IActionResult Process()
        {
            var process = _context.Processes;
            return Json(process);
        }

        public IActionResult IfEmailExist(string email)
        {
            var emailExist = _context.Admins.Any(a => a.Email == email);
            return Content(emailExist.ToString(), "text/plain", Encoding.UTF8);
        }

        public IActionResult GetCaptcha()
        {
            randomCode = CCaptcha.CreateRandomCode(5).ToLower();
            byte[] captcha = CCaptcha.CreatImage(randomCode);
            return File(captcha, "image/jpeg");
        }
    }
}
