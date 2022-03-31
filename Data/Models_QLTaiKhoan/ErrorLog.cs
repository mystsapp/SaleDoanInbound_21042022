using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class ErrorLog
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public string MaCn { get; set; }
        public DateTime NgayTao { get; set; }
        public string LogFile { get; set; }
        public string NguoiTao { get; set; }
    }
}
