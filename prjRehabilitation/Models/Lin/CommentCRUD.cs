using prjRehabilitation.ViewModel.Lin;

namespace prjRehabilitation.Models.Lin
{
    public class CommentCRUD
    {
        public void ArticalCreate(TPostComment comment)
        {
            dbClassContext db = new dbClassContext();
            comment.FDate = DateTime.Now.ToString("G");
            db.TPostComments.Add(comment);
            db.SaveChanges();
        }
       public List<TPostComment> GetPostComments(int id)
        {
            var db = new dbClassContext();
            var data = db.TPostComments.Where(x=>x.FPostId==id&&x.FStatus!=false);

            return data.ToList();
        }


    }
}
