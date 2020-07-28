using Data.Models_IB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class LoaiTourViewModel
    {
        public IPagedList<LoaiTour> LoaiTours { get; set; }
        public LoaiTour LoaiTour { get; set; }
        public string StrUrl { get; set; }

        [Remote("IsStringNameAvailable", "LoaiTours", ErrorMessage = "Tên này đã tồn tại.")]
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string TenCreate { get; set; }
    }
}
