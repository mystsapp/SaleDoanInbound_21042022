using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class Dmdiemtq
    {
        public Dmdiemtq()
        {
            Sightseeing = new HashSet<Sightseeing>();
            SightseeingTemp = new HashSet<SightseeingTemp>();
        }

        public string Code { get; set; }
        public string Diemtq { get; set; }
        public string Tinhtp { get; set; }
        public string Thanhpho { get; set; }
        public decimal? Giave { get; set; }
        public decimal? Giuxe { get; set; }
        public string Congno { get; set; }
        public int? Vatvao { get; set; }
        public int? Vatra { get; set; }
        public decimal? Tilelai { get; set; }
        public string Logfile { get; set; }

        public virtual ICollection<Sightseeing> Sightseeing { get; set; }
        public virtual ICollection<SightseeingTemp> SightseeingTemp { get; set; }
    }
}
