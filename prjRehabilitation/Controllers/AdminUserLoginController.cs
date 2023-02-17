using Google.Apis.Auth;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
using prjRehabilitation.ViewModel;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class AdminUserLoginController : SuperController
    {
        private IWebHostEnvironment _environment;
        public AdminUserLoginController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //show出員工名單
        public IActionResult List(CKeywordViewModel vm)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);//得到工作人員的session
            if (json != null)
            {
                Admin admin = JsonSerializer.Deserialize<Admin>(json);
                string rank = admin.FRank; //看是誰進來
                ViewBag.ank = rank;
                dbClassContext db = new dbClassContext();
                string keyword = vm.txtKeyword;
                IEnumerable<Admin> data = null;
                List<CAdminViewModel> List = new List<CAdminViewModel>();
                if (rank == "主任")
                {
                    if (keyword == null)
                    {
                        data = from c in db.Admins
                               select c;
                    }
                    else
                    {
                        data = db.Admins.Where(c => c.FName.Contains(keyword)).ToList();
                    }
                    foreach (var c in data)
                    {
                        CAdminViewModel a = new CAdminViewModel();
                        a.admin = c;
                        List.Add(a);
                    }
                    return View(List);
                }
                else
                {
                    return RedirectToAction("Edit", "AdminUserLogin", new { @id = admin.Fid });
                }

            };
            return View("Login", "AdminUserLogin");
        }


        //登入寫入Session
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));

            if (admin != null)
            {
                if (admin.FEmail.Equals(vm.txtAccount) && admin.FPassword.Equals(vm.txtPassword))
                {
                    string json = JsonSerializer.Serialize(admin);
                    HttpContext.Session.SetString(CDictionary.SK_ADMIN_User, json);
                    return RedirectToAction("List");
                }
            }
            return View();
        }
        public IActionResult ExistAccount(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail == vm.txtAccount);
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                vm.txtPassword += "putSomeSalt";
                var bytes = Encoding.UTF8.GetBytes(vm.txtPassword);
                var hash = cryptoMD5.ComputeHash(bytes);
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();
                vm.txtPassword = md5;
            }
            if (admin != null)
            {
                if (admin.FEmail == vm.txtAccount && admin.FPassword != vm.txtPassword)
                {
                    return Content("密碼錯誤");
                }
                string json = JsonSerializer.Serialize(admin);
                HttpContext.Session.SetString(CDictionary.SK_ADMIN_User, json);
                return Content("登入成功");
            }
            return Content("無此帳號,請重新登入");
        }
        //註冊or新增
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CAdminViewModel vm)
        {
            //dbClassContext db = new dbClassContext();
            //if (vm.photo != null)
            //{
            //    string photoName = Guid.NewGuid().ToString() + ".jpg";
            //    string path = _environment.WebRootPath + "/images/" + photoName;
            //    vm.Fphoto = photoName;
            //    vm.photo.CopyTo(new FileStream(path, FileMode.Create));
            //}
            //db.Admins.Add(vm.admin);
            //db.SaveChanges();
            //-----qrcode生成及儲存-----
            //qrcode內容="e" + admins.fid
            //長寬各150
            return RedirectToAction("List");
        }
        //編輯
        public IActionResult Edit(int? id)
        {
            dbClassContext db = new dbClassContext();
            Admin ad = db.Admins.FirstOrDefault(c => c.Fid == id);
            CAdminViewModel vm = new CAdminViewModel();
            vm.admin = ad;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(CAdminViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin ad = db.Admins.FirstOrDefault(c => c.Fid == vm.Fid);
            if (ad != null)
            {
                if (vm.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _environment.WebRootPath + "/images/" + photoName;
                    ad.Fphoto = photoName;
                    vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                ad.Fid = vm.Fid;
                ad.FRank = vm.FRank;
                ad.FEmail = vm.FEmail;
                ad.FName = vm.FName;
                ad.FBirth = vm.FBirth;
                ad.FPassword = vm.FPassword;
                ad.FSex = vm.FSex;
                db.SaveChanges();

            }
            return RedirectToAction("List");
        }
        //刪除
        public IActionResult Delete(int? id)
        {
            dbClassContext db = new dbClassContext();
            Admin ad = db.Admins.FirstOrDefault(c => c.Fid == id);
            if (ad != null)
            {
                db.Admins.Remove(ad);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult SendMailByGmail(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail == vm.txtAccount);
            if (admin == null)
            {
                return Content("請輸入信箱帳號");
            }
            admin.FEmail = vm.txtAccount;
            admin.FPassword = Guid.NewGuid().ToString();
            string beforePassword = admin.FPassword;//加密前密碼
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                string afterPassword = beforePassword + "putSomeSalt";
                var bytes = Encoding.UTF8.GetBytes(afterPassword);
                var hash = cryptoMD5.ComputeHash(bytes);
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();
                admin.FPassword = md5; //加密後密碼
            }
            db.SaveChanges();
            string newPassword = beforePassword;
            List<string> MailList = new List<string>();
            MailList.Add(vm.txtAccount);//新增收件人進去
            string Subject = "變更密碼";
            string Body = "您的密碼為:" + newPassword;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("yeee880726@gmail.com", "測試郵件", System.Text.Encoding.UTF8);
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
        public IActionResult PartialLogin()
        {
            return PartialView();
        }
        public IActionResult GetUserSession()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);
            if (string.IsNullOrEmpty(json))
            {
                return Content("");
            }
            Admin admin = JsonSerializer.Deserialize<Admin>(json);
            return Json(admin);
        }
        public IActionResult RemoveUserSession()
        {
            HttpContext.Session.Remove(CDictionary.SK_ADMIN_User);
            return Content("清除session");
        }
        public IActionResult AccountAlive(CAdminViewModel vm)//是否被註冊過
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail == vm.FEmail);
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                vm.FPassword += "putSomeSalt";
                var bytes = Encoding.UTF8.GetBytes(vm.FPassword);
                var hash = cryptoMD5.ComputeHash(bytes);
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();
                vm.FPassword = md5;
            }
            if (admin == null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                string path = _environment.WebRootPath + "/images/" + photoName;
                vm.Fphoto = photoName;
                vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                db.Admins.Add(vm.admin);
                db.SaveChanges();
                //-----qrcode生成及儲存-----
                //長寬各150               
                CCreateqrcode cqr = new CCreateqrcode();
                string emp = "e" + vm.admin.Fid; //圖片要存的內容
                var QRpic = cqr.createqrcode(emp); //生成bitmap類型的圖片
                string qrname = emp + ".jpg";
                string qrpath = _environment.WebRootPath + "/images/" + qrname;
                Admin qradmin = db.Admins.FirstOrDefault(t => t.Fid == vm.Fid);//
                if (qradmin != null)
                {
                    qradmin.FQrcode = qrname;
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
                //呼叫寄信把QRpic寄給員工
                Gmail sendmail = new Gmail();
                string root = _environment.WebRootPath;
                string imagePath = Path.Combine(root, "images", $"{qrname}");
                var sendto = $"{vm.FEmail}";
                var subject = "這是你的QRcode";

                byte[] image = null;
                if (System.IO.File.Exists(imagePath))//如果這個路徑有東西的話
                {
                    image = System.IO.File.ReadAllBytes(imagePath); // 讀取文件並轉成 byte 陣列

                    var imageData = Convert.ToBase64String(image);
                    var htmlBody = $"<html><body><p>你好，你的QR code如下，請妥善保管，謝謝。</p><img src='data:image/jpeg;base64,{imageData}' /></body></html>";
                    var body = htmlBody;
                    sendmail.SendByGmail(sendto, body, subject);
                }//------mail finish------

                return Content("註冊成功!請至信箱查看QR code");
                //後面要記得改
                //return RedirectToAction("List");
            }
            return Content("此帳號已註冊使用,請前往登入");

        }
        public IActionResult ResetPassword(CAdminViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin ad = db.Admins.FirstOrDefault(t => t.FEmail == vm.FEmail);
            return View();
        }
    }
}
