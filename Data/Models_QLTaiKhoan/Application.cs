using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class Application
    {
        public Application()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }

        public string Mact { get; set; }
        public string Chuongtrinh { get; set; }
        public string Link { get; set; }
        public string Mota { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
