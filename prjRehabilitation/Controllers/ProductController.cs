using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
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
        public IActionResult List()
        {
            return View((new ProductCRUD()).GetTakeUpProducts());
        }
        public IActionResult List_B()
        {
            return View((new ProductCRUD()).GetProducts());
        }

        public IActionResult GetAllProduct()
        {
            return Json((new ProductCRUD()).GetProducts);
        }
        public IActionResult Edit(int id)
        {
            return View((new ProductCRUD()).getTargetProduct(id));
        }
        [HttpPost]
        public IActionResult Edit(CProductViewModel vm)
        {
            (new ProductCRUD()).edit(vm);
            return RedirectToAction("List_B");
        }
        public IActionResult Delete(int id)
        {
            (new ProductCRUD()).Delete(id);
            return RedirectToAction("List_B");
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult TakeOff(int id)
        {
            (new ProductCRUD()).TakeOff(id);
            return RedirectToAction("List_B");
        }
        public IActionResult TakeUp(int id)
        {
            (new ProductCRUD()).TakeUp(id);
            return RedirectToAction("List_B");
        }
        [HttpPost]
        public IActionResult Create(CProductViewModel vm)
        {
            (new ProductCRUD()).Create(vm);
            return RedirectToAction("List_B");
        }
    }
}
