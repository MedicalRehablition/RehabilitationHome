using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class UserLoginController : Controller
    {
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
            Customer customer= db.Customers.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));
            string json = "";
            if (customer != null)
            {
                if (customer.FEmail.Equals(vm.txtAccount) && customer.FPassword.Equals(vm.txtPassword))
                {
                    json = JsonSerializer.Serialize(customer);
                    HttpContext.Session.SetString(CDictionary.SK_Login_User, json);
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
                    return Content("密碼錯誤");
                }
                json = JsonSerializer.Serialize(customer);
                HttpContext.Session.SetString(CDictionary.SK_Login_User, json);
                HttpContext.Session.GetString(CDictionary.SK_Login_User);
                customer = JsonSerializer.Deserialize<Customer>(json);
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
            Customer customer = db.Customers.FirstOrDefault(t=>t.FEmail==vm.FEmail);

            if (customer == null)
            {
                db.Customers.Add(vm.Customer);
                db.SaveChanges();
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
            string json=HttpContext.Session.GetString(CDictionary.SK_Login_User);
            if(string.IsNullOrEmpty(json))
            {
                return Content("");
            }
            Customer customer = JsonSerializer.Deserialize<Customer>(json);
            return Content(customer.FName);
        }
        public IActionResult RemoveUserSession()
        {
            HttpContext.Session.Remove(CDictionary.SK_Login_User);
            return Content("清除session");
        }

    }
}
