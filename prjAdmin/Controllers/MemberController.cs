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

        public MemberController(CoffeeContext context, IWebHostEnvironment host)
        {
            _environment = host;
            _context = context;
        }

        public IActionResult Index(CKeywordViewModel vModel)
        {
            string JsonUser = "";
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                JsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                signIn_User = JsonSerializer.Deserialize<Admin>(JsonUser);
                if (signIn_User.MemberOk)
                {
                    IEnumerable<Member> datas = null;
                    var list = _context.Members;
                    if (string.IsNullOrEmpty(vModel.txtKeyword)) // 若沒輸入關鍵字則回傳所有會員
                    {
                        datas = list;
                    }
                    else
                    {
                        if (vModel.txtKeyword == "未停權")
                            datas = list.Where(m => m.BlackList == false); // 輸入未停權回傳未停權的會員
                        else if (vModel.txtKeyword == "已停權")
                            datas = list.Where(m => m.BlackList == true); // 輸入已停權回傳已停權的會員
                        else if (vModel.txtKeyword.Contains("#"))
                        {
                            string keyword = vModel.txtKeyword.Remove(0, 1);
                            datas = list.Where(m => m.MemberId == Convert.ToInt32(keyword));
                        }
                        else
                        {
                            datas = list.Where(m => m.MemberPhone.Contains(vModel.txtKeyword) || // 依輸入關鍵字查詢會員編號, 地址, 會員電話, Email
                                               m.MemberAddress.Contains(vModel.txtKeyword) ||
                                               m.MemberEmail.Contains(vModel.txtKeyword));                                               
                        }
                    }
                    return View(datas);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
