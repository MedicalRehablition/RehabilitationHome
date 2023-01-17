using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TForumArtical
    {
        public int FArticalId { get; set; }
        public int FUserId { get; set; }
        public int FGood { get; set; }
        public int FBad { get; set; }
        public string? FContent { get; set; }
        public string? FTitle { get; set; }
        public string? FTime { get; set; }
        public int? FBelongto { get; set; }
        public bool? FStatus { get; set; }
        public bool FisAnonymous { get; set; }
    }
}
