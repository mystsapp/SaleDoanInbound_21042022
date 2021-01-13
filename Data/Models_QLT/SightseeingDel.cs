using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class SightseeingDel
    {
        public decimal Id { get; set; }
        public string Sgtcode { get; set; }
        public int Stt { get; set; }
        public string Codedtq { get; set; }
        public string Serial { get; set; }
        public string Debit { get; set; }
        public int? Pax { get; set; }
        public decimal? PaxPrice { get; set; }
        public int? Childern { get; set; }
        public decimal? ChildernPrice { get; set; }
        public decimal? Amount { get; set; }
        public int? Vatin { get; set; }
        public int? Vatout { get; set; }
        public string Chinhanh { get; set; }
        public string Httt { get; set; }
        public string Logfile { get; set; }
        public decimal? Idtourprog { get; set; }
    }
}
