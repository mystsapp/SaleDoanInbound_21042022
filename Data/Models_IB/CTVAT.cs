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
        public string Descript { get; set; }

        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [DisplayName("Đơn vị tính")]
        [MaxLength(6), Column(TypeName = "varchar(6)")]
        public string Unit { get; set; }

        [DisplayName("Đơn giá")]
        public decimal UnitPrice { get; set; }
        [DisplayName("Số tiền")]
        public decimal Amount { get; set; }
        [DisplayName("%PDV")]
        public decimal ServiceFee { get; set; }
        [DisplayName("%VAT")]
        public decimal VAT { get; set; }
        [DisplayName("%")]
        public decimal Percent { get; set; }
    }
}