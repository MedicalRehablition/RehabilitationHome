using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;

namespace prjRehabilitation.Controllers
{
    public class UserController : Controller
    {
        private IWebHostEnvironment _environment;
        public UserController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult UserList(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string keyword = vm.txtKeyword;
            IEnumerable<Customer> data = null;
            if (keyword == null)
            {
                data = from c in db.Customers
                       select c;
            }
            else
            {
                data = db.Customers.Where(c => c.FName.Contains(keyword)).ToList();
            }
            List<CCustomerViewModel> List = new List<CCustomerViewModel>();
            foreach (var c in data)
            {
                CCustomerViewModel a = new CCustomerViewModel();
                a.Customer = c;
                List.Add(a);

            }
            return View(List);
        }
        public IActionResult Delete(int? id)
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(c => c.Fid == id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            return RedirectToAction("UserList");
        }
        public IActionResult Edit(int? id)
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(c => c.Fid == id);
            CCustomerViewModel vm = new CCustomerViewModel();
            vm.Customer = customer;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(CCustomerViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Customer customer = db.Customers.FirstOrDefault(c => c.Fid == vm.Fid);
            if (customer != null)
            {
                if (vm.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _environment.WebRootPath + "/images/" + photoName;
                    customer.FPicture = photoName;
                    vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                customer.Fid = vm.Fid;
                customer.FName = vm.FName;
                customer.FEmail = vm.FEmail;
                customer.FPassword = vm.FPassword;
                customer.FAddress = vm.FAddress;
                customer.FPhone = vm.FPhone;
                db.SaveChanges();
            }
            return RedirectToAction("UserList");
        }
    }

}
