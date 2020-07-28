using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class DMHoaHong
    {
        public long Id { get; set; }

        [DisplayName("Tour")]
        public long IdTour { get; set; }

        [ForeignKey("IdTour")]
        public virtual Tour Tour { get; set; }

        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Sales { get; set; }

        public decimal Id_DMKH { get; set; }

        [DisplayName("Tên khách")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenKhach { get; set; }

        [DisplayName("Số CMND")]
        [MaxLength(20), Column(TypeName = "varchar(20)")]
        public string SoCMNN { get; set; }

        [DisplayName("Số Tiền")]
        public decimal SoTien { get; set; }

        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string SalesNM { get; set; }
    }
}