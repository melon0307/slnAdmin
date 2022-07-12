using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        [ValidateAntiForgeryToken]
        public IActionResult SendMailToken(CForgotPasswordViewModel inModel)
        {
            SendMailTokenOut outModel = new SendMailTokenOut();

            // 檢查輸入來源
            if (string.IsNullOrEmpty(inModel.Email))
            {
                outModel.ErrMsg = "請輸入信箱";
                return Json(outModel);
            }

            // 檢查資料庫是否有信箱
            if (_context.Admins.Any(a => a.Email == inModel.Email))
            {
                // 取得使用者信箱
                string userEmail = inModel.Email;

                // 取得系統自訂密鑰，在web.config設定
                string SecretKey = ConfigurationManager.AppSettings["SecretKey"];

                // 產生時間驗證碼
                string sVerify = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                // 驗證碼3DES加密
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] buf = Encoding.UTF8.GetBytes(SecretKey);
                byte[] result = md5.ComputeHash(buf);
                string md5Key = BitConverter.ToString(result).Replace("-", "").ToLower().Substring(0, 24);
                DES.Key = UTF8Encoding.UTF8.GetBytes(md5Key);
                DES.Mode = CipherMode.ECB;
                ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                byte[] Buffer = UTF8Encoding.UTF8.GetBytes(sVerify);
                sVerify = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));// 加密後驗證碼

                // 將加密後的驗證碼使用網址編碼處理
                sVerify = HttpUtility.UrlEncode(sVerify);

                // 網站網址
                string webPath = Request.Scheme + "://" + Request.Host + Url.Content("~/");

                // 從信件連結回到重設密碼頁面
                string receivePage = "Dashboard/ResetPassword";

                // 信件內容
                string mailContent = "請點擊以下連結，返回網站重新設定密碼，逾期 30 分鐘後，此連結將會失效。<br><br>";
                mailContent = mailContent + "<a href='" + webPath + receivePage + "?verify=" + sVerify + "'  target='_blank'>點此連結</a>";

                // 信件主題
                string mailSubject = "[測試]重設密碼";

                // Google發信者
                string GoogleMailUserID = ConfigurationManager.AppSettings["GoogleMailUserId"];
                string GoogleMailUserPwd = ConfigurationManager.AppSettings["GoogleMailUserPwd"];

                // 使用Google Mail Server 發信
                string SmtpServer = "smtp.gmail.com";
                int SmtpPort = 587;
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(GoogleMailUserID);
                mms.Subject = mailSubject;
                mms.Body = mailContent;
                mms.IsBodyHtml = true;
                mms.SubjectEncoding = Encoding.UTF8;
                mms.To.Add(new MailAddress(userEmail));
                using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(GoogleMailUserID, GoogleMailUserPwd); // 寄信帳號
                    client.Send(mms); // 寄出郵件
                }

                outModel.ResultMsg = "請於 30 分鐘內至你的信箱點擊連結重新設定密碼，逾期將無效";
            }
            else
            {
                outModel.ErrMsg = "查無此郵件信箱";
            }

            return Json(outModel);
        }
    }
}
