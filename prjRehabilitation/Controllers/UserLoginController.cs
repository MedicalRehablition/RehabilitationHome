﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Drawing.Imaging;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class UserLoginController : Controller
    {
        private IWebHostEnvironment _environment;
        public UserLoginController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Loginsuccess()
        {
            return View();
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
                    return RedirectToAction("Loginsuccess");
                }
            }
            return View();
        }
        public IActionResult ExistAccount(CLoginViewModel vm)//是否資料庫有資料
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount));
            string json = "";
            if (customer != null)
            {
                if (customer.FEmail == vm.txtAccount && customer.FPassword != vm.txtPassword)
                {
                    return Content("1");
                }
                json = JsonSerializer.Serialize(customer);
                HttpContext.Session.SetString(CDictionary.SK_CUSTOMER_User, json);
                //HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
                //customer = JsonSerializer.Deserialize<Customer>(json);
                return Content(customer.FName);

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
            db.Customers.Add(vm.Customer);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult AccountAlive(CCustomerViewModel vm)//是否被註冊過
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(t => t.FEmail == vm.FEmail);

            if (customer == null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                string path = _environment.WebRootPath + "/images/" + photoName;
                vm.FPicture = photoName;
                vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                db.Customers.Add(vm.Customer);
                db.SaveChanges();
                //---------qrcode生成及儲存-----
                CCreateqrcode cqr = new CCreateqrcode();
                string emp = "c" + vm.Customer.Fid; //圖片要存的內容
                var QRpic = cqr.createqrcode(emp); //生成bitmap類型的圖片
                string qrname = emp + ".jpg";
                string qrpath = _environment.WebRootPath + "/images/" + qrname;
                Customer aqcust = db.Customers.FirstOrDefault(t => t.Fid == vm.Fid);               
                if (aqcust != null)
                {
                    aqcust.FQrcode = qrname;
                    using (var stream = new MemoryStream())
                    {
                        QRpic.Save(stream, ImageFormat.Jpeg);//把bitmap轉png
                        stream.Position = 0;
                        using (var fileStream = new FileStream(qrpath, FileMode.Create))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                    db.SaveChanges();
                }
                return Content("註冊成功!");//後面要記得改
                //return RedirectToAction("List");
            }
            return Content("此帳號已註冊使用,請前往登入");

        }
        public IActionResult PartialRegister()
        {
            return PartialView();
        }
        public IActionResult PartialForgetPassword()
        {
            return PartialView();
        }
        public IActionResult GetUserSession()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            if (string.IsNullOrEmpty(json))
            {
                return Content("");
            }
            Customer customer = JsonSerializer.Deserialize<Customer>(json);
            return Content(customer.FName);
        }
        public IActionResult RemoveUserSession()
        {
            HttpContext.Session.Remove(CDictionary.SK_CUSTOMER_User);
            return Content("清除session");
        }
        private bool checkMD5(CLoginViewModel vm)
        {
            //input=網頁使用者填的 & password=資料庫儲存經MD5處理的密碼
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將輸入的字串編碼成 UTF8 位元組陣列
                vm.txtAccount += "putSomeSalt"; //加鹽，避免駭客知道加密方法後回推密碼
                var bytes = Encoding.UTF8.GetBytes(vm.txtAccount);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();

                //雜湊化密碼相同即回傳正確
                if (md5 == vm.txtPassword) return true;
                return false;
            }
        }
    }
}
