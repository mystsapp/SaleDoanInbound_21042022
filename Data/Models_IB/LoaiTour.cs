using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class LoaiTour
    {
        public int Id { get; set; }

        [DisplayName("Tên loại")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenLoaiTour { get; set; }

        [DisplayName("Sử dụng")]
        public bool SuDung { get; set; }
    }
}