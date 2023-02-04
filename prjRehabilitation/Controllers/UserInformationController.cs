using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class UserInformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PersonInfomation()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            Customer data = JsonSerializer.Deserialize<Customer>(json);
            CCustomerViewModel customer = new CCustomerViewModel();
            customer.Customer = data;
            return View(customer);
        }
        public IActionResult GetUserPatientPartialView()
        {
            return PartialView();
        }
        public IActionResult GetUserPatient(CKeywordViewModel vm)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            Customer customer = JsonSerializer.Deserialize<Customer>(json);
            IEnumerable<PatientInfo> data = null;
            dbClassContext db = new dbClassContext();
            string keyword = vm.txtKeyword;
            if (keyword == null)
            {
                data = db.PatientInfos.Where(c => c.FCustomerid == customer.Fid);
            }
            else
            {
                data = db.PatientInfos.Where(c => c.FName.Contains(keyword)).ToList();
            }
            List<CPatientsViewModel> List = new List<CPatientsViewModel>();
            foreach (var c in data)
            {
                CPatientsViewModel a = new CPatientsViewModel();
                a.Patient = c;
                List.Add(a);

            }
            return View(List);

        }
    }
}
