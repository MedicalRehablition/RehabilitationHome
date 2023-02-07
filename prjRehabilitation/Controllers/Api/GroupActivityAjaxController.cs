using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models;
using prjRehabilitation.ViewModel.Eric;

using System.Text.Json;

namespace prjRehabilitation.Controllers.Api
{
    public class GroupActivityAjaxController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getResident()
        {

            dbClassContext db = new dbClassContext();

            List<Consultation> getLastDate = (from p in db.Consultations
                                              group p by p.PatinetId into grp
                                              select grp.OrderByDescending(g => g.Date).First()).ToList();

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User)))
            {

                string json = HttpContext.Session.GetString(CDictionary.SK_CUSTOMER_User);
                Customer customer = null;

                if (!string.IsNullOrEmpty(json))
                {
                    customer = JsonSerializer.Deserialize<Customer>(json);
                }

                if (db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == customer.Fid) != null) //這邊是為了防止有人註冊munber但沒有綁定住民會出錯
                {
                    PatientInfo getResidentID = db.PatientInfos.FirstOrDefault(_ => _.FCustomerid == customer.Fid);

             
                   getLastDate =  getLastDate.Where(_ => _.PatinetId == getResidentID.Fid).ToList();
                    Dictionary<string, string> _ = new Dictionary<string, string>  
                    {
                        { "fid", getResidentID.Fid.ToString() },
                        { "fName", getResidentID.FName },
                        { "date", getLastDate.FirstOrDefault().Date }  
                    };
                    return Json(new List<Dictionary<string, string>>() { _ });
                }
                else
                {
                    Dictionary<string, string> __ = new Dictionary<string, string>   //當這人沒註冊相關住民時就生一個假的給前端json去拆不會出錯而已
                    {
                        { "fid", "fidNoResident" },
                        { "fName", "並未填相關住民" },
                        { "date", DateTime.Today.AddDays(-28).ToString("yyyy-MM-dd") }  //前面會幫我加28所以扣回來才會是今天。
                    };
                    return Json(new List<Dictionary<string, string>>() { __ }) ;
                }



            }

            //var getResult = db.PatientInfos.Where(_ => _.Status == true).Join(getLastDate, _ => _.Fid, _ => _.PatinetId, (a, b) => new { a.Fid, a.FName, b.Date }).ToList();

            var getResult = getLastDate.Join(db.PatientInfos.Where(_ => _.Status == true), _ => _.PatinetId, _ => _.Fid, (b, a) => new { a.Fid, a.FName, b.Date });
            return Json(getResult);
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

    }
}
