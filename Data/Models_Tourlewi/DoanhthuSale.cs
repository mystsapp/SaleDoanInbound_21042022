using System;
using System.Collections.Generic;

namespace Data.Models_Tourlewi
{
    public partial class DoanhthuSale
    {
        public long Stt { get; set; }
        public string Sgtcode { get; set; }
        public int? Vetourid { get; set; }
        public string Tenkhach { get; set; }
        public int? Chiemcho { get; set; }
        public decimal? Doanhthu { get; set; }
        public decimal? Thucthu { get; set; }
        public string Nguoixuatve { get; set; }
    }
}
