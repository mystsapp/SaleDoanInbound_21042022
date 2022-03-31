using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class HinhAnhs
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string DichVuId { get; set; }

        public virtual DichVus DichVu { get; set; }
    }
}
