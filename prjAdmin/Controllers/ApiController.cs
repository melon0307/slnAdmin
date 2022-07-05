using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class ApiController : Controller
    {
        private readonly CoffeeContext _context;
        private readonly IWebHostEnvironment _host;

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
    }
}
