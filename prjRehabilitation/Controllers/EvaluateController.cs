using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using System.Data;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace prjRehabilitation.Controllers
{
    public class EvaluateController : Controller
    {
        public IActionResult List(CKeywordViewModel vm)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);//得到工作人員的session
            string jsonc = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);  //得到訪客的session
            ViewBag.keyword = false;
            dbClassContext db = new dbClassContext();
            if (!string.IsNullOrEmpty(json))
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
                    ViewBag.Keyword = true;
                }
                return View(List);
            }
            else if (!string.IsNullOrEmpty(jsonc)) 
            {
            Customer customer = JsonSerializer.Deserialize<Customer>(jsonc);
                var cus = db.PatientInfos.FirstOrDefault(c => c.FCustomerid == customer.Fid);
                if (cus != null) 
                {
                    return RedirectToAction("DateList", "Evaluate", new { @id=cus.Fid});
                };
            };
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DateList(int? id)
        {
            string jsonc = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            ViewBag.setting = "";
            if (jsonc != null)
            {
                ViewBag.setting = "customer";
            };
            dbClassContext db = new dbClassContext();
            List<功能評估> datas = db.功能評估s.Where(c => c.Fid == id).ToList();
            CEvaluateViewModle cevm = null;
            List<CEvaluateViewModle> cevmList = new List<CEvaluateViewModle>();
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
            ViewBag.ptid = id;
            return View(cevmList);
        }
        public IActionResult Create(int? id) 
        {
            ViewBag.id = id;
        //    var selectList = new List<SelectListItem>()
        //    {
        //new SelectListItem {Text="衛生習慣", Value="1" },
        //new SelectListItem {Text="居家清潔", Value="2" },
        //new SelectListItem {Text="飲食起居", Value="3" },
        //new SelectListItem {Text="認知能力", Value="4" },
        //new SelectListItem {Text="體能表現", Value="5" },
        //new SelectListItem {Text="人際互動", Value="6" },
        //new SelectListItem {Text="烹飪能力", Value="7" },
        //new SelectListItem {Text="休閒安排", Value="8" },
        //new SelectListItem {Text="財務管理", Value="9" },
        //new SelectListItem {Text="健康促進", Value="10" },
        //      };
        //    //預設選擇哪一筆
        //    selectList.Where(q => q.Value == "1").First().Selected = true;
        //    ViewBag.SelectList = selectList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CEvaluateViewModle eva)
        {
            dbClassContext db = new dbClassContext();
            eva.Deleted = false;
            db.功能評估s.Add(eva.Evaluate);
            db.SaveChanges();
            eva.F功能評估Id參考 = eva.F功能評估Id;
            db.功能評估個表s.Add(eva.Evaluate2);
            db.SaveChanges();
            return RedirectToAction("DateList", "Evaluate", new { @id = eva.Fid });
        }
        public IActionResult Edit(int? id) 
        {
            string jsonc = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            ViewBag.setting = "";
            if (jsonc != null)
            {
                ViewBag.setting = "customer";
            };
            dbClassContext db = new dbClassContext();
            功能評估 ev1 = db.功能評估s.FirstOrDefault(c => c.F功能評估Id == id);
            var bbb = ev1.F功能評估Id;
            功能評估個表 ev2 = db.功能評估個表s.FirstOrDefault(c => c.F功能評估Id == ev1.F功能評估Id);
            CEvaluateViewModle eva = new CEvaluateViewModle();
            eva.Evaluate2 = ev2;
            eva.Evaluate = ev1;
            return View(eva); 
        }
        [HttpPost]
        public IActionResult Edit(CEvaluateViewModle eva)
        {
            dbClassContext db = new dbClassContext();
            功能評估 ev1 = db.功能評估s.FirstOrDefault(c => c.F功能評估Id == eva.F功能評估Id);
            ev1.F日期 = eva.F日期;
            ev1.F身高 = eva.F身高;
            ev1.F體重 = eva.F體重;
            功能評估個表 ev2 = db.功能評估個表s.FirstOrDefault(c=>c.F功能評估Id==ev1.F功能評估Id);
            ev2.F評估項目 = eva.F評估項目;
            ev2.F問題 = eva.F問題;
            ev2.F現狀評估 = eva.F現狀評估;
            ev2.F復健計畫 = eva.F復健計畫;
            ev2.F復健目標 = eva.F復健目標;
            db.SaveChanges();
            return RedirectToAction("DateList", "Evaluate", new { @id = eva.Fid });
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
