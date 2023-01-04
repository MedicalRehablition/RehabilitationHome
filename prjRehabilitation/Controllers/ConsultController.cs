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
                       select new PatientInfo { FName = p.FName, Fid = p.Fid };
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
        public IActionResult DateList(int? id)
        {
            dbClassContext db = new dbClassContext();

            IEnumerable<Consultation> datas = null;
            datas = db.Consultations.Where(c => c.PatinetId == id.Value).ToList();

            List<CConsultationViewModel> list = new List<CConsultationViewModel>();
            foreach (var c in datas)
            {
                CConsultationViewModel consultation = new CConsultationViewModel();
                consultation.Consult = c;
                list.Add(consultation);
            }
            var pin = db.PatientInfos.FirstOrDefault(t => t.Fid == id);
            CPatientsViewModel ptname = new CPatientsViewModel();
            ptname.Patient = pin != null ? pin:new PatientInfo() ;
            ViewBag.name = ptname?.Patient?.FName;           
            return View(list);
        }
        public ActionResult Create(int id)
        {
            CConsultationViewModel consult = new CConsultationViewModel();
            consult.PatinetId = id;
            ViewBag.pid = id;

            return View(consult);
        }
        [HttpPost]
        public ActionResult SaveCreate(CConsultationViewModel vm)
        {        
            dbClassContext db = new dbClassContext();
            
            db.Consultations.Add(vm.Consult);
            db.SaveChanges();
            return RedirectToAction("DateList");

        }




        //先將基本先刪修功能完成，10個類型的刪修可以之後在加到CConsultationViewModel裡面

    }
}
