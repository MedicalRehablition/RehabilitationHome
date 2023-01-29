using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class Customer
    {
        public int Fid { get; set; }
        public string? FEmail { get; set; }
        public string? FPassword { get; set; }
        public string? FPhone { get; set; }
        public string? FName { get; set; }
        public string? FAddress { get; set; }
    }
}
