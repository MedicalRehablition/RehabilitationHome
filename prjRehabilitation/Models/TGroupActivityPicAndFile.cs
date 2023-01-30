using System;
using System.Collections.Generic;

namespace prjRehabilitation.Models
{
    public partial class TGroupActivityPicAndFile
    {
        public int FId { get; set; }
        public bool? FDeleteBool { get; set; }
        public int FGroupActivityId { get; set; }
        public string? FPicture1Path { get; set; }
        public string? FPicture2Path { get; set; }
        public string? FPicture3Path { get; set; }
        public string? FPicture4Path { get; set; }
        public string? FFile1Path { get; set; }
        public byte[]? FPicture1 { get; set; }
        public byte[]? FPicture2 { get; set; }
        public byte[]? FPicture3 { get; set; }
        public byte[]? FPicture4 { get; set; }
        public byte[]? FFile1 { get; set; }

        public virtual TGroupActivity FGroupActivity { get; set; } = null!;
    }
}
