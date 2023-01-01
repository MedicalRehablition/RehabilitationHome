using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class PatientInfo
    {
        public PatientInfo()
        {
            Consultations = new HashSet<Consultation>();
            DiseaseDiagnoses = new HashSet<DiseaseDiagnosis>();
            EmergenceCallers = new HashSet<EmergenceCaller>();
            PatientGetMedDates = new HashSet<PatientGetMedDate>();
            TPersonalPerformances = new HashSet<TPersonalPerformance>();
            功能評估s = new HashSet<功能評估>();
        }

        public int Fid { get; set; }
        public string? FName { get; set; }
        public string? FSex { get; set; }
        public string? FCheckin { get; set; }
        public string? FIdnum { get; set; }
        public string? FBednum { get; set; }
        public string? FBirthday { get; set; }
        public string? FHomeNum { get; set; }
        public string? FPhone { get; set; }
        public string? FEdu { get; set; }
        public string? FMarried { get; set; }
        public string? FAddress { get; set; }
        public string? FHos { get; set; }
        public string? FIdy { get; set; }
        public string? FGrant { get; set; }
        public byte[]? FPicture { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual ICollection<DiseaseDiagnosis> DiseaseDiagnoses { get; set; }
        public virtual ICollection<EmergenceCaller> EmergenceCallers { get; set; }
        public virtual ICollection<PatientGetMedDate> PatientGetMedDates { get; set; }
        public virtual ICollection<TPersonalPerformance> TPersonalPerformances { get; set; }
        public virtual ICollection<功能評估> 功能評估s { get; set; }
    }
}
