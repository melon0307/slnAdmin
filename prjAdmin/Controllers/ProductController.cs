using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _environment;

        public ProductController(IWebHostEnvironment host)
        {
            _environment = host;
        }

        public IActionResult Index(CKeywordViewModel vModel)
        {
            IEnumerable<CProductViewModel> datas = null;
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

            if (string.IsNullOrEmpty(vModel.txtKeyword)) // 若沒輸入關鍵字則回傳所有產品
            {
                datas = list;                
            }
            else
            {
                datas = list.Where(p => p.ProductName.Contains(vModel.txtKeyword) || // 依輸入關鍵字查詢類別, 國家, 產品名
                                        p.Category.CategoriesName.Contains(vModel.txtKeyword) ||
                                        p.Country.CountryName.Contains(vModel.txtKeyword));
            }
            return View(datas);            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
