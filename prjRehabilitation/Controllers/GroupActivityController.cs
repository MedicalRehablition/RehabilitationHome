using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
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
                return View(cgavm);
            }

            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CGroupActivityViewModel vm)
        {

            TGroupActivity? tempGetTarget = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == vm.FGroupActivityId);

            if (tempGetTarget != null)
            {

                tempGetTarget.FEventType = vm.FEventType;
                tempGetTarget.FDate = vm.FDate;
                tempGetTarget.FStartTime = vm.FStartTime;
                tempGetTarget.FEndTime = vm.FEndTime;
                tempGetTarget.FGroupName = vm.FGroupName;
                tempGetTarget.FClassName = vm.FClassName;
                tempGetTarget.FGoal = vm.FGoal;
                tempGetTarget.FLocation = vm.FLocation;
                tempGetTarget.FPeopleCount = vm.FPeopleCount;
                tempGetTarget.FLeader = vm.FLeader;
                tempGetTarget.FRecorder = vm.FRecorder;
                tempGetTarget.FProcess = vm.FProcess;
                tempGetTarget.FAchievement = vm.FAchievement;
                tempGetTarget.FFillFormStaff = vm.FFillFormStaff;
                tempGetTarget.FFillFormDate = vm.FFillFormDate;

                db.SaveChanges();

            }

            return RedirectToAction("List");
        }

        public ActionResult aa() {
            return View();
        }

    }
}
