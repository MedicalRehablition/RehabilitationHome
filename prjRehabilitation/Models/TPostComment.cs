using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TPostComment
    {
        public int Fid { get; set; }
        public int? FPostId { get; set; }
        public string? FName { get; set; }
        public string? FEmail { get; set; }
        public string? FContent { get; set; }
        public string? FDate { get; set; }
        public bool? FStatus { get; set; }
    }
}
