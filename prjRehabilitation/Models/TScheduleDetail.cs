using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TScheduleDetail
    {
        public int FId { get; set; }
        public bool? FDeleteBool { get; set; }
        public int FGroupActivityId { get; set; }
        public string FStartTime { get; set; } = null!;
        public string FEndTime { get; set; } = null!;
        public string? FDepiction { get; set; }

        public virtual TGroupActivity FGroupActivity { get; set; } = null!;
    }
}
