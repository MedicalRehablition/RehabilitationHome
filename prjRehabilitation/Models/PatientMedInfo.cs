using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class PatientMedInfo
    {
        public int IdPatientMedicineInfo { get; set; }
        public int? IdGetMedicineDate { get; set; }
        public string? MedicineChineseName { get; set; }
        public string? MedicineUsage { get; set; }
        public string? MedicineTakeTime { get; set; }
        public string? MedicineDosage { get; set; }
        public string? MedicineAddinfo { get; set; }
        public bool? Deleted { get; set; }

        public virtual PatientGetMedDate? IdGetMedicineDateNavigation { get; set; }
    }
}
