using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class Users
    {
        public Users()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Hoten { get; set; }
        public string Dienthoai { get; set; }
        public string Email { get; set; }
        public string Maphong { get; set; }
        public string Macn { get; set; }
        public string RoleId { get; set; }
        public bool Trangthai { get; set; }
        public DateTime? Ngaydoimk { get; set; }
        public bool? Doimk { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string Nguoitao { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
