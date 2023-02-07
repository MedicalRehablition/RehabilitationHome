using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class CalendarController : Controller
    {
        public Customer getCustomerIfSession() { 
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
        public List<TCalendar> betweenDate(DateTime targetDay)
        {

            dbClassContext db = new dbClassContext();

            DateTime getToday = DateTime.Today;

            string getFront30 = getToday.AddDays(-30).ToString("yyyy-MM-dd");
            string getNext30 = getToday.AddDays(30).ToString("yyyy-MM-dd");

            List<TCalendar> targetRangeDays = (from d in db.TCalendars
                                               where (string.Compare(d.FDate, getFront30) >= 0) && (string.Compare(d.FDate, getNext30) <= 0)
                                               select d).ToList();



            if (getAdminIfSession() == null && getCustomerIfSession() == null)
            {    //最後濾鏡，都沒登入就是只能看到0
                targetRangeDays = targetRangeDays.Where(_ => _.FVisualHierarchy == 0).ToList();
            }
            else if (getAdminIfSession() == null && getCustomerIfSession() != null) {   //前端有登入，後端沒有
                List<TCalendar> findLV = targetRangeDays.Where(_ => _.FVisualHierarchy == 0).ToList();  //先把
                findLV.AddRange( targetRangeDays.Where(_ => _.FCustomerid == getCustomerIfSession().Fid).Where(_=>_.FApplyVisitor == true).ToList()); //雖然優先度是1但是因為是本人就無視優先權
                return findLV;
            }
            return targetRangeDays;
        }


        public IActionResult List()
        {
            dbClassContext db = new dbClassContext();

            CCalendarTotalNeedViewModel cctnvm = new CCalendarTotalNeedViewModel();

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_ADMIN_User)))//假設後台有登入就全秀
            {

                cctnvm.ResidentReVisitDay = (from p in db.Consultations
                                             group p by p.PatinetId into grp
                                             select grp.OrderByDescending(g => g.Date).First()).ToList();
            }
            else if (!string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User)))
            {    //前台有登入的話就秀相關人
                string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
                Customer customer = null;

                if (!string.IsNullOrEmpty(json))
                {
                    customer = JsonSerializer.Deserialize<Customer>(json);
                }

                if (db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == customer.Fid) != null) //這邊是為了防止有人註冊munber但沒有綁定住民會出錯
                {
                    int? getResidentID = db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == customer.Fid).Fid;

                    var getgusaa = db.Consultations.Where(_ => _.PatinetId == getResidentID).OrderBy(_ => _.Date).Last();

                    getgusaa.Patinet = null;    //這一欄會干涉前端json讀取

                    cctnvm.ResidentReVisitDay = new List<Consultation> { getgusaa };
                }
                else { cctnvm.ResidentReVisitDay = new List<Consultation>(); }
            }
            else
            {
                cctnvm.ResidentReVisitDay = new List<Consultation>();
            }

            cctnvm.GetTodayNextAndFrontOneMonth = betweenDate(DateTime.Today);

            cctnvm.getFrontSession = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            cctnvm.getBackSession = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);

            return View(cctnvm);
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_ADMIN_User))) {  return Content("並末登入，請回首頁登入後進行"); }
            return View(new CCalendarViewModel() { fRecorder = getAdminIfSession().FName, fRecorderDate = DateTime.Today.ToShortDateString() });

        }
        [HttpPost]
        public IActionResult Create(CCalendarViewModel ccvm)
        {
            dbClassContext db = new dbClassContext();
            ccvm.FDeleteBool = true;
            ccvm.FAdminId = getAdminIfSession().Fid;

            db.Add(ccvm.calendar);
            db.SaveChanges();

            return RedirectToAction("List");
        }


        public IActionResult CalendarApplyVisitor() {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User))) { return Content("並末登入，請回首頁登入後進行"); }
            return View(new CCalendarViewModel() { fRecorder = getCustomerIfSession().FName, fRecorderDate = DateTime.Today.ToShortDateString() });

        }
        [HttpPost]
        public IActionResult CalendarApplyVisitor(CCalendarViewModel ccvmAV)
        {
            ccvmAV.FCustomerid = getCustomerIfSession().Fid;
            ccvmAV.FDeleteBool = true;
            ccvmAV.eventName = "申請會客";

            ccvmAV.title = "申請會客";

            ccvmAV.FApplyVisitor = false;

            ccvmAV.FVisualHierarchy = 1;    //初步將可視等級為1免得不打會0大家都看的到
            ccvmAV.className = "genric-btn info circle arrow";

            dbClassContext db = new dbClassContext();
       

            db.Add(ccvmAV.calendar);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult CalendarApplyCensorList() {
            dbClassContext db = new dbClassContext();
           var calendarForomDB = db.TCalendars.Where(_ => _.FApplyVisitor == false);
            List<CCalendarViewModel> viewModelForList = new List<CCalendarViewModel>();
            foreach (TCalendar item in calendarForomDB)
            {
                viewModelForList.Add(new CCalendarViewModel() { calendar = item });
            }

            return View(viewModelForList.OrderByDescending(_=>_.fRecorderDate));
        }

    }
}
