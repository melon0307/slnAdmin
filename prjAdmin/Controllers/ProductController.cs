using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var list = (new CoffeeContext()).Products.Select(p => new CProductViewModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Category = p.Category,
                Country = p.Country,
                Price = p.Price,
                Description = p.Description,
                Stock = p.Stock,
                ClickCount = p.ClickCount,
                TakeDown = p.TakeDown,
                Star = p.Star
            });
            return View(list);
        }
    }
}
