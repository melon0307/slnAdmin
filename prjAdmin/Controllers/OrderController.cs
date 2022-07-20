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
    public class OrderController : Controller
    {
        private readonly CoffeeContext db;
        private static Admin signIn_User;

        public OrderController(CoffeeContext context)
        {
            db = context;
        }
        public IActionResult Index(CKeywordViewModel vModel)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.OrderOk)
                {
                    IEnumerable<COrderViewModel> datas = null;
                    List<COrderViewModel> list = new List<COrderViewModel>();
                    var dborder = db.Orders.Select(t => new COrderViewModel
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

                    if (string.IsNullOrEmpty(vModel.txtKeyword))
                    {
                        datas = list;
                    }
                    else
                    {
                        datas = list.Where(p => p.TradeNo.Contains(vModel.txtKeyword) ||
                                    p.OrderReceiver.Contains(vModel.txtKeyword) ||
                                    p.OrderPhone.Contains(vModel.txtKeyword));
                    }

                    return View(datas);
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
            var dborder = db.Orders.Select(t => new COrderViewModel
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
            var couponid = db.Orders.FirstOrDefault(t => t.OrderId == id)?.CouponId;
            var fee = db.Orders.FirstOrDefault(t => t.OrderId == id)?.Fee;

            decimal couponprice = 0;

            if (fee == null) fee = 0;
            if (couponid != null)
            {
                couponprice = db.Coupons.FirstOrDefault(c => c.CouponId == couponid).Money;
            }

            var data = db.OrderDetails.Where(t => t.OrderId == id).Select(o => new
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

        public IActionResult Editstate(int? orderid, int? stateid)
        {
            var data = db.Orders.FirstOrDefault(t => t.OrderId == orderid);
            if (data != null)
            {
                data.OrderStateId = (int)stateid;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public IActionResult example()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                string JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.OrderOk)
                {

                    return View();
                }

                return RedirectToAction("Index", "Dashboard");
            }

            DashboardController.btnSignInText = "登入";
            return RedirectToAction("Index", "Dashboard");
        }


    }
}


