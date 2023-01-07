using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class UserLoginController : Controller
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string keyword = vm.txtKeyword;
            IEnumerable<Admin> data = null;
            if(keyword == null)
            {
                data = from c in db.Admins
                       select c;
            }
            else
            {
                data = db.Admins.Where(c=>c.FName.Contains(keyword)).ToList();
            }
            List<CAdminViewModel> List = new List<CAdminViewModel>();
            foreach(var c in data)
            {
                CAdminViewModel a  = new CAdminViewModel();
                a.admin = c;
                List.Add(a);
                
            }
            return View(List);
        }
        public IActionResult Login(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));

            if (admin != null)
            {
                if(admin.FEmail.Equals(vm.txtAccount) && admin.FPassword.Equals(vm.txtPassword))
                {
                    string json = JsonSerializer.Serialize(admin);
                    HttpContext.Session.SetString(CDictionary.SK_Login_User, json);
                    return RedirectToAction("List");
                }
            }
            else if (admin == null)
            {
                
            }
            return View();
        }
        public IActionResult ExistAccount(CLoginViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(t => t.FEmail.Equals(vm.txtAccount) && t.FPassword.Equals(vm.txtPassword));

            if (vm.txtAccount!=null&&vm.txtPassword!=null)
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
            return Content("0");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CAdminViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            db.Admins.Add(vm.admin);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        
    }
}
