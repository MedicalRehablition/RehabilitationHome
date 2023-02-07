using prjRehabilitation.Models;
using System.ComponentModel;

namespace prjRehabilitation.ViewModel
{
    public class CCustomerViewModel
    {
        private Customer _Customer;
        public CCustomerViewModel()
        {
            _Customer = new Customer();
        }
        public Customer Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        public int Fid
        {
            get { return _Customer.Fid; }
            set { _Customer.Fid = value; }
        }
        [DisplayName("信箱")]
        public string? FEmail
        {
            get { return _Customer.FEmail; }
            set { _Customer.FEmail = value; }
        }
        [DisplayName("密碼")]
        public string? FPassword
        {
            get { return _Customer.FPassword; }
            set { _Customer.FPassword = value; }
        }
        [DisplayName("電話")]
        public string? FPhone
        {
            get { return _Customer.FPhone; }
            set { _Customer.FPhone = value; }
        }
        [DisplayName("姓名")]
        public string? FName
        {
            get { return _Customer.FName; }
            set { _Customer.FName = value; }
        }
        [DisplayName("居住地址")]
        public string? FAddress
        {
            get { return _Customer.FAddress; }
            set { _Customer.FAddress = value; }
        }
        public string? FQrcode
        {
            get { return _Customer.FQrcode; }
            set { _Customer.FQrcode = value; }
        }
        public string? FPicture
        {
            get { return _Customer.FPicture; }
            set { _Customer.FPicture = value; }
        }
        public IFormFile photo { get; set; }
    }
}
