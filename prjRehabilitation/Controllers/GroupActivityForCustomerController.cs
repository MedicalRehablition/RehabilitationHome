using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Eric;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class GroupActivityForCustomerController : Controller
    {

        public Customer getCustomerIfSession()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            Customer customer = null;

            if (!string.IsNullOrEmpty(json))
            {
                customer = JsonSerializer.Deserialize<Customer>(json);
            }
            return customer;
        }
        public Admin getAdminIfSession()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);
            Admin admin = null;

            if (!string.IsNullOrEmpty(json))
            {
                admin = JsonSerializer.Deserialize<Admin>(json);
            }
            return admin;
        }
        public IActionResult List()
        {
            //ViewBag.customer = getCustomerIfSession();
            ViewBag.admin = getAdminIfSession();
            dbClassContext db = new dbClassContext();
            if (getAdminIfSession() != null)
            {
                List<CGroupActivityViewModel> joinGA = new List<CGroupActivityViewModel>();
                List<string> names = new List<string>();
                var temp = db.TPersonalPerformances.Where(_ => _.FDeleteBool == "1").ToList();


                foreach (var item in temp)
                {
                    var tempGa = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == item.FGroupActivityId);
                    joinGA.Add(new CGroupActivityViewModel() { groupActivity = tempGa });
                    names.Add(db.PatientInfos.FirstOrDefault(_=>_.Fid == item.FResidentId).FName);
                }

                ViewBag.joinGroupActivity = joinGA;

                ViewBag.names = names;
                return View(db.TPersonalPerformances.Where(_ => _.FDeleteBool == "1"));
            }
            else if (getCustomerIfSession() != null)
            {
                List<CGroupActivityViewModel> joinGA = new List<CGroupActivityViewModel>();
                List<string> names = new List<string>();
                var Resident = db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == getCustomerIfSession().Fid);

                ViewBag.ResidentName = Resident.FName;


                var temp = db.TPersonalPerformances.Where(_ => _.FResidentId == Resident.Fid).Where(_ => _.FDeleteBool == "1").ToList();

                foreach (var item in temp)
                {
                    var tempGa = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == item.FGroupActivityId);
                  joinGA.Add(new CGroupActivityViewModel() { groupActivity = tempGa });
                    names.Add(Resident.FName);
                }

                ViewBag.joinGroupActivity = joinGA;
                ViewBag.names = names;
                return View(temp);
            }
            else {
                return  RedirectToAction("index", "Home");
            }

            return RedirectToAction("index", "Home");
        }
    }
}
