using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class TourTMP
    {
        public int Id { get; set; }

        [MaxLength(17), Column(TypeName = "varchar(17)")]
        public string Sgtcode { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Khách lẻ")]
        public bool KhachLe { get; set; }

        //[DisplayName("Khách hàng")]
        //[MaxLength(5), Column(TypeName = "varchar(5)")]
        //public string MaKH { get; set; }

        [DisplayName("Tên giao dịch")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string TenGiaoDich { get; set; }

        [DisplayName("Tuyến TQ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string TuyenTQ { get; set; }

        [DisplayName("Chủ đề tour")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string ChuDeTour { get; set; }

        [DisplayName("Bắt đầu")]
        public DateTime BatDau { get; set; }

        [DisplayName("Kết thúc")]
        public DateTime KetThuc { get; set; }

        [DisplayName("Số chổ")]
        public int SoCho { get; set; }

        [DisplayName("Chổ còn lại")]
        public int ChoConLai { get; set; }

        [DisplayName("Người tạo tour")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTaoTour { get; set; }

        [DisplayName("Chi nhánh")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string MaCN { get; set; }
    }
}