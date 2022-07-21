using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class Admin_DashboardController : Controller
    {
        public static Admin signIn_user = null;
        private readonly CoffeeContext _context;
        private readonly IConfiguration _configuration;
        public static string btnSignInText = "登入";        

        public Admin_DashboardController(CoffeeContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_ADMIN))
            {
                return RedirectToAction("Index", "Admin_Dashboard");
            }
            return PartialView();
        }

        [HttpPost]
        public IActionResult Signin(CLoginViewModel vModel)
        {
            string randomCode = Admin_ApiController.randomCode;
            if (vModel.Captcha.ToLower() == randomCode)
            {
                var user = _context.Admins.FirstOrDefault(a => a.Email == vModel.txtAccount);
                if (user != null)
                {
                    if (user.Password.Equals(vModel.txtPassword))
                    {
                        string JsonUser = JsonSerializer.Serialize(user); //user物件轉json
                        HttpContext.Session.SetString(CDictionary.SK_LOGINED_ADMIN, JsonUser); //json放到session
                        signIn_user = JsonSerializer.Deserialize<Admin>(JsonUser);
                        btnSignInText = "登出";
                        return RedirectToAction("Index");
                    }
                }                
            }

            return PartialView();
        }

        [HttpGet]
        public IActionResult Signout()
        {
            //if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            // {
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_ADMIN);
                signIn_user = null;
                btnSignInText = "登入";                
            //}

            return RedirectToAction("Index", "Admin_Dashboard");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Signup(Admin user)
        {            
            _context.Admins.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Signin");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult ResetPassword(string verify)
        {
            // 由信件連結回來會帶參數 verify

            if (verify == "")
            {
                ViewData["ErrorMsg"] = "缺少驗證碼";
                return PartialView();
            }

            // 取得系統自定密鑰，在 appsettings.json 設定
            string SecretKey = _configuration.GetValue<string>("AppConfiguration:SecretKey");

            try
            {
                // 使用 3DES 解密驗證碼
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] buf = Encoding.UTF8.GetBytes(SecretKey);
                byte[] md5result = md5.ComputeHash(buf);
                string md5Key = BitConverter.ToString(md5result).Replace("-", "").ToLower().Substring(0, 24);
                DES.Key = UTF8Encoding.UTF8.GetBytes(md5Key);
                DES.Mode = CipherMode.ECB;
                DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                byte[] Buffer = Convert.FromBase64String(verify);
                string deCode = UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));

                verify = deCode; //解密後還原資料
            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = "驗證碼錯誤";
                return PartialView();
            }

            // 取出帳號
            string UserID = verify.Split('|')[0];

            // 取得重設時間
            string ResetTime = verify.Split('|')[1];

            // 檢查時間是否超過 30 分鐘
            DateTime dResetTime = Convert.ToDateTime(ResetTime);
            TimeSpan TS = new System.TimeSpan(DateTime.Now.Ticks - dResetTime.Ticks);
            double diff = Convert.ToDouble(TS.TotalMinutes);
            if (diff > 30)
            {
                ViewData["ErrorMsg"] = "超過驗證碼有效時間，請重寄驗證碼";
                return PartialView();
            }

            // 驗證碼檢查成功，加入 Session
            HttpContext.Session.SetString(CDictionary.SK_ResetPassword_UserId, UserID);

            return PartialView();
        }       

    }
}
