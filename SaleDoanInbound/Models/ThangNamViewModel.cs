using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class ThangNamViewModel
    {
        [DisplayName("Từ tháng")]
        public string TuThang1 { get; set; }
        [DisplayName("Đến tháng")]
        public string DenThang1 { get; set; }
        [DisplayName("Năm")]
        public int Nam1 { get; set; }

        [DisplayName("Từ tháng")]
        public string TuThang2 { get; set; }
        [DisplayName("Đến tháng")]
        public string DenThang2 { get; set; }
        [DisplayName("Năm")]
        public int Nam2 { get; set; }

        [DisplayName("Chi nhánh")]
        public string MaCN { get; set; }
    }
}
