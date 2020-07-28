using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class DMKhachHang
    {
        
        public long Id { get; set; }

        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string MaCN { get; set; }

        [DisplayName("Tên giao dịch")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenGiaoDich { get; set; }

        [DisplayName("Tên thương mại")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string TenThuongMai { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string DiaChi { get; set; }

        [DisplayName("Quốc gia")]
        public int MaQuocGia { get; set; }

        [DisplayName("Thành phố")]
        public int MaThanhPho { get; set; }

        [MaxLength(15), Column(TypeName = "varchar(20)")]
        [DisplayName("Điện thoại")]
        public string Telephone { get; set; }

        [MaxLength(20), Column(TypeName = "varchar(20)")]
        public string Tax { get; set; }

        [MaxLength(20), Column(TypeName = "varchar(20)")]
        public string Fax { get; set; }

        [MaxLength(20), Column(TypeName = "varchar(20)")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Chưa đúng định dạng email.")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        public string Email { get; set; }

        [DisplayName("Người LH")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiLH { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        [DisplayName("Ghi chu GD")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string GhiChuKhiGiaoDich { get; set; }

        //[MaxLength(250), Column(TypeName = "varchar(250)")]
        //public string Lsthamgiatour { get; set; }

        [DisplayName("Ngành nghề")]
        public int MaNganhNghe { get; set; }

    }
}