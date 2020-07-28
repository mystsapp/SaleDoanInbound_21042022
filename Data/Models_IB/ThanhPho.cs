using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class ThanhPho
    {
        public int Id { get; set; }

        
        [DisplayName("Tên thành phố")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenThanhPho { get; set; }

        [DisplayName("Quốc gia")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int MaQuocGia { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }
    }
}