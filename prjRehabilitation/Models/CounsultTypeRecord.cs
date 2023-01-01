using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class CounsultTypeRecord
    {
        public int Fid { get; set; }
        public int TypeNameId { get; set; }
        public int FConsultId { get; set; }

        public virtual Consultation FConsult { get; set; } = null!;
        public virtual TypeName TypeName { get; set; } = null!;
    }
}
