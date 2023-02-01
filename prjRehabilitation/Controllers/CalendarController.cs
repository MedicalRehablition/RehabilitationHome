using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;

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


			return View(aa	);
		}


	}
}
