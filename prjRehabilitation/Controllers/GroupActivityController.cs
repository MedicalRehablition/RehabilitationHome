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

        public IActionResult ClassThemesPartialView(int? id) {
            //int aa = (int)id;
            IEnumerable<int> aa = db.TGroupActivityClassThemes.Select(_ => _.FClassThemeId);
            return PartialView(aa);
        }

    }
}
