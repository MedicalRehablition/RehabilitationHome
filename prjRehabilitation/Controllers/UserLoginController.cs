using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
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
                if(admin.FEmail.Equals(vm.txtAccount) && admin.FPassword.Equals(vm.txtPassword))
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
                    //string photoName = Guid.NewGuid().ToString() + ".jpg";
                    //string path = _environment.WebRootPath + "/images/" + photoName;
                    //ad.Fphoto = photoName;
                    //vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                ad.Fid = vm.Fid;
                ad.FRank = vm.FRank;
                ad.FEmail = vm.FEmail;
                ad.FName = vm.FName;
                ad.FPassword= vm.FPassword;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
