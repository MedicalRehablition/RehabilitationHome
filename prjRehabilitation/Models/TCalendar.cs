using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TCalendar
    {
        public int FId { get; set; }
        public bool? FDeleteBool { get; set; }
        public string FDate { get; set; } = null!;
        public string FEventName { get; set; } = null!;
        public string? FClassName { get; set; }
        public string? FDateColor { get; set; }
        public string FTitle { get; set; } = null!;
        public string FContent { get; set; } = null!;
        public string FRecorder { get; set; } = null!;
        public string FRecorderDate { get; set; } = null!;
    }
}
