using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel.Lin
{
    public class VMNewArtical
    {
        public VMNewArtical()
        {
            _TForumArtical = new TForumArtical();
        }
        public int fReplyCount { get; set; }
        public TForumArtical _TForumArtical { get; set; }
       public VMUser user { get; set; }
        public int FGood { get; set; }
        public int FBad { get; set; }
        public int? FArticalId
        {
            get { return _TForumArtical.FArticalId; }
            set { _TForumArtical.FArticalId = (int)value; }
        }
        public int? FUserId
        {
            get { return _TForumArtical.FUserId; }
            set { _TForumArtical.FUserId =(int)value; }
        }
        public string? FContent
        {
            get { return _TForumArtical.FContent; }
            set { _TForumArtical.FContent = value; }
        }
        public string? FTitle
        {
            get { return _TForumArtical.FTitle; }
            set { _TForumArtical.FTitle = value; }
        }
        public string? FTime
        {
            get { return _TForumArtical.FTime; }
            set { _TForumArtical.FTime = value; }
        }
        public int? FBelongto
        {
            get { return _TForumArtical.FBelongto; }
            set { _TForumArtical.FBelongto = value; }
        }
        public bool isAnonymous { get; set; }
    }
}
