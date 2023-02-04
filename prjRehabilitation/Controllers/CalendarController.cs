using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
	public class CalendarController : Controller
	{

		public List<TCalendar> betweenDate(DateTime targetDay) {

			dbClassContext db = new dbClassContext();

			DateTime getToday = DateTime.Today;

			string getFront30 = getToday.AddDays(-30).ToString("yyyy-MM-dd");
			string getNext30 = getToday.AddDays(30).ToString("yyyy-MM-dd");

			List<TCalendar> targetRangeDays =	(from d in db.TCalendars
											 where (string.Compare(d.FDate, getFront30) >= 0) && (string.Compare(d.FDate, getNext30) <= 0)
											select d).ToList();

			return targetRangeDays;
		}



		public IActionResult Index()
		{
			dbClassContext db = new dbClassContext();

            CCalendarTotalNeedViewModel cctnvm = new CCalendarTotalNeedViewModel();
        
            cctnvm.ResidentReVisitDay = (from p in db.Consultations
									 group p by p.PatinetId into grp
									 select grp.OrderByDescending(g => g.Date).First()).ToList();

			cctnvm.GetTodayNextAndFrontOneMonth = betweenDate(DateTime.Today);

			return View(cctnvm);
		}
		[HttpGet]
		public IActionResult Create()
		{
			string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
			Customer customer = null;

			if (!string.IsNullOrEmpty( json) )
			{
				customer = JsonSerializer.Deserialize<Customer>(json);
			}

			return View(new CCalendarViewModel() { fRecorder = "某某人", fRecorderDate = DateTime.Today.ToShortDateString() });

		}
		[HttpPost]
		public IActionResult Create(CCalendarViewModel ccvm)
		{
			dbClassContext db = new dbClassContext();
			ccvm.FDeleteBool = true;

			db.Add(ccvm.calendar);
			db.SaveChanges();

			return RedirectToAction("index");
		}

	}
}
