using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class CTVAT
    {
        public long Id { get; set; }

        [DisplayName("Invoice")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string InvoiceId { get; set; } // invoice

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        [DisplayName("Diễn giải")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        //[Required(ErrorMessage = "Trường này không được để trống")]
        public string Descript { get; set; }

        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [DisplayName("Đơn vị tính")]
        [MaxLength(6), Column(TypeName = "varchar(6)")]
        //[Required(ErrorMessage = "Trường này không được để trống")]
        public string Unit { get; set; }

        [DisplayName("Đơn giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [DisplayName("%PDV")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ServiceFee { get; set; }

        [DisplayName("%VAT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal VAT { get; set; }

        [DisplayName("%")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Percent { get; set; }

        [DisplayName("Tiếng anh")]
        public bool TiengAnh { get; set; }

        [DisplayName("Mục")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string Muc { get; set; }
        
        [DisplayName("Tên khoãn mục")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string TenKhoanMuc { get; set; }

        [DisplayName("DS")]
        public bool? DS { get; set; }

        [DisplayName("DLHH")]
        public bool? DLHH { get; set; }


        [DisplayName("Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }
    }
}