using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using NuGet.Protocol;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;

using System.Text.Json;
using static prjRehabilitation.Controllers.Api.GroupActivityAjaxController;

namespace prjRehabilitation.Controllers.Api
{
    public class GroupActivityAjaxController : Controller
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getResident()
        {
            dbClassContext db = new dbClassContext();



            Admin admin = getAdminIfSession();
            Customer customer = getCustomerIfSession();

            List<Consultation> getLastDate = (from p in db.Consultations
                                              group p by p.PatinetId into grp
                                              select grp.OrderByDescending(g => g.Date).First()).ToList();
            if (admin != null)
            {
                //var getResult = db.PatientInfos.Where(_ => _.Status == true).Join(getLastDate, _ => _.Fid, _ => _.PatinetId, (a, b) => new { a.Fid, a.FName, b.Date }).ToList();
                var getResult = getLastDate.Join(db.PatientInfos.Where(_ => _.Status == true), _ => _.PatinetId, _ => _.Fid, (b, a) => new { a.Fid, a.FName, b.Date });
                return Json(getResult);
            }

            if (customer != null)
            {

                if (db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == customer.Fid) != null) //這邊是為了防止有人註冊munber但沒有綁定住民會出錯
                {
                    PatientInfo getResidentID = db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == customer.Fid);

                    getLastDate = getLastDate.Where(_ => _.PatinetId == getResidentID.Fid).ToList();
                    Dictionary<string, string> _ = new Dictionary<string, string>
                    {
                        { "fid", getResidentID.Fid.ToString() },
                        { "fName", getResidentID.FName },
                        { "date", getLastDate.FirstOrDefault().Date }
                    };
                    return Json(new List<Dictionary<string, string>>() { _ });
                }

            }
            else
            {
                Dictionary<string, string> __ = new Dictionary<string, string>   //當這人沒註冊相關住民時就生一個假的給前端json去拆不會出錯而已
                    {
                        { "fid", "fidNoResident" },
                        { "fName", "並未填相關住民" },
                        { "date", DateTime.Today.AddDays(-28).ToString("yyyy-MM-dd") }  //前面會幫我加28所以扣回來才會是今天。
                    };
                return Json(new List<Dictionary<string, string>>() { __ });
            }
            Dictionary<string, string> ___ = new Dictionary<string, string>   //都沒登入時
                    {
                        { "fid", "noLogin" },
                        { "fName", "並未登入" },
                        { "date", DateTime.Today.AddDays(-28).ToString("yyyy-MM-dd") }  //
                    };
            return Json(new List<Dictionary<string, string>>() { ___ });
        }

        public IActionResult CPersonalPerformancesPartialViewViewModelList()
        {

            CPersonalPerformancesPartialViewViewModel cpppvvml = new CPersonalPerformancesPartialViewViewModel();

            List<List<string>> toJosn = new List<List<string>>
            {
                cpppvvml.comboxEmotions,
                cpppvvml.comboxParticipatePersistence,
                cpppvvml.comboxCooperate,
                cpppvvml.comboxHumanInteraction,
                cpppvvml.comboxAttentionRes,
                cpppvvml.comboxParticipatePerformance
            };



            return Json(JsonSerializer.Serialize(toJosn));
        }



        [HttpPost]
        public async Task<IActionResult> PersonalPerformancesEditAjax([FromBody] List<FetchList> fetchList) //因為這邊是拿我設計的class來接所以不用打fid，下面因為是拿現成的modal來接所以沒打fid不承認
        {
            if (fetchList.Count <= 0) { return Json("參與住民為空(後台檢測回覆)"); }
            dbClassContext db = new dbClassContext();

            IQueryable<TPersonalPerformance> oldGroupActivity = db.TPersonalPerformances.Where(_ => _.FGroupActivityId == Convert.ToInt32(fetchList[0].groupActivityID)).Where(_ => _.FDeleteBool == "1");

            List<string> oldCount = new List<string>();
            List<string> newCount = new List<string>();
            foreach (TPersonalPerformance oldGroupActivityItem in oldGroupActivity)
            {
                oldCount.Add(oldGroupActivityItem.FResidentId.ToString());
            }

            foreach (FetchList fetchListItem in fetchList)
            {
                newCount.Add(fetchListItem.ResidentId);
            }

            List<string> oldIntersectNew = oldCount.Intersect(newCount).ToList();       //先求一樣的
            List<string> oldExceptNew = oldCount.Except(newCount).ToList();         //再求舊的被刪掉的抓出來
            List<string> newExceptOld = newCount.Except(oldCount).ToList();         //再求新的要加的抓出來

            //===================================================
            if (oldIntersectNew.Count > 0)
            {
                foreach (string item in oldIntersectNew)
                {
                    FetchList formFetchList = fetchList.FirstOrDefault(_ => _.ResidentId == item);


                    TPersonalPerformance tpp = db.TPersonalPerformances.Where(_ => _.FGroupActivityId == Convert.ToInt32(formFetchList.groupActivityID)).FirstOrDefault(_ => _.FResidentId == Convert.ToInt32(formFetchList.ResidentId));

                    //tpp.FGroupActivityId = Convert.ToInt32(formFetchList.groupActivityID);  //為了快速update而留
                    //tpp.FResidentId = Convert.ToInt32(formFetchList.ResidentId);    //同上
                    //tpp.FDeleteBool = "1";
                    tpp.FEmotions = formFetchList.Emotions;
                    tpp.FParticipatePersistence = formFetchList.ParticipatePersistence;
                    tpp.FCooperate = formFetchList.Cooperate;
                    tpp.FHumanInteraction = formFetchList.HumanInteraction;
                    tpp.FAttention = formFetchList.AttentionRes;
                    tpp.FParticipatePerformance = formFetchList.ParticipatePerformance;
                    tpp.FDepiction = formFetchList.Depiction;

                    db.Update(tpp);
                }
            }
            //================================================
            if (oldExceptNew.Count > 0)
            {
                foreach (string item in oldExceptNew)
                {
                    TPersonalPerformance tpp = oldGroupActivity.FirstOrDefault(_ => _.FResidentId == Convert.ToInt32(item));
                    tpp.FDeleteBool = "0";
                    db.Update(tpp);
                }
            }

            if (newExceptOld.Count > 0)
            {
                foreach (string item in newExceptOld)
                {
                    FetchList formFetchList = fetchList.FirstOrDefault(_ => _.ResidentId == item);

                    TPersonalPerformance tpp = new TPersonalPerformance()
                    {
                        FGroupActivityId = Convert.ToInt32(formFetchList.groupActivityID),
                        FResidentId = Convert.ToInt32(formFetchList.ResidentId),
                        FDeleteBool = "1",
                        FEmotions = formFetchList.Emotions,
                        FParticipatePersistence = formFetchList.ParticipatePersistence,
                        FCooperate = formFetchList.Cooperate,
                        FHumanInteraction = formFetchList.HumanInteraction,
                        FAttention = formFetchList.AttentionRes,
                        FParticipatePerformance = formFetchList.ParticipatePerformance,
                        FDepiction = formFetchList.Depiction
                    };
                    db.Add(tpp);
                }
            }


            db.SaveChanges();
            return Json("");
        }


        public class FetchList  //應該設計直接跟modal一樣
        {
            public string groupActivityID { get; set; }
            public string ResidentId { get; set; }
            public string Emotions { get; set; }
            public string ParticipatePersistence { get; set; }
            public string Cooperate { get; set; }
            public string HumanInteraction { get; set; }
            public string AttentionRes { get; set; }
            public string ParticipatePerformance { get; set; }
            public string Depiction { get; set; }


        }


        [HttpPost]
        public async Task<IActionResult> ClassThemeAjax([FromBody] List<TGroupActivityClassTheme> gaClass)  //要填fid不然會null
        {
            if (gaClass.Count <= 0) { return Json("課程主題為空(後台檢測回覆)"); }

            var aa = HttpContext.Request.Headers;

            dbClassContext db = new dbClassContext();
            List<string> oldCount = new List<string>();
            List<string> newCount = new List<string>();
            var getFormDB = db.TGroupActivityClassThemes.Where(_ => _.FGroupActivityId == gaClass[0].FGroupActivityId).Where(_=>_.FDeleteBool == true);

            foreach (var item in getFormDB)
            {
                oldCount.Add(item.FClassThemeId.ToString());
            }
            foreach (var item in gaClass)
            {
              if ( newCount.Contains(item.FClassThemeId.ToString()))continue;
                newCount.Add(item.FClassThemeId.ToString());
            }

            List<string> oldIntersectNew = oldCount.Intersect(newCount).ToList();       //先求一樣的，這次可以不用管"一樣"的情況
            List<string> oldExceptNew = oldCount.Except(newCount).ToList();         //再求舊的被刪掉的抓出來
            List<string> newExceptOld = newCount.Except(oldCount).ToList();         //再求新的要加的抓出來

            if (oldExceptNew.Count > 0)
            {
                foreach (string item in oldExceptNew)
                {
                    TGroupActivityClassTheme tgact =  getFormDB.Where(_ => _.FClassThemeId == Convert.ToInt32(item)).FirstOrDefault();

                    tgact.FDeleteBool = false;
                    db.Update(tgact);
                }
            }
            if (newExceptOld.Count > 0) {
                foreach (string item in newExceptOld)
                {
                    TGroupActivityClassTheme tgact = new TGroupActivityClassTheme();

                    tgact.FDeleteBool = true;
                    tgact.FGroupActivityId =Convert.ToInt32( gaClass[0].FGroupActivityId);
                    tgact.FClassThemeId = Convert.ToInt32( item);
                    db.Add(tgact);
                }
            }

            db.SaveChanges();


            return Json("");
        }


        [HttpPost]
        public async Task<IActionResult> ScheduleDetailPanelAjax([FromBody] List<TScheduleDetail> tsd) {  //要填fid不然會null
            if(tsd.Count <= 0) { return Json("行程為空(後台檢測回覆)"); }     
            dbClassContext db = new dbClassContext();

            db.RemoveRange(db.TScheduleDetails.Where(_=>_.FGroupActivityId == tsd[0].FGroupActivityId));

            db.AddRange(tsd);
            db.SaveChanges();
            return Json("");
        }
    }
}
