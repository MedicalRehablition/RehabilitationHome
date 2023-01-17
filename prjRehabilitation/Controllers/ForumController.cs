using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
using prjRehabilitation.ViewModel.Lin;

namespace prjRehabilitation.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetHistoryReply(int id)
        {
            var list = (new ArticalCRUD()).GetTargetReply(id);
            return Json(list);
        }
        public IActionResult AddReply(string content, int userId, int articalId)
        {

            var tool = new ArticalCRUD();
            var db = new dbClassContext();
            tool.ArticalCreate(new TForumArtical
            {
                FContent = content,

                FTime = DateTime.Now.ToString(),
                FUserId = userId,
                FGood = 0,
                FBad = 0,
                FBelongto = articalId
            });
            return new EmptyResult();
        }
        public IActionResult GetHistoryArtical()
        {
            var list = (new ArticalCRUD()).GetAllArtical();
            return Json(list);
        }
        public IActionResult CreateArtical(VMNewArtical artical)
        {
            var tool = new ArticalCRUD();
            tool.ArticalCreate(new TForumArtical
            {
                FContent = artical.FContent,
                FTitle = artical.FTitle,
                FTime = DateTime.Now.ToString(),
                FUserId = (int)artical.FUserId,
                FGood = 0,
                FBad = 0,
                FisAnonymous = artical.isAnonymous
            });
            return RedirectToAction("Index");
        }
    }
}
