using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Xml.Linq;

namespace prjRehabilitation.Controllers
{
    public class ConsultController : Controller
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string Keyword = vm.txtKeyword;
            IEnumerable<PatientInfo> data = null;
            if (Keyword == null)
            {
                data = from p in db.PatientInfos
                       select new PatientInfo { FName = p.FName };
            }
            else
                data = db.PatientInfos.Where(c => c.FName.Contains(Keyword)).ToList();
            List<CPatientsViewModel> List = new List<CPatientsViewModel>();
            foreach (var c in data)
            {
                CPatientsViewModel patient = new CPatientsViewModel();
                patient.Patient = c;
                List.Add(patient);
            }
            return View(List);         
        }
        //public IActionResult DateList()
        //{
        //}
            




    }
}
