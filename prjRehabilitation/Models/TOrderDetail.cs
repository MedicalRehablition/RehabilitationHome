using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TOrderDetail
    {
        public int Fid { get; set; }
        public string? FOrderId { get; set; }
        public decimal? FPrice { get; set; }
        public int? FQty { get; set; }
        public string? FProductName { get; set; }
    }
}
