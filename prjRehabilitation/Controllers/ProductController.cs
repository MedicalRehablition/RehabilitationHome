using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Lin;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Text.Json;

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
        public IActionResult pay()
        {
            return View();
        }
        public IActionResult ship(int id,string email)
        {
            string title = "【通知】關心康復之家已寄出您訂購的商品";
            string content = "親愛的顧客您好：" + "<br />";
            content += "　　您向關心康復之家購買商品的訂單已寄出，感謝您的購買，商品若有任何問題，我們隨時聽候您的回復。" + "<br /><br />";
            content += "您的每一份支持與鼓勵，都會讓我們變得更好。" + "<br />";
            content += "祝您有美好的一天" + "<br />";
            content += "關心康復之家　甜心小舖";
            (new Gmail()).SendByGmail(email, content, title);
            (new ProductCRUD()).Shipment(id);
            return RedirectToAction("orders");

        }
        public IActionResult GetOrderDetail(string id)
        {
            return Json((new ProductCRUD()).GetOrderDetail(id));
        }
        public IActionResult orders()
        {
            return View((new ProductCRUD()).GetOrders());
        }
        public IActionResult CreateOrder(VMOrder vm)
        {
			string json;
			VMCart cart = new VMCart();
            try
            {
				HttpContext.Request.Cookies.TryGetValue(CDictionary.SK_Purchased_Products_List, out json);
				cart = JsonSerializer.Deserialize<VMCart>(json);
			}
            catch
            {
				return Json(new { outcome = "錯誤:未建立購物車" });
			}

            (new ProductCRUD()).createOrder(vm, cart);
            string title = "【通知】關心康復之家已收到您的訂單";
            string content =  "親愛的顧客您好：" + "<br />";
            content += "　　我們已收到您向關心康復之家購買商品的訂單，您的商品將在三個工作天內安排出貨，若有最新消息，我們會即刻通知您。" + "<br /><br />";
            content += "祝您有美好的一天" + "<br />";
            content += "關心康復之家　甜心小舖";
            (new Gmail()).SendByGmail(vm.FEmail,content,title);
            return Json(new { outcome="建立訂單成功"});
        }
        public IActionResult CheckPurchasedItem(int id,int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        CartItem_Add(id, CDictionary.SK_Purchased_Products_List);
                        break;
                    case 2:
                        CartItem_romove(id, CDictionary.SK_Purchased_Products_List);
                        break;
                    case 3:
                        CartToPurchase();
                        break;
                    case 4:
                        ResetPurchased();
                        break;
                }
            }
            catch
            {
                return Json(new { outcome = false });
            }
            return Json(new {outcome = true});
        }
        private void ResetPurchased()
        {
            VMCart cart = new VMCart();
            HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));
        }
        private void CartToPurchase()
        {
            string json;
            VMCart cart = new VMCart();
            if (HttpContext.Request.Cookies.TryGetValue(CDictionary.SK_Cart_Products_List, out json))
            {
                cart = JsonSerializer.Deserialize<VMCart>(json);
            }
            HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));
        }
        public IActionResult RemoveCartItem(int id)
        {
            bool output = CartItem_romove(id,CDictionary.SK_Cart_Products_List);
            if(output)
			    return RedirectToAction("Cart");
            else
            {
                return Json("{'outcome' : '發生不該發生的錯誤:購物車不存在'}");
            }
        }
        private bool CartItem_romove(int id,string SK)
        {
            string json;
            VMCart cart = new VMCart();
            if (HttpContext.Request.Cookies.TryGetValue(SK, out json))
            {
                cart = JsonSerializer.Deserialize<VMCart>(json);
            }
            else return false;

            if (cart.Item.Contains(id)) cart.Item.Remove(id);

            HttpContext.Response.Cookies.Append(SK, JsonSerializer.Serialize(cart));
            return true;
        }
        private bool CartItem_Add(int id,string SK)
        {
            try
            {
                string json;
                VMCart cart = new VMCart();
                if (!HttpContext.Request.Cookies.TryGetValue(SK, out json))
                {
                    HttpContext.Response.Cookies.Append(SK, JsonSerializer.Serialize(cart));
                }
                else
                {
                    cart = JsonSerializer.Deserialize<VMCart>(json);
                }
                //MOMO的作法是一個商品買幾件就跳幾筆資料，實際看來這麼做會方便於資料存取，不然在購物車做數量更動想存進資料庫有點麻煩
                //if (cart.Item.Contains(id)) return Json(new { outcome = "該產品已加入購物車" });

                cart.Item.Add(id);
                HttpContext.Response.Cookies.Append(SK, JsonSerializer.Serialize(cart));
            }
            catch
            {
                return false;
            }
            return true;
        }
        public IActionResult ResetCart()
		{
			string json;
			VMCart cart = new VMCart();
			HttpContext.Response.Cookies.Append(CDictionary.SK_Cart_Products_List, JsonSerializer.Serialize(cart));
            HttpContext.Response.Cookies.Append(CDictionary.SK_Purchased_Products_List, JsonSerializer.Serialize(cart));

            return RedirectToAction("Cart");
        }
        public IActionResult AddToCart(int id)
        {
            bool outcome = CartItem_Add(id, CDictionary.SK_Cart_Products_List);
			if(outcome)return Json(new { outcome = "加入購物車成功"});
            else return Json(new { outcome = "加入購物車失敗" });
        }
        public IActionResult Cart()
        {
			string json;
			VMCart cart = null;
			if (HttpContext.Request.Cookies.TryGetValue(CDictionary.SK_Cart_Products_List, out json))
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
