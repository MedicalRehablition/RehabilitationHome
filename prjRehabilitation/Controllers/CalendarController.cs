using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using prjRehabilitation.Controllers.Api;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;
using System.Globalization;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
    public class CalendarController : Controller
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
        //Customer customer = new CalendarAjaxController().getCustomerIfSession(); 
        //Admin admin = new CalendarAjaxController().getAdminIfSession();


        public List<TCalendar> betweenDate(DateTime targetDay)
        {

            dbClassContext db = new dbClassContext();

            DateTime getToday = targetDay;

            string getFront30 = getToday.AddDays(-60).ToString("yyyy-MM-dd");
            string getNext30 = getToday.AddDays(60).ToString("yyyy-MM-dd");

            List<TCalendar> targetRangeDays = (from d in db.TCalendars
                                               where (string.Compare(d.FDate, getFront30) >= 0) && (string.Compare(d.FDate, getNext30) <= 0)
                                               select d).ToList();



            if (getAdminIfSession() == null && getCustomerIfSession() == null)
            {    //最後濾鏡，都沒登入就是只能看到0
                targetRangeDays = targetRangeDays.Where(_ => _.FVisualHierarchy == 0).ToList();
            }
            else if (getAdminIfSession() == null && getCustomerIfSession() != null)
            {   //前端有登入，後端沒有
                List<TCalendar> findLV = targetRangeDays.Where(_ => _.FVisualHierarchy == 0).ToList();  //先把
                findLV.AddRange(targetRangeDays.Where(_ => _.FCustomerid == getCustomerIfSession().Fid).Where(_ => _.FApplyVisitor == true).ToList()); //雖然優先度是1但是因為是本人就無視優先權
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
                List<Consultation> coverStandBy = new List<Consultation>();
                List<Consultation> StandBy  = (from p in db.Consultations
                                             group p by p.PatinetId into grp
                                             select grp.OrderByDescending(g => g.Date).First()).ToList();

                var clearJsonIssues = db.PatientInfos.Where(_ => _.Status == true).ToList();

                foreach (var item in clearJsonIssues)
                {
                    item.Consultations = null; //這一欄會干涉前端json讀取
                    foreach (var item2 in StandBy)  //因為有人出院了但是記錄還在，要篩掉
                    {
                        if (item.Fid == item2.PatinetId) {
                            coverStandBy.Add(item2);
                            break;
                        }
                    }
                }

                cctnvm.ResidentExpireDate = clearJsonIssues;

                cctnvm.ResidentReVisitDay = coverStandBy;

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

                    Consultation getgusaa = db.Consultations.Where(_ => _.PatinetId == getResidentID).OrderBy(_ => _.Date).Last();

                    getgusaa.Patinet = null;    //這一欄會干涉前端json讀取

                    cctnvm.ResidentReVisitDay = new List<Consultation> { getgusaa };

                    List<PatientInfo> clearJsonIssues = db.PatientInfos.Where(_ => _.Status == true).Where(_ => _.FCustomerid == customer.Fid).ToList();    //其實是上面的全撈有事… 我想說上次是這邊有事就先搞這邊結果是上面


                    cctnvm.ResidentExpireDate = clearJsonIssues;
                }
                else { cctnvm.ResidentReVisitDay = new List<Consultation>(); cctnvm.ResidentExpireDate = new List<PatientInfo>(); }
            }
            else
            {
                cctnvm.ResidentReVisitDay = new List<Consultation>();   //只是怕null掛掉，至少給他框框
                cctnvm.ResidentExpireDate = new List<PatientInfo>();
            }

            cctnvm.GetTodayNextAndFrontOneMonth = betweenDate(DateTime.Today);

            cctnvm.getFrontSession = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            cctnvm.getBackSession = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);

            return View(cctnvm);
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_ADMIN_User))) { return Content("並末登入，請回首頁登入後進行"); }
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


        public IActionResult CalendarApplyVisitor()
        {

            if (getAdminIfSession() != null) return Content("後端登入後請上一頁利用後端新增");
            //if (getAdminIfSession() != null)
            //{
            //    ViewBag.getAdminSection = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);
            //    return View(new CCalendarViewModel() { fRecorder = getAdminIfSession().FName, fRecorderDate = DateTime.Today.ToShortDateString() });
            //}
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User))) { return Content("並末登入，請回首頁登入後進行"); }

            return View(new CCalendarViewModel() { fRecorder = getCustomerIfSession().FName, fRecorderDate = DateTime.Today.ToShortDateString() });
        }
        [HttpPost]
        public IActionResult CalendarApplyVisitor(CCalendarViewModel ccvmAV)
        { 
            dbClassContext db = new dbClassContext();
            ccvmAV.FCustomerid = getCustomerIfSession().Fid;
            ccvmAV.FDeleteBool = true;
            ccvmAV.eventName = "申請會客";

            ccvmAV.title = db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == getCustomerIfSession().Fid).FName+ "的會客";

            ccvmAV.FApplyVisitor = false;

            ccvmAV.FVisualHierarchy = 1;    //初步將可視等級為1免得不打會0大家都看的到
            ccvmAV.className = "genric-btn warning circle arrow";


            db.Add(ccvmAV.calendar);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult CalendarApplyCensorList()
        {

            dbClassContext db = new dbClassContext();
            var calendarForomDB = db.TCalendars.Where(_ => _.FApplyVisitor != null);
            List<CCalendarViewModel> viewModelForList = new List<CCalendarViewModel>();
            foreach (TCalendar item in calendarForomDB)
            {
                viewModelForList.Add(new CCalendarViewModel() { calendar = item });
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_ADMIN_User)))
            {
                ViewBag.getAdminSection = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);
            }
            return View(viewModelForList.OrderByDescending(_ => _.fRecorderDate));
        }
        public IActionResult CalendarApplyVisitorAdminCreate()
        {
            dbClassContext db = new dbClassContext();
            CCalendarViewModel ccvm = new CCalendarViewModel()
            {
                fRecorder = getAdminIfSession().FName,
                fRecorderDate = DateTime.Today.ToShortDateString(),
                FAdminId = getAdminIfSession().Fid,
            };

            IQueryable<PatientInfo> getFormDB = db.PatientInfos.Where(_ => _.Status == true);
            ccvm.getAllResidentAndCustomerIDList = new Dictionary<int, string>();
            foreach (PatientInfo? item in getFormDB)
            {
                ccvm.getAllResidentAndCustomerIDList.Add( Convert.ToInt32( item.FCustomerid), item.FName  );
            }

            return View(ccvm);
        }
        [HttpPost]
        public IActionResult CalendarApplyVisitorAdminCreate(CCalendarViewModel ccvmPost)
        {
            dbClassContext db = new dbClassContext();

            ccvmPost.eventName = "申請會客(後端)";
            ccvmPost.FApplyVisitor = false;
            ccvmPost.FDeleteBool = true;
            ccvmPost.className = "genric-btn warning circle arrow";
            ccvmPost.title = db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == ccvmPost.FCustomerid).FName + "的會面";
            db.TCalendars.Add(ccvmPost.calendar);
            db.SaveChanges();

            return RedirectToAction("CalendarApplyCensorList");
        }

        public IActionResult CalendarAuditDecision(int? id) {

            dbClassContext db = new dbClassContext();

            CCalendarViewModel ccvm = new CCalendarViewModel();
            ccvm.calendar = db.TCalendars.FirstOrDefault(_ => _.FId == id);
            ccvm.FAdminId = getAdminIfSession().Fid;
            return View(ccvm);
        }
        [HttpPost]
        public IActionResult CalendarAuditDecision(CCalendarViewModel ccvm,string hiddenTF) {

            dbClassContext db = new dbClassContext();
            ccvm.FApplyVisitor = Convert.ToBoolean( hiddenTF);
            if ((bool)ccvm.FApplyVisitor) { ccvm.className = "genric-btn info circle arrow"; }
            else { ccvm.className = "genric-btn warning circle arrow"; }
            db.Update(ccvm.calendar);
            db.SaveChanges();
            return RedirectToAction("CalendarApplyCensorList");
        }
    }
}
