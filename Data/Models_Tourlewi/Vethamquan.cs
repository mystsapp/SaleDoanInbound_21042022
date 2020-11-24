using System;
using System.Collections.Generic;

namespace Data.Models_Tourlewi
{
    public partial class Vethamquan
    {
        public decimal Id { get; set; }
        public string Matq { get; set; }
        public string Diemtq { get; set; }
        public string Sgtcode { get; set; }
        public string Sophieu { get; set; }
        public DateTime? Ngay { get; set; }
        public int? Sokhach { get; set; }
        public int? Khachnl { get; set; }
        public int? Khachte { get; set; }
        public int? Sokhachdv { get; set; }
        public int? Dichvunl { get; set; }
        public int? Dichvute { get; set; }
        public string Huongdan { get; set; }
    }
}
