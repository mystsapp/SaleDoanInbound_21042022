using Data.Models_QLT;
using Data.Models_QLTaiKhoan;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class KhachHangViewModel
    {
        public IPagedList<Company> Companies { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Data.Models_QLT.Dmchinhanh> Dmchinhanhs { get; set; }
        public IEnumerable<Data.Models_QLTaiKhoan.Quocgia> Quocgias { get; set; }
        public string StrUrl { get; set; }
        [Remote("IsStringNameAvailable", "KhachHangs", ErrorMessage = "Tên KH đã tồn tại")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenCreate { get; set; }
    }
}
