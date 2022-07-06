using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CoffeeContext _context;

        public ProductController(CoffeeContext context, IWebHostEnvironment host)
        {
            _environment = host;
            _context = context;
        }

        public IActionResult Index(CKeywordViewModel vModel)
        {
            IEnumerable<CProductViewModel> datas = null;
            var list = _context.Products.Select(p => new CProductViewModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Category = p.Category,
                Country = p.Country,
                Coffee = p.Coffee,
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
                if (vModel.txtKeyword == "上架")
                    datas = list.Where(p => p.TakeDown == false);
                else if (vModel.txtKeyword == "下架")
                    datas = list.Where(p => p.TakeDown == true);
                else
                {
                    datas = list.Where(p => p.ProductName.Contains(vModel.txtKeyword) || // 依輸入關鍵字查詢類別, 國家, 產品名
                                       p.Category.CategoriesName.Contains(vModel.txtKeyword) ||
                                       p.Country.CountryName.Contains(vModel.txtKeyword));
                }               
            }
            return View(datas);            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CProductViewModel p)
        {

            Product prod = new Product();
            prod.ProductName = p.ProductName;
            prod.CategoryId = p.CategoryId;
            prod.CountryId = p.CountryId;
            prod.Price = p.Price;
            prod.Description = p.Description;
            prod.Stock = p.Stock;
            prod.TakeDown = p.TakeDown;

            _context.Products.Add(prod);
            _context.SaveChanges();

            int productId = _context.Products.AsEnumerable().Last().ProductId;
            int coffeeCateId = _context.Categories.FirstOrDefault(c => c.CategoriesName == "咖啡").CategoryId;

            if(p.CategoryId == coffeeCateId)
            {
                Coffee coffee = new Coffee()
                {
                    ProductId = productId,
                    CoffeeName = p.ProductName,
                    CountryId = (int)p.CountryId,
                    RoastingId = p.RoastingId,
                    PackageId = p.PackageId,
                    ProcessId = p.ProcessId,
                    RainForest = p.RainForest,
                    ConstellationId = p.ConstellationId
                };
                _context.Coffees.Add(coffee);
                _context.SaveChanges();
            }

            if (p.photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                p.photo.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + pName, FileMode.Create));
                Photo photo = new Photo() { PhotoName = pName };
                _context.Photos.Add(photo);
                _context.SaveChanges();

                int photoId = _context.Photos.AsEnumerable().Last().PhotoId;
                PhotoDetail photoDetail = new PhotoDetail
                {
                    ProductId = productId,
                    PhotoId = photoId
                };
                _context.PhotoDetails.Add(photoDetail);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
