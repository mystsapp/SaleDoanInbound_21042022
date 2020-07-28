using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class UsrKhuCN
    {
        public int Id { get; set; }

        [DisplayName("User")]
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        public int IdKhuCN { get; set; }

        [ForeignKey("IdKhuCN")]
        public virtual PhanKhuCN PhanKhuCN { get; set; }

        [DisplayName("Người tạo")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        public DateTime NgaySua { get; set; }
    }
}