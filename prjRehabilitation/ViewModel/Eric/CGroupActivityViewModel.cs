using prjRehabilitation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjRehabilitation.ViewModel
{
    public class CGroupActivityViewModel
    {
        private TGroupActivity _GroupActivity;
        public CGroupActivityViewModel()
        {

            _GroupActivity = new TGroupActivity();

            //TGroupActivityClassThemes = new HashSet<TGroupActivityClassTheme>();
            //TGroupActivityPicAndFiles = new HashSet<TGroupActivityPicAndFile>();
            //TPersonalPerformances = new HashSet<TPersonalPerformance>();
            //TScheduleDetails = new HashSet<TScheduleDetail>();
        }

       public  TGroupActivity groupActivity { 
            get { return _GroupActivity; } 
            set { _GroupActivity = value ; } 
        }

        public int? FId { get { return _GroupActivity.FId; } set {_GroupActivity.FId = value; } }
        public bool? FDeleteBool { get { return _GroupActivity.FDeleteBool; } set { _GroupActivity.FDeleteBool = value; } }
        [DisplayName("記錄編號ID")]
        public int FGroupActivityId { get { return _GroupActivity.FGroupActivityId; } set { _GroupActivity.FGroupActivityId = value; } }
        [DisplayName("團體計畫")]
        public string FEventType { get { return _GroupActivity.FEventType; } set { _GroupActivity.FEventType = value; } }
        [DisplayName("日期")]
        
        public string FDate { get { return _GroupActivity.FDate; } set {   _GroupActivity.FDate = value; } }
        [DisplayName("開始時間")]
        public string FStartTime { get { return _GroupActivity.FStartTime; } set { _GroupActivity.FStartTime = value; } }
        [DisplayName("結束時間")]
        public string FEndTime { get { return _GroupActivity.FEndTime; } set { _GroupActivity.FEndTime = value; } }
        [DisplayName("團體名稱")]
        public string FGroupName { get { return _GroupActivity.FGroupName; } set { _GroupActivity.FGroupName = value; } }
        [DisplayName("課程名稱")]
        public string FClassName { get { return _GroupActivity.FClassName; } set { _GroupActivity.FClassName = value; } }
        [DisplayName("團體目的")]
        public string FGoal { get { return _GroupActivity.FGoal; } set { _GroupActivity.FGoal = value; } }
        [DisplayName("地點")]
        public string FLocation { get { return _GroupActivity.FLocation; } set { _GroupActivity.FLocation = value; } }
        [DisplayName("團體人數")]
        public int FPeopleCount { get { return _GroupActivity.FPeopleCount; } set { _GroupActivity.FPeopleCount = value; } }
        [DisplayName("團體領導")]
        public string FLeader { get { return _GroupActivity.FLeader; } set { _GroupActivity.FLeader = value; } }
        [DisplayName("記錄人員")]
        public string FRecorder { get { return _GroupActivity.FRecorder; } set { _GroupActivity.FRecorder = value; } }
        [DisplayName("過程摘要")]
        public string? FProcess { get { return _GroupActivity.FProcess; } set { _GroupActivity.FProcess = value; } }
        [DisplayName("成果簡述")]
        public string? FAchievement { get { return _GroupActivity.FAchievement; } set { _GroupActivity.FAchievement = value; } }
        [DisplayName("填表人")]
        public string FFillFormStaff { get { return _GroupActivity.FFillFormStaff; } set { _GroupActivity.FFillFormStaff = value; } }
        [DisplayName("填表日期")]
        public string FFillFormDate { get { return _GroupActivity.FFillFormDate; } set { _GroupActivity.FFillFormDate = value; } }

        public virtual ICollection<TGroupActivityClassTheme> TGroupActivityClassThemes { get { return _GroupActivity.TGroupActivityClassThemes; } set { _GroupActivity.TGroupActivityClassThemes = value; } }
        public virtual ICollection<TGroupActivityPicAndFile> TGroupActivityPicAndFiles { get { return _GroupActivity.TGroupActivityPicAndFiles; } set { _GroupActivity.TGroupActivityPicAndFiles = value; } }
        public virtual ICollection<TPersonalPerformance> TPersonalPerformances { get { return _GroupActivity.TPersonalPerformances; } set { _GroupActivity.TPersonalPerformances = value; } }
        [DisplayName("大綱排程")]
        public virtual ICollection<TScheduleDetail> TScheduleDetails { get { return _GroupActivity.TScheduleDetails; } set { _GroupActivity.TScheduleDetails = value; } }

    }
}
