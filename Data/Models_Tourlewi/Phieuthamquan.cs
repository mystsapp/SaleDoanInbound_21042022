using System;
using System.Collections.Generic;

namespace Data.Models_Tourlewi
{
    public partial class Phieuthamquan
    {
        public decimal Id { get; set; }
        public DateTime? Ngay { get; set; }
        public string Matq { get; set; }
        public string Sophieu { get; set; }
        public string Sgtcode { get; set; }
        public int Sokhach { get; set; }
        public int Khachnl { get; set; }
        public int Khachte { get; set; }
        public int Sokhachdv { get; set; }
        public int Dichvunl { get; set; }
        public int Dichvute { get; set; }
        public string Huongdan { get; set; }
        public DateTime Ngaytao { get; set; }
        public string Nguoitao { get; set; }
        public string Nguoisua { get; set; }
        public DateTime? Ngaysua { get; set; }
        public bool Huy { get; set; }
    }
}
