using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class ChiTietBN
    {
        public long Id { get; set; }

        [DisplayName("Biên nhận")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối thiểu 12 ký tự"), Column("varchar(12")]
        public string BienNhanId { get; set; }

        [ForeignKey("BienNhanId")]
        public virtual BienNhan BienNhan { get; set; }

        [DisplayName("Diễn giải")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string Descript { get; set; }

        [DisplayName("Số tiền")]
        public decimal Amount { get; set; }
    }
}