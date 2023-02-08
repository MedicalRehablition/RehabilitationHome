using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Text.Json;
using System.Xml.Linq;

namespace prjRehabilitation.Controllers
{
    public class ConsultController : Controller
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);//得到工作人員的session
            string jsonc = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);  //得到訪客的session
            dbClassContext db = new dbClassContext();
            if (!string.IsNullOrEmpty(json)) //員工有登入
            {
                string Keyword = vm.txtKeyword;
                IEnumerable<PatientInfo> data = null;
                if (Keyword == null)
                {
                    data = from p in db.PatientInfos
                           where p.Status == true
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
            else if (!string.IsNullOrEmpty(jsonc))//家屬有登入
            {
                Customer customer = JsonSerializer.Deserialize<Customer>(jsonc);
                var pin = db.PatientInfos.FirstOrDefault(t => t.FCustomerid == customer.Fid);
                if (pin != null)
                {
                    return RedirectToAction("DateList", "Consult", new { @id = pin.Fid });
                };
            };
            return RedirectToAction("Index", "Home"); //其他狀況一律回主頁
        }
        public IActionResult DateList(int? id)
        {
            //-------custoer setting-----
            string jsonc = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User); 
            ViewBag.setting = "";
            if (jsonc != null)
            {
                ViewBag.setting = "customer";
            };

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
        public ActionResult SaveCreate(CConsultationViewModel vm, [FromForm] List<int> Typeconsult)
        {
            dbClassContext db = new dbClassContext();
            db.Consultations.Add(vm.Consult);
            db.SaveChanges();
            //-------------------------------checkbox存進資料庫
            foreach (var item in Typeconsult)
            {
                var ctr = new CounsultTypeRecord
                {
                    FConsultId = vm.Consult.FConsultId,
                    TypeNameId = item
                };

                db.CounsultTypeRecords.Add(ctr);
            }
            //-------------------------------
            db.SaveChanges();

            return RedirectToAction("DateList", "Consult", new { @id = vm.PatinetId });
        }
        public ActionResult Edit(int? id)
        {
            //-------custoer setting-----
            string jsonc = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            ViewBag.setting = "";
            if (jsonc != null)
            {
                ViewBag.setting = "customer";
            };
            dbClassContext db = new dbClassContext();
            Consultation consult = db.Consultations.FirstOrDefault(t => t.FConsultId == id);      //'查詢'頁面輸入的資料到Tptient去撈資料，並放到CProductViewMode
            CConsultationViewModel vm = new CConsultationViewModel();
            vm.Consult = consult;
            //-----------chechbox呈現
            var q = (from cc in db.CounsultTypeRecords
                     where cc.FConsultId == id
                     select cc.TypeNameId).ToList();
            vm.Typeconsult = q;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Edit(CConsultationViewModel vm, [FromForm] List<int> Typeconsult)
        {
            dbClassContext db = new dbClassContext();
            Consultation consult = db.Consultations.FirstOrDefault(t => t.FConsultId == vm.FConsultId);
            if (consult != null)
            {
                consult.Date = vm.Date;
                consult.Assessment = vm.Assessment;
                consult.Summary = vm.Summary;
                consult.Result = vm.Result;
                db.SaveChanges();
            }
            //-------checkbox儲存---刪舊的再新增
            var ctr = db.CounsultTypeRecords.Where(t => t.FConsultId == vm.FConsultId);
            db.RemoveRange(ctr);
            db.SaveChanges();
            foreach (var item in Typeconsult)
            {
                var ctred = new CounsultTypeRecord
                {
                    FConsultId = vm.Consult.FConsultId,
                    TypeNameId = item
                };
                db.CounsultTypeRecords.Add(ctred);
            };
            db.SaveChanges();
            return RedirectToAction("DateList", "Consult", new { @id = vm.PatinetId });
        }

        public ActionResult Delete(int? id)
        {
            dbClassContext db = new dbClassContext();
            var ctr = db.CounsultTypeRecords.Where(t => t.FConsultId == id);  //先刪除typerecord，因為是foreign-key
            db.RemoveRange(ctr);
            db.SaveChanges();
            Consultation con = db.Consultations.FirstOrDefault(t => t.FConsultId == id);  //刪除該筆會談資料
            int ptid = (int)db.Consultations.FirstOrDefault(t => t.FConsultId == id).PatinetId; //記住病人id，以便回到datelist
            if (con != null)
            {
                db.Remove(con);
                db.SaveChanges();
            }
            return RedirectToAction("DateList", "Consult", new { @id = ptid });

        }

            //先將基本先刪修功能完成，10個類型的刪修可以之後在加到CConsultationViewModel裡面

        }
}
