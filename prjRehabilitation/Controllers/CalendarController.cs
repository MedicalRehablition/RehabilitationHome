using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;
using System.Text.Json;

namespace prjRehabilitation.Controllers
{
	public class CalendarController : Controller
	{
		public IActionResult Index()
		{
			dbClassContext db = new dbClassContext();

			//Consultation aa = (from test in db.Consultations
			//		  orderby test.Date
			//		  select test).Last();

			//List<Consultation> ll = new List<Consultation> { aa };
			List<Consultation> aa = (from p in db.Consultations
										 //where conditions or joins with other tables to be included here
									 group p by p.PatinetId into grp
									 select grp.OrderByDescending(g => g.Date).First()).ToList();

			//foreach (var a in aa) {

			//	Console.WriteLine(a);
			//}


			return View(aa);
		}
		[HttpGet]
		public IActionResult Create()
		{
			string json = "";
			Customer customer = null;
			if (HttpContext.Session.GetString(CDictionary.SK_Login_User) != null)
			{
				customer = JsonSerializer.Deserialize<Customer>(json);
			}

			return View(new CCalendarViewModel() { fRecorder = "某某人", fRecorderDate = DateTime.Today.ToShortDateString() });

		}
		[HttpPost]
		public IActionResult Create(CCalendarViewModel ccvm)
		{


			dbClassContext db = new dbClassContext();



			return View();
		}

	}
}
