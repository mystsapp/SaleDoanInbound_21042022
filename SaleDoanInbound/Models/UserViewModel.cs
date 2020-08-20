using Data.Models_IB;
using Data.Models_QLT;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class UserViewModel
    {
        public IPagedList<User> Users { get; set; }
        public User User { get; set; }
        public IEnumerable<Dmchinhanh> Dmchinhanhs { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Phongban> PhongBans { get; set; }
        public string StrUrl { get; set; }

        [Remote("IsStringNameAvailable", "Users", ErrorMessage = "Tên này đã tồn tại.")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenCreate { get; set; }
        public string OldPassword { get; set; }



    }
}
