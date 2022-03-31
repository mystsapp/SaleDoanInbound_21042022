using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class VUserHoadon
    {
        public string Username { get; set; }
        public string Accounthddt { get; set; }
        public string Passwordhddt { get; set; }
        public string Mausohd { get; set; }
        public string Kyhieuhd { get; set; }
        public bool? IsAdmin { get; set; }
        public string Maviettat { get; set; }
        public string Chinhanh { get; set; }
    }
}
