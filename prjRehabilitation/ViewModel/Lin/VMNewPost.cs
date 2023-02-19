using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel.Lin
{
    public class VMNewPost
    {
        public TOfficialPost fofficialPost{ get; set; }
        public VMNewPost()
        {
            fofficialPost = new TOfficialPost();
        }
        public int? FPostId
        {
            get { return fofficialPost.FPostId; }
            set { fofficialPost.FPostId = (int)value; }
        }
        public string? FDate
        {
            get { return fofficialPost.FDate; }
            set { fofficialPost.FDate = value; }
        }
        public string? FTitle
        {
            get { return fofficialPost.FTitle; }
            set { fofficialPost.FTitle = value; }
        }
        public string? FMain
        {
            get { return fofficialPost.FMain; }
            set { fofficialPost.FMain = value; }
        }
        public string? FContent
        {
            get { return fofficialPost.FContent; }
            set { fofficialPost.FContent = value; }
        }
        public string? FTag
        {
            get { return fofficialPost.FTag; }
            set { fofficialPost.FTag = value; }
        }
        public int? FComment
        {get; set; }
        public bool type_QA { get; set; }
        public bool type_技術 { get; set; }
        public bool type_復健 { get; set; }
        public bool type_評估 { get; set; }
        public bool type_用藥 { get; set; }
        public bool type_介紹 { get; set; }
        public bool type_活動 { get; set; }
        public bool type_公告 { get; set; }
    }
}
