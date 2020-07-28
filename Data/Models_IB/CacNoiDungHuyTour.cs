using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class CacNoiDungHuyTour
    {
        public long Id { get; set; }

        [DisplayName("Nội dung")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Nội dung được để trống.")]
        public string NoiDung { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        public DateTime NgaySua { get; set; }
    }
}