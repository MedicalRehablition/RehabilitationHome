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

            if (customer != null)
            {
                if (customer.FEmail.Equals(vm.txtAccount) && customer.FPassword.Equals(vm.txtPassword))
                {
                    string json = JsonSerializer.Serialize(customer);
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

            if (customer != null)
            {
                if (customer.FEmail == vm.txtAccount && customer.FPassword != vm.txtPassword)
                {
                    return Content("密碼錯誤");
                }
                string json = JsonSerializer.Serialize(customer);
                HttpContext.Session.SetString(CDictionary.SK_Login_User, json);
                return Content("登入成功");
            }
            return Content("無此帳號,請重新登入");
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
    }
}
