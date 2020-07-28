using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class Nuoc_del
    {
        public int Id { get; set; }

        [DisplayName("Tên nước")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenNuoc { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Nguoisua { get; set; }

        [DisplayName("Khu vực")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string IdKhuVuc { get; set; }

        [DisplayName("Phạm vi")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string PhamVi { get; set; }
    }
}