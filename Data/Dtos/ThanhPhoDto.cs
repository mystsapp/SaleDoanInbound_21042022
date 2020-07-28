using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Dtos
{
    public class ThanhPhoDto
    {
        public int Id { get; set; }

        [DisplayName("Tên thành phố")]
        public string TenThanhPho { get; set; }

        [DisplayName("Quốc gia")]
        public string QuocGia { get; set; }

        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        public string NguoiSua { get; set; }
    }
}
