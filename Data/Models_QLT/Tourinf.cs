using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class Tourinf
    {
        public Tourinf()
        {
            Tourprog = new HashSet<Tourprog>();
        }

        public string Sgtcode { get; set; }
        public bool Khachle { get; set; }
        public string CompanyId { get; set; }
        public string Chinhanhtao { get; set; }
        public int? TourkindId { get; set; }
        public DateTime Arr { get; set; }
        public DateTime Dep { get; set; }
        public int? Pax { get; set; }
        public int? Childern { get; set; } // so tre em
        public string Concernto { get; set; } // nguoi tao tour
        public string Operators { get; set; }
        public string Departoperator { get; set; }
        public string Departcreate { get; set; } // phong ban tao
        public string Reference { get; set; } // --. tuyenTQ 
        public string Routing { get; set; } // lo trinh
        public DateTime? Cancel { get; set; }
        public string Cancelnote { get; set; }
        public string Entryport { get; set; } // dau vao
        public string Entryby { get; set; } // dau ra
        public string Note { get; set; }
        public string Visa { get; set; } //
        public string Passtype { get; set; } // loai khach
        public decimal? Revenue { get; set; } // doanh thu
        public string Currency { get; set; }
        public decimal? Rate { get; set; }
        public DateTime? Createtour { get; set; }
        public DateTime? Locktour { get; set; }
        public string Userlock { get; set; }
        public string Chinhanh { get; set; }
        public string Logfile { get; set; }

        public virtual ICollection<Tourprog> Tourprog { get; set; }
    }
}
