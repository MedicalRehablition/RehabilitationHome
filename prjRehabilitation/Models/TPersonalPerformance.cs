using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TPersonalPerformance
    {
        public int FId { get; set; }
        public string? FDeleteBool { get; set; }
        public int FGroupActivityId { get; set; }
        public int FResidentId { get; set; }
        public string FEmotions { get; set; } = null!;
        public string FParticipatePersistence { get; set; } = null!;
        public string FCooperate { get; set; } = null!;
        public string FHumanInteraction { get; set; } = null!;
        public string FAttention { get; set; } = null!;
        public string FParticipatePerformance { get; set; } = null!;
        public string? FDepiction { get; set; }

        public virtual TGroupActivity FGroupActivity { get; set; } = null!;
        public virtual PatientInfo FResident { get; set; } = null!;
    }
}
