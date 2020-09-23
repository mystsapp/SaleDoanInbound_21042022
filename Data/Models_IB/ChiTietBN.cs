using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class ChiTietBN
    {
        public long Id { get; set; }

        [DisplayName("Biên nhận")]
        public long BienNhanId { get; set; }

        [ForeignKey("BienNhanId")]
        public virtual BienNhan BienNhan { get; set; }

        [DisplayName("Diễn giải")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string Descript { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string HTTT { get; set; }

        [DisplayName("Số tiền CT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTienCT { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string GhiChu { get; set; }


        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }
    }
}