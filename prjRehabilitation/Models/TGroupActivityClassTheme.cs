using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TGroupActivityClassTheme
    {
        public int FId { get; set; }
        public bool? FDeleteBool { get; set; }
        public int FGroupActivityId { get; set; }
        public int FClassThemeId { get; set; }

        public virtual TypeName FClassTheme { get; set; } = null!;
        public virtual TGroupActivity FGroupActivity { get; set; } = null!;
    }
}
