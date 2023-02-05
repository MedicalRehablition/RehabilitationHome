using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Lin;
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
        public IActionResult GetUserPatient()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            Customer customer = JsonSerializer.Deserialize<Customer>(json);
            dbClassContext db = new dbClassContext();
           IEnumerable<PatientInfo> data = db.PatientInfos.Where(c=>c.FCustomerid==customer.Fid) ;
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data)
            {
                VMPatientList a = new VMPatientList();
                a.fid = (int)c.Fid;
                a.fName = c.FName;
                a.fPhone = c.FPhone;
                a.fidnum = c.FIdnum;
                a.FCheckin= c.FCheckin;
                a.fCustomerid = (int)c.FCustomerid;
                if (c.FPhotoFile != null)
                {
                    a.fphoto = c.FPhotoFile;
                }
                List.Add(a);

            }
            return View(List);

        }
    }
}
