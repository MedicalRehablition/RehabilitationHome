using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Data;

namespace prjRehabilitation.Controllers
{
    public class EvaluateController : Controller
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
            List<功能評估> datas = db.功能評估s.Where(c => c.Fid == id).ToList();
            //IEnumerable<功能評估> datas = null;
            //IEnumerable<功能評估個表> dataQ = null;
            //var aa = from ce in db.功能評估個表s
            //         join de in datas on ce.F功能評估Id equals de.F功能評估Id
            //         select new {
            //             date = de.F日期,
            //             problem = ce.F現狀評估
            //         };
            //List<string> dataQ = new List<string>();           
            //功能評估 ev = new 功能評估();
            CEvaluateViewModle cevm = null;
            List<CEvaluateViewModle> cevmList = new List<CEvaluateViewModle>();
            //List<CEvaluateViewModle> cevmProblem = new List<CEvaluateViewModle>();
            foreach (功能評估 aa in datas) {
                List<功能評估個表> bb = db.功能評估個表s.Where(_ => _.F功能評估Id == aa.F功能評估Id).ToList();
                foreach (var cc in bb)
                {
                    cevm = new CEvaluateViewModle() { F日期 = aa.F日期 };
                    cevm.F問題= cc.F問題;
                    cevm.F功能評估Id = cc.F功能評估Id;
                    cevm.Fid=aa.Fid;
                    cevmList.Add(cevm);
                }
            }
            //foreach (var aa in datas)
            //{
            //    cevm.F問題 = aa.;
            //    cevmProblem.Add(cevm);
            //}
            //功能評估個表
            //db.功能評估個表s.Where(b => b.F功能評估Id == item.F功能評估Id).ToList();
            //List<CEvaluateViewModle> list = new List<CEvaluateViewModle>();
            //foreach (var c in datas)
            //{
            //    CEvaluateViewModle consultation = new CEvaluateViewModle();
            //    consultation.Evaluate = c;
            //    list.Add(consultation);
            //}
            //var pin = db.PatientInfos.FirstOrDefault(t => t.Fid == id);
            //CPatientsViewModel ptname = new CPatientsViewModel();
            //ptname.Patient = pin != null ? pin : new PatientInfo();
            //ViewBag.name = ptname?.Patient?.FName;
            ViewBag.ptid = id;
            return View(cevmList);
        }
        public IActionResult Create(int? id) 
        {
           dbClassContext db = new dbClassContext();
            var selectList = new List<SelectListItem>()
    {
        new SelectListItem {Text="衛生習慣", Value="1" },
        new SelectListItem {Text="居家清潔", Value="2" },
        new SelectListItem {Text="飲食起居", Value="3" },
        new SelectListItem {Text="認知能力", Value="4" },
        new SelectListItem {Text="體能表現", Value="5" },
        new SelectListItem {Text="人際互動", Value="6" },
        new SelectListItem {Text="烹飪能力", Value="7" },
        new SelectListItem {Text="休閒安排", Value="8" },
        new SelectListItem {Text="財務管理", Value="9" },
        new SelectListItem {Text="健康促進", Value="10" },
    };
            //預設選擇哪一筆
            selectList.Where(q => q.Value == "1").First().Selected = true;
            ViewBag.SelectList = selectList;
            return View();
        }
        [HttpPost]
        public IActionResult Create()
        {
            dbClassContext db = new dbClassContext();

            return View();
        }
        public IActionResult Delete(int id) 
        {
            dbClassContext db = new dbClassContext();
            var dataQ = db.功能評估個表s.Where(b => b.F功能評估Id == id);
            db.RemoveRange(dataQ);
            db.SaveChanges();
            var datas = db.功能評估s.Where(c => c.F功能評估Id == id).FirstOrDefault();      
            db.RemoveRange(datas);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
