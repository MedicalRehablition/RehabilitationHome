using Microsoft.AspNetCore.Mvc;

namespace prjRehabilitation.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
