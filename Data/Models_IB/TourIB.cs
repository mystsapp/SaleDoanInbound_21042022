using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class TourIB
    {
        [DisplayName("Relation")]
        [MaxLength(10), Column(TypeName = "varchar(10)")]
        public string Id { get; set; } // relation

        [Required(ErrorMessage = "Name không được để trống.")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        public string Order { get; set; }

        [MaxLength(17, ErrorMessage = "Chiều dài tối đa là 17 ký tự"), Column(TypeName = "varchar(17)")]
        [Required(ErrorMessage = "SGTCode được để trống.")]
        public string SGTCode { get; set; }

        [DisplayName("Ngày đến")]
        public DateTime Arr { get; set; }

        [DisplayName("Ngày đi")]
        public DateTime? Dep { get; set; }

        [DisplayName("SK")]
        public int Pax { get; set; }

        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string Ref { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deposit { get; set; }

        [DisplayName("Ghi chú")]
        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string Note { get; set; }

        [DisplayName("Nội dung hủy")]
        public long NoiDungHuy { get; set; } // cancel

        [DisplayName("Hãng")]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string CompanyId { get; set; } // qltour
    }
}