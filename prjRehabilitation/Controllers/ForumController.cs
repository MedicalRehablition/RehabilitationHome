using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using prjRehabilitation.Models;
using prjRehabilitation.Models.Lin;
using prjRehabilitation.ViewModel.Lin;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace prjRehabilitation.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult LastedPost()
        {
            return RedirectToAction("PostDetail");
        }
        public IActionResult DeleteReply(int id)
        {
            (new CommentCRUD()).DeleteReply(id);
            return Json(new { outcome = "刪除成功"});
        }
        public IActionResult GetReplyList(int id)
        {
            return Json((new CommentCRUD()).GetPostComments(id));
        }
        public IActionResult PostDelete(int id)
        {
            (new ArticalCRUD()).PostDelete(id);

            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostEdit(VMNewPost vm)
        {
             (new ArticalCRUD()).PostEdit(vm);
            return RedirectToAction("List");
        }
        public IActionResult PostEdit(int id)
        {
            var data = (new ArticalCRUD()).GetTargetPostToEdit(id);
            return View(data);
        }
        public IActionResult NewPost()
        {
            return View();
        }
        public IActionResult getPrePost(int id)
        {
            var list = (new ArticalCRUD()).getPrePost(id);

            return Json(list);
        }
        public IActionResult getNextPost(int id)
        {
            var list = (new ArticalCRUD()).getNextPost(id);

            return Json(list);
        }
        public IActionResult SearchByTime()
        {
            var list = (new ArticalCRUD()).SearchByTime();
            return Json(list);
        }
        public IActionResult Search(string keyword)
        {
           var list= (new ArticalCRUD()).SearchByKeyword(keyword);
            return Json(list);
        }
        public IActionResult SearchByTag(string tag)
        {
            var list = (new ArticalCRUD()).SearchByTag(tag);
            return Json(list);
        }
        public IActionResult NewComment(TPostComment t)
        {
            (new CommentCRUD()).ArticalCreate(t);
            return Json("success");
        }
        public IActionResult GetHistoryComment(int id)
        {
            var list = (new CommentCRUD()).GetPostComments(id);
            return Json(list);
        }
        [HttpPost]
        public IActionResult NewPost(VMNewPost vm)
        {
            vm.FDate = DateTime.Now.ToString("d");
            (new ArticalCRUD()).PostCreate(vm);
            return RedirectToAction("OfficialPost");
        }

        public IActionResult PostDetail(int? id)
        {
            object data = null;
            if (id != null)
            {
                 data = (new ArticalCRUD()).GetTargetPost((int)id);
            }
            else
            {
                 data = (new ArticalCRUD()).GetTargetPost(null);
            }
                return View(data);
        }
        public IActionResult OfficialPost()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetHistoryPost()
        {
            var list = (new ArticalCRUD()).GetHistoryPost(1);
            return Json(list);
        }
        public IActionResult GetAllHistoryPost()
        {
            var list = (new ArticalCRUD()).GetHistoryPost(0);
            return Json(list);
        }
        public IActionResult PostNextPage(int page)
        {
            var list = (new ArticalCRUD()).GetHistoryPost(page);
            return Json(list);
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
