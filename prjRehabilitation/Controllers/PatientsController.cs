using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;

namespace prjRehabilitation.Controllers
{
    public class PatientsController : Controller
    {
        private IWebHostEnvironment _environment;
        public PatientsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult List(CKeywordViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            string Keyword = vm.txtKeyword;
            IEnumerable<PatientInfo> data = null;
            if (Keyword == null)
            {
                data = from p in db.PatientInfos
                       select p;
            }
            else
                data = db.PatientInfos.Where(c => c.FName.Contains(Keyword)).ToList();
            List<CPatientsViewModel> List = new List<CPatientsViewModel>();
            foreach(var c in data)
            {
                CPatientsViewModel patient = new CPatientsViewModel();
                patient.Patient = c;
                List.Add(patient);
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
            PatientInfo patient = db.PatientInfos.FirstOrDefault(c=>c.Fid== id);
            db.PatientInfos.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            dbClassContext db =new dbClassContext();
            PatientInfo patient = db.PatientInfos.FirstOrDefault(c=>c.Fid== id);
            CPatientsViewModel vm = new CPatientsViewModel();
            vm.Patient = patient;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(CPatientsViewModel vm)
        {
            dbClassContext db = new dbClassContext();
            PatientInfo patient = db.PatientInfos.FirstOrDefault(c => c.Fid == vm.Fid);
            if(patient != null)
            {
                //if(vm.photo!= null)
                //{
                //    string photoName = Guid.NewGuid().ToString() + ".jpg";
                //    string path = _environment.WebRootPath + "/images/" + photoName;
                //    patient.FPicture = photoName;
                //    vm.photo.CopyTo(new FileStream(path,FileMode.Create));
                //}
                patient.Fid = vm.Fid;
                patient.FHos = vm.FHos;
                patient.FBednum=vm.FBednum;
                patient.FIdnum = vm.FIdnum;
                patient.FName= vm.FName;
                patient.FSex = vm.FSex;
                patient.FPhone= vm.FPhone;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }

}
