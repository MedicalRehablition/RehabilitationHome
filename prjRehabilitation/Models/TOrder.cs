using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TOrder
    {
        public int Fid { get; set; }
        public string? FOrderId { get; set; }
        public string? FDate { get; set; }
        public string? FName { get; set; }
        public string? FEmail { get; set; }
        public string? FAddress { get; set; }
        public decimal? FTotalPrice { get; set; }
        public bool? FStatus { get; set; }
        public bool FShip { get; set; }
    }
}
