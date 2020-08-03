using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class Invoice
    {
        [DisplayName("Invoice")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string Id { get; set; } // invoice

        [DisplayName("LoaiIV")]
        [MaxLength(1), Column(TypeName = "varchar(1)")]
        public string Type { get; set; }

        [DisplayName("Thay thế bởi")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string Replace { get; set; }

        [DisplayName("Ngày")]
        public DateTime Date { get; set; }

        [DisplayName("Ngày đến")]
        public DateTime Arr { get; set; }

        [DisplayName("Ngày đi")]
        public DateTime? Dep { get; set; }

        [DisplayName("Số khách")]
        public int Pax { get; set; }

        public int SGL { get; set; }
        public int DBL { get; set; }
        public int TPL { get; set; }
        public string MOFP { get; set; }
        public DateTime DOFP { get; set; }

        [DisplayName("TourIB")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string TourIBId { get; set; } // relation (tourib key)

        [ForeignKey("TourIBId")]
        public virtual TourIB TourIB { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }

        [DisplayName("Tỷ giá")]
        public int Rate { get; set; }

        [MaxLength(12, ErrorMessage = "Tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string STT { get; set; }

        [MaxLength(12, ErrorMessage = "Tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string Bill { get; set; }

        public DateTime NgayVAT { get; set; }

        [DisplayName("Reference")]
        [MaxLength(100), Column(TypeName = "varchar(100)")]
        public string Ref { get; set; }

        [DisplayName("MST")]
        [MaxLength(16, ErrorMessage = "Chiều dài tối đa là 16 ký tự"), Column(TypeName = "varchar(16)")]
        public string MsThue { get; set; }

        [DisplayName("Hợp đồng")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa là 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string HopDong { get; set; }

        public DateTime HuyVAT { get; set; }

        [MaxLength(12, ErrorMessage = "Tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string SttMoi { get; set; }

        [MaxLength(12, ErrorMessage = "Tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string BillMoi { get; set; }

        public DateTime NgayVATMoi { get; set; }

        [DisplayName("Ký hiệu HD")]
        [MaxLength(10, ErrorMessage = "Tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string KyHieuHD { get; set; }

        [DisplayName("Mẫu số HD")]
        [MaxLength(11, ErrorMessage = "Tối đa 11 ký tự"), Column(TypeName = "varchar(11)")]
        public string MauSoHD { get; set; }

        [DisplayName("Key HDDT")]
        [MaxLength(120), Column(TypeName = "varchar(120)")]
        public string KeyHDDT { get; set; }
        public DateTime Lock { get; set; }
    }
}