using Microsoft.AspNetCore.Identity;
using prjRehabilitation.ViewModel.Lin;

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
    }
}
