using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class TapDoan
    {
        public TapDoan()
        {
            Supplier = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Ten { get; set; }
        public string Chuoi { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
        public string LogFile { get; set; }

        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
