﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace prjRehabilitation.Controllers
{
    public class AdminUserLoginController : Controller
    {
        private IWebHostEnvironment _environment;
        public AdminUserLoginController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //show出員工名單
        public IActionResult List(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string keyword = vm.txtKeyword;
            IEnumerable<Admin> data = null;
            if (keyword == null)
            {
                data = from c in db.Admins
                       select c;
            }
            else
            {
                data = db.Admins.Where(c => c.FName.Contains(keyword)).ToList();
            }
            List<CAdminViewModel> List = new List<CAdminViewModel>();
            foreach (var c in data)
            {
                CAdminViewModel a = new CAdminViewModel();
                a.admin = c;
                List.Add(a);

            }
            return View(List);
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
            dbClassContext db = new dbClassContext();
            if (vm.photo != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                string path = _environment.WebRootPath + "/images/" + photoName;
                vm.Fphoto = photoName;
                vm.photo.CopyTo(new FileStream(path, FileMode.Create));
            }
            db.Admins.Add(vm.admin);
            db.SaveChanges();
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
                ad.FSex = vm.FSex;
                ad.FPassword = vm.FPassword;
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
            db.SaveChanges();
            string newPassword = admin.FPassword;
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
            MySmtp.Credentials = new System.Net.NetworkCredential("yeee880726@gmail.com", "otdqmlbzkpumgsrw");
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
        public IActionResult Edit2(int? id)
        {
            dbClassContext db = new dbClassContext();
            Admin ad = db.Admins.FirstOrDefault(c => c.Fid == id);
            CAdminViewModel vm = new CAdminViewModel();
            vm.admin = ad;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit2(CAdminViewModel vm)
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
                ad.FSex = vm.FSex;
                ad.FPassword = vm.FPassword;
                db.SaveChanges();
            }
            return RedirectToAction("List");

        }
        public async Task<IActionResult> Callback()
        {
            if (!this.Request.Query.TryGetValue("code", out var code))
            {
                return this.StatusCode(400);
            }

            var (accessToken, idToken) = await this.ExchangeAccessToken(code);

            if (accessToken == null)
            {
                return this.StatusCode(400);
            }

            // TODO: Save AccessToken and IdToken

            // TODO: User Login

            return this.Redirect("/");
        }

        private async Task<(string, string)> ExchangeAccessToken(string code)
        {
            var client =this.Register();

            var request = new HttpRequestMessage(HttpMethod.Post, "AccessTokenUrl");

            request.Content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    ["grant_type"] = "authorization_code",
                    ["code"] = code,
                    ["redirect_uri"] = "RedirectURI",
                    ["client_id"] = "405238953344-evflmg05d4fffkh7avfuvt3b9pnlmetk.apps.googleusercontent.com",
                    ["client_secret"] = "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"
                });

            var response = await client.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK) return (null, null);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonNode.Parse(content);

            return (result["access_token"].GetValue<string>(), result["id_token"].GetValue<string>());
        }
    }
}
