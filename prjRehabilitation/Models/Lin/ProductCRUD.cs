using prjRehabilitation.ViewModel;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;

namespace prjRehabilitation.Models.Lin
{
    public class ProductCRUD
    {
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
	}
}
