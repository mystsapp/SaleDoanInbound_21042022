using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class Tour
    {
        public long Id { get; set; }

        [MaxLength(17), Column(TypeName = "varchar(17)")]
        public string Sgtcode { get; set; }

        [DisplayName("Khách lẻ")]
        public bool KhachLe { get; set; }

        [DisplayName("Chủ đề tour")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [Required]
        public string ChuDeTour { get; set; }

        public DateTime? NgayKhoa { get; set; }

        [DisplayName("Người khóa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiKhoa { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        [DisplayName("Bắt đầu")]
        [Required(ErrorMessage = "Ngày đến không được trống")]
        public DateTime NgayDen { get; set; }

        [DisplayName("Kết thúc")]
        [Required(ErrorMessage = "Ngày đi không được trống")]
        public DateTime NgayDi { get; set; }

        [DisplayName("Tuyến TQ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Tuến TQ không được trống")]
        public string TuyenTQ { get; set; } // Reference --> qltour: tourinf | lay tu qltaikhoan.ThanhPho

        [DisplayName("Số khách DK")]
        [Required(ErrorMessage = "SK không được trống")]
        public int SoKhachDK { get; set; }

        [DisplayName("Doanh thu DK")]
        [Required(ErrorMessage = "Doanh thu không được trống")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DoanhThuDK { get; set; }

        [DisplayName("Ngày đàm phán")]
        public DateTime? NgayDamPhan { get; set; }

        [DisplayName("Hình thức giao dịch")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string HinhThucGiaoDich { get; set; }

        [DisplayName("Ngày ký hợp đồng")]
        public DateTime? NgayKyHopDong { get; set; }

        [DisplayName("Người ký HĐ")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiKyHopDong { get; set; }

        [DisplayName("Hạn xuất vé")]
        public DateTime? HanXuatVe { get; set; }

        [DisplayName("Ngày thanh lý HĐ")]
        public DateTime? NgayThanhLyHD { get; set; }

        [DisplayName("Số khách TT")]
        public int SoKhachTT { get; set; }

        [DisplayName("SK trẻ em")]
        public int SKTreEm { get; set; }

        [DisplayName("Doanh thu TT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DoanhThuTT { get; set; }

        [DisplayName("Chương trình tour")]
        [MaxLength(300), Column(TypeName = "nvarchar(300)")]
        public string ChuongTrinhTour { get; set; }

        [DisplayName("Nội dung thanh lý HĐ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string NoiDungThanhLyHD { get; set; }

        [DisplayName("Dịch vụ")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string DichVu { get; set; }

        [DisplayName("Đại lý")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string DaiLy { get; set; }

        [DisplayName("Trạng thái")]
        [MaxLength(1), Column(TypeName = "varchar(1)")]
        public string TrangThai { get; set; }

        public DateTime? NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        [DisplayName("Loại tour")]
        [Required(ErrorMessage = "Loại tour không được trống")]
        public int? LoaiTourId { get; set; } // tourkind: qltour

        [DisplayName("Chi nhánh")]
        public int ChiNhanhTaoId { get; set; } // chinhanh tao: lay ben qltour

        [DisplayName("Ngày nhận đủ tiền")]
        public DateTime? NgayNhanDuTien { get; set; }

        [DisplayName("Lý do")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string LyDoNhanDu { get; set; }

        [DisplayName("Số HĐ")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string SoHopDong { get; set; }

        [DisplayName("Lãi dự tính chưa gồm vé (vé máy bay/vé xe lửa)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LaiChuaVe { get; set; }

        [DisplayName("Lãi dự tính bao gồm vé (vé máy bay/vé xe lửa)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LaiGomVe { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LaiThucTeGomVe { get; set; }

        [DisplayName("Nguồn tour")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string NguonTour { get; set; }

        [DisplayName("File khách đi tour")]
        [MaxLength(100), Column(TypeName = "varchar(100)")]
        public string FileKhachDiTour { get; set; }

        [DisplayName("File VMB")]
        [MaxLength(100), Column(TypeName = "varchar(100)")]
        public string FileVeMayBay { get; set; }

        [DisplayName("File biên nhận")]
        [MaxLength(100), Column(TypeName = "varchar(100)")]
        public string FileBienNhan { get; set; }

        [DisplayName("Người đại diện")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string NguoiDaiDien { get; set; }

        [DisplayName("Đối tác nước ngoài")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string DoiTacNuocNgoai { get; set; }

        // chinhanh DH
        [DisplayName("Chi nhánh DH")]
        [Required(ErrorMessage = "Chi nhánh DH không được để trống")]
        public int ChiNhanhDHId { get; set; } // dm chi nhanh: qltour

        // PhongBan -> qltour
        [DisplayName("Phòng DH")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string PhongDH { get; set; }

        [DisplayName("Thị trường / khối")]
        [MaxLength(5), Column(TypeName = "nvarchar(5)")]
        //[Required(ErrorMessage = "Trường này không được để trống")]
        public string PhongBanMaCode { get; set; } // phong ban  : qltour

        // chinhanh DH

        [DisplayName("Ngày hủy tour")]
        public DateTime? NgayHuyTour { get; set; }
        
        [DisplayName("Hủy tour")]
        public bool? HuyTour { get; set; }

        [DisplayName("Nội dung hủy")]
        public long NDHuyTourId { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string GhiChu { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TyGia { get; set; }

        // KH
        [DisplayName("Loại khách")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string LoaiKhach { get; set; } // PasstypeId in qltour

        [DisplayName("Mã KH")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        [Required(ErrorMessage = "Mã KH không được trống")]
        public string MaKH { get; set; } // company: qltour

        [DisplayName("Tên KH")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Tên KH không được trống")]
        public string TenKH { get; set; }

        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Chưa đúng định dạng email")]
        public string Email { get; set; }

        [DisplayName("Điện thoại")]
        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string DienThoai { get; set; }

        [MaxLength(15), Column(TypeName = "varchar(15)")]
        public string Fax { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string DiaChi { get; set; }

        // KH

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }
    }
}