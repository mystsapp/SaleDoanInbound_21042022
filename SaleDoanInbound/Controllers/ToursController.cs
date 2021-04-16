﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Services;
using Data.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using OfficeOpenXml;
using SaleDoanInbound.Models;
//using Xceed.Document.NET;
using Xceed.Words.NET;
using Novacode;

namespace SaleDoanInbound.Controllers
{
    public class ToursController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITourService _tourService;
        private readonly IUserQLTourService _userQLTourService;

        [BindProperty]
        public TourViewModel TourVM { get; set; }

        public ToursController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ITourService tourService, IUserQLTourService userQLTourService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _tourService = tourService;
            _userQLTourService = userQLTourService;

            TourVM = new TourViewModel()
            {
                Tour = new Data.Models_IB.Tour(),
                Invoice = new Invoice(),
                Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                Companies = _unitOfWork.khachHangRepository.GetAll(),
                Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                NguonTours = NguonTour(),
                LoaiKhachs = LoaiKhach(),
                listPhongMacode = new List<Data.Models_QLT.Phongban>(),
                listPhongDH = new List<Phongban>(),
                ListCPKhac = new List<ChiPhiKhachDto>(),
                Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                TourDto = new TourDto()
            };
        }
        public async Task<IActionResult> Index(long id = 0, string searchString = null, int page = 1, string searchFromDate = null, string searchToDate = null)
        {
            if(string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate)) // moi load vao
            {
                var fromToDate = GetDate.LoadTuNgayDenNgay(DateTime.Now.Month.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                searchFromDate = fromToDate.Split('-')[0];
                searchToDate = fromToDate.Split('-')[1];
            }

            TourVM.StrUrl = UriHelper.GetDisplayUrl(Request);

            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            // cat bo phai sau % --> too long error
            var newStrUrl = TourVM.StrUrl.Split("+%");
            if (newStrUrl.Length > 1)
            {
                TourVM.StrUrl = newStrUrl[0];
            }

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate; // ngay bat dau
            ViewBag.searchToDate = searchToDate; // ngay ket thuc

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
            List<string> listRoleChiNhanh = new List<string>();
            List<string> phongBansQL = new List<string>();
            var users = _unitOfWork.userRepository.GetAll().ToList();

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    listRoleChiNhanh.Add(user.MaCN);
                    users = users.Where(x => x.PhongBanId == user.PhongBanId).ToList();
                }
                else
                {
                    // add chinhanhs in PhanKhuCN
                    var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(user.RoleId);
                    if (phanKhuCN == null)
                    {
                        ModelState.AddModelError("", "Role của user này chưa được add chi nhánh.");
                        return View(TourVM);
                    }
                    listRoleChiNhanh.AddRange(phanKhuCN.ChiNhanhs.Split(','));
                    if (!string.IsNullOrEmpty(user.PhongBans)) // phongbans trong ==> ql het
                    {
                        //listRoleChiNhanh = new List<string>(); // neu co ql phong ban nao do' ==> tinh theo phongban QL
                        phongBansQL = user.PhongBans.Split(',').ToList();
                        users = users.Where(item1 => phongBansQL.Any(item2 => item1.PhongBanId == item2)).ToList();
                    }

                }
            }
            else
            {
                foreach (var item in chiNhanhs)
                {
                    listRoleChiNhanh.Add(item.Macn);
                }
            }

            TourVM.TourDtos = _unitOfWork.tourRepository.ListTour(searchString,
                                                                  companies,
                                                                  loaiTours,
                                                                  chiNhanhs,
                                                                  cacNoiDungHuyTours,
                                                                  page,
                                                                  searchFromDate,
                                                                  searchToDate,
                                                                  listRoleChiNhanh,
                                                                  users.Select(x => x.Username).ToList());


            //--> click vao tour
            var tour = _unitOfWork.tourRepository.GetById(id);
            if (tour != null)
            {
                TourVM.Tour = tour;
                TourVM.listTourProgAsync = await listTourProgAsync(tour.Sgtcode);
                TourVM.TourNoteAsync = await TourNoteAsync(tour.Sgtcode);
                TourVM.ListDsKhach = ListDsKhach(tour.Sgtcode);
                TourVM.ListCPKhac = await ListCPKhac(tour.Sgtcode);
                TourVM.ListYeucauxe = await ListYeucauxe(tour.Sgtcode);
                TourVM.ListHuongdan = ListHuongdan(tour.Sgtcode);

                // DS invoice theo tour --> appear invoices and biennhan tabs
                TourVM.Invoices = _unitOfWork.invoiceRepository.Find(x => x.TourId == id);

            }
            //--> click vao tour

            return View(TourVM);
        }

        public async Task<IActionResult> KeToan_TourInfoByTourPartial(long tourId)
        {
            var tour = _unitOfWork.tourRepository.GetById(tourId);

            // tour program in qltour
            TourVM.listTourProgAsync = await listTourProgAsync(tour.Sgtcode);
            TourVM.TourNoteAsync = await TourNoteAsync(tour.Sgtcode);
            TourVM.ListDsKhach = ListDsKhach(tour.Sgtcode);
            TourVM.ListCPKhac = await ListCPKhac(tour.Sgtcode);
            TourVM.ListYeucauxe = await ListYeucauxe(tour.Sgtcode);
            TourVM.ListHuongdan = ListHuongdan(tour.Sgtcode);

            // KeToan
            TourVM.Tour = tour;
            TourVM.Invoices = _unitOfWork.invoiceRepository.ListInvoice("", tourId).OrderByDescending(x => x.Date);
            TourVM.BienNhans = _unitOfWork.bienNhanRepository.ListBienNhan("", tourId, "", "");

            return PartialView(TourVM);
        }

        public IActionResult BienNhanAndCTBNPartial(long tourId)
        {
            // KeToan
            TourVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            TourVM.BienNhans = _unitOfWork.bienNhanRepository.ListBienNhan("", tourId, "", "");

            return PartialView(TourVM);
        }

        public IActionResult Create(string strUrl)
        {

            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            
            if (user.Role.RoleName == "KeToans")
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
            if (user.PhongBanId == "KDOB")
            {
                
                TourVM.Tour.MaKH = "50001";
                TourVM.Tour.TenKH = "DU LICH NOI DIA";
            }
            TourVM.StrUrl = strUrl;
            TourVM.Tour.SoHopDong = "";
            ViewBag.chiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id;

            //ViewBag.tuyenTQ = "BAL,BAN"; //"[BAL,BAN]"; // for test
            // get list phong ban / thi truong
            GetListPhongBanMacode(user.PhongBanId); // sinh ma cho sgtgode / phongbanid = maphong in qltour
                                                    // get list phong ban / thi truong

            // get list phong ban / dh 
            //TourVM.listPhongDH = GetListPhongBanDH(); // departoperator (qltour)
            TourVM.listPhongDH = GetListPhongBanDH().Where(x => x.Maphong == "DH" || x.Maphong == "TB" || x.Maphong == "KDOB").ToList();
            // get list phong ban / dh

            // CompaniesViewModel
            var listCompany = new List<ListViewModel>();
            foreach (var item in TourVM.Companies)
            {
                listCompany.Add(new ListViewModel() { CompanyId = item.CompanyId, CompanyName = item.CompanyId + " - " + item.Name });
            }
            TourVM.CompaniesViewModel = listCompany;
            // CompaniesViewModel
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

            // kiem tra tungay > denngay
            if (TourVM.Tour.NgayDen > TourVM.Tour.NgayDi)
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
                ModelState.AddModelError("", "Ngày bắt đầu không được lớn hơn ngày kết thúc");
                return View(TourVM);
            }
            // kiem tra tungay > denngay

            // kiem tra trang thai
            TourVM.Tour.TrangThai = "0";//mac dinh la moi tao

            if (TourVM.Tour.NgayDamPhan != null) // da dam phan
            {
                TourVM.Tour.TrangThai = "1";
            }

            if (TourVM.Tour.NgayKyHopDong != null) // da ky HD
            {
                TourVM.Tour.TrangThai = "2";
            }

            if (TourVM.Tour.NgayThanhLyHD != null) // da thanh ly
            {
                TourVM.Tour.TrangThai = "3";
            }
            // kiem tra trang thai

            //TourVM.Tour = new Data.Models_IB.Tour();
            TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace(',', '-');

            TourVM.Tour.ChiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id;
            TourVM.Tour.NgayTao = DateTime.Now;
            TourVM.Tour.NguoiTao = user.Username;
            TourVM.Tour.ChuDeTour = TourVM.Tour.ChuDeTour.ToUpper();

            // next SoHopdong --> bat buoc phai co'
            if (TourVM.Tour.NgayKyHopDong != null)
            {
                DateTime dateTime;
                //dateTime = DateTime.Now;
                dateTime = TourVM.Tour.NgayKyHopDong.Value;

                var currentYear = dateTime.Year;
                var subfix = "/IB/" + currentYear.ToString();
                var tour = _unitOfWork.tourRepository.GetAllAsNoTracking().OrderByDescending(x => x.SoHopDong).ToList().FirstOrDefault();
                if (string.IsNullOrEmpty(tour.SoHopDong))
                {
                    TourVM.Tour.SoHopDong = GetNextId.NextID("", "") + subfix;
                }
                else
                {
                    var oldYear = tour.SoHopDong.Substring(10, 4);
                    // cung nam
                    if (oldYear == currentYear.ToString())
                    {
                        var oldSoHopdong = tour.SoHopDong.Substring(0, 6);
                        TourVM.Tour.SoHopDong = GetNextId.NextID(oldSoHopdong, "") + subfix;
                    }
                    else
                    {
                        // sang nam khac' chay lai tu dau
                        TourVM.Tour.SoHopDong = GetNextId.NextID("", "") + subfix;
                    }

                }
            }
            else
            {
                TourVM.Tour.SoHopDong = "";
            }
            
            // next SoHopdong

            //if (string.IsNullOrEmpty(TourVM.Tour.SoHopDong))
            //{
            //    TourVM.Tour.SoHopDong = "";
            //}
            TourVM.Tour.NguoiTao = user.Username;

            // create sgtcode
            var companies = await _unitOfWork.khachHangRepository.FindAsync(x => x.CompanyId == TourVM.Tour.MaKH); // find company by MaKH(companyId)
            var quocgias = await _unitOfWork.quocGiaRepository.FindAsync(x => x.Nation == companies.FirstOrDefault().Nation); // find by nation(vn)
            string sgtCode = "";
            //if (user.PhongBanId == "TF") // FRONT DESK
            if (quocgias.FirstOrDefault().Telcode == "000") // FRONT DESK
            {
                sgtCode = _tourService.newSgtcode(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, "000"); // 000 --> macode cua front desk
            }

            // KDOB
            if (user.PhongBanId == "KDOB")
            {
                sgtCode = _tourService.newSgtcodeKDOB(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, quocgias.FirstOrDefault().Telcode);
            }
            else // nhung thi truong khac' lay theo telcode cua quocgia
            {
                sgtCode = _tourService.newSgtcode(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, quocgias.FirstOrDefault().Telcode);
            }

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
            tourinf.Reference = TourVM.Tour.ChuDeTour;
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

                // insert tourlewi
                if (quocgias.FirstOrDefault().Telcode == "000") // FRONT DESK
                {
                    // insert 
                    Data.Models_Tourlewi.Tour tourlewi = new Data.Models_Tourlewi.Tour();
                    tourlewi.Sgtcode = TourVM.Tour.Sgtcode;
                    tourlewi.Khachle = false;
                    tourlewi.Makh = TourVM.Tour.MaKH;
                    tourlewi.Loaitour = TourVM.Tour.LoaiTourId.ToString();
                    tourlewi.Batdau = TourVM.Tour.NgayDen;
                    tourlewi.Ketthuc = TourVM.Tour.NgayDi;
                    tourlewi.Socho = TourVM.Tour.SoKhachTT;
                    tourlewi.Chudetour = TourVM.Tour.ChuDeTour;
                    tourlewi.Tuyentq = TourVM.Tour.TuyenTQ;
                    tourlewi.Nguoitaotour = user.Username; // nguoi tao tour
                                                           //tourlewi.Operators = ""; // nguoi dieu hanh
                                                           //tourlewi.Departoperator = TourVM.Tour.PhongDH; //departoperator : qltour / phong dh
                                                           //tourlewi.Departcreate = user.PhongBanId; // phong ban tao
                                                           //tourlewi.Routing = "";
                                                           //tourlewi.Rate = TourVM.Tour.TyGia;
                                                           //tourlewi.Revenue = (TourVM.Tour.DoanhThuTT > 0) ? TourVM.Tour.DoanhThuTT : TourVM.Tour.DoanhThuDK;
                                                           //tourlewi.PasstypeId = TourVM.Tour.LoaiKhach; // Inbound or tau bien
                                                           //tourlewi.Currency = TourVM.Tour.LoaiTien;
                    tourlewi.Chinhanh = user.MaCN; // chinhanh tao
                                                   //tourlewi.Chinhanhtao = user.MaCN; // user login
                    tourlewi.Ngaytao = TourVM.Tour.NgayTao;
                    tourlewi.Logfile = TourVM.Tour.LogFile;
                    // insert tourlewi

                    _unitOfWork.tourWIRepository.Create(tourlewi);
                }
                //else
                //{
                //    //_unitOfWork.tourInfRepository.Create(tourinf);
                //    //// insert tourinf
                //    ////await _unitOfWork.Complete();

                //}
                // insert tourlewi
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");


                var fileCheck = Request.Form.Files;
                if (fileCheck.Count > 0)
                {

                    // upload excel
                    UploadExcelAsync(TourVM.Tour.Sgtcode);
                    // upload excel

                }

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
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            if (user.Role.RoleName == "KeToans")
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
            var chiNhanhDHId = _unitOfWork.tourRepository.GetById(id).ChiNhanhDHId;
            // user logon khac user DH
            if (user.MaCN != TourVM.Dmchinhanhs.Where(x => x.Id == chiNhanhDHId).FirstOrDefault().Macn)
            {
                return RedirectToAction(nameof(Details), new { id, strUrl });
            }
            ViewBag.chiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id; // for compare

            TourVM.StrUrl = strUrl;
            if (id == 0)
            {
                ViewBag.ErrorMessage = "Tour này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            TourVM.Tour = await _unitOfWork.tourRepository.GetByLongIdAsync(id);
            //TourVM.InvoicesInTour = await _unitOfWork.invoiceRepository.FindAsync(x => x.TourId == id);
            ViewBag.maCn = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn; // for view

            if (TourVM.Tour == null)
            {
                ViewBag.ErrorMessage = "Tour này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }


            // gang qua hid tuyentq
            TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace('-', ',');
            // gang qua hid tuyentq

            // get list phong ban / thi truong
            TourVM.listPhongDH = GetListPhongBanDH(); // departoperator (qltour)
                                                      // get list phong ban / thi truong

            //// get list phong ban / dh 
            //GetListPhongBanDH(); // departoperator (qltour)
            //// get list phong ban / dh

            // get list phong ban / dh 
            //TourVM.listPhongDH = GetListPhongBanDH(); // departoperator (qltour)
            TourVM.listPhongDH = GetListPhongBanDH().Where(x => x.Maphong == "DH" || x.Maphong == "TB" || x.Maphong == "KDOB").ToList();
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
            {
                ViewBag.ErrorMessage = "Tour này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (ModelState.IsValid)
            {
                TourVM.Tour.NgaySua = DateTime.Now;
                TourVM.Tour.NguoiSua = user.Username;

                TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace(',', '-');
                TourVM.Tour.ChuDeTour = TourVM.Tour.ChuDeTour.ToUpper();

                // kiem tra trang thai

                if (TourVM.Tour.NgayDamPhan != null)
                {
                    TourVM.Tour.TrangThai = "1";
                }

                if (TourVM.Tour.NgayKyHopDong != null)
                {
                    TourVM.Tour.TrangThai = "2";
                }

                if (TourVM.Tour.NgayThanhLyHD != null)
                {
                    TourVM.Tour.TrangThai = "3";
                }

                TourVM.Tour.TrangThai = TourVM.Tour.TrangThai ?? "0";
                // kiem tra trang thai

                // next SoHopdong --> bat buoc phai co'                
                if (string.IsNullOrEmpty(TourVM.Tour.SoHopDong) && 
                    TourVM.Tour.NgayKyHopDong != null) // SoHopDong = rong~ va` co' NgayKyHopDong
                {
                    DateTime dateTime;
                    // dateTime = DateTime.Now;
                    dateTime = TourVM.Tour.NgayKyHopDong.Value;

                    var currentYear = dateTime.Year;
                    var subfix = "/IB/" + currentYear.ToString();
                    var tour = _unitOfWork.tourRepository.GetAllAsNoTracking().OrderByDescending(x => x.SoHopDong).ToList().FirstOrDefault();
                    if (string.IsNullOrEmpty(tour.SoHopDong))
                    {
                        TourVM.Tour.SoHopDong = GetNextId.NextID("", "") + subfix;
                    }
                    else
                    {
                        var oldYear = tour.SoHopDong.Substring(10, 4);
                        // cung nam
                        if (oldYear == currentYear.ToString())
                        {
                            var oldSoHopdong = tour.SoHopDong.Substring(0, 6);
                            TourVM.Tour.SoHopDong = GetNextId.NextID(oldSoHopdong, "") + subfix;
                        }
                        else
                        {
                            // sang nam khac' chay lai tu dau
                            TourVM.Tour.SoHopDong = GetNextId.NextID("", "") + subfix;
                        }

                    }
                }
                //else
                //{
                //    TourVM.Tour.SoHopDong = "";
                //}

                // next SoHopdong

                TourVM.Tour.SoHopDong = TourVM.Tour.SoHopDong ?? "";

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.tourRepository.GetSingleNoTracking(x => x.Id == id);
                
                if (t.ChiNhanhDHId != TourVM.Tour.ChiNhanhDHId)
                {
                    temp += String.Format("- CN DH thay đổi: {0}->{1}", _unitOfWork.dmChiNhanhRepository.GetById(t.ChiNhanhDHId).Macn, _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn);
                }

                if (t.PhongDH != TourVM.Tour.PhongDH)
                {
                    temp += String.Format("- Phòng điều hành thay đổi: {0}->{1}", t.PhongDH, TourVM.Tour.PhongDH);
                }

                if (t.SoHopDong != TourVM.Tour.SoHopDong)
                {
                    temp += String.Format("- Số HD thay đổi: {0}->{1}", t.SoHopDong, TourVM.Tour.SoHopDong);
                }
                if (t.NgayDen != TourVM.Tour.NgayDen)
                {
                    temp += String.Format("- Từ ngày thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayDen, TourVM.Tour.NgayDen);
                }
                if (t.NgayDi != TourVM.Tour.NgayDi)
                {
                    temp += String.Format("- Đến ngày thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayDi, TourVM.Tour.NgayDi);
                }
                if (t.ChuDeTour != TourVM.Tour.ChuDeTour)
                {
                    temp += String.Format("- Chủ đề tour thay đổi: {0}->{1}", t.ChuDeTour, TourVM.Tour.ChuDeTour);
                }

                if (t.TuyenTQ != TourVM.Tour.TuyenTQ)
                {
                    temp += String.Format("- Tuyến tham quan thay đổi: {0}->{1}", t.TuyenTQ, TourVM.Tour.TuyenTQ);
                }
                if (t.SoKhachDK != TourVM.Tour.SoKhachDK)
                {
                    temp += String.Format("- Số khách DK thay đổi: {0}->{1}", t.SoKhachDK, TourVM.Tour.SoKhachDK);
                }
                if (t.DoanhThuDK != TourVM.Tour.DoanhThuDK)
                {
                    temp += String.Format("- Doanh thu DK thay đổi: {0:N0}->{1:N0}", t.DoanhThuDK, TourVM.Tour.DoanhThuDK);
                }
                if (t.SoKhachTT != TourVM.Tour.SoKhachTT)
                {
                    temp += String.Format("- Số khách TT thay đổi: {0}->{1}", t.SoKhachTT, TourVM.Tour.SoKhachTT);
                }
                if (t.SKTreEm != TourVM.Tour.SKTreEm)
                {
                    temp += String.Format("- SK trẻ em thay đổi: {0}->{1}", t.SKTreEm, TourVM.Tour.SKTreEm);
                }
                if (t.DoanhThuTT != TourVM.Tour.DoanhThuTT)
                {
                    temp += String.Format("- Doanh thu TT thay đổi: {0:N0}->{1:N0}", t.DoanhThuTT, TourVM.Tour.DoanhThuTT);
                }
                if (t.NguonTour != TourVM.Tour.NguonTour)
                {
                    temp += String.Format("- Nguồn tour thay đổi: {0}->{1}", t.NguonTour, TourVM.Tour.NguonTour);
                }
                if (t.LoaiTourId != TourVM.Tour.LoaiTourId)
                {
                    temp += String.Format("- Loại tour thay đổi: {0}->{1}",
                        (t.LoaiTourId == 0) ? "0" : _unitOfWork.tourKindRepository.GetById(t.LoaiTourId.Value).TourkindInf,
                        (TourVM.Tour.LoaiTourId == 0) ? "0" : _unitOfWork.tourKindRepository.GetById(TourVM.Tour.LoaiTourId.Value).TourkindInf);
                }
                if (t.LoaiTien != TourVM.Tour.LoaiTien)
                {
                    temp += String.Format("- Loại tour thay đổi: {0}->{1}", t.LoaiTien, TourVM.Tour.LoaiTien);
                }
                if (t.TyGia != TourVM.Tour.TyGia)
                {
                    temp += String.Format("- Loại tiền thay đổi: {0:N0}->{1:N0}", t.TyGia, TourVM.Tour.TyGia);
                }
                if (t.LoaiKhach != TourVM.Tour.LoaiKhach)
                {
                    temp += String.Format("- Loại khách thay đổi: {0}->{1}", t.LoaiKhach, TourVM.Tour.LoaiKhach);
                }
                if (t.MaKH != TourVM.Tour.MaKH)
                {
                    temp += String.Format("- Hãng thay đổi: {0}->{1}", t.MaKH, TourVM.Tour.MaKH);
                }
                if (t.NgayDamPhan != TourVM.Tour.NgayDamPhan)
                {
                    temp += String.Format("- Ngày đàm phán thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayDamPhan, TourVM.Tour.NgayDamPhan);
                }
                if (t.NgayKyHopDong != TourVM.Tour.NgayKyHopDong)
                {
                    temp += String.Format("- Ngày ký HD thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayKyHopDong, TourVM.Tour.NgayKyHopDong);
                }
                if (t.NguoiKyHopDong != TourVM.Tour.NguoiKyHopDong)
                {
                    temp += String.Format("- Người ký HD thay đổi: {0}->{1}", t.NguoiKyHopDong, TourVM.Tour.NguoiKyHopDong);
                }
                if (t.NguoiDaiDien != TourVM.Tour.NguoiDaiDien)
                {
                    temp += String.Format("- Người đại diện thay đổi: {0}->{1}", t.NguoiDaiDien, TourVM.Tour.NguoiDaiDien);
                }
                if (t.DoiTacNuocNgoai != TourVM.Tour.DoiTacNuocNgoai)
                {
                    temp += String.Format("- Đối tác nước ngoài thay đổi: {0}->{1}", t.DoiTacNuocNgoai, TourVM.Tour.DoiTacNuocNgoai);
                }
                if (t.HinhThucGiaoDich != TourVM.Tour.HinhThucGiaoDich)
                {
                    temp += String.Format("- Hình thức giao dịch thay đổi: {0}->{1}", t.HinhThucGiaoDich, TourVM.Tour.HinhThucGiaoDich);
                }

                if (t.NgayThanhLyHD != TourVM.Tour.NgayThanhLyHD)
                {
                    //temp += String.Format("- Ngày thanh lý HD thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.NgayThanhLyHD, TourVM.Tour.NgayThanhLyHD);
                    temp += String.Format("- Ngày thanh lý HD thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayThanhLyHD, TourVM.Tour.NgayThanhLyHD);
                }
                if (t.LyDoNhanDu != TourVM.Tour.LyDoNhanDu)
                {
                    temp += String.Format("- Hình thức giao dịch thay đổi: {0}->{1}", t.HinhThucGiaoDich, TourVM.Tour.HinhThucGiaoDich);
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
                tourinf.Reference = TourVM.Tour.ChuDeTour;
                tourinf.Concernto = TourVM.Tour.NguoiTao; // nguoi tao tour
                tourinf.Operators = "";
                tourinf.Departoperator = TourVM.Tour.PhongDH; //departoperator : qltour
                //tourinf.Departcreate = "IB";
                tourinf.Departcreate = user.PhongBanId;
                tourinf.Routing = "";
                //tourinf.Rate = TourVM.Tour.TyGia;
                tourinf.Rate = 1;
                tourinf.Revenue = (TourVM.Tour.DoanhThuTT > 0) ? TourVM.Tour.DoanhThuTT : TourVM.Tour.DoanhThuDK;
                tourinf.PasstypeId = ""; // tourIB ko co'
                tourinf.Currency = TourVM.Tour.LoaiTien;
                tourinf.Chinhanh = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn; // chinhanh trien khai
                tourinf.Chinhanhtao = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhTaoId).Macn; // user login
                tourinf.Createtour = TourVM.Tour.NgayTao;
                tourinf.Logfile += TourVM.Tour.LogFile;
                if (TourVM.Tour.NgayHuyTour.HasValue)
                {
                    tourinf.Cancel = TourVM.Tour.NgayHuyTour;
                }
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
                        var khachTours = _unitOfWork.khachTourRepository.Find(x => x.Sgtcode == t.Sgtcode);
                        // xoa' di de upload lai
                        foreach (var item in khachHangs) // ds khachhang trong model_ib
                        {
                            _unitOfWork.dSKhachHangRepository.Delete(item);
                        }
                        foreach (var item in khachTours) // ds khachhang trong model_qlt - khachtour
                        {
                            _unitOfWork.khachTourRepository.Delete(item);
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
                    var tourId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id; // for compare
                    ViewBag.chiNhanhTaoId = tourId;
                    TourVM = new TourViewModel();

                    TourVM.Tour = _unitOfWork.tourRepository.GetById(id);
                    TourVM.Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll();
                    TourVM.Companies = _unitOfWork.khachHangRepository.GetAll();
                    TourVM.Tourkinds = _unitOfWork.tourKindRepository.GetAll();
                    TourVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll();
                    TourVM.LoaiKhachs = LoaiKhach();
                    TourVM.listPhongMacode = new List<Data.Models_QLT.Phongban>();
                    TourVM.listPhongDH = GetListPhongBanDH();
                    TourVM.Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll();
                    TourVM.NguonTours = NguonTour();
                    TourVM.StrUrl = strUrl;

                    return View(TourVM);
                }
            }
            // not valid
            ViewBag.chiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id; // for compare
            TourVM = new TourViewModel()
            {
                Tour = await _unitOfWork.tourRepository.GetByLongIdAsync(id),
                Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                Companies = _unitOfWork.khachHangRepository.GetAll(),
                Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                LoaiKhachs = LoaiKhach(),
                listPhongMacode = new List<Data.Models_QLT.Phongban>(),
                listPhongDH = GetListPhongBanDH(),
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
            {
                ViewBag.ErrorMessage = "Tour này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            var tour = _unitOfWork.tourRepository.GetById(id);

            if (tour == null)
                return NotFound();

            TourVM.Tour = tour;

            TourVM.TourDto = TourDtoReturn(tour); // tourdto

            return View(TourVM);
        }

        private TourDto TourDtoReturn(Tour tour)
        {
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
            tourDto.HuyTour = tour.HuyTour;
            tourDto.NDHuyTour = (tour.NDHuyTourId == 0) ? "" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(tour.NDHuyTourId).NoiDung;
            tourDto.GhiChu = tour.GhiChu;
            tourDto.LoaiTien = tour.LoaiTien;
            tourDto.TyGia = tour.TyGia;
            tourDto.LogFile = tour.LogFile;

            return tourDto;
        }

        [HttpGet, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl, bool huy)
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

        private List<Phongban> GetListPhongBanDH()
        {

            // get list maphong after distinct space and split ','
            var phongbans = _unitOfWork.phongBanRepository.GetAll();

            return phongbans.Where(x => !string.IsNullOrEmpty(x.Macode)).ToList();
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
                        {
                            DateTime ngaySinh;
                            try
                            {
                                ngaySinh = DateTime.Parse(workSheet.Cells[i, 4].Value.ToString());
                                khachHang.NgaySinh = ngaySinh;
                            }
                            catch (Exception ex)
                            {
                                khachHang.NgaySinh = null;
                            }
                        }
                            

                        if (workSheet.Cells[i, 5].Value != null)
                            khachHang.GioiTinh = (workSheet.Cells[i, 5].Value.ToString().ToLower() == "nam") ? true : false;

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
                        khachHang.Sgtcode = sgtCode;

                        khachHangs.Add(khachHang);
                    }

                    var abc = "";

                    //_db.Customers.AddRange(customerList);
                    //_db.SaveChanges();
                    try
                    {
                        _unitOfWork.dSKhachHangRepository.CreateRange(khachHangs); // and savechange
                        List<Khachtour> khachtours = new List<Khachtour>();
                        foreach (var item in khachHangs)
                        {
                            khachtours.Add(new Khachtour()
                            {
                                Sgtcode = sgtCode,
                                Stt = item.STT,
                                Makh = item.MaKH,
                                Hoten = item.TenKH,
                                Ngaysinh = item.NgaySinh,
                                Phai = item.GioiTinh,
                                Diachi = item.DiaChi,
                                Quoctich = item.QuocTich,
                                Loaiphong = item.LoaiPhong,
                                Cmnd = item.CMND.ToString(),
                                Hochieu = item.HoChieu,
                                Del = false,
                                Visa = item.Visa,
                                YeuCauVisa = item.YeuCauVisa
                            });
                        }
                        _unitOfWork.khachTourRepository.CreateRange(khachtours);// and savechange
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

        public FileResult DownloadExcel()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string folderPath = webRootPath + @"\Doc\";
            string newPath = Path.Combine(webRootPath, folderPath, "DSKhach.xlsx");

            //return File(newPath, "application/vnd.ms-excel", "Book3.xlsx");

            string filePath = newPath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", "File_mau.xlsx");
        }

        //-----------Tour Programe------------
        public async Task<IEnumerable<Tourprog>> listTourProgAsync(string id) // id == sgtcode
        {
            //var progtemp = _unitOfWork.tourproRepository.Find(x => x.Sgtcode == id).ToList();// _tourprogRepository.ListTourProg(id);
            var progtemp = _unitOfWork.tourproRepository.ListTourProg(id);
            var t = await _unitOfWork.tourInfRepository.GetByIdAsync(id);// _tourinfRepository.GetById(id);
            ViewBag.cn = HttpContext.Session.GetString("chinhanh");

            // ViewBag.dieuhanh = HttpContext.Session.GetString("username");
            //ViewBag.concernto = t.concernto;
            if (t != null)
            {
                ViewBag.operators = t.Operators ?? "";
            }
            foreach (var item in progtemp)
            {
                // Mượn nguoihuydv để so sánh với điều hành=> cho phép kéo thả dịch vụ
                item.Nguoihuydv = t.Operators;
                //listChinhanh(item.chinhanh);

                //if (!string.IsNullOrEmpty(item.dieuhanh))
                //{
                //    var user = _userRepository.GetById(item.dieuhanh);
                //    item.dieuhanh = user.Hoten;
                //}
                item.Logfile = item.Date > 0 ? t.Arr.AddDays(item.Date.Value - 1).ToString("ddd").ToUpper() + "  " + t.Arr.AddDays(item.Date.Value - 1).ToString("dd/MM") : "";
                switch (item.Srvtype)
                {
                    case "LUN":
                    case "DIN":
                    case "SHP":
                        if (item.Supplierid != null)
                        {
                            var supplier = await _unitOfWork.supplierRepository.GetByIdAsync(item.Supplierid); // _supplierRepository.GetById(item.supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich + " - " + supplier.Diachi + " phone: " + supplier.Dienthoai;
                        }
                        else
                        {
                            item.TourItem = item.TourItem;
                        }
                        break;
                    case "OVR":
                    case "PAC":
                    case "CAN":
                    case "MUS":
                    case "SHW":
                    case "TRA":
                    case "WPU":
                        if (item.Supplierid != null)
                        {
                            var supplier = await _unitOfWork.supplierRepository.GetByIdAsync(item.Supplierid); // _supplierRepository.GetById(item.supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich;
                        }
                        else
                        {
                            item.TourItem = item.TourItem;
                        }
                        break;
                    case "AIR":
                        item.TourItem = item.TourItem + " chặng: " + item.Dep + "-" + item.Arr + " chuyến: " + item.Carrier + " * " + String.Format("{0:#,##0.0}", item.Unitpricea) + " " + item.Currency;
                        break;
                    case "HTL":
                        if (item.Supplierid != null)
                        {
                            string gia = "";
                            var supplier = await _unitOfWork.supplierRepository.GetByIdAsync(item.Supplierid); // _supplierRepository.GetById(item.supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich + " - " + supplier.Diachi + " phone: " + supplier.Dienthoai;
                            var hotel = _unitOfWork.hotelRepository.Find(x => x.Sgtcode == item.Sgtcode && x.Stt == item.Stt);//chi lay dich vu chua xoa
                            if (hotel != null)
                            {
                                foreach (var a in hotel)
                                {
                                    if (a.Sgl > 0)
                                    {
                                        gia += a.Sgl + "SGN" + "*" + string.Format("{0:#,##0.0}", a.Sglcost) + a.Currency;
                                    }
                                    if (a.Extsgl > 0)
                                    {
                                        gia += "," + a.Extsgl + "EXT-SGN" + "*" + string.Format("{0:#,##0.0}", a.Extsglcost) + a.Currency;
                                    }
                                    //if (a.sglfoc > 0)
                                    //{
                                    //    gia += "," + gia + " " + a.sglfoc + "SGN FOC";
                                    //}
                                    if (a.Dbl > 0)
                                    {
                                        gia += "," + a.Dbl + "DBL" + "*" + string.Format("{0:#,##0.0}", a.Dblcost) + a.Currency;
                                    }
                                    if (a.Extdbl > 0)
                                    {
                                        gia += "," + a.Extdbl + "EXT-DBL" + "*" + string.Format("{0:#,##0.0}", a.Extdblcost) + a.Currency;
                                    }
                                    //if (a.dblfoc > 0)
                                    //{
                                    //    gia += "," + gia + " " + a.dblfoc + "DBL FOC";
                                    //}
                                    if (a.Twn > 0)
                                    {
                                        gia += "," + a.Twn + "TWN" + "*" + string.Format("{0:#,##0.0}", a.Twncost) + a.Currency;
                                    }
                                    if (a.Exttwn > 0)
                                    {
                                        gia += "," + a.Exttwn + "EXT-TWN" + "*" + string.Format("{0:#,##0.0}", a.Exttwncost) + a.Currency;
                                    }

                                    if (a.Homestay > 0)
                                    {
                                        gia += "," + a.Homestay + "Home stay" + "*" + a.Homestaypax + " pax*" + string.Format("{0:#,##0.0}", a.Homestaycost) + "/1pax" + a.Currency;
                                    }

                                    if (a.Oth > 0)
                                    {
                                        gia += "," + a.Oth + " OTH-" + a.Othpax + "pax" + "*" + string.Format("{0:#,##0.0}", a.Othcost) + a.Currency + "-" + a.Othtype;
                                    }

                                }

                            }
                            item.TourItem = item.TourItem + " " + gia;
                        }
                        else
                        {
                            item.TourItem = item.TourItem;
                        }
                        break;
                    case "SSE":
                        var sse = _unitOfWork.sightseeingRepository.Find(x => x.Sgtcode == item.Sgtcode && x.Stt == item.Stt);
                        var diemtq = await _unitOfWork.dMDiemTQRepository.GetByIdAsync(item.TourItem);// _diemtqRepository.GetById(item.TourItem);
                        string tq = "";
                        if (diemtq != null)
                        {
                            tq = diemtq.Diemtq;
                        }

                        if (sse.Count() > 0)
                        {
                            string tendtq = "";
                            foreach (var d in sse)
                            {
                                if (tendtq == "")
                                {
                                    // tendtq = _diemtqRepository.GetById(d.Codedtq).Diemtq;
                                    var dmDiemTQ = await _unitOfWork.dMDiemTQRepository.GetByIdAsync(d.Codedtq);
                                    tendtq = dmDiemTQ.Diemtq;
                                }

                                else
                                {
                                    // tendtq += ", " + _diemtqRepository.GetById(d.Codedtq).Diemtq;
                                    var dmDiemTQ = await _unitOfWork.dMDiemTQRepository.GetByIdAsync(d.Codedtq);
                                    tendtq += ", " + dmDiemTQ.Diemtq;
                                }

                            }
                            item.TourItem = "Tham quan " + (string.IsNullOrEmpty(tq) ? "" : tq) + "  " + tendtq;
                        }
                        else
                        {
                            item.TourItem = tq;
                        }
                        break;
                    default:
                        item.TourItem = item.TourItem;
                        break;
                }

            }
            return progtemp;
        }
        //-----------Tour Programe------------

        //-----------Tour Programe------------
        public async Task<Tournode> TourNoteAsync(string id)
        {
            var tournote = await _unitOfWork.tournoteRepository.GetByIdAsync(id);

            return tournote;
        }
        //-----------Tour Programe------------

        //----------- KhachTour------------

        public IEnumerable<Khachtour> ListDsKhach(string id)
        {
            var hd = _unitOfWork.khachTourRepository.Find(x => x.Sgtcode == id && x.Del == false).OrderBy(x => x.Stt).ToList();

            return hd;
        }

        //----------- KhachTour------------

        //-----------CP Khac------------
        public async Task<List<ChiPhiKhachDto>> ListCPKhac(string id)//id = sgtcode
        {
            List<Chiphikhac> cp = _unitOfWork.chiPhiKhacRepository.Find(x => x.Sgtcode == id && x.Del == false).ToList();
            ViewBag.cn = HttpContext.Session.GetString("chinhanh");
            ViewBag.sgtcode = id;
            List<ChiPhiKhachDto> lst = new List<ChiPhiKhachDto>();
            foreach (Chiphikhac c in cp)
            {
                ChiPhiKhachDto v = new ChiPhiKhachDto();
                v.Chiphikhac = c;
                try
                {
                    var dichvu = await _unitOfWork.dichVuRepository.GetByIdAsync(c.Srvtype);// _dichvuRepository.GetById(c.srvtype).Tendv;
                    v.Tendv = dichvu.Tendv;
                }
                catch { }
                try
                {
                    var supplier = await _unitOfWork.supplierRepository.GetByIdAsync(c.Srvcode);// _supplierRepository.GetById(c.srvcode).Tengiaodich;
                    v.Tengiaodich = supplier.Tengiaodich;
                }
                catch { }
                lst.Add(v);
            }

            return lst;
        }

        //-----------CP Khac------------

        //-----------Xe------------
        public async Task<IEnumerable<Dieuxe>> ListYeucauxe(string id) // id == sgtcode
        {
            var xe = _unitOfWork.dieuXeRepository.Find(x => x.Sgtcode == id && x.Del == false).OrderBy(x => x.Sttxe);// _dieuxeRepository.ListXe(id);
            //ViewBag.cn = HttpContext.Session.GetString("chinhanh");
            foreach (var x in xe)
            {
                if (!String.IsNullOrEmpty(x.Supplierid))
                {
                    var supplier = await _unitOfWork.supplierRepository.GetByIdAsync(x.Supplierid);// _supplierRepository.GetById(x.SupplierId).Tengiaodich;
                    x.Supplierid = supplier.Tengiaodich;
                }
            }
            //ViewBag.sgtcode = id;
            return xe;
        }
        //-----------Xe------------

        //-----------HD------------
        public IEnumerable<Huongdan> ListHuongdan(string id)
        {
            var hd = _unitOfWork.huongDanRepository.Find(x => x.Sgtcode == id && x.Del == false).OrderBy(x => x.Stt);// _huongdanRepository.ListHuongdan(id);
            //ViewBag.cn = HttpContext.Session.GetString("chinhanh");
            //ViewBag.sgtcode = id;
            return hd;
        }

        //-----------HD------------

        //-----------HuyTour------------
        public async Task<IActionResult> HuyTourPartial(long id, string strUrl)
        {
            if (id == 0)
                return NotFound();

            TourVM.StrUrl = strUrl;
            TourVM.Tour = _unitOfWork.tourRepository.GetById(id);
            TourVM.CacNoiDungHuyTours = await _unitOfWork.cacNoiDungHuyTourRepository.FindAsync(x => x.Xoa == false);

            return PartialView(TourVM);
        }

        [HttpPost]
        public async Task<IActionResult> HuyTour()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log;

            // tour
            var tour = _unitOfWork.tourRepository.GetById(TourVM.Tour.Id);
            tour.NgayHuyTour = TourVM.Tour.NgayHuyTour;
            tour.NDHuyTourId = TourVM.Tour.NDHuyTourId;
            tour.GhiChu = TourVM.Tour.GhiChu;
            tour.HuyTour = true;
            tour.TrangThai = "4";

            // tourinf - qltour
            var tourInfo = await _unitOfWork.tourInfRepository.GetByIdAsync(tour.Sgtcode);
            tourInfo.Cancel = tour.NgayHuyTour;

            // kiem tra thay doi

            if (TourVM.Tour.NDHuyTourId > 0)
            {
                var cacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(TourVM.Tour.NDHuyTourId);
                temp += String.Format("- Nội dung huy: {0}", cacNoiDungHuyTour.NoiDung);
            }

            if (!string.IsNullOrEmpty(TourVM.Tour.GhiChu))
            {
                temp += String.Format("- Ghi chú: {0}", TourVM.Tour.GhiChu);
            }

            if (TourVM.Tour.NgayHuyTour.HasValue)
            {
                temp += String.Format("- Ngày hủy: {0:dd/MM/yyyy} - Người hủy: {1}", TourVM.Tour.NgayHuyTour, user.Username); // username
            }

            if (temp.Length > 0)
            {

                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp;
                tour.LogFile = tour.LogFile + log;
                tourInfo.Logfile += log;
            }

            try
            {
                _unitOfWork.tourRepository.Update(tour);
                _unitOfWork.tourInfRepository.Update(tourInfo);
                await _unitOfWork.Complete();
                SetAlert("Hủy thành công.", "success");
                return Redirect(TourVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert("Error: " + ex.Message, "error");
                return Redirect(TourVM.StrUrl);

            }
        }
        //-----------HuyTour------------

        //-----------KhoiPhucTour------------

        [HttpPost]
        public async Task<IActionResult> KhoiPhucTour(long id, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log;

            // tour
            var tour = _unitOfWork.tourRepository.GetById(id);
            tour.NgayHuyTour = null;
            tour.NDHuyTourId = 0;
            tour.GhiChu = "";
            tour.HuyTour = false;

            if (tour.NgayDamPhan != null)
            {
                tour.TrangThai = "1";
            }


            if (tour.NgayKyHopDong != null)
            {
                tour.TrangThai = "2";
            }

            if (tour.NgayThanhLyHD != null)
            {
                tour.TrangThai = "3";
            }

            else
            {
                tour.TrangThai = "0";
            }

            // tourinf - qltour
            var tourInfo = await _unitOfWork.tourInfRepository.GetByIdAsync(tour.Sgtcode);
            tourInfo.Cancel = tour.NgayHuyTour;

            // kiem tra thay doi

            temp += String.Format("- Ngày khôi phục: {0:dd/MM/yyyy} - Người khôi phục: {1}", DateTime.Now, user.Username); // username
            if (temp.Length > 0)
            {

                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp;
                tour.LogFile = tour.LogFile + log;
                tourInfo.Logfile += log;
            }

            try
            {
                _unitOfWork.tourRepository.Update(tour);
                _unitOfWork.tourInfRepository.Update(tourInfo);
                await _unitOfWork.Complete();
                SetAlert("Khôi phục thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert("Error: " + ex.Message, "error");
                return Redirect(strUrl);

            }
        }
        //-----------KhoiPhucTour------------

        #region in chuong trinh tour
        public IActionResult ExportWord(string sgtcode, bool ingia)
        {
            ingia = true;
            var listTourPro = TourProgBySGTCode(sgtcode, ingia).OrderBy(x => x.Ngaythang);
            var tourInfo = _unitOfWork.tourInfRepository.GetById(sgtcode);
            var tourNote = _unitOfWork.tournoteRepository.GetById(sgtcode);
            var userInfo = _userQLTourService.GetUserByUsername(tourInfo.Operators);
            var saleInfo = _unitOfWork.userQLTourRepository.getSaleByUsername(tourInfo.Concernto, tourInfo.PasstypeId, tourInfo.Khachle);
            var khachTours = _unitOfWork.khachTourRepository.ListKhachTour(sgtcode);
            var congty = _unitOfWork.khachHangRepository.GetCompanyByCode(tourInfo.PasstypeId, tourInfo.CompanyId); // KhachHang = Company
            var xe = _unitOfWork.dieuXeRepository.ListXe(sgtcode);
            var hd = _unitOfWork.huongDanRepository.ListHuongdan(sgtcode);
            var vListhc = _unitOfWork.traHauCanRepository.ListChiphiHaucan(sgtcode);
            string tencongty = "";
            if (congty != null)
            {
                tencongty = " - " + congty.Fullname;
            }


            Novacode.DocX doc = null;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string fileName = webRootPath + @"\WordTemplateForTour.docx";
            doc = Novacode.DocX.Load(fileName);

            doc.AddCustomProperty(new Novacode.CustomProperty("sgtcode", "CHƯƠNG TRÌNH TOUR: " + sgtcode));
            doc.AddCustomProperty(new CustomProperty("tuyen", "Tuyến: " + (string.IsNullOrEmpty(tourInfo.Reference) ? "" : tourInfo.Reference)));// _tuyentqRepository.GetById(tourInfo.routing).Tentuyen));
            doc.AddCustomProperty(new CustomProperty("batDau", "Bắt đầu: " + tourInfo.Arr.ToString("dd/MM/yyyy")));
            doc.AddCustomProperty(new CustomProperty("ketThuc", "Kết thúc: " + tourInfo.Dep.ToString("dd/MM/yyyy")));
            string dh = "", dt = "", sale = "", dtsale = "";
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.hoten))
            {
                dh = userInfo.hoten;
                dt = string.IsNullOrEmpty(userInfo.dienthoai) ? "" : " - " + userInfo.dienthoai;
            }
            else
            {
                userInfo = new UserInfo();
            }
            if (saleInfo != null && !string.IsNullOrEmpty(saleInfo.hoten))
            {
                sale = saleInfo.hoten.ToUpper();
                dtsale = string.IsNullOrEmpty(saleInfo.dienthoai) ? "" : " - " + saleInfo.dienthoai;
            }
            else
            {
                sale = tourInfo.Concernto.ToUpper();
            }
            doc.AddCustomProperty(new CustomProperty("dieuHanh", "  Sale: " + sale + dtsale + " / Điều hành: " + dh + dt));
            doc.AddCustomProperty(new CustomProperty("sk", "Sk: " + tourInfo.Pax + tencongty));
            if (tourNote != null && !string.IsNullOrEmpty(tourNote.Headernode))
            {
                doc.InsertParagraph(tourNote.Headernode).Font("Times New Roman").FontSize(10);
            }


            var tourProgramTbl = doc.AddTable(1, 3);

            tourProgramTbl.Rows[0].Cells[0].Paragraphs[0].Append("Giờ").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            tourProgramTbl.Rows[0].Cells[1].Paragraphs[0].Append("SK").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            tourProgramTbl.Rows[0].Cells[2].Paragraphs[0].Append("Chương trình").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;

            int dong = 0;
            int tongkhach = 0;
            string sokhach = "";
            foreach (var i in listTourPro)
            {
                var row = tourProgramTbl.InsertRow();
                tongkhach = i.Pax.Value + i.Childern.Value;
                switch (i.Srvtype)
                {
                    case "ITI":
                    case "TXT":
                        break;
                    default:
                        if (tongkhach > 0)
                            sokhach = tongkhach.ToString();
                        else sokhach = "";
                        break;
                }
                if (i.Date > 0)
                {
                    string ngaythang = i.Ngaythang == null ? "" : i.Ngaythang.Value.ToString("ddd, dd/MM/yyyy").ToUpper();
                    string tu = "";
                    string from = "", to = "";
                    if (i.Srvtype == "ITI")
                    {
                        if (!string.IsNullOrEmpty(i.TourItem))
                        {

                            //from += _thanhphoRepository.ListThanhpho1().Where(x => x.Matp == i.TourItem).SingleOrDefault().Tentp;
                            var thanhpho = _unitOfWork.thanhPhoForTuyenTQRepository.ListThanhpho1(x => x.Matp == i.TourItem);
                            var count = thanhpho.Count();
                            thanhpho = thanhpho.Where(x => x.Matp == i.TourItem);
                            from += thanhpho.SingleOrDefault().Tentp;
                        }
                        if (!string.IsNullOrEmpty(i.Srvnode))
                        {
                            to += _unitOfWork.thanhPhoForTuyenTQRepository.ListThanhpho1(x => x.Matp == i.Srvnode).SingleOrDefault().Tentp;
                        }

                        if (from == "" && to != "")
                        {
                            tu += " Đến " + to;
                        }
                        else
                        {
                            if (from == to)
                            {
                                tu = from;
                            }
                            else
                            {
                                tu += from + (string.IsNullOrEmpty(to) ? "" : " - " + to);
                            }

                        }
                    }

                    string thu = ngaythang.Substring(0, 3);
                    switch (thu)
                    {
                        case "MON": thu = "Thứ hai " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                        case "TUE": thu = "Thứ ba " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                        case "WED": thu = "Thứ tư " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                        case "THU": thu = "Thứ năm " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                        case "FRI": thu = "Thứ sáu " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                        case "SAT": thu = "Thứ bảy " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                        case "SUN": thu = "Chủ nhật " + i.Ngaythang.Value.ToString("dd/MM/yyyy") + " " + tu; break;
                    }

                    //     row.Cells[0].Paragraphs[0].Append("\n" + i.time).Alignment = Alignment.center;
                    //row.Cells[1].Paragraphs[0].Append("\n"+i.pax.ToString()=="0"?"":i.pax.ToString()).Alignment = Alignment.center;

                    //  row.Cells[1].Paragraphs[0].Append("\n" + sokhach).Alignment = Alignment.center;
                    row.Cells[2].Paragraphs[0].Append("" + thu).Bold().Font("Times New Roman").FontSize(10);
                    //row.Cells[2].Paragraphs[0].Append("\n- " + i.TourItem);
                }
                else
                {
                    row.Cells[0].Paragraphs[0].Append(i.Time).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[1].Paragraphs[0].Append(sokhach).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[2].Paragraphs[0].Append("- " + i.TourItem).Font("Times New Roman").FontSize(10);
                }
                dong++;
                if (dong > 1)
                {
                    row.Cells[0].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDotDash, BorderSize.one, 0, Color.White));
                    row.Cells[1].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDotDash, BorderSize.one, 0, Color.White));
                    row.Cells[2].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDotDash, BorderSize.one, 0, Color.White));
                }
                if (dong == listTourPro.Count())
                {

                    row.Cells[2].Paragraphs[0].Append("\n Kết thúc chương trình").Bold().Font("Times New Roman").FontSize(10);
                }
            }
            tourProgramTbl.AutoFit = AutoFit.Window;

            tourProgramTbl.SetWidthsPercentage(new[] { 5f, 5f, 90f }, 500);

            doc.InsertTable(tourProgramTbl);

            if (tourNote != null && !string.IsNullOrEmpty(tourNote.Footernode))
            {
                doc.InsertParagraph();
                doc.InsertParagraph(tourNote.Footernode).Font("Times New Roman").FontSize(10);
            }
            doc.InsertParagraph();

            if (vListhc.Count() > 0)
            {
                string dsquatang = "";
                foreach (var i in vListhc)
                {
                    if (string.IsNullOrEmpty(dsquatang))
                    {
                        dsquatang = (i.Soluong - i.Soluongtra) + " " + i.Tenhh;
                    }
                    else
                    {
                        dsquatang += " + " + (i.Soluong - i.Soluongtra) + " " + i.Tenhh;
                    }
                }
                doc.InsertParagraph("** Quà tặng: " + dsquatang).Font("Times New Roman").FontSize(10);
            }

            string roomingList = "Danh sách khách (Rooming List): ";

            //Formatting Title  
            Novacode.Formatting titleFormat = new Novacode.Formatting();

            titleFormat.Bold = true;
            doc.InsertParagraph(roomingList, false, titleFormat);

            var roomingListTbl = doc.AddTable(1, 6);
            roomingListTbl.Rows[0].Cells[0].Paragraphs[0].Append("STT").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            roomingListTbl.Rows[0].Cells[1].Paragraphs[0].Append("Tên khách").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            roomingListTbl.Rows[0].Cells[2].Paragraphs[0].Append("Phái").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            roomingListTbl.Rows[0].Cells[3].Paragraphs[0].Append("Điện thoại").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            roomingListTbl.Rows[0].Cells[4].Paragraphs[0].Append("Hộ chiếu").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            roomingListTbl.Rows[0].Cells[5].Paragraphs[0].Append("Loại phòng").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
            int dong_ = 0;
            foreach (var k in khachTours)
            {
                var row = roomingListTbl.InsertRow();
                row.Cells[0].Paragraphs[0].Append(k.Stt.ToString()).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                row.Cells[1].Paragraphs[0].Append(string.IsNullOrEmpty(k.Hoten) ? "" : k.Hoten.ToUpper()).Font("Times New Roman").FontSize(10);
                row.Cells[2].Paragraphs[0].Append(!k.Phai.Value ? "Nữ" : "Nam").Font("Times New Roman").FontSize(10);
                row.Cells[3].Paragraphs[0].Append(k.Dienthoai).Font("Times New Roman").FontSize(10);
                row.Cells[4].Paragraphs[0].Append(k.Hochieu).Font("Times New Roman").FontSize(10);
                row.Cells[5].Paragraphs[0].Append(k.Loaiphong).Font("Times New Roman").FontSize(10);
                dong_++;
                if (dong_ > 1)
                {
                    row.Cells[0].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    row.Cells[1].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    row.Cells[2].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    row.Cells[3].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    row.Cells[4].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    row.Cells[5].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                }
            }

            roomingListTbl.AutoFit = AutoFit.Window;
            roomingListTbl.SetWidthsPercentage(new[] { 2f, 35f, 5f, 20f, 20f, 10f }, 500);
            doc.InsertTable(roomingListTbl);
            // Chèn danh sách lái xe
            if (xe.Count() > 0)
            {
                doc.InsertParagraph();
                doc.InsertParagraph("Danh sách xe").Font("Times New Roman").FontSize(10).Bold();
                var ListxeTbl = doc.AddTable(1, 7);
                ListxeTbl.Rows[0].Cells[0].Paragraphs[0].Append("Xe").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListxeTbl.Rows[0].Cells[1].Paragraphs[0].Append("Số xe").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListxeTbl.Rows[0].Cells[2].Paragraphs[0].Append("Lái xe").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListxeTbl.Rows[0].Cells[3].Paragraphs[0].Append("Điện thoại").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListxeTbl.Rows[0].Cells[4].Paragraphs[0].Append("Từ ngày").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListxeTbl.Rows[0].Cells[5].Paragraphs[0].Append("Đến ngày").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListxeTbl.Rows[0].Cells[6].Paragraphs[0].Append("CN").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                int dong__ = 0;
                foreach (var k in xe)
                {
                    var row = ListxeTbl.InsertRow();
                    row.Cells[0].Paragraphs[0].Append(k.Loaixe.ToString()).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[1].Paragraphs[0].Append(k.Soxe).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[2].Paragraphs[0].Append(k.Laixe).Font("Times New Roman").FontSize(10).Alignment = Alignment.left;
                    row.Cells[3].Paragraphs[0].Append(k.Dienthoai).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;

                    row.Cells[4].Paragraphs[0].Append(k.Ngaydon.HasValue ? k.Ngaydon.Value.ToString("dd/MM/yyyy") : "").Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[5].Paragraphs[0].Append(k.Denngay.HasValue ? k.Denngay.Value.ToString("dd/MM/yyyy") : "").Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[6].Paragraphs[0].Append(k.Chinhanh).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    dong__++;
                    if (dong__ > 1)
                    {
                        row.Cells[0].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[1].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[2].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[3].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[4].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[5].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[6].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    }
                }

                ListxeTbl.AutoFit = AutoFit.Window;
                ListxeTbl.SetWidthsPercentage(new[] { 10f, 10f, 25f, 10f, 15f, 15f, 10f }, 500);

                doc.InsertTable(ListxeTbl);
            }

            if (hd.Count() > 0)
            {
                doc.InsertParagraph();
                doc.InsertParagraph("Danh sách hướng dẫn").Font("Times New Roman").FontSize(10).Bold();
                var ListHuongdanTbl = doc.AddTable(1, 7);
                ListHuongdanTbl.Rows[0].Cells[0].Paragraphs[0].Append("STT").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListHuongdanTbl.Rows[0].Cells[1].Paragraphs[0].Append("Tên hướng dẫn").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListHuongdanTbl.Rows[0].Cells[2].Paragraphs[0].Append("Điện thoại").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListHuongdanTbl.Rows[0].Cells[3].Paragraphs[0].Append("Từ ngày").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListHuongdanTbl.Rows[0].Cells[4].Paragraphs[0].Append("Đến ngày").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListHuongdanTbl.Rows[0].Cells[5].Paragraphs[0].Append("Ghi chú").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                ListHuongdanTbl.Rows[0].Cells[6].Paragraphs[0].Append("CN").Bold().Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                int dong__ = 0;
                foreach (var k in hd)
                {
                    var row = ListHuongdanTbl.InsertRow();
                    row.Cells[0].Paragraphs[0].Append(k.Stt.ToString()).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[1].Paragraphs[0].Append(k.Tenhd).Font("Times New Roman").FontSize(10).Alignment = Alignment.left;
                    row.Cells[2].Paragraphs[0].Append(k.Dienthoai).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[3].Paragraphs[0].Append(k.Batdau.HasValue ? k.Batdau.Value.ToString("dd/MM/yyyy") : "").Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[4].Paragraphs[0].Append(k.Ketthuc.HasValue ? k.Ketthuc.Value.ToString("dd/MM/yyyy") : "").Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    row.Cells[5].Paragraphs[0].Append(k.Ghichu).Font("Times New Roman").FontSize(10).Alignment = Alignment.left;
                    row.Cells[6].Paragraphs[0].Append(k.Chinhanh).Font("Times New Roman").FontSize(10).Alignment = Alignment.center;
                    dong__++;
                    if (dong__ > 1)
                    {
                        row.Cells[0].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[1].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[2].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[3].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[4].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[5].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                        row.Cells[6].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_inset, BorderSize.one, 0, Color.Silver));
                    }
                }

                ListHuongdanTbl.AutoFit = AutoFit.Window;
                ListHuongdanTbl.SetWidthsPercentage(new[] { 8f, 25f, 15f, 15f, 15f, 25f, 10f }, 500);

                doc.InsertTable(ListHuongdanTbl);
            }

            MemoryStream stream = new MemoryStream();

            // Saves the Word document to MemoryStream
            doc.SaveAs(stream);
            stream.Position = 0;
            // Download Word document in the browser
            return File(stream, "application/msword", "Chuongtrinhtour_" + sgtcode + "_" + DateTime.Now + ".docx");
        }

        public IEnumerable<Tourprog> TourProgBySGTCode(string id, bool ingia)
        {
            string gia = "";
            string ck = "";
            var progtemp = _unitOfWork.tourproRepository.ListTourProg(id);
            var t = _unitOfWork.tourInfRepository.GetById(id);
            ViewBag.cn = HttpContext.Session.GetString("chinhanh");
            foreach (var item in progtemp)
            {
                item.Logfile = item.Date > 0 ? t.Arr.AddDays(item.Date.Value - 1).ToString("ddd, dd/MM/yyyy").ToUpper() : "";
                if (item.Debit)
                {
                    if (item.Foc > 0)
                    {
                        ck = ", trong đó có " + item.Foc + "FOC (" + item.Chinhanh + " CK)";
                    }
                    else
                    {
                        ck = " (" + item.Chinhanh + " CK)";
                    }

                }
                else
                {
                    if (item.Foc > 0)
                    {
                        ck = ", trong đó có " + item.Foc + "FOC (" + item.Chinhanh + " TM)";
                    }
                    else
                    {
                        ck = " (" + item.Chinhanh + " TM)";
                    }

                }
                switch (item.Srvtype)
                {
                    case "LUN":
                    case "DIN":
                    case "BRK":
                        //case "SHP":

                        if (item.Supplierid != null)
                        {
                            var supplier = _unitOfWork.supplierRepository.getSupplierById(item.Supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich + " phone: " + supplier.Dienthoai;
                        }
                        else
                        {
                            item.TourItem = item.TourItem;
                        }
                        if (ingia) // nếu được phép in giá thì in số khách và giá tiền
                        {
                            gia = "";
                            if (item.Amount > 0)
                            {
                                gia = " Tổng chi phí: " + string.Format("{0:#,##0}", item.Amount) + item.Currency;
                            }
                            else
                            {
                                gia = " gồm có " + item.Pax + " Pax*" + string.Format("{0:#,##0}", item.Unitpricea) + item.Currency;
                                if (item.Childern > 0)
                                {
                                    gia += " + " + item.Childern + " Childern*" + string.Format("{0:#,##0}", item.Unitpricec) + item.Currency;
                                }
                            }
                            item.TourItem = item.TourItem + " " + gia;
                        }
                        else // nếu không được in giá thì chỉ in số khách
                        {
                            item.TourItem += item.TourItem + " gồm có " + (item.Pax + item.Childern) + "Pax";
                        }
                        item.TourItem = item.TourItem + (string.IsNullOrEmpty(item.Srvnode) ? "" : " (" + item.Srvnode + ")") + ck;
                        break;
                    case "OVR":
                    case "PAC":
                    case "OTH":
                    case "GLF":
                        if (item.Supplierid != null)
                        {
                            
                            var supplier = _unitOfWork.supplierRepository.getSupplierById(item.Supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich;
                        }
                        else
                        {
                            item.TourItem = item.TourItem;
                        }
                        if (ingia)
                        {
                            gia = "";
                            if (item.Amount > 0)
                            {
                                gia = " Tổng chi phí: " + string.Format("{0:#,##0}", item.Amount) + item.Currency;
                            }
                            else
                            {
                                gia = " gồm có " + item.Pax + " Pax*" + string.Format("{0:#,##0}", item.Unitpricea) + item.Currency;
                                if (item.Childern > 0)
                                {
                                    gia += " + " + item.Childern + " Childern*" + string.Format("{0:#,##0}", item.Unitpricec) + item.Currency;
                                }
                            }
                            item.TourItem = item.TourItem + " " + gia;
                        }
                        else // nếu không được in giá thì chỉ in số khách
                        {
                            item.TourItem += item.TourItem + " gồm có " + (item.Pax + item.Childern) + "Pax";
                        }
                        item.TourItem = item.TourItem + (string.IsNullOrEmpty(item.Srvnode) ? "" : " (" + item.Srvnode + ")") + ck;
                        break;
                    case "SHW":
                    case "MUS":
                    case "WPU":
                        if (!string.IsNullOrEmpty(item.Supplierid))
                        {
                            
                            var supplier = _unitOfWork.supplierRepository.getSupplierById(item.Supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich + item.Time ?? " " + item.Carrier;
                        }
                        else
                        {
                            item.TourItem = item.TourItem + " " + item.Time ?? " " + item.Carrier; ;
                        }
                        if (ingia)
                        {
                            gia = "";
                            if (item.Amount > 0)
                            {
                                gia = " Tổng chi phí: " + string.Format("{0:#,##0}", item.Amount) + item.Currency;
                            }
                            else
                            {
                                gia = " gồm có " + item.Pax + " Pax*" + string.Format("{0:#,##0}", item.Unitpricea) + item.Currency;
                                if (item.Childern > 0)
                                {
                                    gia += " + " + item.Childern + " Childern*" + string.Format("{0:#,##0}", item.Unitpricec) + item.Currency;
                                }
                            }
                            item.TourItem = item.TourItem + " " + gia;
                        }
                        else // nếu không được in giá thì chỉ in số khách
                        {
                            item.TourItem += item.TourItem + " gồm có " + (item.Pax + item.Childern) + "Pax";
                        }
                        item.TourItem = item.TourItem + (string.IsNullOrEmpty(item.Srvnode) ? "" : " (" + item.Srvnode + ")") + ck;
                        break;
                    case "AIR":
                    case "TRA":
                        if (item.Airtype == "DON")
                        {
                            //item.TourItem = item.TourItem + " " + item.dep + "-" + item.arr + " chuyến: " + item.carrier + " đáp lúc " + item.time + " * " + String.Format("{0:#,##0.0}", item.unitpricea) + " " + item.currency;
                            item.TourItem = item.TourItem + " " + item.Dep + "-" + item.Arr + " chuyến: " + item.Carrier + " đáp lúc " + item.Time + "  ";// + string.Format("{0:#,##0.0}", item.unitpricea) + " " + item.currency;
                        }
                        else if (item.Airtype == "TIEN")
                        {
                            //item.TourItem = item.TourItem + " tiển khách chặng: " + item.dep + "-" + item.arr + " chuyến: " + item.carrier + " * " + String.Format("{0:#,##0.0}", item.unitpricea) + " " + item.currency;
                            item.TourItem = item.TourItem + " " + item.Dep + "-" + item.Arr + " chuyến: " + item.Carrier + " ";// + string.Format("{0:#,##0.0}", item.unitpricea) + " " + item.currency;
                        }
                        else
                        {
                            //item.TourItem = item.TourItem + " bay cùng khách chặng: " + item.dep + "-" + item.arr + " chuyến: " + item.carrier + " * " + String.Format("{0:#,##0.0}", item.unitpricea) + " " + item.currency;
                            item.TourItem = item.TourItem + " " + item.Dep + "-" + item.Arr + " chuyến: " + item.Carrier + "  ";// + string.Format("{0:#,##0.0}", item.unitpricea) + " " + item.currency;
                        }
                        if (ingia)
                        {
                            gia = "";
                            if (item.Amount > 0)
                            {
                                gia = " Tổng chi phí: " + string.Format("{0:#,##0}", item.Amount) + item.Currency;
                            }
                            else
                            {
                                gia = " gồm có " + item.Pax + " Pax*" + string.Format("{0:#,##0}", item.Unitpricea) + item.Currency;
                                if (item.Childern > 0)
                                {
                                    gia += " + " + item.Childern + " Childern*" + string.Format("{0:#,##0}", item.Unitpricec) + item.Currency;
                                }
                                if (item.Infant > 0)
                                {
                                    gia += " + " + item.Infant + " Infant*" + string.Format("{0:#,##0}", item.Unitpricei) + item.Currency;
                                }
                            }

                            item.TourItem = item.TourItem + " " + gia;
                        }
                        else
                        {
                            item.TourItem += item.TourItem + " gồm có " + (item.Pax + item.Childern) + "Pax";
                        }
                        item.TourItem = item.TourItem + " (" + (string.IsNullOrEmpty(item.Time) ? "giờ cất/ hạ cánh?" : item.Time.Replace('/', '-')) + ") " + (string.IsNullOrEmpty(item.Srvnode) ? "" : " (" + item.Srvnode + ")") + ck;
                        break;
                    case "HTL":
                    case "CRU":
                        if (item.Supplierid != null)
                        {
                            
                            var supplier = _unitOfWork.supplierRepository.getSupplierById(item.Supplierid);
                            item.TourItem = item.TourItem + " " + supplier.Tengiaodich + " - " + supplier.Diachi + " phone: " + supplier.Dienthoai;
                            // item.TourItem = item.TourItem + " " + gia;
                        }
                        else
                        {
                            item.TourItem = item.TourItem;
                        }
                        var hotel = _unitOfWork.hotelRepository.listHotelByIdtourprog(item.Id);// (item.sgtcode, item.stt);//chi lay dich vu chua xoa
                        if (hotel != null)
                        {
                            gia = "";
                            foreach (var a in hotel)
                            {
                                if (a.Sgl > 0)
                                {
                                    gia += a.Sgl + "SGL" + "*";// +  string.Format("{0:#,##0.0}", a.sglcost) + a.currency;
                                    if (ingia)
                                    {
                                        gia += string.Format("{0:#,##0}", a.Sglcost) + a.Currency;
                                    }
                                }
                                /////////////////////////////////////////////////////////////////
                                //if (a.sglfoc > 0)
                                //{
                                //    gia += ", có " + a.sglfoc + "SGL FOC";
                                //}
                                /////////////////////////////////////////////////////////////////
                                if (a.Dbl > 0)
                                {
                                    gia += "," + a.Dbl + "DBL";// + "*" + string.Format("{0:#,##0.0}", a.dblcost) + a.currency;
                                    if (ingia)
                                    {
                                        gia += "*" + string.Format("{0:#,##0}", a.Dblcost) + a.Currency;
                                    }
                                }
                                if (a.Extdbl > 0)
                                {
                                    gia += "," + a.Extdbl + "EXT-DBL";// + "*" + string.Format("{0:#,##0.0}", a.extdblcost) + a.currency;
                                    if (ingia)
                                    {
                                        gia += "*" + string.Format("{0:#,##0}", a.Extdblcost) + a.Currency;
                                    }
                                }
                                /////////////////////////////////////////////////////////////////
                                //if (a.dblfoc > 0)
                                //{
                                //    gia += ", có " + a.dblfoc + "DBL FOC";
                                //}
                                /////////////////////////////////////////////////////////////////
                                if (a.Twn > 0)
                                {
                                    gia += "," + a.Twn + "TWN";// + "*" + string.Format("{0:#,##0.0}", a.twncost) + a.currency;
                                    if (ingia)
                                    {
                                        gia += "*" + string.Format("{0:#,##0}", a.Twncost) + a.Currency;
                                    }
                                }
                                if (a.Exttwn > 0)
                                {
                                    gia += "," + a.Exttwn + "EXT-TWN";// + "*" + string.Format("{0:#,##0.0}", a.exttwncost) + a.currency;
                                    if (ingia)
                                    {
                                        gia += "*" + string.Format("{0:#,##0}", a.Exttwncost) + a.Currency;
                                    }
                                }
                                /////////////////////////////////////////////////////////////////
                                //if (a.twnfoc > 0)
                                //{
                                //    gia += ", có " + a.twnfoc + "TWN FOC";
                                //}
                                /////////////////////////////////////////////////////////////////
                                if (a.Homestay > 0)
                                {
                                    gia += "," + a.Homestay + " Home stay" + "*" + a.Homestaypax + " pax ";// + string.Format("{0:#,##0.0}", a.homestaycost)+"/1pax " + a.currency+" - "+a.homestaynote;
                                    if (ingia)
                                    {
                                        gia += string.Format("{0:#,##0}", a.Homestaycost) + "/1pax " + a.Currency + " - " + a.Homestaynote;
                                    }
                                }
                                if (a.Oth > 0)
                                {
                                    gia += "," + a.Oth + " OTH-" + a.Othpax + "pax";// + "*" + string.Format("{0:#,##0.0}", a.othcost) + a.currency + "-" + a.othtype;
                                    if (ingia)
                                    {
                                        gia += "*" + string.Format("{0:#,##0}", a.Othcost) + a.Currency + "-" + a.Othtype;
                                    }
                                }
                                gia += string.IsNullOrEmpty(a.Note) ? "" : " (" + a.Note + ")";
                            }
                        }
                        item.TourItem = item.TourItem + " " + gia;
                        item.TourItem = item.TourItem + (string.IsNullOrEmpty(item.Srvnode) ? "" : " (" + item.Srvnode + ")") + ck;
                        break;
                    case "SSE":

                        //var diemtq = _diemtqRepository.GetById(item.TourItem);
                        var diemtq = _unitOfWork.dMDiemTQRepository.GetById(item.TourItem);
                        string tq = "";
                        if (diemtq != null)
                        {
                            tq = diemtq.Diemtq;
                            if (ingia)
                            {
                                gia = "";
                                if (item.Amount > 0)
                                {
                                    gia = " Tổng chi phí: " + string.Format("{0:#,##0}", item.Amount) + item.Currency;
                                }
                                else
                                {
                                    gia = " gồm có " + item.Pax + "Pax*" + string.Format("{0:#,##0}", item.Unitpricea) + item.Currency;
                                    if (item.Childern > 0)
                                    {
                                        gia += " + " + item.Childern + "Childern*" + string.Format("{0:#,##0}", item.Unitpricec) + item.Currency;
                                    }
                                }
                                tq += " " + gia;
                            }
                            else
                            {
                                tq += " gồm có " + (item.Pax + item.Childern) + "Pax";
                            }
                        }
                        var sse = _unitOfWork.sightseeingRepository.listSighseeingByIdtourprog(item.Id);// (item.sgtcode, item.stt);
                        if (sse.Count() > 0)
                        {
                            string tendtq = "";
                            foreach (var d in sse)
                            {
                                var getdiemtq = _unitOfWork.dMDiemTQRepository.GetById(d.Codedtq);
                                if (tendtq == "")
                                {
                                    //tendtq = _diemtqRepository.GetById(d.Codedtq).Diemtq;
                                    tendtq = getdiemtq.Diemtq;
                                    if (ingia)
                                    {
                                        gia = "";
                                        if (d.Pax > 0 && d.PaxPrice > 0)
                                        {
                                            tendtq += " " + d.Pax + "Pax*" + string.Format("{0:#,##0}", d.PaxPrice);
                                        }
                                        if (d.Childern > 0 && d.ChildernPrice > 0)
                                        {
                                            tendtq += " " + d.Childern + "Chidern*" + string.Format("{0:#,##0}", d.ChildernPrice);
                                        }
                                    }
                                }
                                else
                                {
                                    //tendtq += "," + _diemtqRepository.GetById(d.Codedtq).Diemtq;
                                    tendtq += ", " + getdiemtq.Diemtq;
                                    if (ingia)
                                    {
                                        if (d.Pax > 0 && d.PaxPrice > 0)
                                        {
                                            tendtq += d.Pax + "Pax*" + string.Format("{0:#,##0}", d.PaxPrice);
                                        }
                                        if (d.Childern > 0 && d.ChildernPrice > 0)
                                        {
                                            tendtq += d.Childern + "Chidern*" + string.Format("{0:#,##0}", d.ChildernPrice);
                                        }
                                    }
                                }

                            }
                            item.TourItem = "Tham quan " + (string.IsNullOrEmpty(tq) ? "" : tq) + " " + tendtq;
                        }
                        else
                        {
                            item.TourItem = tq;
                        }
                        item.TourItem = item.TourItem + (string.IsNullOrEmpty(item.TourItem) ? "" : ", " + item.Srvnode) + ck;
                        break;
                    case "ITI":
                        item.TourItem = item.TourItem;
                        break;
                    default:
                        item.TourItem = item.TourItem + (string.IsNullOrEmpty(item.Srvnode) ? "" : " (" + item.Srvnode + ")");
                        break;
                }

            }
            return progtemp;
        }
        #endregion

        //public async Task<JsonResult> CheckInvoices(long tourId)
        //{
        //    var tours = await _unitOfWork.invoiceRepository.FindAsync(x => x.TourId == tourId);

        //    if (tours.Count() != 0)
        //    {
        //        return Json(new
        //        {
        //            status = true,
        //            toursCount = tours.Count()
        //        });
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            status = false
        //        });
        //    }
        //}
        //public JsonResult CheckHuy(long tourId)
        //{
        //    var tour = _unitOfWork.tourRepository.GetById(tourId);

        //    Debug.WriteLine(tour.Id + " - " + tour.HuyTour + "\n");
        //    return Json(new
        //    {
        //        status = tour.HuyTour
        //    });

        //}

        //public static string ReformatDate(string input)
        //{
        //    try
        //    {
        //        return Regex.Replace(input,
        //              "\\b(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})\\b",
        //              "${year}-${month}-${day}",
        //              RegexOptions.IgnoreCase,
        //              TimeSpan.FromMilliseconds(1000));
        //    }
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return input;
        //    }
        //}
    }
}