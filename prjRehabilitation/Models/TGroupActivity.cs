using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TGroupActivity
    {
        public TGroupActivity()
        {
            TGroupActivityClassThemes = new HashSet<TGroupActivityClassTheme>();
            TGroupActivityPicAndFiles = new HashSet<TGroupActivityPicAndFile>();
            TPersonalPerformances = new HashSet<TPersonalPerformance>();
            TScheduleDetails = new HashSet<TScheduleDetail>();
        }

        public int? FId { get; set; }
        public bool? FDeleteBool { get; set; }
        public int FGroupActivityId { get; set; }
        public string FEventType { get; set; } = null!;
        public string FDate { get; set; } = null!;
        public string FStartTime { get; set; } = null!;
        public string FEndTime { get; set; } = null!;
        public string FGroupName { get; set; } = null!;
        public string FClassName { get; set; } = null!;
        public string FGoal { get; set; } = null!;
        public string FLocation { get; set; } = null!;
        public int FPeopleCount { get; set; }
        public string FLeader { get; set; } = null!;
        public string FRecorder { get; set; } = null!;
        public string? FProcess { get; set; }
        public string? FAchievement { get; set; }
        public string FFillFormStaff { get; set; } = null!;
        public string FFillFormDate { get; set; } = null!;

        public virtual ICollection<TGroupActivityClassTheme> TGroupActivityClassThemes { get; set; }
        public virtual ICollection<TGroupActivityPicAndFile> TGroupActivityPicAndFiles { get; set; }
        public virtual ICollection<TPersonalPerformance> TPersonalPerformances { get; set; }
        public virtual ICollection<TScheduleDetail> TScheduleDetails { get; set; }
    }
}
