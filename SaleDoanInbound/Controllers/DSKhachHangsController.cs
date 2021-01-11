using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Services;
using Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class DSKhachHangsController : BaseController
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
                GioiTinhs = ListGioiTinh().OrderByDescending(x => x.GioiTinhName)
            };
        }

        private IEnumerable<ListViewModel> ListGioiTinh()
        {
            return new List<ListViewModel>()
            {
                //new ListViewModel(){GioiTinhId = "-- Select --", GioiTinhName = "-- Select --"},
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

        public async Task<IActionResult> DSKhachHangPartial(long tourId)
        {

            DSKhachHangVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            //DSKhachHangVM.KhachHangs = await _unitOfWork.dSKhachHangRepository.FindIncludeOneAsync(x => x.Tour, y => y.TourId == tourId);

            //DSKhachHangVM.KhachHangs = await _unitOfWork.dSKhachHangRepository.FindAsync(x => x.TourId == tourId);
            var hd = _unitOfWork.khachTourRepository.Find(x => x.Sgtcode == DSKhachHangVM.Tour.Sgtcode && x.Del != true).ToList();

            DSKhachHangVM.ListDsKhach = hd;
            return PartialView(DSKhachHangVM);
        }

        [HttpPost, ActionName("KhachHangCreatePartialPost")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> KhachHangCreatePartialPost(long tourId)
        {
            var tour = await _unitOfWork.tourRepository.GetByLongIdAsync(tourId);
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    // SaleDoanIB
                    _unitOfWork.dSKhachHangRepository.Create(DSKhachHangVM.KhachHang);

                    // qltour
                    Khachtour khachtour = new Khachtour()
                    {
                        Sgtcode = tour.Sgtcode,
                        Stt = DSKhachHangVM.KhachHang.STT,
                        Makh = DSKhachHangVM.KhachHang.MaKH,
                        Hoten = DSKhachHangVM.KhachHang.TenKH,
                        Ngaysinh = DSKhachHangVM.KhachHang.NgaySinh,
                        Phai = DSKhachHangVM.KhachHang.GioiTinh,
                        Diachi = DSKhachHangVM.KhachHang.DiaChi,
                        Quoctich = DSKhachHangVM.KhachHang.QuocTich,
                        Loaiphong = DSKhachHangVM.KhachHang.LoaiPhong,
                        Cmnd = DSKhachHangVM.KhachHang.CMND.ToString(),
                        Hochieu = DSKhachHangVM.KhachHang.HoChieu,
                        Del = false
                    };

                    _unitOfWork.khachTourRepository.Create(khachtour);

                    await _unitOfWork.Complete();
                    //SetAlert("Thêm mới thành công.", "success");
                    //return Redirect(strUrl);

                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    ModelState.AddModelError("", ex.Message);

                    return Json(new
                    {
                        status = false
                    });
                }

                return Json(new
                {
                    status = true
                });

            }
            return Json(new
            {
                status = false
            });

        }


    }
}