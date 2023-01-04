using Microsoft.AspNetCore.Mvc;

namespace prjRehabilitation.Controllers
{
    public class UserLoginController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
