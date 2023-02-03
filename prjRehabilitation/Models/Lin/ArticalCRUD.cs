using Microsoft.AspNetCore.Identity;
using prjRehabilitation.ViewModel.Lin;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace prjRehabilitation.Models.Lin
{
    public class ArticalCRUD
    {
        public void ArticalCreate(TForumArtical artical)
        {
            dbClassContext db = new dbClassContext();
            db.TForumArticals.Add(artical);
            db.SaveChanges();
        }

        public void ArticalEdit(TForumArtical artical)
        {
            dbClassContext db = new dbClassContext();
            var oldA = db.TForumArticals.FirstOrDefault(x=>x.FArticalId==artical.FArticalId);
            if (oldA != null)
            {
                oldA.FTitle = artical.FTitle;
                oldA.FTime = artical.FTime;
                oldA.FTitle = artical.FTitle;
                oldA.FContent = artical.FContent;
            }
            db.SaveChanges();
        }
        public void ArticalDelete(int id)
        {
            dbClassContext db = new dbClassContext();
            var oldA = db.TForumArticals.FirstOrDefault(x => x.FArticalId == id);
            if (oldA != null)
            {
                oldA.FStatus = false;
                db.SaveChanges();   
            }
        }
        public void ArticalRecover(int id)
        {
            dbClassContext db = new dbClassContext();
            var oldA = db.TForumArticals.FirstOrDefault(x => x.FArticalId == id);
            if (oldA != null)
            {
                oldA.FStatus = true;
                db.SaveChanges();
            }
        }
        public void ArticalAddGood(int id)
        {
            dbClassContext db = new dbClassContext();
            var oldA = db.TForumArticals.FirstOrDefault(x => x.FArticalId == id);
            if (oldA != null)
            {
                oldA.FGood += 1;
                db.SaveChanges();
            }
        }
        public void ArticalAddBad(int id)
        {
            dbClassContext db = new dbClassContext();
            var oldA = db.TForumArticals.FirstOrDefault(x => x.FArticalId == id);
            if (oldA != null)
            {
                oldA.FBad += 1;
                db.SaveChanges();
            }
        }

        public  object GetAllArtical()
        {
            var db = new dbClassContext();
            var data = db.TForumArticals.Where(x => x.FStatus != false&&x.FBelongto==null);
            var list = new List<VMNewArtical>();
            var userdata = new VMUser();
            foreach(var c in data.ToList())
            {
                if (c.FUserId != 0)
                {
                    var a = db.PatientInfos.FirstOrDefault(x => x.Fid == c.FUserId);
                    userdata.FPhoto = a.FPhotoFile;
                    userdata.FName = a.FName;
                }
                int num = db.TForumArticals.Where(x => x.FBelongto == c.FArticalId).Count();
                list.Add(new VMNewArtical
                {
                    fReplyCount = num,
                    _TForumArtical = c,
                    user= userdata
                });
            }
            return list;
        }

        public object GetTargetReply(int id)
        {
            dbClassContext db = new dbClassContext();
            var list = db.TForumArticals.Where(x => x.FBelongto == id && x.FStatus != false);
            return list;
        }

        public void PostCreate(VMNewPost vm)
        {
            dbClassContext db = new dbClassContext();
            var post = new TOfficialPost();
            post = vm.fofficialPost;
            post = processType(post, vm);

            db.TOfficialPosts.Add(post);
            db.SaveChanges();
        }
        public void PostEdit(VMNewPost vm)
        {
            dbClassContext db = new dbClassContext();
           
            var post = db.TOfficialPosts.Where(x=>x.FPostId==vm.FPostId).FirstOrDefault();
            post.FTitle = vm.FTitle;
            post.FMain = vm.FMain;
            post.FContent = vm.FContent;

            post.FTag = "";
            if (vm.type_用藥 != false) post.FTag += "用藥";
            if (vm.type_評估 != false) post.FTag += "評估";
            if (vm.type_活動 != false) post.FTag += "活動";
            if (vm.type_復健 != false) post.FTag += "復健";
            if (vm.type_介紹 != false) post.FTag += "介紹";
            if (vm.type_QA != false) post.FTag += "QA";
            if (vm.type_技術 != false) post.FTag += "技術";
            if (vm.type_公告 != false) post.FTag += "公告";

            db.SaveChanges();
        }
        public object GetHistoryPost(int page)
        {
            dbClassContext db = new dbClassContext();
            var list = new List<VMNewPost>();
            IEnumerable<TOfficialPost> q;
            if (page==0)
            {
                q = db.TOfficialPosts.Where(x => x.FStatus != false).OrderByDescending(x => x.FPostId);
            }
            else
            {
                page -= 1;
                //降冪排列，優先顯示最新的文章
                q = db.TOfficialPosts.Where(x => x.FStatus != false).OrderByDescending(x => x.FPostId).Skip(page * 5).Take(5);
            }

            foreach (var c in q.ToList())
            {
                var post = new VMNewPost();
                post.fofficialPost = c;
                string newtag = "";
                if (c.FTag != null)
                {
                    if (c.FTag.Contains("用藥")) newtag += "用藥 ";
                    if (c.FTag.Contains("評估")) newtag += "評估 ";
                    if (c.FTag.Contains("活動")) newtag += "活動 ";
                    if (c.FTag.Contains("復健")) newtag += "復健 ";
                    if (c.FTag.Contains("介紹")) newtag += "介紹 ";
                    if (c.FTag.Contains("QA")) newtag += "QA ";
                    if (c.FTag.Contains("技術")) newtag += "技術 ";
                    if (c.FTag.Contains("公告")) newtag += "公告 ";
                    c.FTag = newtag.Remove(newtag.Length - 1);

                }

                list.Add(post);
            }
            return list;
        }
        public List<VMNewPost> SearchByKeyword(string keyword)
        {
            dbClassContext db = new dbClassContext();
            var list = new List<VMNewPost>();
            IEnumerable<TOfficialPost> q;
            q = db.TOfficialPosts.Where(x => x.FStatus !=false).OrderByDescending(x => x.FDate);

            foreach (var c in q.ToList())
            {
                if (!c.FMain.Contains(keyword)) 
                { 
                    if(!c.FTitle.Contains(keyword)) continue;
                }
                var post = new VMNewPost();
                post.fofficialPost = c;
                string newtag = "";
                if (c.FTag != null)
                {
                    if (c.FTag.Contains("用藥")) newtag += "用藥 ";
                    if (c.FTag.Contains("評估")) newtag += "評估 ";
                    if (c.FTag.Contains("活動")) newtag += "活動 ";
                    if (c.FTag.Contains("復健")) newtag += "復健 ";
                    if (c.FTag.Contains("介紹")) newtag += "介紹 ";
                    if (c.FTag.Contains("QA")) newtag += "QA ";
                    if (c.FTag.Contains("技術")) newtag += "技術 ";
                    if (c.FTag.Contains("公告")) newtag += "公告 ";
                    c.FTag = newtag.Remove(newtag.Length - 1);

                }

                list.Add(post);
            }
            return list;
        }
        public object GetTargetPost(int? id)
        {
            dbClassContext db = new dbClassContext();
            TOfficialPost c;
            if(id.HasValue)
                c = db.TOfficialPosts.First(x => x.FStatus != false && x.FPostId == id);
            else
                c = db.TOfficialPosts.Where(x => x.FStatus != false).OrderByDescending(x=>x.FPostId).First();
            var post = new VMNewPost();
                post.fofficialPost = c;
                string newtag = "";
                if (c.FTag != null)
                {
                    if (c.FTag.Contains("用藥")) newtag += "用藥 ";
                    if (c.FTag.Contains("評估")) newtag += "評估 ";
                    if (c.FTag.Contains("活動")) newtag += "活動 ";
                    if (c.FTag.Contains("復健")) newtag += "復健 ";
                    if (c.FTag.Contains("介紹")) newtag += "介紹 ";
                    if (c.FTag.Contains("QA")) newtag += "QA ";
                    if (c.FTag.Contains("技術")) newtag += "技術 ";
                    if (c.FTag.Contains("公告")) newtag += "公告 ";

                    post.FTag = newtag.Remove(newtag.Length - 1);
                }

            return post;
        }
        public object GetTargetPostToEdit(int id)
        {
            dbClassContext db = new dbClassContext();

            var c = db.TOfficialPosts.First(x => x.FStatus != false && x.FPostId == id);

            var post = new VMNewPost();
            post.fofficialPost = c;
            if (c.FTag != null)
            {
                if (c.FTag.Contains("用藥")) post.type_用藥 =true;
                if (c.FTag.Contains("評估")) post.type_評估 = true;
                if (c.FTag.Contains("活動")) post.type_活動 = true;
                if (c.FTag.Contains("復健")) post.type_復健 = true;
                if (c.FTag.Contains("介紹")) post.type_介紹 = true;
                if (c.FTag.Contains("QA")) post.type_QA = true;
                if (c.FTag.Contains("技術")) post.type_技術 = true;
                if (c.FTag.Contains("公告")) post.type_公告 = true;

            }

            return post;
        }
        public  object SearchByTag(string tag)
        {
            dbClassContext db = new dbClassContext();
            var list = new List<VMNewPost>();
            IEnumerable<TOfficialPost> q;
            q = db.TOfficialPosts.Where(x => x.FStatus != false).OrderByDescending(x => x.FDate);

            foreach (var c in q.ToList())
            {
                var post = new VMNewPost();
                post.fofficialPost = c;
                string newtag = "";
                if (c.FTag != null)
                {
                    if (!c.FTag.Contains(tag)) continue;  
                    if (c.FTag.Contains("用藥")) newtag += "用藥 ";
                    if (c.FTag.Contains("評估")) newtag += "評估 ";
                    if (c.FTag.Contains("活動")) newtag += "活動 ";
                    if (c.FTag.Contains("復健")) newtag += "復健 ";
                    if (c.FTag.Contains("介紹")) newtag += "介紹 ";
                    if (c.FTag.Contains("QA")) newtag += "QA ";
                    if (c.FTag.Contains("技術")) newtag += "技術 ";
                    if (c.FTag.Contains("公告")) newtag += "公告 ";
                    c.FTag = newtag.Remove(newtag.Length - 1);

                }
                list.Add(post);
            }
            return list;
        }

        public object SearchByTime()
        {
            dbClassContext db = new dbClassContext();
            var list = new List<VMNewPost>();
            IEnumerable<TOfficialPost> q;
            q = db.TOfficialPosts.Where(x => x.FStatus != false).OrderByDescending(x => x.FPostId).Take(5);

            foreach (var c in q.ToList())
            {
                var post = new VMNewPost();
                post.fofficialPost = c;
                string newtag = "";
                list.Add(post);
            }
            return list;
        }

        public object getPrePost(int id)
        {
            dbClassContext db = new dbClassContext();
            IQueryable<TOfficialPost> q=null;
            TOfficialPost? a= null;
            try
            {
                a = db.TOfficialPosts.Where(x => x.FStatus != false && x.FPostId < id).OrderByDescending(x => x.FPostId).First();
            }
            catch
            {
                a = db.TOfficialPosts.OrderByDescending(x => x.FPostId).First();
            }

            return a;
        }
        public object getNextPost(int id)
        {
            dbClassContext db = new dbClassContext();
            TOfficialPost q = null;

            try
            {
                q = db.TOfficialPosts.OrderBy(x => x.FPostId).First();

            }
            catch
            {
            }
             return q;
    
        }
        private TOfficialPost processType(TOfficialPost post, VMNewPost vm)
        {
            if (vm.type_用藥 != false) post.FTag += "用藥";
            if (vm.type_評估 != false) post.FTag += "評估";
            if (vm.type_活動 != false) post.FTag += "活動";
            if (vm.type_復健 != false) post.FTag += "復健";
            if (vm.type_介紹 != false) post.FTag += "介紹";
            if (vm.type_QA != false) post.FTag += "QA";
            if (vm.type_技術 != false) post.FTag += "技術";
            if (vm.type_公告 != false) post.FTag += "公告";
            return post;
        }

        public void PostDelete(int id)
        {
            dbClassContext db = new dbClassContext();
            try
            {
                var a = db.TOfficialPosts.Where(x => x.FPostId == id).First();
                a.FStatus = false;
            }
            catch { }
            db.SaveChanges();
        }
    }
}
