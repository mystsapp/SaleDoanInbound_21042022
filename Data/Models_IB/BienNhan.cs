using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class BienNhan
    {
        [DisplayName("Biên nhận")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối thiểu 12 ký tự"), Column("varchar(12")]
        public string Id { get; set; } // biennhan in Mr.Son db

        [DisplayName("Tour")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối thiểu 10 ký tự"), Column("varchar(10")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public long TourId { get; set; } // relation --> foreignkey to tourIB

        [ForeignKey("TourId")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Ngày BN")]
        public DateTime NgayBN { get; set; }

        [DisplayName("Tên khách hàng")]
        [MaxLength(50), Column("nvarchar(50")]
        public string TenKhach { get; set; }

        public int SK { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(150), Column("nvarchar(150")]
        public string GhiChu { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Tối đa ba ký tự"), Column("varchar(3")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TyGia { get; set; }

        [DisplayName("Nội dung hủy")]
        public long NoiDungHuy { get; set; } // huy

        [DisplayName("Khách lẽ")]
        public bool KhachLe { get; set; }
    }
}