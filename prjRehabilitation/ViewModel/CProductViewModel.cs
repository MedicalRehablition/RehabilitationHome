using prjRehabilitation.Models;
using System.ComponentModel;
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
        [DisplayName("商品編號")]
        public int Fid
        {
            get { return _product.Fid; }
            set { _product.Fid = value; }
        }
        [DisplayName("商品名稱")]
        public string? FName
        {
            get { return _product.FName; }
            set { _product.FName = value; }
        }
        [DisplayName("數量")]
        public int? FQty
        {
            get { return _product.FQty; }
            set { _product.FQty = value; }
        }
        [DisplayName("價格")]
        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { _product.FPrice = value; }
        }
        public byte[]? FPhoto 
        { 
            get { return _product.FPhoto; } 
            set {_product.FPhoto = value; } 
        }
        [DisplayName("是否上架")]
        public bool FStatus
        {
            get { return _product.FStatus; }
            set { _product.FStatus = value; }
        }
        ////[DisplayName("商品圖片")]
        public IFormFile photo { get; set; }
    }
}
