using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class Admin
    {
        public int Fid { get; set; }
        public string? FName { get; set; }
        public string? FEmail { get; set; }
        public string? FPassword { get; set; }
        public string? FRank { get; set; }
        public string? Fphoto { get; set; }
        public string? FSex { get; set; }
        public string? FBirth { get; set; }
        public string? FQrcode { get; set; }
    }
}
