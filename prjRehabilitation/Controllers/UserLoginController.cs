using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;

namespace prjRehabilitation.Controllers
{
    public class UserLoginController : Controller
    {
        private IWebHostEnvironment _environment;
        public UserLoginController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Login()
        {
            return View();
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
        public IActionResult List(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string keyword = vm.txtKeyword;
            IEnumerable<Admin> admins = null;
            if (keyword == null)
            {
                admins = from c in db.Admins
                       select c;
            }
            else
            {
                admins = db.Admins.Where(c => c.FName.Contains(keyword)).ToList();
            }
            
            List<CAdminViewModel> LIST = new List<CAdminViewModel>();
            foreach (var c in admins)
            {
                CAdminViewModel v = new CAdminViewModel();
                v.admin = c;
                LIST.Add(v);
            }
            return View(LIST);
        }
        public IActionResult Edit(CAdminViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Admin admin = db.Admins.FirstOrDefault(c => c.Fid == vm.Fid);
            if (admin != null)
            {
                if (vm.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _environment.WebRootPath + "/images/" + photoName;
                    admin.Fphoto = photoName;
                    vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
               admin.Fid = vm.Fid;
                admin.FEmail = vm.FEmail;
                admin.FRank = vm.FRank;
                admin.FName = vm.FName;
                admin.FPassword = vm.FPassword;
                admin.FBirth=vm.FBirth;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
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
    }
}
