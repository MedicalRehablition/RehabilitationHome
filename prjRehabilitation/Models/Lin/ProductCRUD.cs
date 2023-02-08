using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Lin;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;

namespace prjRehabilitation.Models.Lin
{
    public class ProductCRUD
    {
        public void createOrder(VMOrder vm, VMCart cart)
        {
            dbClassContext db = new dbClassContext();
            TOrder order = new TOrder();
            var time = DateTime.Now.ToString("G");
            var id = "Med" + DateTime.Now.ToString("d") + "_" + (new Random()).Next(99999);
            order.FAddress = vm.FAddress;
            order.FOrderId = id;
            order.FTotalPrice = Convert.ToDecimal(vm.FPrice);
            order.FDate = time;
            order.FEmail = vm.FEmail;
            db.TOrders.Add(order);
            db.SaveChanges();

            createOrderTail(cart, id);
        }
        private void createOrderTail(VMCart cart, string id)
        {
            dbClassContext db = new dbClassContext();
            foreach (var item in cart.Item)
            {
                var p = db.Products.Where(p => p.Fid == item).First();

                db.TOrderDetails.Add(new TOrderDetail
                {
                    FOrderId = id,
                    FPrice = p.FPrice,
                    FProductName = p.FName,
                    FQty = 1
                });
            }
            db.SaveChanges();
        }
        public void edit(CProductViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Product product = db.Products.FirstOrDefault(c => c.Fid == vm.Fid);
            if (product != null)
            {
                if (vm.photo != null)
                {
                    byte[] FileBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        vm.photo.CopyTo(ms);
                        vm.FPhoto = ms.GetBuffer();
                    }
                }
                product.Fid = vm.Fid;
                product.FPrice = vm.FPrice;
                product.FQty = vm.FQty;
                product.FName = vm.FName;
                product.FPhoto = vm.FPhoto;
                product.FType = vm.FType;
                product.FStatus= vm.FStatus;
                db.SaveChanges();
            } 
        }
        public object getTargetProduct(int id)
        {
            dbClassContext db = new dbClassContext();
            Product product = db.Products.FirstOrDefault(c => c.Fid == id);
            CProductViewModel vm = new CProductViewModel();

            vm.Product = product;
            return vm;
        }
        public void Create(CProductViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            if (vm.photo != null)
            {
                byte[] FileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    vm.photo.CopyTo(ms);
                    FileBytes = ms.GetBuffer();
                    vm.Product.FPhoto = FileBytes;
                }
            }
            db.Products.Add(vm.Product);
            db.SaveChanges();
        }
        public object GetProducts() {
            dbClassContext db = new dbClassContext();
            var data= db.Products.OrderByDescending(x=>x.FStatus).ToList();
            List <CProductViewModel> list = new List< CProductViewModel >();

            foreach (var c in data)
            {
                list.Add(new CProductViewModel
                {
                    Product = c,
                });
            }
            return list;
        }
        public void Delete(int id)
        {
            dbClassContext db = new dbClassContext();
            Product prod = db.Products.FirstOrDefault(c => c.Fid == id);
            if (prod != null)
            {
                db.Products.Remove(prod);
                db.SaveChanges();
            }
        }
        public void TakeOff(int id)
        {
            dbClassContext db = new dbClassContext();
            Product prod = db.Products.FirstOrDefault(c => c.Fid == id);
            if (prod != null)
            {
                prod.FStatus = false;
                db.SaveChanges();
            }
        }
        public void TakeUp(int id)
        {
            dbClassContext db = new dbClassContext();
            Product prod = db.Products.FirstOrDefault(c => c.Fid == id);
            if (prod != null)
            {
                prod.FStatus = true;
                db.SaveChanges();
            }
        }

		public object GetTakeUpProducts()
		{
			dbClassContext db = new dbClassContext();
			var data = db.Products.Where(x => x.FStatus==true).ToList();
			List<CProductViewModel> list = new List<CProductViewModel>();

			foreach (var c in data)
			{
				list.Add(new CProductViewModel
				{
					Product = c,
				});
			}
			return list;
		}

  //      public void AddToCart(int id)
  //      {

		//}
        public object GetCartItem()
        {
            return new Product();
        }

		public object GetCartItems(VMCart? cart)
		{
            List<CProductViewModel> items = new List<CProductViewModel>();
            foreach(int c in cart.Item) 
            {
                items.Add((CProductViewModel)getTargetProduct(c));
            }
            return items;
		}

        public  List<TOrder> GetOrders()
        {
            dbClassContext db = new dbClassContext();
            return db.TOrders.ToList();
        }

        public object GetOrderDetail(string id)
        {
            dbClassContext db = new dbClassContext();
            return db.TOrderDetails.Where(x => x.FOrderId == id).ToList();
        }

        public void Shipment(int id)
        {
            dbClassContext db = new dbClassContext();
            db.TOrders.First(x => x.Fid == id).FShip = true;
            db.SaveChanges();
        }
    }
}
