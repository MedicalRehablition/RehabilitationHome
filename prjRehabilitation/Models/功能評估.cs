using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class 功能評估
    {
        public 功能評估()
        {
            功能評估個表s = new HashSet<功能評估個表>();
        }

        public int F功能評估Id { get; set; }
        public int Fid { get; set; }
        public string? F日期 { get; set; }
        public decimal? F身高 { get; set; }
        public decimal? F體重 { get; set; }
        public bool? Deleted { get; set; }

        public virtual PatientInfo FidNavigation { get; set; } = null!;
        public virtual ICollection<功能評估個表> 功能評估個表s { get; set; }
    }
}
