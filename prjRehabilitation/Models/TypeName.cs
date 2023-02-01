using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TypeName
    {
        public TypeName()
        {
            CounsultTypeRecords = new HashSet<CounsultTypeRecord>();
            TGroupActivityClassThemes = new HashSet<TGroupActivityClassTheme>();
            功能評估個表s = new HashSet<功能評估個表>();
        }

        public int Fid { get; set; }
        public int? TypeNameId { get; set; }
        public string? TypeName1 { get; set; }

        public virtual ICollection<CounsultTypeRecord> CounsultTypeRecords { get; set; }
        public virtual ICollection<TGroupActivityClassTheme> TGroupActivityClassThemes { get; set; }
        public virtual ICollection<功能評估個表> 功能評估個表s { get; set; }
    }
}
