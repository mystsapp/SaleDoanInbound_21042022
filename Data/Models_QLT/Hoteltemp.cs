﻿using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class Hoteltemp
    {
        public decimal Id { get; set; }
        public string Code { get; set; }
        public int? Stt { get; set; }
        public int Sgl { get; set; }
        public int Sglpax { get; set; }
        public decimal Sglcost { get; set; }
        public int Sglfoc { get; set; }
        public int Extsgl { get; set; }
        public decimal Extsglcost { get; set; }
        public int Dbl { get; set; }
        public int Dblpax { get; set; }
        public decimal Dblcost { get; set; }
        public int Dblfoc { get; set; }
        public int Extdbl { get; set; }
        public decimal Extdblcost { get; set; }
        public int Twn { get; set; }
        public int Twnpax { get; set; }
        public decimal Twncost { get; set; }
        public int Twnfoc { get; set; }
        public int Exttwn { get; set; }
        public decimal Exttwncost { get; set; }
        public int Homestay { get; set; }
        public int Homestaypax { get; set; }
        public decimal Homestaycost { get; set; }
        public string Homestaynote { get; set; }
        public int Oth { get; set; }
        public int Othpax { get; set; }
        public decimal Othcost { get; set; }
        public string Othtype { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public decimal? Idtourprog { get; set; }
    }
}
