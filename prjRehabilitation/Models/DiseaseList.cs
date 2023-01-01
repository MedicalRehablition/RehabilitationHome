using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class DiseaseList
    {
        public string IdDisease { get; set; } = null!;
        public double? F2 { get; set; }
        public string? DiseaseEnglishName { get; set; }
        public string? DiseaseChineseName { get; set; }
        public string? F5 { get; set; }
    }
}
