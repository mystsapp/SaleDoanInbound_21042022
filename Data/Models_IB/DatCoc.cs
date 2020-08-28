using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class DatCoc
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DisplayName("Ngày đặt cọc")]
        public DateTime? NgayDatCoc { get; set; }

        [DisplayName("Tour")]
        public long IdTour { get; set; }

        [ForeignKey("IdTour")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Số biên nhận")]
        [Required(ErrorMessage = "SBN không được để trống.")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string SoBienNhan { get; set; }

        //[DisplayName("Người làm BN")]
        //[MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự."), Column(TypeName = "varchar(50)")]
        //public string NguoiLamBN { get; set; }

        [DisplayName("Đại lý")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự."), Column(TypeName = "nvarchar(50)")]
        public string DaiLy { get; set; }

        [DisplayName("Tên khách")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự."), Column(TypeName = "nvarchar(50)")]
        public string TenKhach { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [MaxLength(20), Column(TypeName = "varchar(20)")]
        [DisplayName("Điện Thoại")]
        public string DienThoai { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        [DisplayName("HTTT")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string HinhThucThanhToan { get; set; }

        [DisplayName("Chứng từ gốc")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string ChungTuGoc { get; set; }

        [MaxLength(300), Column(TypeName = "nvarchar(3000)")]
        public string TenMay { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TyGia { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }
    }
}