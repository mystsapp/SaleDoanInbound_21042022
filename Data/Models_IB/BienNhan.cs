using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class BienNhan
    {
        public long Id { get; set; }
        [DisplayName("Số biên nhận")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string SoBN { get; set; } // biennhan in Mr.Son db

        [DisplayName("Tour")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public long TourId { get; set; } // relation --> foreignkey to tourIB

        [ForeignKey("TourId")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Ngày BN")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime NgayBN { get; set; }


        [DisplayName("Mã KH")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        [Required(ErrorMessage = "Mã KH không được trống")]
        public string MaKH { get; set; } // company: qltour

        [DisplayName("Tên khách hàng")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string TenKhach { get; set; }

        public int SK { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string GhiChu { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Tối đa ba ký tự"), Column(TypeName = "varchar(3)")]
        [Required(ErrorMessage = "Loại tiền không được để trống")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Tỷ giá không được để trống")]
        public decimal TyGia { get; set; }

        [DisplayName("Ngày hủy")]
        public DateTime? NgayHuy { get; set; }

        [DisplayName("Hủy BN")]
        public bool? HuyBN { get; set; }

        [DisplayName("Nội dung hủy")]
        public long NDHuyBNId { get; set; }

        [DisplayName("Khách lẽ")]
        public bool KhachLe { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        [MaxLength(20), Column(TypeName = "varchar(20)")]
        public string DienThoai { get; set; }

        [DisplayName("Nội dung")]
        [MaxLength(300), Column(TypeName = "nvarchar(300)")]
        public string NoiDung { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }

    }
}