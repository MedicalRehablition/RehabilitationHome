using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class EmployeeDuty
    {
        public int Fid { get; set; }
        public int Employeeid { get; set; }
        public string? Onduty { get; set; }
        public string? Offduty { get; set; }
    }
}
