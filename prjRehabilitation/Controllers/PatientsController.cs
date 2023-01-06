using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Lin;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using System.Xml.Linq;

namespace prjRehabilitation.Controllers
{
    public class PatientsController : Controller
    {
        List<VMDisease> list = new List<VMDisease>();
        private IWebHostEnvironment _environment;
        public PatientsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult List(VMPatientList vm)
        {
            dbClassContext db = new dbClassContext();
            IEnumerable<PatientInfo> data = db.PatientInfos.Take(100);
            //if (Keyword == null)
            //{
            //    data = from p in db.PatientInfos
            //           select p;
            //}
            //else
            //data = db.PatientInfos.Where(c => c.FName.Contains(Keyword)).ToList();
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data.ToList())
            {
                VMPatientList p = new VMPatientList();
                p.fid = (int)c.Fid;
                p.fName = c.FName;
                p.fPhone = c.FPhone;
                p.fidnum = c.FIdnum;
                List.Add(p);
            }
            return View(List);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CPatientsViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            db.PatientInfos.Add(vm.Patient);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            dbClassContext db = new dbClassContext();
            PatientInfo patient = db.PatientInfos.FirstOrDefault(c => c.Fid == id);
            db.PatientInfos.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            dbClassContext db = new dbClassContext();
            //抓住民
            var data = new VMPatientInfoDetail();
            var p = db.PatientInfos.FirstOrDefault(x => x.Fid == id);
            if (p == null) return RedirectToAction("List");
            //抓住民疾病
            data._patientInfo = p;
            var d = db.DiseaseDiagnoses.Where(x => x.IdPatientDisease == p.Fid).Take(5).ToList();
            foreach (var c in d)
            {
                data.DiseaseList.Add(new VMDisease
                {
                    DiseaseChineseName = c.DiseaseChineseName,
                    ID_Disease = c.IdDisease
                });
            }
            //抓緊急聯絡人
            var ecaller = db.EmergenceCallers.Where(x => x.FPatientId == p.Fid).Take(2);
            if (ecaller != null) {
                //data.emerCaller1 = ecaller.Take(1).First();
            }
            
            //data.emerCaller2 = ecaller.TakeLast(1).First();

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(VMPatientInfoDetail vm)
        {
            dbClassContext db = new dbClassContext();

            return RedirectToAction("List");
        }
        public IActionResult Create_F(string disease)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create_F(VMPatientInfoDetail vm, string disease, string id, string name)
        {
            if (id != null) goto part2;
            if (!string.IsNullOrEmpty(disease))
            {
                string json = JsonSerializer.Serialize((new DiseaseCRUD()).search(disease));
                return Json(json);
            }
        part2:
            if (!string.IsNullOrEmpty(disease))
            {
                list.Add(new VMDisease
                {
                    DiseaseChineseName = name,
                    ID_Disease = id
                });
                return Json("");
            }

            foreach (var c in list)
            {
                vm.DiseaseList.Add(c);
            }
            var tool = new PatientInfoCRUD();
            //TODO 回傳字串處理
            string message = tool.c_PatientInfo(vm);
            return RedirectToAction("List");
        }


    }

}
