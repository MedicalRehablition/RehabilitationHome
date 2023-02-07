using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Lin;
using System.Text.Json;

using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace prjRehabilitation.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _environment;
        public IActionResult pay()
        {
            return View();
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
        public ProductController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult RemoveCartItem(int id)
        {
			string json;
			VMCart cart = new VMCart();
			if (HttpContext.Request.Cookies.TryGetValue(CDictionary.SK_Purchased_Products_List, out json))
			{
				cart = JsonSerializer.Deserialize<VMCart>(json);
			}
            else return Json("{'outcome' : '發生不該發生的錯誤:購物車不存在'}");
			
            if (cart.Item.Contains(id)) cart.Item.Remove(id);
			
			HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));

			return RedirectToAction("Cart");
		}
		public IActionResult ResetCart()
		{
			string json;
			VMCart cart = new VMCart();
			HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));

            return RedirectToAction("Cart");
        }
        public IActionResult AddToCart(int id)
        {
            string json;
            VMCart cart = new VMCart();
            if (!HttpContext.Request.Cookies.TryGetValue(CDictionary.SK_Purchased_Products_List, out json))
            {
                HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));
            }
            else
            {
                cart = JsonSerializer.Deserialize<VMCart>(json);
            }

            if (cart.Item.Contains(id)) return Json(new { outcome = "該產品已加入購物車" });

			cart.Item.Add(id);
            HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));

			return Json(new { outcome = "加入購物車成功"});
        }
        public IActionResult Cart()
        {
			string json;
			VMCart cart = null;
			if (HttpContext.Request.Cookies.TryGetValue(CDictionary.SK_Purchased_Products_List, out json))
			{
				cart = JsonSerializer.Deserialize<VMCart>(json);
			}
			else
			{
				return RedirectToAction("List_B");
			}
			return View((new ProductCRUD()).GetCartItems(cart));
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
