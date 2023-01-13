using Microsoft.AspNetCore.Mvc;

namespace prjRehabilitation.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
