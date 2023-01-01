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
        }

        public int Fid { get; set; }
        public int? TypeNameId { get; set; }
        public string? TypeName1 { get; set; }

        public virtual ICollection<CounsultTypeRecord> CounsultTypeRecords { get; set; }
        public virtual ICollection<TGroupActivityClassTheme> TGroupActivityClassThemes { get; set; }
    }
}
