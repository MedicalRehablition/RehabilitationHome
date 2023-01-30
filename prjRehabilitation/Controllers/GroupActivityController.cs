using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Eric;
using System.Dynamic;
using System.Security.Claims;

namespace prjRehabilitation.Controllers
{
    public class GroupActivityController : Controller
    {
        dbClassContext db = new dbClassContext();

        public IActionResult List()
        {
            List<TGroupActivity> gaList = db.TGroupActivities.ToList();

            List<CGroupActivityViewModel> gavmList = new List<CGroupActivityViewModel>();

            foreach (var item in gaList)
            {
                CGroupActivityViewModel gavm = new CGroupActivityViewModel();
                gavm.groupActivity = item;
                gavmList.Add(gavm);
            }

            return View(gavmList);
        }

        public ActionResult Edit(int? id)
        {

            TGroupActivity? tempTGA = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == id);
            CGroupActivityViewModel cgavm = new CGroupActivityViewModel();
            if (tempTGA != null)
            {
                cgavm.groupActivity = tempTGA;
                CGroupActivityEditViewModel mymodel = new CGroupActivityEditViewModel();
                mymodel.cgavm = cgavm;

                return View(mymodel);
            }

            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CGroupActivityEditViewModel vm)
        {

            TGroupActivity? tempGetTarget = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == vm.cgavm.FGroupActivityId);

            if (tempGetTarget != null)
            {

                tempGetTarget.FEventType = vm.cgavm.FEventType;
                tempGetTarget.FDate = vm.cgavm.FDate;
                tempGetTarget.FStartTime = vm.cgavm.FStartTime;
                tempGetTarget.FEndTime = vm.cgavm.FEndTime;
                tempGetTarget.FGroupName = vm.cgavm.FGroupName;
                tempGetTarget.FClassName = vm.cgavm.FClassName;
                tempGetTarget.FGoal = vm.cgavm.FGoal;
                tempGetTarget.FLocation = vm.cgavm.FLocation;
                tempGetTarget.FPeopleCount = vm.cgavm.FPeopleCount;
                tempGetTarget.FLeader = vm.cgavm.FLeader;
                tempGetTarget.FRecorder = vm.cgavm.FRecorder;
                tempGetTarget.FProcess = vm.cgavm.FProcess;
                tempGetTarget.FAchievement = vm.cgavm.FAchievement;
                tempGetTarget.FFillFormStaff = vm.cgavm.FFillFormStaff;
                tempGetTarget.FFillFormDate = vm.cgavm.FFillFormDate;

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }

        public IActionResult ClassThemesPartialView(int? id)
        {
            //int aa = (int)id;
            int[] aa = db.TGroupActivityClassThemes.Where(_ => _.FGroupActivityId == id).Select(_ => _.FClassThemeId).ToArray();
            string[] resultArray = new string[aa.Count()];
            CClassThemesPartialViewViewModel cctpvvm = new CClassThemesPartialViewViewModel();
            for (int i = 0; i < resultArray.Length; i++)
            {
                resultArray[i] = db.TypeNames.FirstOrDefault(_ => _.TypeNameId == aa[i]).TypeName1;
                cctpvvm.ThemeIndexString = resultArray[i];  //為了要只顯示一個字而加。
            }
            cctpvvm.ClassThemesEach = resultArray;

            cctpvvm.ClassThemesList = db.TypeNames.Select(_ => _.TypeName1).ToArray();

            return PartialView(cctpvvm);
        }

        public IActionResult ScheduleDetailsPartialView(int? id)
        {

            CScheduleDetailsPartialViewViewModel csdpvv = new CScheduleDetailsPartialViewViewModel();

            csdpvv.tsd = db.TScheduleDetails.Where(_ => _.FGroupActivityId == id).ToArray();

            return PartialView(csdpvv);
        }

        public IActionResult PersonalPerformancesPartialView(int? id)
        {

            CPersonalPerformancesPartialViewViewModel cpppvvm = new CPersonalPerformancesPartialViewViewModel();

            cpppvvm.tpp = db.TPersonalPerformances.Where(_ => _.FGroupActivityId == id).ToArray();

         var Patients = from tempAA in db.PatientInfos select new { tempAA.Fid,tempAA.FName  };

            string[] names = new string[Patients.Count()];
            int count = 0;
            foreach (var item in Patients)
            {

                names[count] = item.Fid + "," + item.FName;

                count++;
            }
            cpppvvm.ResidentName = names;

            return PartialView(cpppvvm);
        }


    }
}
