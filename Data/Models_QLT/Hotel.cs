using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Hotel
    {
        public decimal Id { get; set; }
        public string Sgtcode { get; set; }
        public int? Stt { get; set; }
        public int Sgl { get; set; }
        public int Sglpax { get; set; }
        public decimal Sglcost { get; set; }
        public int Extsgl { get; set; }
        public decimal Extsglcost { get; set; }
        public int Dbl { get; set; }
        public int Dblpax { get; set; }
        public decimal Dblcost { get; set; }
        public int Extdbl { get; set; }
        public decimal Extdblcost { get; set; }
        public int Twn { get; set; }
        public int Twnpax { get; set; }
        public decimal Twncost { get; set; }
        public int Exttwn { get; set; }
        public decimal Exttwncost { get; set; }
        public int Tpl { get; set; }
        public int Tplpax { get; set; }
        public decimal Tplcost { get; set; }
        public int Exttpl { get; set; }
        public decimal Exttplcost { get; set; }
        public int Oth { get; set; }
        public int Othpax { get; set; }
        public decimal Othcost { get; set; }
        public string Othtype { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public string Logfile { get; set; }
    }
}
