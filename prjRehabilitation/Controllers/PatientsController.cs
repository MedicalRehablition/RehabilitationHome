using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
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
        public IActionResult G_Patients()
        {
            dbClassContext db = new dbClassContext();
            IEnumerable<PatientInfo> data = db.PatientInfos.Where(x => x.Status == false);
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data.ToList())
            {
                VMPatientList p = new VMPatientList();
                p.fid = (int)c.Fid;
                p.fName = c.FName;
                p.fPhone = c.FPhone;
                p.fidnum = c.FIdnum;
                if (c.FPhotoFile != null)
                {
                    p.fphoto = c.FPhotoFile;
                }
                List.Add(p);
            }
            return View(List);
        }
        public IActionResult patients()
        {
            dbClassContext db = new dbClassContext();
            IEnumerable<PatientInfo> data = db.PatientInfos.Where(x => x.Status != false);
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data.ToList())
            {
                VMPatientList p = new VMPatientList();
                p.fid = (int)c.Fid;
                p.fName = c.FName;
                p.fPhone = c.FPhone;
                p.fidnum = c.FIdnum;
                if (c.FPhotoFile != null)
                {
                    p.fphoto = c.FPhotoFile;
                }
                List.Add(p);
            }
            return PartialView(List);
        }
        public IActionResult form()
        {
            return View();
        }
        public IActionResult Empty()
        {
            return PartialView();
        }
        public IActionResult fetchDis(int id)
        {
            dbClassContext db = new dbClassContext();
            return Json(db.DiseaseDiagnoses.Where(x => x.Fid == id));
        }
        public IActionResult Delete(int id)
        {
            (new PatientInfoCRUD()).c_Delete(id);
            return RedirectToAction("patients");
        }
        public IActionResult Recover(int id)
        {
            (new PatientInfoCRUD()).c_Recover(id);
            return RedirectToAction("G_Patients");
        }
        public IActionResult List(VMPatientList vm)
        {
            dbClassContext db = new dbClassContext();
            IEnumerable<PatientInfo> data = db.PatientInfos.Where(x => x.Status != false).Take(100);
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data.ToList())
            {
                VMPatientList p = new VMPatientList();
                p.fid = (int)c.Fid;
                p.fName = c.FName;
                p.fPhone = c.FPhone;
                p.fidnum = c.FIdnum;
                if (c.FPhotoFile != null)
                {
                    p.fphoto = c.FPhotoFile;
                }
                List.Add(p);
            }
            return View(List);
        }


        [HttpPost]
        public IActionResult Edit(VMPatientInfoDetail vm, string? disease)
        {
            if (!string.IsNullOrEmpty(disease))
            {
                string json = JsonSerializer.Serialize((new DiseaseCRUD()).search(disease));
                return Json(json);
            }
            vm.fIDY = "";
            if (vm.fidy_健保 == true) vm.fIDY += "健保";
            if (vm.fidy_福保 == true) vm.fIDY += "福保";
            if (vm.fidy_身心障礙 == true) vm.fIDY += "身障";
            if (vm.fidy_重大傷病 == true) vm.fIDY += "重大";
            if (vm.fidy_低收 == true) vm.fIDY += "低收";


            //同上，這邊是有無補助

            if (vm.fGrant_無 == "111") vm.fGrant += vm.fGrantType;

            string result = (new PatientInfoCRUD()).c_edit(vm);


            return Json("patients");
        }
        public IActionResult Edit(int? id)
        {
            dbClassContext db = new dbClassContext();
            //抓住民
            var data = new VMPatientInfoDetail();
            var p = db.PatientInfos.FirstOrDefault(x => x.Fid == id);
            if (p == null) return RedirectToAction("List");
            data._patientInfo = p;
            //將資料庫的單一資料欄繫結到checkbox
            if (data.fIDY != null)
            {
                if (p.FPhotoFile != null)
                {
                    data.fimg = p.FPhotoFile;
                }
                if (p.FIdy.Contains("健保")) data.fidy_健保 = true;
                if (p.FIdy.Contains("福保")) data.fidy_福保 = true;
                if (p.FIdy.Contains("重大")) data.fidy_重大傷病 = true;
                if (p.FIdy.Contains("身障")) data.fidy_身心障礙 = true;
                if (p.FIdy.Contains("低收")) data.fidy_低收 = true;
            }

            //同上，這邊是有無補助
            if (data.fGrant != null)
            {
                if (string.IsNullOrEmpty(data.fGrant)) data.fGrant_無 = "000";
                else
                {
                    data.fGrant_無 = "111";
                    data.fGrantType = p.FGrant;
                }
            }
            //住民疾病另外抓，這邊不用
            //抓緊急聯絡人
            var ecaller = db.EmergenceCallers.Where(x => x.FPatientId == p.Fid);
            int count = 1;
            foreach (var c in ecaller)
            {
                if (count > 2 || c == null) break;
                if (count == 1)
                    data.emerCaller1 = c;
                else
                    data.emerCaller2 = c;
                count++;
            }
            return View(data);
        }


        public IActionResult Create_F(string disease)
        {
            var vm = new VMPatientInfoDetail();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create_F(VMPatientInfoDetail vm, string disease, string id, string name)
        {
            if (!string.IsNullOrEmpty(disease))
            {
                string json = JsonSerializer.Serialize((new DiseaseCRUD()).search(disease));
                return Json(json);
            }
            return Json("");
        }
        public IActionResult Create(VMPatientInfoDetail vm) {
            if (vm.fidy_健保 == true) vm.fIDY += "健保";
            if (vm.fidy_福保 == true) vm.fIDY += "福保";
            if (vm.fidy_身心障礙 == true) vm.fIDY += "身障";
            if (vm.fidy_重大傷病 == true) vm.fIDY += "重大";
            if (vm.fidy_低收 == true) vm.fIDY += "低收";

            vm.fGrant += vm.fGrantType;


            var tool = new PatientInfoCRUD();
            //TODO 回傳字串處理
            string message = tool.c_PatientInfo(vm);
            if (message == "失敗: 該住民已被註冊") return Json(new { outcome = "錯誤" });
            return Json(new { outcome = "ok" });
            //return RedirectToAction("List");
        }

    }

}
