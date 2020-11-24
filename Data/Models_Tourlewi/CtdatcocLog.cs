using System;
using System.Collections.Generic;

namespace Data.Models_Tourlewi
{
    public partial class CtdatcocLog
    {
        public decimal Id { get; set; }
        public decimal Iddatcoc { get; set; }
        public decimal Idvetour { get; set; }
        public string Httt { get; set; }
        public string Chungtugoc { get; set; }
        public decimal? Sotien { get; set; }
        public string Ghichu { get; set; }
        public DateTime? Capnhat { get; set; }
        public string Nguoicapnhat { get; set; }
        public string Type { get; set; }
        public string Computer { get; set; }
    }
}
