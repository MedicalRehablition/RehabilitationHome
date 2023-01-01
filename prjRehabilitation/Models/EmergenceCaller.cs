using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class EmergenceCaller
    {
        public int Fid { get; set; }
        public int? FPatientId { get; set; }
        public string? FEmergencyName { get; set; }
        public string? Frelation { get; set; }
        public string? FPhone { get; set; }

        public virtual PatientInfo? FPatient { get; set; }
    }
}
