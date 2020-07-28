using Data.Models_IB;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class CacNoiDungHuyTourViewModel
    {
        public IPagedList<CacNoiDungHuyTour> CacNoiDungHuyTours { get; set; }
        public CacNoiDungHuyTour CacNoiDungHuyTour { get; set; }
        public string StrUrl { get; set; }

        [Remote("IsStringNameAvailable", "CacNoiDungHuyTours", ErrorMessage = "Nội dung này đã tồn tại.")]
        [Required(ErrorMessage = "Nội dung không được để trống.")]
        public string TenCreate { get; set; }
    }
}