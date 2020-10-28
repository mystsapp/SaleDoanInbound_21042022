using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> Index(long id = 0, string searchString = null, int page = 1, 
                                               string searchFromDate = null, string searchToDate = null , 
                                               string invoiceId = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            TourVM.StrUrl = UriHelper.GetDisplayUrl(Request);

            //if (!string.IsNullOrEmpty(TourVM.StrUrl))
            //{
            //    // cat bo phai sau % --> too long error
            //    var newStrUrl = TourVM.StrUrl.Split('%');
            //    if (newStrUrl.Length > 1)
            //    {
            //        TourVM.StrUrl = newStrUrl[0];
            //    }
            //}

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

            var companies = TourVM.Companies;
            var loaiTours = TourVM.Tourkinds;
            var chiNhanhs = TourVM.Dmchinhanhs;
            var cacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll();
            List<string> listRoleChiNhanh = new List<string>();
            if(user.Role.RoleName != "Admins")
            {
                if(user.Role.RoleName == "Users")
                {
                    listRoleChiNhanh.Add(user.MaCN);
                }
                else
                {
                    // add chinhanhs in PhanKhuCN
                    var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(user.RoleId);
                    listRoleChiNhanh.AddRange(phanKhuCN.ChiNhanhs.Split(','));
                }
            }
            else
            {
                foreach(var item in chiNhanhs)
                {
                    listRoleChiNhanh.Add(item.Macn);
                }
            }
            TourVM.TourDtos = _unitOfWork.tourRepository.ListTour(searchString, companies, loaiTours, chiNhanhs, cacNoiDungHuyTours, page, searchFromDate, searchToDate, listRoleChiNhanh);
            
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

                // DS invoice theo tour
                TourVM.Invoices = _unitOfWork.invoiceRepository.Find(x => x.TourId == id);

                // DS biên nhận theo tour
                TourVM.BienNhans = _unitOfWork.bienNhanRepository.Find(x => x.TourId == id);

                // click on invoice
                if (!string.IsNullOrEmpty(invoiceId))
                {
                    TourVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    TourVM.CTVATs = await _unitOfWork.cTVATRepository.FindIncludeOneAsync(x => x.Invoice, y => y.InvoiceId == invoiceId && !y.TiengAnh);
                    TourVM.CTInvoices = await _unitOfWork.cTVATRepository.FindIncludeOneAsync(x => x.Invoice, y => y.InvoiceId == invoiceId && y.TiengAnh);
                }
                // click on invoice
            }
            //--> click vao tour

            //if (!string.IsNullOrEmpty(tabActive))
            //{
            //    TourVM.tabActive = tabActive;

            //    // reset url -> cut tabActive
            //    var newStrUrl = TourVM.StrUrl.Split("&tabActive");
            //    if (newStrUrl.Length > 1)
            //    {
            //        TourVM.StrUrl = newStrUrl[0];
            //    }

            //}
            return View(TourVM);
        }

        public IActionResult Create(string strUrl)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            if (user.Role.RoleName == "KeToans")
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
            TourVM.StrUrl = strUrl;
            TourVM.Tour.SoHopDong = "";
            ViewBag.chiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id;

            //ViewBag.tuyenTQ = "BAL,BAN"; //"[BAL,BAN]"; // for test
            // get list phong ban / thi truong
            GetListPhongBanMacode(user.PhongBanId); // sinh ma cho sgtgode / phongbanid = maphong in qltour
                                                    // get list phong ban / thi truong

            // get list phong ban / dh 
            TourVM.listPhongDH = GetListPhongBanDH(); // departoperator (qltour)
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
            // kiem tra trang thai

            //TourVM.Tour = new Data.Models_IB.Tour();
            TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace(',', '-');

            TourVM.Tour.ChiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id;
            TourVM.Tour.NgayTao = DateTime.Now;
            TourVM.Tour.NguoiTao = user.Username;
            if (string.IsNullOrEmpty(TourVM.Tour.SoHopDong))
            {
                TourVM.Tour.SoHopDong = "";
            }
            TourVM.Tour.NguoiTao = user.Username;

            // create sgtcode
            var companies = await _unitOfWork.khachHangRepository.FindAsync(x => x.CompanyId == TourVM.Tour.MaKH); // find company by MaKH(companyId)
            var quocgias = await _unitOfWork.quocGiaRepository.FindAsync(x => x.Nation == companies.FirstOrDefault().Nation); // find by nation(vn)
            var sgtCode = _unitOfWork.tourInfRepository.newSgtcode(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, quocgias.FirstOrDefault().Telcode);
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
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();
            if (user.Role.RoleName == "KeToans")
            {
                return View("~/Views/Shared/AccessDenied.cshtml");
            }
            var chiNhanhDHId = _unitOfWork.tourRepository.GetById(id).ChiNhanhDHId;
            // user logon khac user DH
            if(user.MaCN != TourVM.Dmchinhanhs.Where(x => x.Id == chiNhanhDHId).FirstOrDefault().Macn)
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
            {
                ViewBag.ErrorMessage = "Tour này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (ModelState.IsValid)
            {
                TourVM.Tour.NgaySua = DateTime.Now;
                TourVM.Tour.NguoiSua = user.Username;

                TourVM.Tour.TuyenTQ = TourVM.Tour.TuyenTQ.Replace(',', '-');

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
                // kiem tra trang thai

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.tourRepository.GetByIdAsNoTracking(x => x.Id == id);

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
                        (t.LoaiTourId == 0)? "0": _unitOfWork.tourKindRepository.GetById(t.LoaiTourId.Value).TourkindInf, 
                        (TourVM.Tour.LoaiTourId == 0)? "0": _unitOfWork.tourKindRepository.GetById(TourVM.Tour.LoaiTourId.Value).TourkindInf);
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
                    temp += String.Format("- Ngày ký HD phán thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayKyHopDong, TourVM.Tour.NgayKyHopDong);
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
                    ViewBag.chiNhanhTaoId = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == user.MaCN).FirstOrDefault().Id; // for compare
                    TourVM = new TourViewModel();

                    TourVM.Tour = new Data.Models_IB.Tour();
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
                                Del = false
                            });
                        }
                        _unitOfWork.khachTourRepository.CreateRange(khachtours);
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
            var progtemp = _unitOfWork.tourproRepository.Find(x => x.Sgtcode == id);// _tourprogRepository.ListTourProg(id);
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
                        var diemtq = await _unitOfWork.dMDiemTQRepository.GetByIdAsync(item.TourItem);// _diemtqRepository.GetById(item.tour_item);
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
            var hd = _unitOfWork.khachTourRepository.Find(x => x.Sgtcode == id && x.Del == false).ToList();

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
    }
}