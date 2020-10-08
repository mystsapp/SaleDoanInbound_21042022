//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Data.Models_IB
//{
//    public class DMDaiLy
//    {
//        public int Id { get; set; }

//        [Required(ErrorMessage = "Tên không được để trống.")]
//        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
//        [DisplayName("Văn Phòng")]
//        public string TenDaiLy { get; set; }

//        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
//        [DisplayName("Địa chỉ")]
//        public string DiaChi { get; set; }

//        [MaxLength(15), Column(TypeName = "varchar(15)")]
//        [DisplayName("Điện thoại")]
//        public string DienThoai { get; set; }

//        [MaxLength(15), Column(TypeName = "varchar(15)")]
//        public string Fax { get; set; }

//        [DisplayName("Chi Nhánh")]
//        public int IdChiNhanh { get; set; }

//        [ForeignKey("IdChiNhanh")]
//        public virtual ChiNhanh ChiNhanh { get; set; }

//        [DisplayName("Trạng thái")]
//        public bool TrangThai { get; set; }

//        public DateTime NgayTao { get; set; }

//        [DisplayName("Người tạo")]
//        [MaxLength(50), Column(TypeName = "varchar(50)")]
//        public string NguoiTao { get; set; }

//        public DateTime NgaySua { get; set; }

//        [DisplayName("Người sửa")]
//        [MaxLength(50), Column(TypeName = "varchar(50)")]
//        public string NguoiSua { get; set; }
//    }
//}