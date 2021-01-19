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

            //DSKhachHangVM.KhachHangs = await _unitOfWork.dSKhachHangRepository.FindAsync(x => x.TourId == tourId && x.Del == false);
            var hd = _unitOfWork.khachTourRepository.Find(x => x.Sgtcode == DSKhachHangVM.Tour.Sgtcode && x.Del == false).ToList();

            DSKhachHangVM.ListDsKhach = hd;
            return PartialView(DSKhachHangVM);
        }

        [HttpPost, ActionName("KhachHangCreatePartialPost")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> KhachHangCreatePartialPost()
        {
            var tour = await _unitOfWork.tourRepository.GetByLongIdAsync(DSKhachHangVM.KhachHang.TourId);

            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            if (ModelState.IsValid)
            {
                try
                {

                    // ghi log
                    DSKhachHangVM.KhachHang.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

                    // SaleDoanIB
                    DSKhachHangVM.KhachHang.Sgtcode = tour.Sgtcode;
                    _unitOfWork.dSKhachHangRepository.Create(DSKhachHangVM.KhachHang);
                     // await _unitOfWork.Complete();
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
                        Del = false,
                        Visa = DSKhachHangVM.KhachHang.Visa,
                        YeuCauVisa = DSKhachHangVM.KhachHang.YeuCauVisa,
                        Logfile = DSKhachHangVM.KhachHang.LogFile
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

        public IActionResult KhachHangEditPartial(decimal id)
        {
            if (id == 0)
                return NotFound();

            var khachtour = _unitOfWork.khachTourRepository.GetById(id);
            //BienNhanVM.BienNhan.TourIBId = tourIBId;

            if (khachtour == null)
                return NotFound();

            DSKhachHangVM.KhachTour = khachtour;
            DSKhachHangVM.Tour = _unitOfWork.tourRepository.Find(x => x.Sgtcode == khachtour.Sgtcode).FirstOrDefault();

            return PartialView(DSKhachHangVM);
        }

        [HttpPost, ActionName("KhachHangEditPartialPost")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> KhachHangEditPartialPost()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (ModelState.IsValid)
            {
                try
                {
                    #region log file
                    
                    var t = _unitOfWork.khachTourRepository.GetByIdAsNoTracking(x => x.Idkhach == DSKhachHangVM.KhachTour.Idkhach);
                    if (t.Stt != DSKhachHangVM.KhachTour.Stt)
                    {
                        temp += String.Format("- STT thay đổi: {0}->{1}", t.Stt, DSKhachHangVM.KhachTour.Stt);
                    }
                    if (t.Makh != DSKhachHangVM.KhachTour.Makh)
                    {
                        temp += String.Format("- Mã khách thay đổi: {0}->{1}", t.Makh, DSKhachHangVM.KhachTour.Makh);
                    }
                    if (t.Hoten != DSKhachHangVM.KhachTour.Hoten)
                    {
                        temp += String.Format("- Tên khách thay đổi: {0}->{1}", t.Hoten, DSKhachHangVM.KhachTour.Hoten);
                    }
                    if (t.Ngaysinh != DSKhachHangVM.KhachTour.Ngaysinh)
                    {
                        temp += String.Format("- Ngày sinh thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.Ngaysinh, DSKhachHangVM.KhachTour.Ngaysinh);
                    }
                    if (t.Phai != DSKhachHangVM.KhachTour.Phai)
                    {
                        temp += String.Format("- giới tính thay đổi: {0}->{1}", t.Phai, DSKhachHangVM.KhachTour.Phai);
                    }
                    if (t.Quoctich != DSKhachHangVM.KhachTour.Quoctich)
                    {
                        temp += String.Format("- Quốc tịch thay đổi: {0}->{1}", t.Quoctich, DSKhachHangVM.KhachTour.Quoctich);
                    }
                    if (t.Hochieu != DSKhachHangVM.KhachTour.Hochieu)
                    {
                        temp += String.Format("- Hộ chiếu thay đổi: {0}->{1}", t.Hochieu, DSKhachHangVM.KhachTour.Hochieu);
                    }

                    if (t.Cmnd != DSKhachHangVM.KhachTour.Cmnd)
                    {
                        temp += String.Format("- CMND thay đổi: {0}->{1}", t.Cmnd, DSKhachHangVM.KhachTour.Cmnd);
                    }
                    if (t.Loaiphong != DSKhachHangVM.KhachTour.Loaiphong)
                    {
                        temp += String.Format("- Loại phòng thay đổi: {0}->{1}", t.Loaiphong, DSKhachHangVM.KhachTour.Loaiphong);
                    }
                    if (t.Diachi != DSKhachHangVM.KhachTour.Diachi)
                    {
                        temp += String.Format("- Địa chỉ thay đổi: {0}->{1}", t.Diachi, DSKhachHangVM.KhachTour.Diachi);
                    }
                    if (t.Dienthoai != DSKhachHangVM.KhachTour.Dienthoai)
                    {
                        temp += String.Format("- Điện thoại thay đổi: {0}->{1}", t.Dienthoai, DSKhachHangVM.KhachTour.Dienthoai);
                    }

                    
                    #endregion
                    // kiem tra thay doi
                    if (temp.Length > 0)
                    {

                        log = System.Environment.NewLine;
                        log += "=============";
                        log += System.Environment.NewLine;
                        log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                        t.Logfile = t.Logfile + log;
                        DSKhachHangVM.KhachTour.Logfile = t.Logfile;
                    }

                    // khachtour
                    _unitOfWork.khachTourRepository.Update(DSKhachHangVM.KhachTour);
                    await _unitOfWork.Complete(); // cap nhat xong khachtour

                    // delete all DSKhachHang by sgtcode and CreateRange == update for khachtour
                    var khachHangs = await _unitOfWork.dSKhachHangRepository.FindAsync(x => x.Sgtcode == DSKhachHangVM.KhachTour.Sgtcode);

                    _unitOfWork.dSKhachHangRepository.DeleteRange(khachHangs);
                    await _unitOfWork.Complete();

                    var khachtours = await _unitOfWork.khachTourRepository.FindAsync(x => x.Sgtcode == DSKhachHangVM.KhachTour.Sgtcode);
                    var tours = await _unitOfWork.tourRepository.FindAsync(x => x.Sgtcode == DSKhachHangVM.KhachTour.Sgtcode);
                    
                    foreach (var item in khachtours) // add lai
                    {
                        KhachHang khachHang = new KhachHang()
                        {
                            TourId = tours.FirstOrDefault().Id,
                            Sgtcode = item.Sgtcode,
                            STT = item.Stt ?? 0,
                            MaKH = item.Makh,
                            TenKH = item.Hoten,
                            NgaySinh = item.Ngaysinh,
                            GioiTinh = item.Phai.Value,
                            DiaChi = item.Diachi,
                            QuocTich = item.Quoctich,
                            LoaiPhong = item.Loaiphong,
                            CMND = Convert.ToInt32(item.Cmnd),
                            HoChieu = item.Hochieu,
                            Del = item.Del ?? false,
                            Visa = item.Visa,
                            YeuCauVisa = item.YeuCauVisa
                        };
                        _unitOfWork.dSKhachHangRepository.Create(khachHang);
                    }
                    await _unitOfWork.Complete();
                    // delete all DSKhachHang by sgtcode and CreateRange == update for khachtour

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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteKhachHangPartialPost(decimal id)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            var khachtour = _unitOfWork.khachTourRepository.GetById(id);
            if (khachtour == null)
                return NotFound();

            try
            {
                _unitOfWork.khachTourRepository.Delete(khachtour);
                await _unitOfWork.Complete();

                // delete all DSKhachHang by sgtcode and CreateRange == update for khachtour
                var khachHangs = await _unitOfWork.dSKhachHangRepository.FindAsync(x => x.Sgtcode == khachtour.Sgtcode);

                _unitOfWork.dSKhachHangRepository.DeleteRange(khachHangs);
                await _unitOfWork.Complete();

                var khachtours = await _unitOfWork.khachTourRepository.FindAsync(x => x.Sgtcode == khachtour.Sgtcode);
                var tours = await _unitOfWork.tourRepository.FindAsync(x => x.Sgtcode == khachtour.Sgtcode);

                foreach (var item in khachtours) // add lai
                {
                    KhachHang khachHang = new KhachHang()
                    {
                        TourId = tours.FirstOrDefault().Id,
                        Sgtcode = item.Sgtcode,
                        STT = item.Stt ?? 0,
                        MaKH = item.Makh,
                        TenKH = item.Hoten,
                        NgaySinh = item.Ngaysinh,
                        GioiTinh = item.Phai.Value,
                        DiaChi = item.Diachi,
                        QuocTich = item.Quoctich,
                        LoaiPhong = item.Loaiphong,
                        CMND = Convert.ToInt32(item.Cmnd),
                        HoChieu = item.Hochieu,
                        Del = item.Del ?? false,
                        Visa = item.Visa,
                        YeuCauVisa = item.YeuCauVisa
                    };
                    _unitOfWork.dSKhachHangRepository.Create(khachHang);
                }
                await _unitOfWork.Complete();
                // delete all DSKhachHang by sgtcode and CreateRange == update for khachtour

                return Json(new
                {
                    status = true,
                    tourid = tours.FirstOrDefault().Id
                });
            }
            catch (Exception ex)
            {
                //SetAlert(ex.Message, "error");
                //return Redirect(ChiTietBNVM.StrUrl);

                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }
    }
}