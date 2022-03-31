﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using Data.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
using Data.Models_IB;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using NumToWords;
using Data.Services;

namespace SaleDoanInbound.Controllers
{
    public class BienNhansController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBienNhanService _bienNhanService;

        [BindProperty]
        public BienNhanViewModel BienNhanVM { get; set; }

        public BienNhansController(IUnitOfWork unitOfWork, IBienNhanService bienNhanService)
        {
            _unitOfWork = unitOfWork;
            _bienNhanService = bienNhanService;
            BienNhanVM = new BienNhanViewModel()
            {
                BienNhan = new Data.Models_IB.BienNhan(),
                Tour = new Data.Models_IB.Tour(),
                Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                CacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll(),
                ChiTietBNPrint = new ListViewModel()
            };
        }

        public async Task<IActionResult> Index(long id = 0, string searchString = null, string searchFromDate = null, string searchToDate = null, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            BienNhanVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            //BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(id);

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            // phan quyen
            List<string> listRoleChiNhanh = new List<string>();
            List<string> phongBansQL = new List<string>();
            var users = _unitOfWork.userRepository.GetAll().ToList();

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users") // Role Users
                {
                    //listRoleChiNhanh.Add(user.MaCN);
                    users = new List<User>();
                    users.Add(user);
                }
                else // Role admin khu vuc
                {
                    // add chinhanhs in PhanKhuCN
                    var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(user.RoleId);
                    if (phanKhuCN == null)
                    {
                        ModelState.AddModelError("", "Role của user này chưa được add chi nhánh.");
                        return View(BienNhanVM);
                    }
                    else
                    {
                        listRoleChiNhanh.AddRange(phanKhuCN.ChiNhanhs.Split(','));// lay het tat ca user trong chinhanh minh QL
                        users = users.Where(item1 => listRoleChiNhanh.Any(item2 => item1.MaCN == item2)).ToList();

                        if (!string.IsNullOrEmpty(user.PhongBans)) // PhongBans: nhung phongban QL (chi nhung phongban duoc chi dinh)
                        {
                            phongBansQL = user.PhongBans.Split(',').ToList();
                            users = users.Where(item1 => phongBansQL.Any(item2 => item1.PhongBanId == item2)).ToList();
                        }
                    }
                }

                BienNhanVM.BienNhanPagedList = await _bienNhanService.BienNhanPagedList(searchString, searchFromDate, searchToDate, page, users);
            }
            else // user logon == admin
            {
                BienNhanVM.BienNhanPagedList = await _bienNhanService.BienNhanPagedList(searchString, searchFromDate, searchToDate, page, null);
            }

            // phan quyen
            //BienNhanVM.BienNhanPagedList = await _bienNhanService.BienNhanPagedList(searchString, searchFromDate, searchToDate, page);

