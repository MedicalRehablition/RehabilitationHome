using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class UserLoginController : Controller
    {
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
    }
}
