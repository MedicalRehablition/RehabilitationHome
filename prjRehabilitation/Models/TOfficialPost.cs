using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TOfficialPost
    {
        public int FPostId { get; set; }
        public string? FDate { get; set; }
        public string? FTitle { get; set; }
        public string? FMain { get; set; }
        public string? FContent { get; set; }
        public string? FTag { get; set; }
        public bool? FStatus { get; set; }
    }
}
