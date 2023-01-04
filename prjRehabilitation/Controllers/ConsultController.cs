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
            int spid = 0;
            IEnumerable<Consultation> datas = null;
            if (id != null)
            {
                HttpContext.Session.SetInt32("Pid",(int)id);
            }
            spid = (int)HttpContext.Session.GetInt32("Pid");
            datas = db.Consultations.Where(c => c.PatinetId == spid).ToList();
            
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
            int spid = (int)HttpContext.Session.GetInt32("Pid");
            CConsultationViewModel consult = new CConsultationViewModel();
            consult.PatinetId = spid;
            ViewBag.pid = spid;   //viewbag.name秀在create的view上，需要用spid抓pationinfo的資料庫的姓名

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

        public ActionResult Delete(int? id)    //刪除功能
        {
            dbClassContext db = new dbClassContext();
            Consultation consul = db.Consultations.FirstOrDefault(t => t.FConsultId == id);
            if (consul != null)
            {
                db.Consultations.Remove(consul);
                db.SaveChanges();
            }
            return RedirectToAction("DateList");
        }


        //先將基本先刪修功能完成，10個類型的刪修可以之後在加到CConsultationViewModel裡面

    }
}
