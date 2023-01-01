using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class 功能評估個表
    {
        public int Id評估表 { get; set; }
        public int F功能評估Id { get; set; }
        public string? F評估項目 { get; set; }
        public string? F現狀評估 { get; set; }
        public string? F問題 { get; set; }
        public string? F復健目標 { get; set; }
        public string? F復健計畫 { get; set; }
        public bool? Deleted { get; set; }

        public virtual 功能評估 F功能評估 { get; set; } = null!;
    }
}
