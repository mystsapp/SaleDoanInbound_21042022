using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class SightseeingTemp
    {
        public decimal Id { get; set; }
        public string Code { get; set; }
        public int Stt { get; set; }
        public string Codedtq { get; set; }
        public string Serial { get; set; }
        public string Debit { get; set; }
        public decimal? Unitpricea { get; set; }
        public int? Paxv { get; set; }
        public decimal? Unitpricec { get; set; }
        public decimal? Amount { get; set; }
        public int? Vatin { get; set; }
        public int? Vatout { get; set; }
        public decimal? Srvprofit { get; set; }
        public string Httt { get; set; }
        public string Chinhanh { get; set; }
    }
}
