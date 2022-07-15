using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CoffeeContext _context;
        private static Admin signIn_User;

        public ProductController(CoffeeContext context, IWebHostEnvironment host)
        {
            _environment = host;
            _context = context;
        }

        public IActionResult Index(CKeywordViewModel vModel)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
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
                        Star = p.Star,
                        MainPhotoPath = p.MainPhotoPath
                    });

                    if (string.IsNullOrEmpty(vModel.txtKeyword)) // 若沒輸入關鍵字則回傳所有產品
                    {
                        datas = list;
                    }
                    else
                    {
                        if (vModel.txtKeyword == "上架")
                            datas = list.Where(p => p.TakeDown == false); // 輸入上架回傳上架商品
                        else if (vModel.txtKeyword == "下架")
                            datas = list.Where(p => p.TakeDown == true); // 輸入下架回傳下架商品
                        else
                        {
                            datas = list.Where(p => p.ProductName.Contains(vModel.txtKeyword) || // 依輸入關鍵字查詢類別, 國家, 產品名
                                               p.Category.CategoriesName.Contains(vModel.txtKeyword) ||
                                               p.Country.CountryName.Contains(vModel.txtKeyword));
                        }
                    }
                    return View(datas);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
                {
                    return View();
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult Create(CProductViewModel p)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
                {
                    // 新增產品
                    Product prod = new Product();
                    prod.ProductName = p.ProductName;
                    prod.CategoryId = p.CategoryId;
                    prod.CountryId = p.CountryId;
                    prod.Price = p.Price;
                    prod.Description = p.Description;
                    prod.Stock = p.Stock;
                    prod.TakeDown = p.TakeDown;

                    // 新增產品圖片
                    if (p.photo != null)
                    {
                        string pName = Guid.NewGuid().ToString() + ".jpg";
                        p.photo.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + pName, FileMode.Create));
                        prod.MainPhotoPath = pName;
                    }

                    _context.Products.Add(prod);
                    _context.SaveChanges();

                    // 若新增的產品為咖啡類別，在咖啡資料表內新增該產品
                    int productId = _context.Products.AsEnumerable().Last().ProductId;
                    int coffeeCateId = _context.Categories.FirstOrDefault(c => c.CategoriesName == "咖啡").CategoryId;

                    if (p.CategoryId == coffeeCateId)
                    {
                        Coffee coffee = new Coffee()
                        {
                            ProductId = productId,
                            CoffeeName = p.ProductName,
                            CountryId = (int)p.CountryId,
                            RoastingId = p.RoastingId,
                            PackageId = p.PackageId,
                            ProcessId = p.ProcessId,
                            RainForest = p.RainForest
                        };

                        _context.Coffees.Add(coffee);
                        _context.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult TakeDown(int id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
                {
                    Product p = _context.Products.Find(id);
                    p.TakeDown = true;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Launch(int id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
                {
                    Product p = _context.Products.Find(id);
                    p.TakeDown = false;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
                {
                    List<CProductViewModel> prod = null;
                    if (_context.Coffees.Any(c => c.ProductId == id))
                    {
                        prod = _context.Products.Include(p => p.Photos).Where(p => p.ProductId == id).Select(p => new CProductViewModel()
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = p.CategoryId,
                            CountryId = p.CountryId,
                            Coffee = p.Coffee,
                            PackageId = p.Coffee.PackageId,
                            ProcessId = p.Coffee.ProductId,
                            RoastingId = p.Coffee.RoastingId,
                            RainForest = p.Coffee.RainForest,
                            Price = p.Price,
                            Description = p.Description,
                            Stock = p.Stock,
                            ClickCount = p.ClickCount,
                            TakeDown = p.TakeDown,
                            Star = p.Star,
                            MainPhotoPath = p.MainPhotoPath,
                            Subphotos = p.Photos.Select(p=>p.ImagePath).ToList()
                        })
                            .ToList();
                    }
                    else
                    {
                        prod = _context.Products.Include(p=>p.Photos).Where(p => p.ProductId == id).Select(p => new CProductViewModel()
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = p.CategoryId,
                            CountryId = p.CountryId,
                            Coffee = p.Coffee,
                            Price = p.Price,
                            Description = p.Description,
                            Stock = p.Stock,
                            ClickCount = p.ClickCount,
                            TakeDown = p.TakeDown,
                            Star = p.Star,
                            MainPhotoPath = p.MainPhotoPath,
                            Subphotos = p.Photos.Select(p => p.ImagePath).ToList()
                        })
                            .ToList();
                    }

                    var lstCatrgory = _context.Categories.ToList();
                    var lstCountry = _context.Countries.ToList();
                    var lstPackage = _context.Packages.ToList();
                    var lstProcess = _context.Processes.ToList();
                    var lstRoasting = _context.Roastings.ToList();

                    ViewBag.CateListItem = CSelectList.ToSelectList(lstCatrgory);
                    ViewBag.CtryListItem = CSelectList.ToSelectList(lstCountry);
                    ViewBag.PkgListItem = CSelectList.ToSelectList(lstPackage);
                    ViewBag.PrcsListItem = CSelectList.ToSelectList(lstProcess);
                    ViewBag.RoastListItem = CSelectList.ToSelectList(lstRoasting);

                    if (prod.Count == 0)
                        return RedirectToAction("Index");
                    return View(prod[0]);                    
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult Edit(CProductViewModel p)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.ProductOk)
                {
                    // 修改產品
                    Product prod = _context.Products.Find(p.ProductId);
                    if (prod != null)
                    {
                        if (p.photo != null)
                        {
                            string pName = Guid.NewGuid().ToString() + ".jpg";
                            p.photo.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + pName, FileMode.Create));
                            string aa = p.photo.FileName;
                            //string bb1 = p.Subphotos[0].FileName;
                            //string bb2 = p.Subphotos[1].FileName;
                            prod.MainPhotoPath = pName;
                        }
                        prod.ProductName = p.ProductName;
                        prod.CategoryId = p.CategoryId;
                        prod.CountryId = p.CountryId;
                        prod.Price = p.Price;
                        prod.Description = p.Description;
                        prod.Stock = p.Stock;
                        prod.TakeDown = p.TakeDown;
                    };

                    _context.SaveChanges();

                    // 假如產品修改類別為咖啡
                    int coffeeCateId = _context.Categories.FirstOrDefault(c => c.CategoriesName == "咖啡").CategoryId;

                    if (p.CategoryId == coffeeCateId)
                    {
                        // 若產品原本不是咖啡類別，在咖啡資料表內新增該產品
                        Coffee product = _context.Coffees.FirstOrDefault(c => c.ProductId == p.ProductId);
                        if (product == null)
                        {
                            _context.Coffees.Add(new Coffee()
                            {
                                ProductId = p.ProductId,
                                CoffeeName = p.ProductName,
                                PackageId = p.PackageId,
                                ProcessId = p.ProcessId,
                                RoastingId = p.RoastingId,
                                RainForest = p.RainForest,
                                CountryId = (int)p.CountryId
                            });
                        }
                        else
                        {
                            // 若產品原本就是咖啡類別，更新產品資料
                            product.CoffeeName = p.ProductName;
                            product.PackageId = p.PackageId;
                            product.ProcessId = p.ProcessId;
                            product.RoastingId = p.RoastingId;
                            product.RainForest = p.RainForest;
                            product.CountryId = (int)p.CountryId;
                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        // 若產品類別不是咖啡，且在咖啡資料表內找到該產品
                        Coffee coffee = _context.Coffees.FirstOrDefault(c => c.ProductId == p.ProductId);
                        if (coffee != null)
                        {
                            // 在咖啡資料表中刪除該產品
                            _context.Coffees.Remove(coffee);
                            _context.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
