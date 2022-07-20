using Microsoft.AspNetCore.Hosting;
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
    public class MemberController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CoffeeContext _context;
        private static Admin signIn_User;
        private static int currentMembersOrderDetails;

        public MemberController(CoffeeContext context, IWebHostEnvironment host)
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
                if (signIn_User.MemberOk)
                {
                    List<CMemberViewModel> list = new List<CMemberViewModel>();
                    var db = _context.Members;
                    if (string.IsNullOrEmpty(vModel.txtKeyword)) // 若沒輸入關鍵字則回傳所有會員
                    {
                        foreach(Member m in db)
                        {
                            CMemberViewModel MemVModel = new CMemberViewModel();
                            MemVModel.member = m;
                            list.Add(MemVModel);
                        }                        
                    }
                    else
                    {
                        if (vModel.txtKeyword == "未停權") // 輸入未停權回傳未停權的會員
                        {
                            foreach (Member m in db.Where(m => m.BlackList == false)) 
                            {
                                CMemberViewModel MemVModel = new CMemberViewModel();
                                MemVModel.member = m;
                                list.Add(MemVModel);
                            }                             
                        }                            
                        else if (vModel.txtKeyword == "已停權") // 輸入已停權回傳已停權的會員
                        {
                            foreach (Member m in db.Where(m => m.BlackList == true))
                            {
                                CMemberViewModel MemVModel = new CMemberViewModel();
                                MemVModel.member = m;
                                list.Add(MemVModel);
                            }                            
                        }                        
                        else if (vModel.txtKeyword.StartsWith("#"))
                        {
                            string keyword = vModel.txtKeyword.Remove(0, 1); // 假如使用者輸入#查找會員編號(ex:#10)，則去除掉"#"後查找
                            foreach(Member m in db.Where(m=>m.MemberId == Convert.ToInt32(keyword)))
                            {
                                CMemberViewModel MemVModel = new CMemberViewModel();
                                MemVModel.member = m;
                                list.Add(MemVModel);
                            }                            
                        }
                        else
                        {
                            foreach(Member m in db.Where(m => m.MemberPhone.Contains(vModel.txtKeyword) || // 依輸入關鍵字查詢會員編號, 地址, 會員電話, Email
                                                              m.MemberAddress.Contains(vModel.txtKeyword) ||
                                                              m.MemberName.Contains(vModel.txtKeyword) ||                                                              
                                                              m.MemberEmail.Contains(vModel.txtKeyword)))
                            {
                                CMemberViewModel MemVModel = new CMemberViewModel();
                                MemVModel.member = m;
                                list.Add(MemVModel);
                            }                            
                        }
                    }

                    return View(list);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult suspend(int id)
        {            
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {                
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.MemberOk)
                {
                    Member m = _context.Members.Find(id);
                    m.BlackList = true;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult restart(int id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.MemberOk)
                {
                    Member m = _context.Members.Find(id);
                    m.BlackList = false;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Details(int? id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.MemberOk)
                {
                    CMemberDetailsViewModel modelDetails = new CMemberDetailsViewModel();
                    CMemberViewModel model = new CMemberViewModel();
                    currentMembersOrderDetails = (int)id;
                    model.member = _context.Members.Find(id);
                    if (model.member == null)
                    {
                        return RedirectToAction("Index");
                    }

                    modelDetails.member = model;
                    var list = _context.Orders.Where(o => o.MemberId == id).Select(o => new COrderViewModel()
                    {
                        order = o,
                        Payment = o.Payment,
                        Coupon = o.Coupon,
                        OrderState = o.OrderState,
                        
                    })
                        .ToList();

                    modelDetails.order = list;
                    return View(modelDetails);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Filter1(int? id)
        {
            IEnumerable<COrderViewModel> datas = null;
            List<COrderViewModel> list = new List<COrderViewModel>();
            var dborder = _context.Orders.Where(o => o.MemberId == currentMembersOrderDetails).Select(t => new COrderViewModel
            {
                OrderId = t.OrderId,
                Payment = t.Payment,
                OrderState = t.OrderState,
                TradeNo = t.TradeNo,
                MemberId = t.MemberId,
                OrderDate = t.OrderDate,
                OrderStateId = t.OrderStateId,
                PaymentId = t.PaymentId,
                OrderAddress = t.OrderAddress,
                OrderPhone = t.OrderPhone,
                OrderReceiver = t.OrderReceiver
            });

            foreach (COrderViewModel item in dborder)
            {
                COrderViewModel o = new COrderViewModel();
                o.OrderId = item.OrderId;
                o.Payment = item.Payment;
                o.OrderState = item.OrderState;
                o.TradeNo = item.TradeNo;
                o.MemberId = item.MemberId;
                o.OrderDate = item.OrderDate;
                o.OrderStateId = item.OrderStateId;
                o.PaymentId = item.PaymentId;
                o.OrderAddress = item.OrderAddress;
                o.OrderPhone = item.OrderPhone;
                o.OrderReceiver = item.OrderReceiver;
                list.Add(o);
            }
            if (id == 0)
            {
                datas = list;
            }
            else
                datas = list.Where(p => p.OrderStateId == id);

            return PartialView(datas);
        }

        public IActionResult Detail(int? id)
        {
            var couponid = _context.Orders.FirstOrDefault(t => t.OrderId == id)?.CouponId;
            var fee = _context.Orders.FirstOrDefault(t => t.OrderId == id)?.Fee;

            decimal couponprice = 0;

            if (fee == null) fee = 0;
            if (couponid != null)
            {
                couponprice = _context.Coupons.FirstOrDefault(c => c.CouponId == couponid).Money;
            }

            var data = _context.OrderDetails.Where(t => t.OrderId == id).Select(o => new
            {
                d產品名 = o.Product.ProductName,
                d單價 = o.Product.Price,
                d數量 = o.Quantity,
                d小計 = o.Product.Price * o.Quantity,
                d運費 = fee,
                d優惠卷金額 = couponprice
            });

            return Json(data);
        }
    }
}
