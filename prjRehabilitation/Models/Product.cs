using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class Product
    {
        public int Fid { get; set; }
        public string? FName { get; set; }
        public int? FQty { get; set; }
        public decimal? FPrice { get; set; }
    }
}
