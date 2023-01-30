using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;
using System.Text.Json;

namespace prjRehabilitation.Controllers.Api
{
    public class GroupActivityAjaxController : Controller
    {
        dbClassContext db = new dbClassContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getResident() {

            

            return Json(db.PatientInfos.Where(_ => _.Status == true));
        }

        public IActionResult CPersonalPerformancesPartialViewViewModelList() {

            CPersonalPerformancesPartialViewViewModel cpppvvml = new CPersonalPerformancesPartialViewViewModel();

            List<List<string>> toJosn = new List<List<string>>
            {
                cpppvvml.comboxEmotions,
                cpppvvml.comboxParticipatePersistence,
                cpppvvml.comboxCooperate,
                cpppvvml.comboxHumanInteraction,
                cpppvvml.comboxAttentionRes,
                cpppvvml.comboxParticipatePerformance
            };



            return Json(JsonSerializer.Serialize(toJosn));
        }

    }
}
