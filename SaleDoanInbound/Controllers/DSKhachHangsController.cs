using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class DSKhachHangsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDSKhachHangService _dSKhachHangService;

        [BindProperty]
        public DSKhachHangViewModel DSKhachHangVM { get; set; }

        public DSKhachHangsController(IUnitOfWork unitOfWork, IDSKhachHangService dSKhachHangService)
        {
            _unitOfWork = unitOfWork;
            _dSKhachHangService = dSKhachHangService;

            DSKhachHangVM = new DSKhachHangViewModel()
            {
                KhachHang = new Data.Models_IB.KhachHang(),
                Tour = new Data.Models_IB.Tour(),
                GioiTinhs = ListGioiTinh()
            };
        }

        private IEnumerable<ListViewModel> ListGioiTinh()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel(){GioiTinhId = "-- Select --", GioiTinhName = "Null"},
                new ListViewModel(){GioiTinhId = "True", GioiTinhName = "Nam"},
                new ListViewModel(){GioiTinhId = "False", GioiTinhName = "Nữ"}
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> KhachHangCreatePartial(long tourId)
        {

            DSKhachHangVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            DSKhachHangVM.KhachHang.TourId = DSKhachHangVM.Tour.Id;

            //DSKhachHangVM.KhachHangs = await _unitOfWork.dSKhachHangRepository.FindAsync(x => x.TourId == tourId);

            return PartialView(DSKhachHangVM);
        }

    }
}