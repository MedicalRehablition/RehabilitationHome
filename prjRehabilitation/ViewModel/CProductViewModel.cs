using prjRehabilitation.Models;
using System.Security.AccessControl;

namespace prjRehabilitation.ViewModel
{
    public class CProductViewModel
    {

        private Product _product;
        public CProductViewModel()
        {
            _product = new Product();
        }
        public Product Product { get { return _product; } set { _product = value; } }
        public int Fid
        {
            get { return _product.Fid; }
            set { _product.Fid = value; }
        }
        public string? FName
        {
            get { return _product.FName; }
            set { _product.FName = value; }
        }
        public int? FQty
        {
            get { return _product.FQty; }
            set { _product.FQty = value; }
        }
        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { _product.FPrice = value; }
        }
        public string? FPhoto 
        { 
            get { return _product.FPhoto; } 
            set {_product.FPhoto = value; } 
        }
        public IFormFile photo { get; set; }
    }
}
