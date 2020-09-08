using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class LoaiIV
    {
        [DisplayName("ID")]
        [MaxLength(1, ErrorMessage = "Tối đa 1 ký tự"), Column(TypeName = "varchar(1)")]
        public string Id { get; set; }

        [DisplayName("Tên loai")]
        [MaxLength(1, ErrorMessage = "Tối đa 1 ký tự"), Column(TypeName = "varchar(1)")]
        public string TenLoaiIV { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(1, ErrorMessage = "Tối đa 1 ký tự"), Column(TypeName = "varchar(1)")]
        public string GhiChu { get; set; }
    }
}