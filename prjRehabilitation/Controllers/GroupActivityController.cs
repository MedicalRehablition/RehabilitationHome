using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel;
using prjRehabilitation.ViewModel.Eric;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Security.Claims;
using System.Text.Json;
using static prjRehabilitation.Controllers.Api.GroupActivityAjaxController;

namespace prjRehabilitation.Controllers
{
    public class GroupActivityController : Controller
    {
        public Customer getCustomerIfSession()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
            Customer customer = null;

            if (!string.IsNullOrEmpty(json))
            {
                customer = JsonSerializer.Deserialize<Customer>(json);
            }
            return customer;
        }
        public Admin getAdminIfSession()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_ADMIN_User);
            Admin admin = null;

            if (!string.IsNullOrEmpty(json))
            {
                admin = JsonSerializer.Deserialize<Admin>(json);
            }
            return admin;
        }

        public IActionResult List()
        {
            dbClassContext db = new dbClassContext();
            List<TGroupActivity> gaList = db.TGroupActivities.Where(_ => _.FDeleteBool == true).ToList();

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
            if (getAdminIfSession() == null) { return Content("此功能需後端登入。"); }

            dbClassContext db = new dbClassContext();
            TGroupActivity? tempTGA = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == id);
            CGroupActivityViewModel cgavm = new CGroupActivityViewModel();
            if (tempTGA != null)
            {
                cgavm.groupActivity = tempTGA;
                CGroupActivityEditViewModel mymodel = new CGroupActivityEditViewModel();
                mymodel.cgavm = cgavm;

                TGroupActivityPicAndFile TgroupactitypicandfileifNull = db.TGroupActivityPicAndFiles.FirstOrDefault(_ => _.FGroupActivityId == id);    //帶圖的內容過去
                if (TgroupactitypicandfileifNull != null) { mymodel.tgapaf = TgroupactitypicandfileifNull; }
                else { mymodel.tgapaf = new TGroupActivityPicAndFile(); }//還是要給一個空的不然那邊連null都沒辦法判定。

                return View(mymodel);
            }

            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CGroupActivityEditViewModel vm, IFormFile FPicture1, IFormFile FPicture2, IFormFile FPicture3, IFormFile FPicture4)
        {
            dbClassContext db = new dbClassContext();
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

                var ifGetId = db.TGroupActivityPicAndFiles.FirstOrDefault(_ => _.FGroupActivityId == vm.cgavm.FGroupActivityId);

                //byte[]? imgByte = null;
                if (ifGetId != null)
                {

                    getImamge(ifGetId, FPicture1, FPicture2, FPicture3, FPicture4);

                }
                else
                {

                    db.TGroupActivityPicAndFiles.Add(getImamge(new TGroupActivityPicAndFile() { FGroupActivityId = vm.cgavm.FGroupActivityId }, FPicture1, FPicture2, FPicture3, FPicture4));
                }


                db.SaveChanges();

            }

            return RedirectToAction("List");
        }

        public IActionResult ClassThemesPartialView(int? id)
        {
            dbClassContext db = new dbClassContext();
            //int aa = (int)id;
            int[] aa = db.TGroupActivityClassThemes.Where(_ => _.FGroupActivityId == id).Where(_ => _.FDeleteBool == true).Select(_ => _.FClassThemeId).ToArray();
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
            dbClassContext db = new dbClassContext();
            CScheduleDetailsPartialViewViewModel csdpvv = new CScheduleDetailsPartialViewViewModel();

            csdpvv.tsd = db.TScheduleDetails.Where(_ => _.FGroupActivityId == id).ToArray();

            return PartialView(csdpvv);
        }

        public IActionResult PersonalPerformancesPartialView(int? id)
        {
            dbClassContext db = new dbClassContext();
            CPersonalPerformancesPartialViewViewModel cpppvvm = new CPersonalPerformancesPartialViewViewModel();

            cpppvvm.tpp = db.TPersonalPerformances.Where(_ => _.FGroupActivityId == id).Where(_ => _.FDeleteBool == "1").ToArray();

            var Patients = from tempAA in db.PatientInfos select new { tempAA.Fid, tempAA.FName };

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


        public IActionResult delete(int? id)
        {

            dbClassContext db = new dbClassContext();

            db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == id).FDeleteBool = false;

            db.SaveChanges();

            return RedirectToAction("List");
        }

        #region
        //public IActionResult PicAndFileSaveIn(int? id, IFormFile FPicture1, IFormFile FPicture2, IFormFile FPicture3, IFormFile FPicture4) {  //改由submit直接存了
        //    dbClassContext db = new dbClassContext();
        //    var ifGetId = db.TGroupActivityPicAndFiles.FirstOrDefault(_ => _.FGroupActivityId == id);

        //        //byte[]? imgByte = null;
        //    if (ifGetId != null) {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            if (FPicture1 != null)
        //            {
        //                FPicture1.CopyTo(memoryStream);
        //                ifGetId.FPicture1 = memoryStream.ToArray();
        //            }
        //            if (FPicture2 != null)
        //            {
        //                FPicture2.CopyTo(memoryStream);
        //                ifGetId.FPicture2 = memoryStream.ToArray();
        //            }
        //            if (FPicture3 != null)
        //            {
        //                FPicture3.CopyTo(memoryStream);
        //                ifGetId.FPicture3 = memoryStream.ToArray();
        //            }
        //            if (FPicture4 != null)
        //            {
        //                FPicture4.CopyTo(memoryStream);
        //                ifGetId.FPicture4 = memoryStream.ToArray();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        TGroupActivityPicAndFile tPicAndFile = new TGroupActivityPicAndFile();
        //        tPicAndFile.FGroupActivityId =Convert.ToInt32( id);
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            if (FPicture1 != null)
        //            {
        //                FPicture1.CopyTo(memoryStream);
        //                tPicAndFile.FPicture1 = memoryStream.ToArray();
        //            }
        //            if (FPicture2 != null)
        //            {
        //                FPicture2.CopyTo(memoryStream);
        //                tPicAndFile.FPicture2 = memoryStream.ToArray();
        //            }
        //            if (FPicture3 != null)
        //            {
        //                FPicture3.CopyTo(memoryStream);
        //                tPicAndFile.FPicture3 = memoryStream.ToArray();
        //            }
        //            if (FPicture4 != null)
        //            {
        //                FPicture4.CopyTo(memoryStream);
        //                tPicAndFile.FPicture4 = memoryStream.ToArray();
        //            }
        //        }

        //        db.TGroupActivityPicAndFiles.Add(tPicAndFile);
        //    }

        //    db.SaveChanges();
        //    return View();
        //}
        #endregion

        public IActionResult Create()
        {

            if (getAdminIfSession() == null) { return Content("此功能需後端登入。"); }
            string name = getAdminIfSession().FName;
            dbClassContext db = new dbClassContext();
            //TGroupActivity? tempTGA = db.TGroupActivities.FirstOrDefault(_ => _.FGroupActivityId == id);
            CGroupActivityViewModel cgavm = new CGroupActivityViewModel();
            cgavm.FRecorder = name;
            cgavm.FLeader = name;
            cgavm.FFillFormStaff = name;
            cgavm.FFillFormDate = DateTime.Today.ToString("yyyy-MM-dd");
            CGroupActivityEditViewModel mymodel = new CGroupActivityEditViewModel();
            mymodel.cgavm = cgavm;

            //mymodel.tgapaf = new TGroupActivityPicAndFile();




            return View(mymodel);


        }


        public TGroupActivityPicAndFile getImamge(TGroupActivityPicAndFile tgapf, IFormFile FPicture1, IFormFile FPicture2, IFormFile FPicture3, IFormFile FPicture4)
        {

            if (FPicture1 != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    FPicture1.CopyTo(memoryStream);
                    tgapf.FPicture1 = memoryStream.ToArray();
                    tgapf.FPicture1Path = FPicture1.FileName;
                }
            }
            if (FPicture2 != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    FPicture2.CopyTo(memoryStream);
                    tgapf.FPicture2 = memoryStream.ToArray();
                    tgapf.FPicture2Path = FPicture2.FileName;
                }
            }
            if (FPicture3 != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    FPicture3.CopyTo(memoryStream);
                    tgapf.FPicture3 = memoryStream.ToArray();
                    tgapf.FPicture3Path = FPicture3.FileName;
                }
            }
            if (FPicture4 != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    FPicture4.CopyTo(memoryStream);
                    tgapf.FPicture4 = memoryStream.ToArray();
                    tgapf.FPicture4Path = FPicture4.FileName;
                }
            }

            return tgapf;
        }

       
        [HttpPost]
        public IActionResult Create(CGroupActivityEditViewModel vm, IFormFile FPicture1, IFormFile FPicture2, IFormFile FPicture3, IFormFile FPicture4)
        {

            dbClassContext db = new dbClassContext();
            vm.cgavm.FDeleteBool = true;
          
            vm.cgavm.TGroupActivityClassThemes = vm.cgavm.TGroupActivityClassThemes.DistinctBy(_ => _.FClassThemeId).ToList();

            foreach (var item in vm.cgavm.TGroupActivityClassThemes)
            {
                item.FDeleteBool = true;
            }

            foreach (TPersonalPerformance item in vm.cgavm.TPersonalPerformances)
            {
                item.FDeleteBool = "1";
            }
            db.Add(vm.cgavm.groupActivity);
            db.SaveChanges();

            int gaFidLast = Convert.ToInt32(db.TGroupActivities.OrderBy(_ => _.FGroupActivityId).LastOrDefault().FGroupActivityId);

            db.TGroupActivityPicAndFiles.Add(getImamge(new TGroupActivityPicAndFile() { FGroupActivityId = gaFidLast }, FPicture1, FPicture2, FPicture3, FPicture4));


            db.SaveChanges();

            return RedirectToAction("List");
        }





    }
}
