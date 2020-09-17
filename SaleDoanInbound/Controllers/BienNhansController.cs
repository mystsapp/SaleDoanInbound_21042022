using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using Data.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
using Data.Models_IB;

namespace SaleDoanInbound.Controllers
{
    public class BienNhansController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public BienNhanViewModel BienNhanVM { get; set; }

        public BienNhansController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            BienNhanVM = new BienNhanViewModel()
            {
                BienNhan = new Data.Models_IB.BienNhan(),
                Tour = new Data.Models_IB.Tour(),
                Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                CacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll()
            };
        }
        public async Task<IActionResult> Index(long tourId, string id = null, string searchString = null, string searchFromDate = null, string searchToDate = null)
        {
            BienNhanVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            // for delete
            //if (id != 0)
            //{
            //    var nganhNghe = _unitOfWork.dMNganhNgheRepository.GetById(id);
            //    if (nganhNghe == null)
            //    {
            //        var lastId = _unitOfWork.dMNganhNgheRepository
            //                                  .GetAll().OrderByDescending(x => x.Id)
            //                                  .FirstOrDefault().Id;
            //        id = lastId;
            //    }
            //}

            BienNhanVM.BienNhans = _unitOfWork.bienNhanRepository.ListBienNhan(searchString, tourId, searchFromDate, searchToDate);

            // click BienNhan row
            if (!string.IsNullOrEmpty(id))
            {
                BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);
                BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == id);
            }
            return View(BienNhanVM);
        }

        public IActionResult Create(long tourId/*, string tabActive*/, string strUrl)
        {
            BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            BienNhanVM.BienNhan.TourId = tourId;
            BienNhanVM.BienNhan.SK = BienNhanVM.Tour.SoKhachDK;
            BienNhanVM.BienNhan.LoaiTien = BienNhanVM.Tour.LoaiTien;
            BienNhanVM.BienNhan.TyGia = BienNhanVM.Tour.TyGia.Value;
            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            //000099IB2020
            if (!ModelState.IsValid)
            {
                return View(BienNhanVM);
            }

            BienNhanVM.BienNhan.NgayTao = DateTime.Now;
            BienNhanVM.BienNhan.NguoiTao = user.Username;

            // next id (so bien nhan)
            var currentYear = DateTime.Now.Year;
            var subfix = "IB" + currentYear.ToString();
            var bienNhan = _unitOfWork.bienNhanRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (bienNhan == null)
            {
                BienNhanVM.BienNhan.Id = GetNextId.NextID("", "") + subfix;
            }
            else
            {
                var oldYear = bienNhan.Id.Substring(8, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    var oldId = bienNhan.Id.Substring(0, 6);
                    BienNhanVM.BienNhan.Id = GetNextId.NextID(oldId, "") + subfix;
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    BienNhanVM.BienNhan.Id = GetNextId.NextID("", "") + subfix;
                }

            }

            // next id (so bien nhan)

            // ghi log
            BienNhanVM.BienNhan.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {
                _unitOfWork.bienNhanRepository.Create(BienNhanVM.BienNhan);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(BienNhanVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(BienNhanVM);
            }

        }

        public async Task<IActionResult> Edit(string id, long tourId, /*string tabActive, */string strUrl)
        {
            BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            //BienNhanVM.BienNhan.TourIBId = tourIBId;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);

            if (BienNhanVM.BienNhan == null)
                return NotFound();

            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id/*, string strUrl*/)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != BienNhanVM.BienNhan.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                BienNhanVM.BienNhan.NgaySua = DateTime.Now;
                BienNhanVM.BienNhan.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.bienNhanRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.NgayBN != BienNhanVM.BienNhan.NgayBN)
                {
                    temp += String.Format("- Ngày BN thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayBN, BienNhanVM.BienNhan.NgayBN);
                }
                if (t.TenKhach != BienNhanVM.BienNhan.TenKhach)
                {
                    temp += String.Format("- Tên khách thay đổi: {0}->{1}", t.TenKhach, BienNhanVM.BienNhan.TenKhach);
                }
                if (t.SK != BienNhanVM.BienNhan.SK)
                {
                    temp += String.Format("- SK thay đổi: {0}->{1}", t.SK, BienNhanVM.BienNhan.SK);
                }
                if (t.GhiChu != BienNhanVM.BienNhan.GhiChu)
                {
                    temp += String.Format("- Ghi chú thay đổi: {0}->{1}", t.GhiChu, BienNhanVM.BienNhan.GhiChu);
                }
                if (t.LoaiTien != BienNhanVM.BienNhan.LoaiTien)
                {
                    temp += String.Format("- Loại tiền thay đổi: {0}->{1}", t.LoaiTien, BienNhanVM.BienNhan.LoaiTien);
                }
                if (t.TyGia != BienNhanVM.BienNhan.TyGia)
                {
                    temp += String.Format("- Tỷ giá thay đổi: {0}->{1}", t.TyGia, BienNhanVM.BienNhan.TyGia);
                }
                if (t.KhachLe != BienNhanVM.BienNhan.KhachLe)
                {
                    temp += String.Format("- Khách lẻ thay đổi: {0}->{1}", t.KhachLe, BienNhanVM.BienNhan.KhachLe);
                }

                if (t.NoiDungHuy != BienNhanVM.BienNhan.NoiDungHuy)
                {
                    temp += String.Format("- Nội dung thay đổi: {0}->{1}",
                        (t.NoiDungHuy == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(t.NoiDungHuy).NoiDung,
                        (BienNhanVM.BienNhan.NoiDungHuy == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(BienNhanVM.BienNhan.NoiDungHuy).NoiDung);
                }

                // loai tien, ty gia mac dinh: vnd, 1
                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    BienNhanVM.BienNhan.LogFile = t.LogFile;
                }

                try
                {

                    _unitOfWork.bienNhanRepository.Update(BienNhanVM.BienNhan);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(BienNhanVM.StrUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(BienNhanVM);
                }
            }

            return View(BienNhanVM);
        }

        public async Task<IActionResult> Details(string id, long tourId/*, string tabActive*/, string strUrl)
        {
            BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);

            if (string.IsNullOrEmpty(id))
                return NotFound();

            var bienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);

            if (bienNhan == null)
                return NotFound();

            BienNhanVM.BienNhan = bienNhan;
            ViewBag.NDHuy = (bienNhan.NoiDungHuy == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(bienNhan.NoiDungHuy).NoiDung;

            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl/*, string tabActive*/)
        {
            BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

            var bienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);
            if (bienNhan == null)
                return NotFound();
            try
            {
                _unitOfWork.bienNhanRepository.Delete(bienNhan);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(BienNhanVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(BienNhanVM.StrUrl);
            }
        }
    }
}