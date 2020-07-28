using Data.Models_IB;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class NganhNgheViewModel
    {
        public IPagedList<DMNganhNghe> DMNganhNghes { get; set; }
        public DMNganhNghe DMNganhNghe { get; set; }
        public string StrUrl { get; set; }

        [Remote("IsStringNameAvailable", "NganhNghes", ErrorMessage = "Tên này đã tồn tại.")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenCreate { get; set; }
    }
}