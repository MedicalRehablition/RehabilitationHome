using prjRehabilitation.Models;

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
        public string? FEmail
        {
            get { return _Customer.FEmail; }
            set { _Customer.FEmail = value; }
        }
        public string? FPassword
        {
            get { return _Customer.FPassword; }
            set { _Customer.FPassword = value; }
        }
        public string? FPhone
        {
            get { return _Customer.FPhone; }
            set { _Customer.FPhone = value; }
        }
        public string? FName
        {
            get { return _Customer.FName; }
            set { _Customer.FName = value; }
        }
        public string? FAddress
        {
            get { return _Customer.FAddress; }
            set { _Customer.FAddress = value; }
        }
        public IFormFile photo { get; set; }
    }
}
