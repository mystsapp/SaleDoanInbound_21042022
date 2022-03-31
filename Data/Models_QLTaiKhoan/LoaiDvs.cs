using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class LoaiDvs
    {
        public LoaiDvs()
        {
            DichVus = new HashSet<DichVus>();
        }

        public int Id { get; set; }
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string GhiChu { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
        public string LogFile { get; set; }

        public virtual ICollection<DichVus> DichVus { get; set; }
    }
}
