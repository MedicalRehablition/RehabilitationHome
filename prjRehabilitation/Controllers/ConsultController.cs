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
            ViewBag.ptid = id;
            return View(list);
        }
        public ActionResult Create(int? id)
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
            return RedirectToAction("DateList","Consult", new { @id = vm.PatinetId });
        }
        public ActionResult Edit(int? id)
        {
            dbClassContext db = new dbClassContext();
            Consultation consult = db.Consultations.FirstOrDefault(t => t.FConsultId == id);      //'查詢'頁面輸入的資料到Tptient去撈資料，並放到CProductViewMode
            CConsultationViewModel vm = new CConsultationViewModel();
            vm.Consult = consult;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Edit(CConsultationViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            Consultation consult = db.Consultations.FirstOrDefault(t => t.FConsultId == vm.FConsultId);
            if (consult!=null)
            {
                consult.Date = vm.Date;
                consult.Assessment = vm.Assessment;
                consult.Summary = vm.Summary;
                consult.Result = vm.Result;
                db.SaveChanges();
            }
            return RedirectToAction("DateList", "Consult", new { @id = vm.PatinetId }); 
        }

        public ActionResult Delete(int? id)
        {
            dbClassContext db = new dbClassContext();
            Consultation con = db.Consultations.FirstOrDefault(t => t.FConsultId == id);
            int ptid = (int)db.Consultations.FirstOrDefault(t => t.FConsultId == id).PatinetId;
            if (con != null)
            {
                db.Consultations.Remove(con);
                db.SaveChanges();
            }
            return RedirectToAction("DateList","Consult", new { @id = ptid});
            
        }

            //先將基本先刪修功能完成，10個類型的刪修可以之後在加到CConsultationViewModel裡面

        }
}