            // click BienNhan row
            if (id != 0)
            {
                BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdIncludeOneAsync(id);
                BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == id);
            }
            return View(BienNhanVM);
        }

        //public async Task<IActionResult> CTBNPartial(long bienNhanId)
        //{
        //        BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(bienNhanId);
        //        BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == bienNhanId);

        //    return PartialView(BienNhanVM);
        //}

        public async Task<IActionResult> CTBienNhanInBienNhanPartial(long bienNhanId)
        {
            BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(bienNhanId);
            BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == bienNhanId);
            return PartialView(BienNhanVM);
        }

        //public IActionResult BienNhansByTourPartial(long tourId)
        //{
        //    if (tourId == 0)
        //        return NotFound();
        //    BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
        //    BienNhanVM.BienNhans = _unitOfWork.bienNhanRepository.ListBienNhan("", tourId, "", "");

        //    return PartialView(BienNhanVM);
        //}

        //public IActionResult Create(long tourId/*, string tabActive*/, string strUrl)
        //{
        //    BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
        //    BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
        //    BienNhanVM.BienNhan.TourId = tourId;
        //    BienNhanVM.BienNhan.SK = (BienNhanVM.Tour.SoKhachTT == 0) ? BienNhanVM.Tour.SoKhachDK : BienNhanVM.Tour.SoKhachTT;
        //    BienNhanVM.BienNhan.LoaiTien = BienNhanVM.Tour.LoaiTien;
        //    BienNhanVM.BienNhan.TyGia = BienNhanVM.Tour.TyGia.Value;
        //    BienNhanVM.BienNhan.MaKH = BienNhanVM.Tour.MaKH;
        //    BienNhanVM.BienNhan.TenKhach = BienNhanVM.Tour.TenKH;
        //    BienNhanVM.BienNhan.DienThoai = BienNhanVM.Tour.DienThoai;
        //    BienNhanVM.BienNhan.DiaChi = BienNhanVM.Tour.DiaChi;
        //    return View(BienNhanVM);
        //}

        public async Task<IActionResult> CreateBienNhanPartial(long tourId)
        {
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            BienNhanVM.BienNhan.TourId = tourId;
            BienNhanVM.BienNhan.SK = (BienNhanVM.Tour.SoKhachTT == 0) ? BienNhanVM.Tour.SoKhachDK : BienNhanVM.Tour.SoKhachTT;
            BienNhanVM.BienNhan.LoaiTien = BienNhanVM.Tour.LoaiTien;
            BienNhanVM.BienNhan.TyGia = BienNhanVM.Tour.TyGia.Value;
            BienNhanVM.BienNhan.MaKH = BienNhanVM.Tour.MaKH;
            BienNhanVM.BienNhan.TenKhach = BienNhanVM.Tour.TenKH;
            BienNhanVM.BienNhan.DienThoai = BienNhanVM.Tour.DienThoai;
            BienNhanVM.BienNhan.DiaChi = BienNhanVM.Tour.DiaChi;

            BienNhanVM.Httts = await _bienNhanService.GetAllHTTT();
            return PartialView(BienNhanVM);
        }

        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePost()
        //{
        //    // from login session
        //    var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

        //    //000099IB2020
        //    if (!ModelState.IsValid)
        //    {
        //        return View(BienNhanVM);
        //    }

        //    BienNhanVM.BienNhan.NgayBN = DateTime.Now;
        //    BienNhanVM.BienNhan.NgayTao = DateTime.Now;
        //    BienNhanVM.BienNhan.NguoiTao = user.Username;
        //    BienNhanVM.BienNhan.DienThoai = BienNhanVM.BienNhan.DienThoai ?? "";
        //    BienNhanVM.BienNhan.SoBN = "";

        //    // next SoBN (so bien nhan)
        //    var currentYear = DateTime.Now.Year;
        //    var subfix = "IB" + currentYear.ToString();
        //    var bienNhan = _unitOfWork.bienNhanRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
        //    if (bienNhan == null)
        //    {
        //        BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
        //    }
        //    else
        //    {
        //        var oldYear = bienNhan.SoBN.Substring(8, 4);
        //        // cung nam
        //        if (oldYear == currentYear.ToString())
        //        {
        //            var oldSoBN = bienNhan.SoBN.Substring(0, 6);
        //            BienNhanVM.BienNhan.SoBN = GetNextId.NextID(oldSoBN, "") + subfix;
        //        }
        //        else
        //        {
        //            // sang nam khac' chay lai tu dau
        //            BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
        //        }
        //    }

        //    // next id (so bien nhan)

        //    // ghi log
        //    BienNhanVM.BienNhan.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
        //    try
        //    {
        //        _unitOfWork.bienNhanRepository.Create(BienNhanVM.BienNhan);
        //        await _unitOfWork.Complete();
        //        SetAlert("Thêm mới thành công.", "success");
        //        return Redirect(BienNhanVM.StrUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        SetAlert(ex.Message, "error");
        //        ModelState.AddModelError("", ex.Message);
        //        return View(BienNhanVM);
        //    }
        //}

        [HttpPost, ActionName("CreateBienNhanPartial")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBienNhanPartialPost()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            //000099IB2020
            if (!ModelState.IsValid)
            {
                return View(BienNhanVM);
            }

            BienNhanVM.BienNhan.NgayBN = DateTime.Now;
            BienNhanVM.BienNhan.NgayTao = DateTime.Now;
            BienNhanVM.BienNhan.NguoiTao = user.Username;
            BienNhanVM.BienNhan.DienThoai = BienNhanVM.BienNhan.DienThoai ?? "";
            BienNhanVM.BienNhan.SoBN = "";

            // next SoBN (so bien nhan)
            var currentYear = DateTime.Now.Year;
            var subfix = "IB" + currentYear.ToString();
            var bienNhan = _unitOfWork.bienNhanRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (bienNhan == null)
            {
                BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
            }
            else
            {
                var oldYear = bienNhan.SoBN.Substring(8, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    var oldSoBN = bienNhan.SoBN.Substring(0, 6);
                    BienNhanVM.BienNhan.SoBN = GetNextId.NextID(oldSoBN, "") + subfix;
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
                }
            }

            // next id (so bien nhan)

            // ghi log
            BienNhanVM.BienNhan.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {
                _unitOfWork.bienNhanRepository.Create(BienNhanVM.BienNhan);
                await _unitOfWork.Complete();
                //SetAlert("Thêm mới thành công.", "success");
                //return Redirect(BienNhanVM.StrUrl);

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                //SetAlert(ex.Message, "error");
                //ModelState.AddModelError("", ex.Message);
                //return View(BienNhanVM);

                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        //public IActionResult Edit(long id, long tourId, /*string tabActive, */string strUrl)
        //{
        //    BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
        //    BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
        //    //BienNhanVM.BienNhan.TourIBId = tourIBId;

        //    if (id == 0)
        //        return NotFound();

        //    BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(id);

        //    if (BienNhanVM.BienNhan == null)
        //        return NotFound();

        //    return View(BienNhanVM);
        //}

        public async Task<IActionResult> EditBienNhanPartial(long bienNhanId, long tourId)
        {
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            //BienNhanVM.BienNhan.TourIBId = tourIBId;

            if (bienNhanId == 0)
                return NotFound();

            BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(bienNhanId);

            if (BienNhanVM.BienNhan == null)
                return NotFound();

            BienNhanVM.Httts = await _bienNhanService.GetAllHTTT();
            return PartialView(BienNhanVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id/*, string strUrl*/)
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
                if (t.NgayBN.ToShortDateString() != BienNhanVM.BienNhan.NgayBN.ToShortTimeString())
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

                if (t.NDHuyBNId != BienNhanVM.BienNhan.NDHuyBNId)
                {
                    temp += String.Format("- Nội dung thay đổi: {0}->{1}",
                        (t.NDHuyBNId == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(t.NDHuyBNId).NoiDung,
                        (BienNhanVM.BienNhan.NDHuyBNId == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(BienNhanVM.BienNhan.NDHuyBNId).NoiDung);
                }

                // loai tien, ty gia mac dinh: vnd, 1

                #endregion log file

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
                    ModelState.AddModelError("", ex.Message);
                    return View(BienNhanVM);
                }
            }

            return View(BienNhanVM);
        }

        [HttpPost, ActionName("EditBienNhanPartial")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBienNhanPartialPost()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (ModelState.IsValid)
            {
                BienNhanVM.BienNhan.NgaySua = DateTime.Now;
                BienNhanVM.BienNhan.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view

                #region log file

                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.bienNhanRepository.GetByIdAsNoTracking(x => x.Id == BienNhanVM.BienNhan.Id);
                //if (t.NgayBN.ToString("dd/MM/yyyy") != BienNhanVM.BienNhan.NgayBN.ToString("dd/MM/yyyy"))
                //{
                //    temp += String.Format("- Ngày BN thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayBN, BienNhanVM.BienNhan.NgayBN);
                //}
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

                if (t.NDHuyBNId != BienNhanVM.BienNhan.NDHuyBNId)
                {
                    temp += String.Format("- Nội dung thay đổi: {0}->{1}",
                        (t.NDHuyBNId == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(t.NDHuyBNId).NoiDung,
                        (BienNhanVM.BienNhan.NDHuyBNId == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(BienNhanVM.BienNhan.NDHuyBNId).NoiDung);
                }

                // loai tien, ty gia mac dinh: vnd, 1

                #endregion log file

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
                    //SetAlert("Cập nhật thành công", "success");
                    //return Redirect(BienNhanVM.StrUrl);

                    return Json(new
                    {
                        status = true
                    });
                }
                catch (Exception ex)
                {
                    //SetAlert(ex.Message, "error");
                    //ModelState.AddModelError("", ex.Message);
                    //return View(BienNhanVM);

                    return Json(new
                    {
                        status = false,
                        message = ex.Message
                    });
                }
            }
            // not valid

            return Json(new
            {
                status = false,
                message = "Form not valid."
            });
        }

        public IActionResult Details(long id, long tourId/*, string tabActive*/, string strUrl)
        {
            BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.Tour = _unitOfWork.tourRepository.GetById(tourId);

            if (id == 0)
                return NotFound();

            var bienNhan = _unitOfWork.bienNhanRepository.GetById(id);

            if (bienNhan == null)
                return NotFound();

            BienNhanVM.BienNhan = bienNhan;
            ViewBag.NDHuy = (bienNhan.NDHuyBNId == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(bienNhan.NDHuyBNId).NoiDung;

            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl/*, string tabActive*/)
        {
            BienNhanVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

            var bienNhan = _unitOfWork.bienNhanRepository.GetById(id);
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
                ModelState.AddModelError("", ex.Message);
                return Redirect(BienNhanVM.StrUrl);
            }
        }

        //-----------HuyBN------------
        public async Task<IActionResult> HuyBNPartialIndex(long id, string strUrl)
        {
            if (id == 0)
                return NotFound();

            BienNhanVM.StrUrl = strUrl;
            BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(id);
            BienNhanVM.CacNoiDungHuyTours = await _unitOfWork.cacNoiDungHuyTourRepository.FindAsync(x => x.Xoa == false);

            return PartialView(BienNhanVM);
        }

        [HttpPost]
        public async Task<IActionResult> HuyBN()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log;

            // BN
            var bienNhan = _unitOfWork.bienNhanRepository.GetById(BienNhanVM.BienNhan.Id);
            if (BienNhanVM.BienNhan.NgayHuy.HasValue)
            {
                bienNhan.NgayHuy = BienNhanVM.BienNhan.NgayHuy;
            }
            else
            {
                bienNhan.NgayHuy = DateTime.Now;
            }
            bienNhan.NDHuyBNId = BienNhanVM.BienNhan.NDHuyBNId;
            bienNhan.HuyBN = true;

            // kiem tra thay doi

            if (BienNhanVM.BienNhan.NDHuyBNId > 0)
            {
                var cacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(BienNhanVM.BienNhan.NDHuyBNId);
                temp += String.Format("- Nội dung huy: {0}", cacNoiDungHuyTour.NoiDung);
            }

            //if (!string.IsNullOrEmpty(BienNhanVM.BienNhan.GhiChu))
            //{
            //    temp += String.Format("- Ghi chú: {0}", BienNhanVM.BienNhan.GhiChu);
            //}

            if (BienNhanVM.BienNhan.NgayHuy.HasValue)
            {
                temp += String.Format("- Ngày hủy: {0:dd/MM/yyyy} - Người hủy: {1}", BienNhanVM.BienNhan.NgayHuy, user.Username); // username
            }

            if (temp.Length > 0)
            {
                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp;
                bienNhan.LogFile = bienNhan.LogFile + log;
            }

            try
            {
                _unitOfWork.bienNhanRepository.Update(bienNhan);
                await _unitOfWork.Complete();
                SetAlert("Hủy thành công.", "success");
                return Redirect(BienNhanVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert("Error: " + ex.Message, "error");
                ModelState.AddModelError("", ex.Message);
                return Redirect(BienNhanVM.StrUrl);
            }
        }

        //-----------HuyBN------------
        public async Task<IActionResult> HuyBNPartial(long id, string strUrl)
        {
            if (id == 0)
                return NotFound();

            BienNhanVM.StrUrl = strUrl;
            BienNhanVM.BienNhan = _unitOfWork.bienNhanRepository.GetById(id);
            BienNhanVM.CacNoiDungHuyTours = await _unitOfWork.cacNoiDungHuyTourRepository.FindAsync(x => x.Xoa == false);

            return PartialView(BienNhanVM);
        }

        [HttpPost]
        public async Task<IActionResult> HuyBienNhanPartial()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log;

            // BN
            var bienNhan = _unitOfWork.bienNhanRepository.GetById(BienNhanVM.BienNhan.Id);
            if (BienNhanVM.BienNhan.NgayHuy.HasValue)
            {
                bienNhan.NgayHuy = BienNhanVM.BienNhan.NgayHuy;
            }
            else
            {
                bienNhan.NgayHuy = DateTime.Now;
            }
            bienNhan.NDHuyBNId = BienNhanVM.BienNhan.NDHuyBNId;
            bienNhan.HuyBN = true;

            // kiem tra thay doi

            if (BienNhanVM.BienNhan.NDHuyBNId > 0)
            {
                var cacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(BienNhanVM.BienNhan.NDHuyBNId);
                temp += String.Format("- Nội dung huy: {0}", cacNoiDungHuyTour.NoiDung);
            }

            //if (!string.IsNullOrEmpty(BienNhanVM.BienNhan.GhiChu))
            //{
            //    temp += String.Format("- Ghi chú: {0}", BienNhanVM.BienNhan.GhiChu);
            //}

            if (BienNhanVM.BienNhan.NgayHuy.HasValue)
            {
                temp += String.Format("- Ngày hủy: {0:dd/MM/yyyy} - Người hủy: {1}", BienNhanVM.BienNhan.NgayHuy, user.Username); // username
            }

            if (temp.Length > 0)
            {
                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp;
                bienNhan.LogFile = bienNhan.LogFile + log;
            }

            try
            {
                _unitOfWork.bienNhanRepository.Update(bienNhan);
                await _unitOfWork.Complete();
                //SetAlert("Hủy thành công.", "success");
                //return Redirect(BienNhanVM.StrUrl);

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                //SetAlert("Error: " + ex.Message, "error");
                //ModelState.AddModelError("", ex.Message);
                //return Redirect(BienNhanVM.StrUrl);

                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        //-----------HuyBN------------

        //----------- Print BN partial ------------
        public async Task<IActionResult> PrintBNPartial(long id, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (id == 0)
                return NotFound();

            BienNhanVM.StrUrl = strUrl;
            BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdIncludeOneAsync(id);
            BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == id);

            ///// Currency to money
            string s = SoSangChu.DoiSoSangChu(BienNhanVM.BienNhan.SoTien.ToString().Split('.')[0]);
            string c = AmountToWords.changeCurrencyToWords(BienNhanVM.BienNhan.SoTien.ToString().ToLower());
            //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            BienNhanVM.SoTienBangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";

            // ChiTietBNPrint
            BienNhanVM.ChiTietBNPrint = new ListViewModel();
            foreach (var item in BienNhanVM.ChiTietBNs)
            {
                if (string.IsNullOrEmpty(item.Descript))
                {
                    BienNhanVM.ChiTietBNPrint.DienGiais += "" + "<br />";
                }
                else
                {
                    BienNhanVM.ChiTietBNPrint.DienGiais += "<b>" + item.Descript + "</b>" + "<br>";
                }
                BienNhanVM.ChiTietBNPrint.SoTiens += "<b>" + item.Amount.ToString("N0") + "</b>" + "<br />";
            }
            // ChiTietBNPrint

            BienNhanVM.BienNhan.NguoiTao = user.HoTen;
            return PartialView(BienNhanVM);
        }

        public async Task<IActionResult> GetDetailBN(long id)
        {
            if (id == 0)
                return NotFound();

            var bienNhan = await _unitOfWork.bienNhanRepository.GetByIdIncludeOneAsync(id);
            if (string.IsNullOrEmpty(bienNhan.SoBN))
            {
                return Json(new
                {
                    status = false
                });
            }

            return Json(new
            {
                status = true,
                data = bienNhan
            });
        }

        public async Task<IActionResult> ExportPdf(long id)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (id == 0)
                return NotFound();

            BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdIncludeOneAsync(id);
            BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == id);

            // ChiTietBNPrint
            BienNhanVM.ChiTietBNPrint = new ListViewModel();
            foreach (var item in BienNhanVM.ChiTietBNs)
            {
                if (string.IsNullOrEmpty(item.Descript))
                {
                    BienNhanVM.ChiTietBNPrint.DienGiais += "" + "<br />";
                }
                else
                {
                    BienNhanVM.ChiTietBNPrint.DienGiais += "<b>" + item.Descript + "</b>" + "<br>";
                }
                BienNhanVM.ChiTietBNPrint.SoTiens += "<b>" + item.Amount.ToString("N0") + "</b>" + "<br />";
            }
            // ChiTietBNPrint

            ////next SoBN(so bien nhan)
            //if (string.IsNullOrEmpty(BienNhanVM.BienNhan.SoBN))
            //{
            //    var currentYear = DateTime.Now.Year;
            //    var subfix = "IB" + currentYear.ToString();
            //    var bienNhan = _unitOfWork.bienNhanRepository.Find(x => !string.IsNullOrEmpty(x.SoBN)).OrderByDescending(x => x.SoBN).FirstOrDefault();
            //    if (bienNhan == null)
            //    {
            //        BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
            //    }
            //    else
            //    {
            //        var oldYear = bienNhan.SoBN.Substring(8, 4);
            //        // cung nam
            //        if (oldYear == currentYear.ToString())
            //        {
            //            var oldSoBN = bienNhan.SoBN.Substring(0, 6);
            //            BienNhanVM.BienNhan.SoBN = GetNextId.NextID(oldSoBN, "") + subfix;
            //        }
            //        else
            //        {
            //            // sang nam khac' chay lai tu dau
            //            BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
            //        }

            //    }
            //    _unitOfWork.bienNhanRepository.Update(BienNhanVM.BienNhan);

            //    await _unitOfWork.Complete();

            //}

            ////next id(so bien nhan)

            ///// Currency to money
            string s = SoSangChu.DoiSoSangChu(BienNhanVM.BienNhan.SoTien.ToString().Split('.')[0]);
            string c = AmountToWords.changeCurrencyToWords(BienNhanVM.BienNhan.SoTien.ToString().ToLower());
            //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            BienNhanVM.SoTienBangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";

            BienNhanVM.BienNhan.NguoiTao = user.HoTen;

            var pdf = new ViewAsPdf
            {
                FileName = "BienNhan_" + BienNhanVM.BienNhan.SoBN + "_" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 5, Left = 5, Right = 5, Top = 5 },
                Model = BienNhanVM
            };

            return pdf;
        }

        [HttpPost]
        public async Task<IActionResult> ExportPdfPartial(long id)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (id == 0)
                return NotFound();

            BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdIncludeOneAsync(id);
            BienNhanVM.ChiTietBNs = await _unitOfWork.chiTietBNRepository.FindAsync(x => x.BienNhanId == id);

            // ChiTietBNPrint
            BienNhanVM.ChiTietBNPrint = new ListViewModel();
            foreach (var item in BienNhanVM.ChiTietBNs)
            {
                if (string.IsNullOrEmpty(item.Descript))
                {
                    BienNhanVM.ChiTietBNPrint.DienGiais += "" + "<br />";
                }
                else
                {
                    BienNhanVM.ChiTietBNPrint.DienGiais += "<b>" + item.Descript + "</b>" + "<br>";
                }
                BienNhanVM.ChiTietBNPrint.SoTiens += "<b>" + item.Amount.ToString("N0") + "</b>" + "<br />";
            }
            // ChiTietBNPrint

            //next SoBN(so bien nhan)
            if (string.IsNullOrEmpty(BienNhanVM.BienNhan.SoBN))
            {
                var currentYear = DateTime.Now.Year;
                var subfix = "IB" + currentYear.ToString();
                var bienNhan = _unitOfWork.bienNhanRepository.Find(x => !string.IsNullOrEmpty(x.SoBN)).OrderByDescending(x => x.SoBN).FirstOrDefault();
                if (bienNhan == null)
                {
                    BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
                }
                else
                {
                    var oldYear = bienNhan.SoBN.Substring(8, 4);
                    // cung nam
                    if (oldYear == currentYear.ToString())
                    {
                        var oldSoBN = bienNhan.SoBN.Substring(0, 6);
                        BienNhanVM.BienNhan.SoBN = GetNextId.NextID(oldSoBN, "") + subfix;
                    }
                    else
                    {
                        // sang nam khac' chay lai tu dau
                        BienNhanVM.BienNhan.SoBN = GetNextId.NextID("", "") + subfix;
                    }
                }
                _unitOfWork.bienNhanRepository.Update(BienNhanVM.BienNhan);

                await _unitOfWork.Complete();
            }

            //next id(so bien nhan)

            ///// Currency to money
            string s = SoSangChu.DoiSoSangChu(BienNhanVM.BienNhan.SoTien.ToString().Split('.')[0]);
            string c = AmountToWords.changeCurrencyToWords(BienNhanVM.BienNhan.SoTien.ToString().ToLower());
            //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            BienNhanVM.SoTienBangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";

            BienNhanVM.BienNhan.NguoiTao = user.HoTen;

            var pdf = new ViewAsPdf
            {
                FileName = "BienNhan_" + BienNhanVM.BienNhan.SoBN + "_" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 5, Left = 5, Right = 5, Top = 5 },
                Model = BienNhanVM
            };

            return pdf;
        }

        //----------- Print BN partial ------------
    }
}