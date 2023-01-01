using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class Employee
    {
        public int Fid { get; set; }
        public string? FRank { get; set; }
        public string? FName { get; set; }
        public string? FPassword { get; set; }
        public string? FAddress { get; set; }
    }
}
