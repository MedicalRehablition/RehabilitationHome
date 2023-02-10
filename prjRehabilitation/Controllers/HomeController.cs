using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _environment;
        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            ViewBag.User = "2";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));
            string json = "";
            if (customer != null)
            {
                if (customer.FEmail.Equals(vm.txtAccount) && customer.FPassword.Equals(vm.txtPassword))
                {
                    json = JsonSerializer.Serialize(customer);
                    HttpContext.Session.SetString(CDictionary.SK_CUSTOMER_User, json);
                    return RedirectToAction("Index");
                }
            }
            
            return View();
        }
        public IActionResult PartialLogin()
        {
            return PartialView();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult SendMailByGmail(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(t => t.FEmail == vm.txtAccount);
            if (string.IsNullOrEmpty(customer.FEmail))
            {
                return Content("無此信箱帳號");
            }
            customer.FEmail = vm.txtAccount;
            customer.FPassword = Guid.NewGuid().ToString();
            string beforePassword = customer.FPassword;//加密前密碼
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
               string afterPassword=beforePassword + "putSomeSalt";
                var bytes = Encoding.UTF8.GetBytes(afterPassword);
                var hash = cryptoMD5.ComputeHash(bytes);
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();
                customer.FPassword = md5; //加密後密碼
            }
            db.SaveChanges();
            string newPassword = beforePassword;
            List<string> MailList = new List<string>();
            MailList.Add(vm.txtAccount);//新增收件人進去
            string Subject = "變更密碼";
            string Body = "您的密碼為:" + newPassword;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("yeeee880726@gmail.com", "測試郵件", System.Text.Encoding.UTF8);
            msg.To.Add(string.Join(",", MailList.ToArray()));//收件人
            msg.Subject = Subject; //主旨
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = Body;//內容
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Priority = MailPriority.Normal;
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
            //寄件人
            MySmtp.Credentials = new System.Net.NetworkCredential("yeeee880726@gmail.com", "dkyzsdpffgrgount");
            MySmtp.EnableSsl = true;
            MySmtp.Send(msg);
            return Content("已發送郵件");
        }
        public IActionResult ExistAccount(CLoginViewModel vm)//是否資料庫有資料
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount));
            string json = "";
            if (customer != null)
            {
                if (customer.FEmail == vm.txtAccount)
                {
                    return Content("1");
                }
            }
            return Content("0");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CCustomerViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            //if (vm.photo != null)
            //{
            //    string photoName = Guid.NewGuid().ToString() + ".jpg";
            //    string path = _environment.WebRootPath + "/images/" + photoName;
            //    vm.FPicture = photoName;
            //    vm.photo.CopyTo(new FileStream(path, FileMode.Create));
            //}
            //db.Customers.Add(vm.Customer);
            //db.SaveChanges();
            return RedirectToAction("index");
        }
     
    }
}