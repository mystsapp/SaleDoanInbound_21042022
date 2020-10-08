﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class PhanKhuCN
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        ////[DisplayName("Tên phân khu")]
        ////[MaxLength(50), Column(TypeName = "nvarchar(50)")]
        ////public string TenKhuCN { get; set; }

        [DisplayName("Tên phân khu")]
        [Key, ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        [DisplayName("Chi nhánh")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Chi nhánh không được trống")]
        public string ChiNhanhs { get; set; }

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