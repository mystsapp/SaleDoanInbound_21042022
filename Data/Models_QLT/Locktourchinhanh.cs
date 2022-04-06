using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class Locktourchinhanh
    {
        public string Sgtcode { get; set; }
        public string Chinhanh { get; set; }
        public DateTime? Datelock { get; set; }
        public string Userlock { get; set; }
        public string Logfile { get; set; }
    }
}
