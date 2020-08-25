using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class KhachHang
    {
        public long Id { get; set; }
        public int STT { get; set; }

        [DisplayName("Mã KH")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string MaKH { get; set; }

        [DisplayName("Tên KH")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenKH { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [DisplayName("Gới tính")]
        public bool GioiTinh { get; set; }

        [DisplayName("Quốc tịch")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string QuocTich { get; set; }

        [DisplayName("Hộ chiếu")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string HoChieu { get; set; }

        public int CMND { get; set; }

        [DisplayName("Loại phòng")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string LoaiPhong { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string DiaChi { get; set; }

        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string Visa { get; set; }

        [DisplayName("Yêu cầu visa")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string YeuCauVisa { get; set; }

        [DisplayName("Tour")]
        public long TourId { get; set; }

        [ForeignKey("TourId")]
        public virtual Tour Tour { get; set; }
    }
}