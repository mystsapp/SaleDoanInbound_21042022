﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using OfficeOpenXml;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class ToursController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public TourViewModel TourVM { get; set; }

        public ToursController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

            TourVM = new TourViewModel()
            {
                Tour = new Data.Models_IB.Tour(),
                Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                Companies = _unitOfWork.khachHangRepository.GetAll(),
                Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                NguonTours = NguonTour(),
                LoaiKhachs = LoaiKhach(),
                listPhongMacode = new List<Data.Models_QLT.Phongban>(),
                listPhongDH = new List<Phongban>(),
                Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                TourDto = new TourDto()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            TourVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            ViewBag.searchString = searchString;

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

            var companies = TourVM.Companies;
            var loaiTours = TourVM.Tourkinds;
            var chiNhanhs = TourVM.Dmchinhanhs;
            var cacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll();

            TourVM.TourDtos = _unitOfWork.tourRepository.ListTour(searchString, companies, loaiTours, chiNhanhs, cacNoiDungHuyTours, page);
            if (TourVM.TourDtos == null)
            {

            }
            return View(TourVM);
        }

        public IActionResult Create(string strUrl)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            TourVM.StrUrl = strUrl;
            TourVM.Tour.SoHopDong = "";
            ViewBag.chiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id;

            //ViewBag.tuyenTQ = "BAL,BAN"; //"[BAL,BAN]"; // for test
            // get list phong ban / thi truong
            GetListPhongBanMacode(user.PhongBanId); // sinh ma cho sgtgode / phongbanid = maphong in qltour
                                                    // get list phong ban / thi truong

            // get list phong ban / dh 
            GetListPhongBanDH(); // departoperator (qltour)
            // get list phong ban / dh
            return View(TourVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                TourVM = new TourViewModel()
                {
                    Tour = new Data.Models_IB.Tour(),
                    Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                    Companies = _unitOfWork.khachHangRepository.GetAll(),
                    Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                    Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                    LoaiKhachs = LoaiKhach(),
                    listPhongMacode = new List<Data.Models_QLT.Phongban>(),
                    listPhongDH = new List<Phongban>(),
                    Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                    NguonTours = NguonTour(),
                    StrUrl = strUrl
                };

                return View(TourVM);
            }

            //TourVM.Tour = new Data.Models_IB.Tour();
            TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace(',', '-');

            TourVM.Tour.ChiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id;
            TourVM.Tour.NgayTao = DateTime.Now;
            TourVM.Tour.NguoiTao = user.Username;

            // create sgtcode
            var sgtCode = _unitOfWork.tourInfRepository.newSgtcode(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, TourVM.Tour.PhongBanMaCode);
            TourVM.Tour.Sgtcode = sgtCode;
            // create sgtcode

            // ghi log
            TourVM.Tour.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            // insert tourinf

            Tourinf tourinf = new Tourinf();

            tourinf.Sgtcode = TourVM.Tour.Sgtcode;
            tourinf.Khachle = false;
            tourinf.CompanyId = TourVM.Tour.MaKH;
            tourinf.TourkindId = TourVM.Tour.LoaiTourId;
            tourinf.Arr = TourVM.Tour.NgayDen;
            tourinf.Dep = TourVM.Tour.NgayDi;
            tourinf.Pax = TourVM.Tour.SoKhachTT;
            tourinf.Childern = TourVM.Tour.SKTreEm;
            tourinf.Reference = TourVM.Tour.TuyenTQ;
            tourinf.Concernto = user.Username; // nguoi tao tour
            tourinf.Operators = ""; // nguoi dieu hanh
            tourinf.Departoperator = TourVM.Tour.PhongDH; //departoperator : qltour / phong dh
            tourinf.Departcreate = user.PhongBanId; // phong ban tao
            tourinf.Routing = "";
            //tourinf.Rate = TourVM.Tour.TyGia;
            tourinf.Rate = TourVM.Tour.TyGia;
            tourinf.Revenue = (TourVM.Tour.DoanhThuTT > 0) ? TourVM.Tour.DoanhThuTT : TourVM.Tour.DoanhThuDK;
            tourinf.PasstypeId = TourVM.Tour.LoaiKhach; // Inbound or tau bien
            //tourinf.Currency = TourVM.Tour.LoaiTien;
            tourinf.Currency = TourVM.Tour.LoaiTien;
            tourinf.Chinhanh = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn; // chinhanh dieu hanh
            tourinf.Chinhanhtao = user.MaCN; // user login
            tourinf.Createtour = TourVM.Tour.NgayTao;
            tourinf.Logfile = TourVM.Tour.LogFile;

            // insert tourinf

            try
            {

                _unitOfWork.tourRepository.Create(TourVM.Tour);
                // insert tourinf
                _unitOfWork.tourInfRepository.Create(tourinf);
                // insert tourinf
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");

                // upload excel
                UploadExcelAsync(TourVM.Tour.Sgtcode);
                // upload excel

                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(TourVM);
            }

        }

        public async Task<IActionResult> Edit(long id, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            ViewBag.chiNhanhSession = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id; // for compare

            TourVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            TourVM.Tour = await _unitOfWork.tourRepository.GetByLongIdAsync(id);
            ViewBag.maCn = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn; // for view

            if (TourVM.Tour == null)
                return NotFound();

            // gang qua hid tuyentq
            TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace('-', ',');
            // gang qua hid tuyentq

            // get list phong ban / thi truong
            GetListPhongBanMacode(user.PhongBanId); // phongbanid = maphong in qltour
            // get list phong ban / thi truong

            // get list phong ban / dh 
            GetListPhongBanDH(); // departoperator (qltour)
            // get list phong ban / dh
            return View(TourVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != TourVM.Tour.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                TourVM.Tour.NgaySua = DateTime.Now;
                TourVM.Tour.NguoiSua = user.Username;

                TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace(',', '-');

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.tourRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.MaKH != TourVM.Tour.MaKH)
                {
                    temp += String.Format("- Hãng thay đổi: {0}->{1}", t.MaKH, TourVM.Tour.MaKH);
                }
                if (t.LoaiTourId != TourVM.Tour.LoaiTourId)
                {
                    temp += String.Format("- Loại tour thay đổi: {0}->{1}", t.LoaiTourId, TourVM.Tour.LoaiTourId);
                }
                if (t.NgayDen != TourVM.Tour.NgayDen)
                {
                    temp += String.Format("- Từ ngày thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayDen, TourVM.Tour.NgayDen);
                }
                if (t.NgayDi != TourVM.Tour.NgayDi)
                {
                    temp += String.Format("- Đến ngày thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayDi, TourVM.Tour.NgayDi);
                }
                if (t.SoKhachTT != TourVM.Tour.SoKhachTT)
                {
                    temp += String.Format("- Số khách thay đổi: {0}->{1}", t.SoKhachTT, TourVM.Tour.SoKhachTT);
                }
                if (t.SKTreEm != TourVM.Tour.SKTreEm)
                {
                    temp += String.Format("- Trẻ em thay đổi: {0}->{1}", t.SKTreEm, TourVM.Tour.SKTreEm);
                }
                if (t.PhongDH != TourVM.Tour.PhongDH)
                {
                    temp += String.Format("- Phòng điều hành thay đổi: {0}->{1}", t.PhongDH, TourVM.Tour.PhongDH);
                }

                if (t.TuyenTQ != TourVM.Tour.TuyenTQ)
                {
                    temp += String.Format("- Tuyến tham quan thay đổi: {0}->{1}", t.TuyenTQ, TourVM.Tour.TuyenTQ);
                }
                if (t.DoanhThuTT != TourVM.Tour.DoanhThuTT)
                {
                    temp += String.Format("- Doanh thu thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.DoanhThuTT, TourVM.Tour.DoanhThuTT);
                }

                var fileCheck = Request.Form.Files;
                if (fileCheck.Count > 0)
                {
                    temp += String.Format("- DS Khách đã thay đổi");
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
                    TourVM.Tour.LogFile = t.LogFile;
                }

                // update tourinf
                Tourinf tourinf = new Tourinf();

                tourinf.Sgtcode = TourVM.Tour.Sgtcode;
                tourinf.Khachle = false;
                tourinf.CompanyId = TourVM.Tour.MaKH;
                tourinf.TourkindId = TourVM.Tour.LoaiTourId;
                tourinf.Arr = TourVM.Tour.NgayDen;
                tourinf.Dep = TourVM.Tour.NgayDi;
                tourinf.Pax = TourVM.Tour.SoKhachTT;
                tourinf.Childern = TourVM.Tour.SKTreEm;
                tourinf.Reference = TourVM.Tour.TuyenTQ;
                tourinf.Concernto = TourVM.Tour.NguoiTao; // nguoi tao tour
                tourinf.Operators = "";
                tourinf.Departoperator = TourVM.Tour.PhongDH; //departoperator : qltour
                tourinf.Departcreate = "IB";
                tourinf.Routing = "";
                //tourinf.Rate = TourVM.Tour.TyGia;
                tourinf.Rate = 1;
                tourinf.Revenue = (TourVM.Tour.DoanhThuTT > 0) ? TourVM.Tour.DoanhThuTT : TourVM.Tour.DoanhThuDK;
                tourinf.PasstypeId = ""; // tourIB ko co'
                tourinf.Currency = TourVM.Tour.LoaiTien;
                tourinf.Chinhanh = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn; // chinhanh trien khai
                tourinf.Chinhanhtao = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhTaoId).Macn; // user login
                tourinf.Createtour = TourVM.Tour.NgayTao;
                tourinf.Logfile = TourVM.Tour.LogFile;
                // update tourinf

                try
                {

                    _unitOfWork.tourRepository.Update(TourVM.Tour);
                    // insert tourinf
                    _unitOfWork.tourInfRepository.Update(tourinf);
                    // insert tourinf
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");

                    // update excel
                    //IFormFile file = Request.Form.Files[0];

                    if (fileCheck.Count > 0)
                    {
                        var khachHangs = _unitOfWork.dSKhachHangRepository.Find(x => x.TourId == TourVM.Tour.Id);
                        // xoa' di de upload lai
                        foreach (var item in khachHangs)
                        {
                            _unitOfWork.dSKhachHangRepository.Delete(item);
                        }
                        await _unitOfWork.Complete();
                        // xoa' di de upload lai
                        UploadExcelAsync(TourVM.Tour.Sgtcode);
                    }
                    // update excel

                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");

                    TourVM = new TourViewModel()
                    {
                        Tour = new Data.Models_IB.Tour(),
                        Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                        Companies = _unitOfWork.khachHangRepository.GetAll(),
                        Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                        Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                        LoaiKhachs = LoaiKhach(),
                        listPhongMacode = new List<Data.Models_QLT.Phongban>(),
                        listPhongDH = new List<Phongban>(),
                        Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                        NguonTours = NguonTour(),
                        StrUrl = strUrl
                    };

                    return View(TourVM);
                }
            }
            // not valid
            TourVM = new TourViewModel()
            {
                Tour = new Data.Models_IB.Tour(),
                Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                Companies = _unitOfWork.khachHangRepository.GetAll(),
                Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                LoaiKhachs = LoaiKhach(),
                listPhongMacode = new List<Data.Models_QLT.Phongban>(),
                listPhongDH = new List<Phongban>(),
                Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                NguonTours = NguonTour(),
                StrUrl = strUrl
            };

            return View(TourVM);
        }

        public IActionResult Details(long id, string strUrl)
        {
            TourVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var tour = _unitOfWork.tourRepository.GetById(id);

            if (tour == null)
                return NotFound();

            TourVM.Tour = tour;
            var tourDto = new TourDto();

            tourDto.Id = tour.Id;
            tourDto.Sgtcode = tour.Sgtcode;
            tourDto.KhachLe = tour.KhachLe;
            tourDto.ChuDeTour = tour.ChuDeTour;
            tourDto.ThiTruong = tour.PhongDH;
            tourDto.NgayKhoa = tour.NgayKhoa;
            tourDto.NguoiKhoa = tour.NguoiKhoa;
            tourDto.NgayTao = tour.NgayTao;
            tourDto.NguoiTao = tour.NguoiTao;
            tourDto.NgayDen = tour.NgayDen;
            tourDto.NgayDi = tour.NgayDi;
            tourDto.TuyenTQ = tour.TuyenTQ;
            tourDto.SoKhachDK = tour.SoKhachDK;
            tourDto.DoanhThuDK = tour.DoanhThuDK;
            //tourDto.CompanyName = companies.Where(x => x.CompanyId == tour.MaKH).FirstOrDefault().Name;
            if (tour.NgayDamPhan.HasValue)
            {
                tourDto.NgayDamPhan = tour.NgayDamPhan.Value;
            }

            tourDto.HinhThucGiaoDich = tour.HinhThucGiaoDich;
            if (tour.NgayKyHopDong.HasValue)
            {
                tourDto.NgayKyHopDong = tour.NgayKyHopDong.Value;
            }

            tourDto.NguoiKyHopDong = tour.NguoiKyHopDong;
            if (tour.HanXuatVe.HasValue)
            {
                tourDto.HanXuatVe = tour.HanXuatVe.Value;
            }
            if (tour.NgayThanhLyHD.HasValue)
            {
                tourDto.NgayThanhLyHD = tour.NgayThanhLyHD.Value;
            }

            tourDto.SoKhachTT = tour.SoKhachTT;
            tourDto.SKTreEm = tour.SKTreEm;
            tourDto.DoanhThuTT = tour.DoanhThuTT;
            tourDto.ChuongTrinhTour = tour.ChuongTrinhTour;
            tourDto.NoiDungThanhLyHD = tour.NoiDungThanhLyHD;
            tourDto.DichVu = tour.DichVu;
            tourDto.DaiLy = tour.DaiLy;
            tourDto.TrangThai = tour.TrangThai;
            tourDto.NgaySua = tour.NgaySua;
            tourDto.NguoiSua = tour.NguoiSua;
            tourDto.TenLoaiTour = (tour.LoaiTourId == 0) ? "" : _unitOfWork.tourKindRepository.GetById(tour.LoaiTourId.Value).TourkindInf;
            tourDto.MaCNTao = (tour.ChiNhanhTaoId == 0) ? "" : TourVM.Dmchinhanhs.Where(x => x.Id == tour.ChiNhanhTaoId).FirstOrDefault().Macn;
            if (tour.NgayNhanDuTien.HasValue)
            {
                tourDto.NgayNhanDuTien = tour.NgayNhanDuTien.Value;
            }

            tourDto.LyDoNhanDu = tour.LyDoNhanDu;
            tourDto.SoHopDong = tour.SoHopDong;
            tourDto.LaiChuaVe = tour.LaiChuaVe;
            tourDto.LaiGomVe = tour.LaiGomVe;
            tourDto.LaiThucTeGomVe = tour.LaiThucTeGomVe;
            tourDto.NguonTour = tour.NguonTour;
            tourDto.FileKhachDiTour = tour.FileKhachDiTour;
            tourDto.FileVeMayBay = tour.FileVeMayBay;
            tourDto.FileBienNhan = tour.FileBienNhan;
            tourDto.NguoiDaiDien = tour.NguoiDaiDien;
            tourDto.DoiTacNuocNgoai = tour.DoiTacNuocNgoai;
            tourDto.MaCNDH = TourVM.Dmchinhanhs.Where(x => x.Id == tour.ChiNhanhDHId).FirstOrDefault().Macn;
            if (tour.NgayHuyTour.HasValue)
            {
                tourDto.NgayHuyTour = tour.NgayHuyTour.Value;
            }

            tourDto.NDHuyTour = (tour.NDHuyTourId == 0) ? "" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(tour.NDHuyTourId).NoiDung;
            tourDto.GhiChu = tour.GhiChu;
            tourDto.LoaiTien = tour.LoaiTien;
            tourDto.TyGia = tour.TyGia;
            tourDto.LogFile = tour.LogFile;

            TourVM.TourDto = tourDto;
            return View(TourVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl)
        {
            var tour = _unitOfWork.tourRepository.GetById(id);
            if (tour == null)
                return NotFound();

            // tourinf
            //var tourInf = await _unitOfWork.tourInfRepository.GetByIdAsync(tour.Sgtcode);
            //if (tourInf == null)
            //    return NotFound();
            // tourinf

            try
            {
                // _unitOfWork.tourInfRepository.Delete(tourInf);
                _unitOfWork.tourRepository.Delete(tour);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(strUrl);
            }
        }

        private List<ListViewModel> Visa()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel(){id = 1, Name = "Không có"},
                new ListViewModel(){id = 2, Name = "Nước ngoài"},
                new ListViewModel(){id = 3, Name = "Cửa khẩu"},
            };

        }

        private List<ListViewModel> NguonTour()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel(){id = 1, Name = "Nội bộ" },
                new ListViewModel(){id = 2, Name = "TMDT" },

            };
        }

        private List<ListViewModel> LoaiKhach()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel(){id = 1, Name = "INBOUND" },
                new ListViewModel(){id = 2, Name = "TÀU BIỂN" },

            };
        }

        public async Task<JsonResult> GetKHByMaKH(string maKH)
        {
            var khachHang = JsonConvert.SerializeObject(await _unitOfWork.khachHangRepository.GetByIdAsync(maKH));
            return Json(new
            {
                khachHang = khachHang
            });
        }

        private void GetListPhongBanMacode(string maPhong)
        {

            // get list maphong after distinct space and split ','
            var phongbans = _unitOfWork.phongBanRepository.GetAll().Where(x => x.Maphong == maPhong);// chi lay macode theo maphong user login
            var maPhongs = phongbans.Select(x => x.Macode).Distinct();

            var listString = new List<string>();
            foreach (var item in maPhongs)
            {
                var itemArray = item.Split(',');
                for (int i = 0; i < itemArray.Length; i++)
                {
                    listString.Add(itemArray[i]);

                }
            }

            int j = 1;
            foreach (var maCode in listString)
            {
                TourVM.listPhongMacode.Add(new Phongban()
                {
                    Maphong = phongbans.Where(x => x.Macode.Contains(maCode)).FirstOrDefault().Maphong,
                    Macode = maCode
                });

            }
            // get list maphong after distinct space and split ','
        }

        private void GetListPhongBanDH()
        {

            // get list maphong after distinct space and split ','
            var phongbans = _unitOfWork.phongBanRepository.GetAll();

            TourVM.listPhongDH = phongbans.Where(x => !string.IsNullOrEmpty(x.Macode)).ToList();
            // get list maphong after distinct space and split ','
        }


        [HttpPost]
        public void UploadExcelAsync(string sgtCode)
        {
            var tour = _unitOfWork.tourRepository.Find(x => x.Sgtcode == sgtCode).FirstOrDefault();
            IFormFile file = Request.Form.Files[0];
            string folderName = "excelfolder";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            string folderPath = webRootPath + @"\excelfolder\";
            FileInfo fileInfo = new FileInfo(Path.Combine(folderPath, file.FileName));

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                    //var list = workSheet.Cells.ToList();
                    //var table = workSheet.Tables.ToList(); 
                    int totalRows = workSheet.Dimension.Rows;

                    List<KhachHang> khachHangs = new List<KhachHang>();

                    for (int i = 2; i <= totalRows; i++)
                    {
                        var khachHang = new KhachHang();

                        if (workSheet.Cells[i, 1].Value != null)
                            khachHang.STT = int.Parse(workSheet.Cells[i, 1].Value.ToString());

                        if (workSheet.Cells[i, 2].Value != null)
                            khachHang.MaKH = workSheet.Cells[i, 2].Value.ToString();

                        if (workSheet.Cells[i, 3].Value != null)
                            khachHang.TenKH = workSheet.Cells[i, 3].Value.ToString();

                        if (workSheet.Cells[i, 4].Value != null)
                            khachHang.NgaySinh = DateTime.Parse(workSheet.Cells[i, 4].Value.ToString());

                        if (workSheet.Cells[i, 5].Value != null)
                            khachHang.GioiTinh = (workSheet.Cells[i, 5].Value.ToString() == "Nam") ? true : false;

                        if (workSheet.Cells[i, 6].Value != null)
                            khachHang.QuocTich = workSheet.Cells[i, 6].Value.ToString();

                        if (workSheet.Cells[i, 7].Value != null)
                            khachHang.HoChieu = workSheet.Cells[i, 7].Value.ToString();

                        if (workSheet.Cells[i, 8].Value != null)
                            khachHang.CMND = int.Parse(workSheet.Cells[i, 8].Value.ToString());

                        if (workSheet.Cells[i, 9].Value != null)
                            khachHang.LoaiPhong = workSheet.Cells[i, 9].Value.ToString();

                        if (workSheet.Cells[i, 10].Value != null)
                            khachHang.DiaChi = workSheet.Cells[i, 10].Value.ToString();

                        if (workSheet.Cells[i, 11].Value != null)
                            khachHang.Visa = workSheet.Cells[i, 11].Value.ToString();

                        if (workSheet.Cells[i, 12].Value != null)
                            khachHang.YeuCauVisa = workSheet.Cells[i, 12].Value.ToString();

                        //if (workSheet.Cells[i, 20].Value != null)
                        khachHang.TourId = tour.Id;

                        khachHangs.Add(khachHang);
                    }

                    //_db.Customers.AddRange(customerList);
                    //_db.SaveChanges();
                    try
                    {
                        _unitOfWork.dSKhachHangRepository.CreateRange(khachHangs);
                        _unitOfWork.khachTour
                        //await _unitOfWork.Complete();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            if (System.IO.File.Exists(fileInfo.ToString()))
                System.IO.File.Delete(fileInfo.ToString());

            //return Json(new
            //{
            //    status = true
            //});
        }

    }
}