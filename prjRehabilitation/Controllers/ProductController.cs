using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace prjRehabilitation.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _environment;
        public ProductController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult List(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string keyword = vm.txtKeyword;
            IEnumerable<Product> prod = null;
            if(keyword== null)
            {
                prod = from c in db.Products
                       select c;
            }
            else
            {
                prod= db.Products.Where(c=>c.FName.Contains(keyword)).ToList();
            }
            List<CProductViewModel> LIST = new List<CProductViewModel>();
            foreach(var c in prod)
            {
                CProductViewModel v = new CProductViewModel();
                v.Product = c;
                LIST.Add(v);
            }
            return View(LIST);
        }
        public IActionResult Edit(int? id)
        {
            dbClassContext db = new dbClassContext();
            Product product = db.Products.FirstOrDefault(c=>c.Fid==id);
            CProductViewModel vm = new CProductViewModel();
            vm.Product = product;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(CProductViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Product product = db.Products.FirstOrDefault(c => c.Fid == vm.Fid);
            if (product != null)
            {
                if (vm.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _environment.WebRootPath + "/images/" + photoName;
                    product.FPhoto = new byte[3];   //暫時只改這邊不出錯而已
                    vm.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                product.Fid=vm.Fid;
                product.FPrice=vm.FPrice;
                product.FQty=vm.FQty;
                product.FName=vm.FName;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            dbClassContext db = new dbClassContext();
            Product prod = db.Products.FirstOrDefault(c => c.Fid == id);
            if(prod != null)
            {
                db.Products.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CProductViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            db.Products.Add(vm.Product);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
