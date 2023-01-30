using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Net.Mail;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class textLoginController : Controller
    {
        public IActionResult Index()
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
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));

            if (admin != null)
            {
                if (admin.FEmail.Equals(vm.txtAccount) && admin.FPassword.Equals(vm.txtPassword))
                {
                    string json = JsonSerializer.Serialize(admin);
                    HttpContext.Session.SetString(CDictionary.SK_Login_User, json);
                    return RedirectToAction("List");
                }
            }
            return View();
        }
        public IActionResult ExistAccount(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));

            if (vm.txtAccount != null && vm.txtPassword != null)
            {
                if (admin.FEmail != vm.txtAccount)
                {
                    if (admin.FPassword != vm.txtPassword)
                    {
                        return Content("1");
                    }
                    return Content("2");
                }
                return Content("3");

            }
            else
            {
                string json = JsonSerializer.Serialize(admin);
                HttpContext.Session.SetString(CDictionary.SK_Login_User, json);
                return RedirectToAction("List");
            }
        }
       

    }
}
