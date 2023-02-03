using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;

namespace prjRehabilitation.Controllers
{
    public class UserInformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserPatientPartialView()
        {
            return PartialView();
        }
        public IActionResult GetUserPatient()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_Login_User);
            return Json(json);   
        }
    }
}
