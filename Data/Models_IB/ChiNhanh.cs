//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Data.Models_IB
//{
//    public class ChiNhanh
//    {
//        public int Id { get; set; }

//        [DisplayName("Mã CN")]
//        [Required(ErrorMessage = "Mã CN không được để trống.")]
//        [MaxLength(5), Column(TypeName = "varchar(5)")]
//        public string MaCN { get; set; }

//        [Required(ErrorMessage = "Tên CN không được để trống.")]
//        [DisplayName("Tên CN")]
//        [MaxLength(200), Column(TypeName = "nvarchar(200)")]
//        public string TenCN { get; set; }

//        [DisplayName("Địa chỉ")]
//        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
//        public string DiaChi { get; set; }

//        [DisplayName("Thành phố")]
//        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
//        public string ThanhPho { get; set; }

//        [DisplayName("Điện thoại")]
//        [MaxLength(20), Column(TypeName = "varchar(20)")]
//        public string DienThoai { get; set; }

//        [MaxLength(20), Column(TypeName = "varchar(20)")]
//        public string Fax { get; set; }

//        [DisplayName("MST")]
//        public int MaSoThue { get; set; }

//        [DisplayName("Trạng Thái")]
//        public bool TrangThai { get; set; }

//        public DateTime NgayTao { get; set; }

//        [DisplayName("Người tạo")]
//        [MaxLength(50), Column(TypeName = "varchar(50)")]
//        public string NguoiTao { get; set; }

//        public DateTime NgaySua { get; set; }

//        [DisplayName("Người sửa")]
//        [MaxLength(50), Column(TypeName = "varchar(50)")]
//        public string NguoiSua { get; set; }

//        [DisplayName("Phân khu CN")]
//        public int IdPhanKhuCN { get; set; }
//        [ForeignKey("IdPhanKhuCN")]
//        public virtual PhanKhuCN PhanKhuCN { get; set; }
//    }
//}