using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class DMKhachTour
    {
        public long Id { get; set; } // Id_dsk

        [DisplayName("Tour")]
        public long IdTour { get; set; }

        [ForeignKey("IdTour")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Tên khách")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự."), Column(TypeName = "nvarchar(50)")]
        public string TenKhach { get; set; }

        [DisplayName("Hộ chiếu")]
        [MaxLength(50, ErrorMessage = "Không vượt qua 50 ký tự."), Column(TypeName = "varchar(50)")]
        public string HoChieu { get; set; }

        [DisplayName("Hiệu lực")]
        public DateTime HieuLucHoChieu { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [DisplayName("Số CMND")]
        [MaxLength(15, ErrorMessage = "Không vượt qua 15 ký tự."), Column(TypeName = "varchar(15)")]
        public string SoCMND { get; set; }

        [DisplayName("Ngày cấp CMND")]
        public DateTime NgayCMND { get; set; }

        [DisplayName("Nơi cấp CMND")]
        [MaxLength(100, ErrorMessage = "Không vượt qua 100 ký tự."), Column(TypeName = "nvarchar(100)")]
        public string NoiCapCMND { get; set; }

        [DisplayName("Giới tính")]
        [MaxLength(15, ErrorMessage = "Không vượt qua 15 ký tự."), Column(TypeName = "nvarchar(15)")]
        public string GioiTinh { get; set; }

        [MaxLength(15), Column(TypeName = "varchar(15)")]
        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [MaxLength(15)]
        [DisplayName("Quốc tịch"), Column(TypeName = "nvarchar(15)")]
        public string QuocTich { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }
    }
}