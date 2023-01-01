using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class MedList
    {
        public int IdMedicine { get; set; }
        public string? MedicineChineseName { get; set; }
        public string? MedicineInformation { get; set; }
    }
}
