using Data.Dtos;
using Data.Models_IB;
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
    public class ThanhPhoViewModel
    {
        public IPagedList<ThanhPhoDto> thanhPhoDtos { get; set; }
        public IEnumerable<Quocgia> Quocgias { get; set; }
        public ThanhPho ThanhPho { get; set; }
        public ThanhPhoDto ThanhPhoDto { get; set; }
        public ThanhPhoEditDto ThanhPhoEditDto { get; set; }
        public string StrUrl { get; set; }

        [Remote("IsStringNameAvailable", "ThanhPhos", ErrorMessage = "Tên này đã tồn tại.")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenCreate { get; set; }
    }
}
