using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class DiseaseDiagnosis
    {
        public int? Fid { get; set; }
        public int IdPatientDisease { get; set; }
        public string? IdDisease { get; set; }
        public string? DiseaseChineseName { get; set; }
        public string? Remark { get; set; }
        public string? Section { get; set; }
        public bool? Deleted { get; set; }

        public virtual PatientInfo? FidNavigation { get; set; }
    }
}
