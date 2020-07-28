using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class VeMayBay
    {
        public long Id { get; set; }

        [DisplayName("Tour")]
        public long IdTour { get; set; }

        [ForeignKey("IdTour")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Chuyến bay")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string ChuyenBay { get; set; }

        [DisplayName("Ngày bay")]
        public DateTime NgayBay { get; set; }

        [DisplayName("Điểm đi")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string DiemDi { get; set; }

        [DisplayName("Điểm đến")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string DiemDen { get; set; }

        [DisplayName("Giờ đi")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string GioDi { get; set; }

        [DisplayName("Giờ đến")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string GioDen { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        public DateTime NgaySua { get; set; }
        public int SoVe_ADL_D { get; set; }
        public int SoVe_CHL_D { get; set; }
        public int SoVe_INF_D { get; set; }
        public int SoVe_ADL_V { get; set; }
        public int SoVe_CHL_V { get; set; }
        public int SoVe_INF_V { get; set; }

        [DisplayName("Lượt đi về")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string LuotDiVe { get; set; }
    }
}