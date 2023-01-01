using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class Consultation
    {
        public Consultation()
        {
            CounsultTypeRecords = new HashSet<CounsultTypeRecord>();
        }

        public int FConsultId { get; set; }
        public int? PatinetId { get; set; }
        public string? Date { get; set; }
        public string? Summary { get; set; }
        public string? Assessment { get; set; }
        public string? Result { get; set; }

        public virtual PatientInfo? Patinet { get; set; }
        public virtual ICollection<CounsultTypeRecord> CounsultTypeRecords { get; set; }
    }
}
