using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username không được để trống.")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Password không được để trống.")]
        [DataType(DataType.Password)]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Password { get; set; }

        [DisplayName("Họ tên")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string HoTen { get; set; }

        [DisplayName("Điện thoại")]
        [MaxLength(20), Column(TypeName = "varchar(20)")]
        public string DienThoai { get; set; }

        [DisplayName("Đại lý")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string DaiLy { get; set; }

        [DisplayName("Tạo tour")]
        public bool TaoTour { get; set; }

        [DisplayName("Bán vé")]
        public bool BanVe { get; set; }

        [DisplayName("Sửa vé")]
        public bool SuaVe { get; set; }

        [DisplayName("Đống tour")]
        public bool DongTour { get; set; }

        [DisplayName("Dc danh mục")]
        public bool DCDanhMuc { get; set; }

        [DisplayName("Sửa tour")]
        public bool SuaTour { get; set; }

        [DisplayName("Admin KL")]
        public bool AdminKL { get; set; }

        [DisplayName("Admin KD")]
        public bool AdminKD { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Chưa đúng định dạng email.")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [DisplayName("Email CC")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Chưa đúng định dạng email.")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string EmailCC { get; set; }

        [DisplayName("Chi nhánh")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string MaCN { get; set; }
        
        [DisplayName("Phòng ban")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string PhongBanId { get; set; } // in qltour

        [DisplayName("Bán tour")]
        public bool BanTour { get; set; }

        [DisplayName("Role")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [DisplayName("Đổi MK")]
        public bool DoiMK { get; set; }

        [DisplayName("Ngày Đổi MK")]
        public DateTime NgayDoiMK { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiCapNhat { get; set; }

        public DateTime NgayCapNhat { get; set; }
    }
}