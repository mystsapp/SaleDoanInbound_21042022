using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class ThongTinTour
    {
        public long Id { get; set; }

        [DisplayName("Tour")]
        public long IdTour { get; set; }

        [ForeignKey("IdTour")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Nội dung tin")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string NoiDungTin { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Loại tin")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string LoaiTin { get; set; }
    }
}