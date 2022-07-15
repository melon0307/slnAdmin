using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace prjAdmin.Controllers
{
    public class ApiController : Controller
    {
        private readonly CoffeeContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _configuration;
        public static string randomCode;

        public ApiController(CoffeeContext context, IWebHostEnvironment host, IConfiguration configuration)
        {
            _context = context;
            _host = host;
            _configuration = configuration;
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
            CSendMailTokenOut outModel = new CSendMailTokenOut();

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

                // 取得系統自訂密鑰，在appsettings.json設定
                string SecretKey = _configuration.GetValue<string>("AppConfiguration:SecretKey");

                // 產生時間驗證碼
                string sVerify = inModel.Email + "|" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

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
                mailContent = mailContent + "<a href='" + webPath + receivePage + "?verify=" + sVerify + "'  target='_blank'>點我重新設定密碼</a>";

                // 信件主題
                string mailSubject = "[CSharpCoffee]重新設定密碼";

                // Google發信者
                string GoogleMailUserID = _configuration.GetValue<string>("AppConfiguration:GoogleMailUserID");
                string GoogleMailUserPwd = _configuration.GetValue<string>("AppConfiguration:GoogleMailUserPwd");

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

        [ValidateAntiForgeryToken]
        public ActionResult DoResetPwd(CDoResetPwdIn inModel)
        {
            CDoResetPwdOut outModel = new CDoResetPwdOut();
            Regex r = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$");

            // 檢查是否有輸入密碼
            if (string.IsNullOrEmpty(inModel.NewUserPwd))
            {
                outModel.ErrMsg = "請輸入新密碼";
                return Json(outModel);
            }
            if (!r.IsMatch(inModel.CheckUserPwd))
            {
                outModel.ErrMsg = "密碼格式錯誤";
                return Json(outModel);
            }
            if (string.IsNullOrEmpty(inModel.CheckUserPwd))
            {
                outModel.ErrMsg = "請輸入確認新密碼";
                return Json(outModel);
            }
            if (inModel.NewUserPwd != inModel.CheckUserPwd)
            {
                outModel.ErrMsg = "新密碼與確認新密碼不相同";
                return Json(outModel);
            }

            // 檢查帳號 Session 是否存在
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_ResetPassword_UserId)||
                HttpContext.Session.GetString(CDictionary.SK_ResetPassword_UserId)=="")
            {
                outModel.ErrMsg = "無修改帳號";
                return Json(outModel);
            }

            string userEmail = HttpContext.Session.GetString(CDictionary.SK_ResetPassword_UserId);
            var user = _context.Admins.FirstOrDefault(a => a.Email == userEmail);
            user.Password = inModel.NewUserPwd;
            _context.SaveChanges();

            outModel.ResultMsg = "重設密碼完成";

            // 回傳 Json 給前端
            return Json(outModel);
        }

        public async Task<IActionResult> DeleteSubPhoto(string url)
        {
            string imageName = url.Split("/Images/")[1];
            int subPhotoId = _context.Photos.FirstOrDefault(p => p.ImagePath == imageName).PhotoId;
            var subPhoto = await _context.Photos.FindAsync(subPhotoId);
            if(subPhoto == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(subPhoto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
