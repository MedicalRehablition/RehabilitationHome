using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class PatientGetMedDate
    {
        public PatientGetMedDate()
        {
            PatientMedInfos = new HashSet<PatientMedInfo>();
        }

        public int IdGetMedicineDate { get; set; }
        public int Fid { get; set; }
        public string DateGetMedicine { get; set; } = null!;
        public bool? Deleted { get; set; }

        public virtual PatientInfo FidNavigation { get; set; } = null!;
        public virtual ICollection<PatientMedInfo> PatientMedInfos { get; set; }
    }
}
