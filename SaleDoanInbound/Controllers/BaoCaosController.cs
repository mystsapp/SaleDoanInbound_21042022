using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Services;
using Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using SaleDoanInbound.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaleDoanInbound.Controllers
{
    public class BaoCaosController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaoCaoService _baoCaoService;

        [BindProperty]
        public BaoCaoViewModel BaoCaoVM { get; set; }

        public BaoCaosController(IUnitOfWork unitOfWork, IBaoCaoService baoCaoService)
        {
            _unitOfWork = unitOfWork;
            _baoCaoService = baoCaoService;
            BaoCaoVM = new BaoCaoViewModel()
            {
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                Thangs = Thangs(),
                Phongbans = _unitOfWork.phongBanRepository.GetAll().Where(x => !string.IsNullOrEmpty(x.Macode)),

                TourBaoCaoDtosTheoNgay = new TourBaoCaoDtosTheoNgay()
            };
        }

        private IEnumerable<ListViewModel> Thangs()
        {
            return new List<ListViewModel>
            {
                new ListViewModel(){Name = "1" },
                new ListViewModel(){Name = "2" },
                new ListViewModel(){Name = "3" },
                new ListViewModel(){Name = "4" },
                new ListViewModel(){Name = "5" },
                new ListViewModel(){Name = "6" },
                new ListViewModel(){Name = "7" },
                new ListViewModel(){Name = "8" },
                new ListViewModel(){Name = "9" },
                new ListViewModel(){Name = "10" },
                new ListViewModel(){Name = "11" },
                new ListViewModel(){Name = "12" },
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Doanh so theo sale

        public async Task<IActionResult> DoanhSoTheoSale(string searchFromDate = null, string searchToDate = null, string Macn = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            //// moi load vao
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                var currentTime = DateTime.Now;
                string TuNgayDenNgayString = LoadTuNgayDenNgay(currentTime.Month.ToString(), currentTime.Month.ToString(), currentTime.Year.ToString());
                searchFromDate = TuNgayDenNgayString.Split('-')[0];
                searchToDate = TuNgayDenNgayString.Split('-')[1];

            }
            else // da chon ngay thang - // check date correct
            {
                try
                {
                    Convert.ToDateTime(searchFromDate);
                    Convert.ToDateTime(searchToDate);
                }
                catch (Exception)
                {
                    BaoCaoVM = new BaoCaoViewModel()
                    {
                        Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                        Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                        TourBaoCaoDtosTheoNgay = new TourBaoCaoDtosTheoNgay()
                    };

                    ViewBag.Macn = Macn;
                    ViewBag.searchFromDate = searchFromDate;
                    ViewBag.searchToDate = searchToDate;

                    ModelState.AddModelError("", "Lỗi định dạng ngày tháng.");
                    return View(BaoCaoVM);
                }
            }

            ViewBag.Macn = Macn;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;


            List<string> maCns = new List<string>();
            if (user.Role.RoleName != "Admins")
            {

                if (user.Role.RoleName == "Users")
                {
                    BaoCaoVM.Dmchinhanhs = new List<Dmchinhanh>() { new Dmchinhanh() { Macn = user.MaCN } };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
                else
                {

                    if (string.IsNullOrEmpty(Macn)) // moi load vao
                    {
                        var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                        foreach (var item in phanKhuCNs)
                        {
                            maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                        }
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                        BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                        DoanhSoTheoSaleGroupbyNguoiTao();

                    }
                    else // co' chon chinhanh
                    {
                        maCns = new List<string>() { Macn };
                        BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, maCns);
                        DoanhSoTheoSaleGroupbyNguoiTao();

                        var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                        foreach (var item in phanKhuCNs)
                        {
                            maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                        }
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Macn))
                {
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
                else // co' chon chinhanh
                {
                    maCns = new List<string>() { Macn };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, maCns);
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
            }

            return View(BaoCaoVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DoanhSoTheoSaleExcel(string searchFromDate = null, string searchToDate = null, string Macn = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll();

            ViewBag.Macn = Macn;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            string fromTo = "";
            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 10;// STT
            xlSheet.Column(2).Width = 20;// Code đoàn
            xlSheet.Column(3).Width = 35;// Tên công ty/Khách hàng
            xlSheet.Column(4).Width = 15;// bat dau
            xlSheet.Column(5).Width = 15;// ket thuc
            xlSheet.Column(6).Width = 40;// Chủ đề tour
            xlSheet.Column(7).Width = 40;// Tuyến tham quan
            xlSheet.Column(8).Width = 10;// SK dự kiến
            xlSheet.Column(9).Width = 20;// Doanh số dự kiến
            xlSheet.Column(10).Width = 10;// SK thực tế
            xlSheet.Column(11).Width = 20;// Doanh số thực tế
            xlSheet.Column(12).Width = 10;// Sales

            xlSheet.Cells[1, 1].Value = "CÔNG TY DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 12].Merge = true;

            xlSheet.Cells[2, 1].Value = "BÁO CÁO DOANH SỐ THEO SALES";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 12].Merge = true;
            setCenterAligment(2, 1, 2, 12, xlSheet);
            // dinh dang tu ngay den ngay
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
                SetAlert("Từ ngày đến ngày không được để trống.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoSale));
            }
            if (searchFromDate == searchToDate)
            {
                fromTo = "Ngày: " + searchFromDate;
            }
            else
            {
                fromTo = "Từ ngày: " + searchFromDate + " đến ngày: " + searchToDate;
            }
            xlSheet.Cells[3, 1].Value = fromTo;
            xlSheet.Cells[3, 1, 3, 12].Merge = true;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            setCenterAligment(3, 1, 3, 10, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "STT";
            xlSheet.Cells[5, 2].Value = "Code đoàn ";
            xlSheet.Cells[5, 3].Value = "Tên công ty/Khách hàng ";
            xlSheet.Cells[5, 4].Value = "Bắt đầu ";
            xlSheet.Cells[5, 5].Value = "Kết thúc";
            xlSheet.Cells[5, 6].Value = "Chủ đề tour";
            xlSheet.Cells[5, 7].Value = "Tuyến tham quan";
            xlSheet.Cells[5, 8].Value = "SK dự kiến";
            xlSheet.Cells[5, 9].Value = "Doanh số dự kiến";
            xlSheet.Cells[5, 10].Value = "SK thực tế";
            xlSheet.Cells[5, 11].Value = "Doanh số thực tế";
            xlSheet.Cells[5, 12].Value = "Sales";

            xlSheet.Cells[5, 1, 5, 12].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 12, xlSheet);
            setCenterAligment(5, 1, 5, 12, xlSheet);
            // do du lieu tu table
            int dong = 6;

            //// moi load vao
            List<string> maCns = new List<string>();
            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    BaoCaoVM.Dmchinhanhs = new List<Dmchinhanh>() { new Dmchinhanh() { Macn = user.MaCN } };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
                else
                {

                    if (string.IsNullOrEmpty(Macn)) // moi load vao
                    {
                        var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                        foreach (var item in phanKhuCNs)
                        {
                            maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                        }
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                        BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                        DoanhSoTheoSaleGroupbyNguoiTao();

                    }
                    else // co' chon chinhanh
                    {
                        maCns = new List<string>() { Macn };
                        BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, maCns);
                        DoanhSoTheoSaleGroupbyNguoiTao();

                        var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                        foreach (var item in phanKhuCNs)
                        {
                            maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                        }
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Macn))
                {
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
                else // co' chon chinhanh
                {
                    maCns = new List<string>() { Macn };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, maCns);
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
            }

            //return View(BaoCaoVM);

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (BaoCaoVM.TourBaoCaoDtos != null)
            {
                foreach (var vm in BaoCaoVM.TourBaoCaoDtosGroupByNguoiTaos)
                {
                    foreach (var item in vm.TourBaoCaoDtos)
                    {
                        xlSheet.Cells[dong, 1].Value = idem;
                        TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 2].Value = item.Sgtcode;
                        xlSheet.Cells[dong, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        if (item.TrangThai == "3")
                        {
                            xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(colorThanhLy);
                        }
                        else if (item.TrangThai == "2")
                        {
                            xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        }
                        else if (item.TrangThai == "4")
                        {
                            xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Red);
                        }
                        else
                        {
                            xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.White);
                        }

                        TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 3].Value = item.CompanyName;
                        TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 4].Value = item.NgayDen.ToShortDateString();
                        TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 5].Value = item.NgayDi.ToShortDateString();
                        TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 6].Value = item.ChuDeTour;
                        TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 7].Value = item.TuyenTQ;
                        TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 8].Value = item.SoKhachDK;
                        TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 9].Value = item.DoanhThuDK;
                        xlSheet.Cells[dong, 9].Style.Numberformat.Format = "#,##0";
                        TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 10].Value = item.SoKhachTT;
                        TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 11].Value = item.DoanhThuTT;
                        xlSheet.Cells[dong, 11].Style.Numberformat.Format = "#,##0";
                        TrSetCellBorder(xlSheet, dong, 11, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 12].Value = item.NguoiTao;
                        TrSetCellBorder(xlSheet, dong, 12, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        //setBorder(5, 1, dong, 10, xlSheet);

                        dong++;
                        idem++;
                    }

                    xlSheet.Cells[dong, 2].Value = "TỔNG CỘNG:";
                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    xlSheet.Cells[dong, 3].Value = "CHƯA THANH LÝ HỢP ĐỒNG:";
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    xlSheet.Cells[dong, 4].Value = vm.TourBaoCaoDtos.FirstOrDefault().ChuaThanhLyHopDong;
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                    xlSheet.Cells[dong + 1, 3].Value = "ĐÃ THANH LÝ HỢP ĐỒNG:";
                    TrSetCellBorder(xlSheet, dong + 1, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    xlSheet.Cells[dong + 1, 4].Value = vm.TourBaoCaoDtos.FirstOrDefault().DaThanhLyHopDong;
                    TrSetCellBorder(xlSheet, dong + 1, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                    xlSheet.Cells[dong + 2, 3].Value = "TỔNG CỘNG:";
                    TrSetCellBorder(xlSheet, dong + 2, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    xlSheet.Cells[dong + 2, 4].Value = vm.TourBaoCaoDtos.FirstOrDefault().TongSoKhachTheoSale;
                    TrSetCellBorder(xlSheet, dong + 2, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                    xlSheet.Cells[dong + 2, 5].Value = vm.TourBaoCaoDtos.FirstOrDefault().TongCongTheoTungSale;
                    TrSetCellBorder(xlSheet, dong + 2, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                    setBorder(dong, 1, dong + 2, 12, xlSheet);
                    setFontBold(dong, 1, dong + 2, 12, 12, xlSheet);
                    NumberFormat(dong, 1, dong + 2, 5, xlSheet);

                    //xlSheet.Cells[dong, 1, dong, 12].Merge = true;
                    //xlSheet.Cells[dong, 1].Value = vm.NguoiTao;
                    //xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                    ////TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Font.Bold = true;

                    //NumberFormat(6, 8, dong + 1, 9, xlSheet);
                    dong = dong + 3;
                    //idem = 1;
                }

                xlSheet.Cells[dong, 2].Value = "TỔNG CỘNG:";
                TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong, 4].Value = BaoCaoVM.TongSK;
                TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong, 5].Value = BaoCaoVM.TongCong.Value;
                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                NumberFormat(dong, 2, dong, 5, xlSheet);
                setFontBold(dong, 2, dong, 5, 12, xlSheet);
                setBorder(dong, 2, dong, 5, xlSheet);
            }
            else
            {
                SetAlert("No sale.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoSale));
            }

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "DoanhSoTheoSale_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DoanhSoTheoSaleExcelChart(string searchFromDate = null, string searchToDate = null, string Macn = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll();

            ViewBag.Macn = Macn;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            string fromTo = "";
            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 20;// STT
            xlSheet.Column(2).Width = 20;// Code đoàn

            xlSheet.Cells[1, 1].Value = "CÔNG TY DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 17].Merge = true;

            xlSheet.Cells[2, 1].Value = "BÁO CÁO DOANH SỐ THEO SALES";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 17].Merge = true;
            setCenterAligment(2, 1, 2, 17, xlSheet);
            // dinh dang tu ngay den ngay
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
                SetAlert("Từ ngày đến ngày không được để trống.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoSale));
            }
            if (searchFromDate == searchToDate)
            {
                fromTo = "Ngày: " + searchFromDate;
            }
            else
            {
                fromTo = "Từ ngày: " + searchFromDate + " đến ngày: " + searchToDate;
            }
            xlSheet.Cells[3, 1].Value = fromTo;
            xlSheet.Cells[3, 1, 3, 17].Merge = true;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            setCenterAligment(3, 1, 3, 17, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "Sale";
            xlSheet.Cells[5, 2].Value = "Doanh số ";

            xlSheet.Cells[5, 1, 5, 2].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 2, xlSheet);
            setCenterAligment(5, 1, 5, 2, xlSheet);
            // do du lieu tu table
            int dong = 6;

            //// moi load vao

            List<string> maCns = new List<string>();
            if (user.Role.RoleName != "Admins")
            {

                if (user.Role.RoleName == "Users")
                {
                    BaoCaoVM.Dmchinhanhs = new List<Dmchinhanh>() { new Dmchinhanh() { Macn = user.MaCN } };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
                else
                {

                    if (string.IsNullOrEmpty(Macn)) // moi load vao
                    {
                        var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                        foreach (var item in phanKhuCNs)
                        {
                            maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                        }
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                        BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                        DoanhSoTheoSaleGroupbyNguoiTao();

                    }
                    else // co' chon chinhanh
                    {
                        maCns = new List<string>() { Macn };
                        BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, maCns);
                        DoanhSoTheoSaleGroupbyNguoiTao();

                        var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                        foreach (var item in phanKhuCNs)
                        {
                            maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                        }
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Macn))
                {
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
                else // co' chon chinhanh
                {
                    maCns = new List<string>() { Macn };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, maCns);
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
            }

            //return View(BaoCaoVM);

            //du lieu
            //int iRowIndex = 6;

            IEnumerable<TourBaoCaoDtoChart> tourBaoCaoDtoCharts = BaoCaoVM.TourBaoCaoDtos.GroupBy(x => x.NguoiTao).Select(x => new TourBaoCaoDtoChart
            {
                TenTheoCN = x.First().MaCNTao + " - " + x.First().NguoiTao,
                MaCN = x.First().MaCNTao,
                NguoiTao = x.First().NguoiTao,
                DoanhThuTT = x.Sum(x => x.DoanhThuTT)
            });

            var iTotalRow1 = tourBaoCaoDtoCharts.Count();

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            if (tourBaoCaoDtoCharts != null)
            {
                foreach (var item in tourBaoCaoDtoCharts)
                {
                    xlSheet.Cells[dong, 1].Value = item.TenTheoCN;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.DoanhThuTT;
                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    NumberFormat(dong, 2, dong, 2, xlSheet);
                    dong++;
                }
            }
            else
            {
                SetAlert("No sale.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoSale));
            }

            #region "Chart"

            // int iTotalRow1 = dt1.Rows.Count;
            //if (tourBaoCaoDtoCharts != null)
            //{
            //    xlSheet.Cells[1, 1].LoadFromText("Sale");
            //    DungChung.TrSetCellBorder(xlSheet, 1, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Black, "Times New Roman", 12, FontStyle.Bold);

            //    xlSheet.Cells[1, 2].LoadFromText("Doanh số");
            //    DungChung.TrSetCellBorder(xlSheet, 1, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Black, "Times New Roman", 12, FontStyle.Bold);

            //    int iRowIndex1 = 2;
            //    foreach (DataRow item in dt1.Rows)
            //    {
            //        //COT 5
            //        xlSheet.Cells[iRowIndex1, 1].Value = item["tentheocn"].ToString();
            //        DungChung.TrSetCellBorder(xlSheet, iRowIndex1, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Black, "Times New Roman", 12, FontStyle.Regular);

            //        //COT 6
            //        xlSheet.Cells[iRowIndex1, 2].Value = Decimal.Parse(item["doanhthutt"].ToString() == "" ? "0" : @item["doanhthutt"].ToString());
            //        xlSheet.Cells[iRowIndex1, 2].Style.Numberformat.Format = "#,##0";
            //        DungChung.TrSetCellBorder(xlSheet, iRowIndex1, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Black, "Times New Roman", 12, FontStyle.Regular);

            //        iRowIndex1 = iRowIndex1 + 1;
            //    }
            //}

            // chart
            var lineChart = xlSheet.Drawings.AddChart("lineChart", eChartType.ColumnClustered);
            //var lineChart = ExcelApp.Workbook.Worksheets.AddChart("lineChart", eChartType.ColumnClustered);
            //xlSheet.Cells["A1"].LoadFromDataTable(dt1, false);
            //set the title
            lineChart.Title.Font.LatinFont = "Times New Roman";
            lineChart.Title.Font.Size = 16;
            lineChart.Title.Font.Bold = true;
            lineChart.Title.Text = "Đoàn đi tour từ ngày " + fromTo;
            //create the ranges for the chart
            iTotalRow1 = iTotalRow1 + 6;//+1 do bat dau tu row a2,b2
            var rangeLabel = xlSheet.Cells["A6:A" + iTotalRow1];
            var range1 = xlSheet.Cells["B6:B" + iTotalRow1];
            //var range2 = xlSheet.Cells["B3:K3"];
            //add the ranges to the chart
            var lineSerires = (ExcelBarChartSerie)lineChart.Series.Add(range1, rangeLabel);
            //lineChart.Series.Add(range2, rangeLabel);

            lineSerires.DataLabel.Font.LatinFont = "Times New Roman";
            lineSerires.DataLabel.Font.Size = 13;
            //set the names of the legend
            lineChart.Series[0].Header = "Doanh số";
            //lineChart.Series[1].Header = xlSheet.Cells["A3"].Value.ToString();
            //position of the legend
            lineChart.Legend.Position = eLegendPosition.Right;

            //size of the chart
            if (iTotalRow1 < 10)
            {
                lineChart.SetSize(800, 600);
            }
            else if (iTotalRow1 >= 10 && iTotalRow1 < 20)
            {
                lineChart.SetSize(1024, 786);
            }
            else
            {
                lineChart.SetSize(1920, 1080);
            }

            //add the chart at cell B6
            lineChart.SetPosition(4, 0, 4, 0);

            xlSheet.Cells.AutoFitColumns();
            #endregion "Chart"

            // chart

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            //setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            //setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "DoanhSoTheoSaleExcelChart_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DoanhSoTheoSaleGroupbyNguoiTao()
        {
            ///////////////////////////////// group by ////////////////////////////////////////////

            //With Query Syntax
            var results1 = (
                from p in BaoCaoVM.TourBaoCaoDtos
                group p by p.NguoiTao into g
                select new TourBaoCaoDtosGroupByNguoiTaoViewModel()
                {
                    NguoiTao = g.Key,
                    TourBaoCaoDtos = g.ToList()
                }
                ).ToList();
            BaoCaoVM.TourBaoCaoDtosGroupByNguoiTaos = results1;
            ////////////// tinh TC /////////////////////

            foreach (var item in results1)
            {
                ////decimal? tongCong = 0;
                //// chua thanh ly hop dong
                //var chuaThanhLyHopDong = item.TourBaoCaoDtos.Where(x => string.IsNullOrEmpty(x.NgayThanhLyHD.ToString())).Sum(x => (x.DoanhThuTT == 0) ? x.DoanhThuDK : x.DoanhThuTT);
                //// da thanh ly hop dong
                //var daThanhLyHopDong = item.TourBaoCaoDtos.Where(x => !string.IsNullOrEmpty(x.NgayThanhLyHD.ToString())).Sum(x => (x.DoanhThuTT == 0) ? x.DoanhThuDK : x.DoanhThuTT);
                //// tong cong theo tung sale
                //var tongCongTheoTungSale = chuaThanhLyHopDong + daThanhLyHopDong;
                // sokhach
                var soKhach = item.TourBaoCaoDtos.Sum(x => (x.SoKhachTT == 0) ? x.SoKhachDK : x.SoKhachTT);

                decimal chuaThanhLyHopDong = 0, daThanhLyHopDong = 0;
                foreach (var itemDto in item.TourBaoCaoDtos)
                {
                    var ngayThanhLyHD = itemDto.NgayThanhLyHD.ToString("dd/MM/yyyy");
                    if (ngayThanhLyHD == "01/01/0001")
                    {
                        chuaThanhLyHopDong += (itemDto.DoanhThuTT == 0) ? itemDto.DoanhThuDK : itemDto.DoanhThuTT;
                    }
                    else
                    {
                        daThanhLyHopDong += (itemDto.DoanhThuTT == 0) ? itemDto.DoanhThuDK : itemDto.DoanhThuTT;
                    }
                }

                foreach (var item1 in item.TourBaoCaoDtos)
                {
                    item1.ChuaThanhLyHopDong = chuaThanhLyHopDong;
                    item1.DaThanhLyHopDong = daThanhLyHopDong;
                    item1.TongCongTheoTungSale = chuaThanhLyHopDong + daThanhLyHopDong;
                    item1.TongSoKhachTheoSale = soKhach;
                }

                //foreach (var item1 in item.ChiTietHdViewModels)
                //{
                //    item1.TC = tongCong;
                //}
            }

            decimal? tongCong = 0;
            int tongCongSK = 0;
            foreach (var item in results1)
            {
                tongCong += item.TourBaoCaoDtos.FirstOrDefault().ChuaThanhLyHopDong + item.TourBaoCaoDtos.FirstOrDefault().DaThanhLyHopDong;
                tongCongSK += item.TourBaoCaoDtos.FirstOrDefault().TongSoKhachTheoSale;
            }
            BaoCaoVM.TongCong = tongCong;
            BaoCaoVM.TongSK = tongCongSK;
            ////////////// tinh TC /////////////////////

            //foreach (var item in results1)
            //{
            //    System.Diagnostics.Debug.WriteLine(item.NoiLamViec);
            //    foreach (var car in item.ChiTietHdViewModels)
            //    {
            //        System.Diagnostics.Debug.WriteLine(car.TenMon);
            //    }
            //}

            //System.Diagnostics.Debug.WriteLine("-----------");

            //////////////////////////// group by/////////////////////////////////////////////////
        }

        #endregion

        #region Doanh so theo thang

        public IActionResult DoanhSoTheoThang(string tuThang1, string denThang1, string nam1,
                                              string tuThang2, string denThang2, string nam2, string chiNhanh)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            // moi load vao
            var currentYear = DateTime.Now.Year;
            var previousYear = currentYear - 1;

            BaoCaoVM = new BaoCaoViewModel()
            {
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                Thangs = Thangs()
            };

            if (string.IsNullOrEmpty(nam1))
            {
                nam1 = previousYear.ToString();
            }

            if (string.IsNullOrEmpty(nam2))
            {
                nam2 = currentYear.ToString();
            }

            tuThang1 ??= "1";
            denThang1 ??= "12";
            tuThang2 ??= "1";
            denThang2 ??= "12";

            ViewBag.tuThang1 = tuThang1;
            ViewBag.denThang1 = denThang1;
            ViewBag.nam1 = nam1;

            ViewBag.tuThang2 = tuThang2;
            ViewBag.denThang2 = denThang2;
            ViewBag.nam2 = nam2;

            ViewBag.chiNhanh = chiNhanh;

            // Error: bat dau phai nho hon ket thuc
            if ((int.Parse(tuThang1) > int.Parse(denThang1)) || (int.Parse(tuThang2) > int.Parse(denThang2)))
            {
                ModelState.AddModelError("", "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
                //1
                //.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels("1", "12", nam1, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                //1

                //2
                //BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels("1", "12", nam2, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                //2
                return View(BaoCaoVM);
            }
            // Error: bat dau phai nho hon ket thuc

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(x => x.Macn == user.MaCN);
                    List<string> listMaCN = new List<string>();
                    listMaCN.Add(user.MaCN);
                    //1
                    BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, listMaCN);
                    //1

                    //2
                    BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, listMaCN);
                    //2
                }
                else
                {

                    if (!string.IsNullOrEmpty(chiNhanh))  // da chon chinhanh
                    {
                        List<string> listMaCN = new List<string>();
                        listMaCN.Add(chiNhanh);

                        //dmchinhanh theo role
                        var listMaCNTheoRole = _unitOfWork.phanKhuCNRepository.GetById(user.RoleId).ChiNhanhs.Split(',').ToList();
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => listMaCNTheoRole.Any(item2 => item1.Macn == item2));
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Append(new Dmchinhanh() { Macn = "-- Select --" }).OrderBy(x => x.Macn);
                        //dmchinhanh theo role

                        //1                    
                        BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, listMaCN);
                        //1

                        //2
                        BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, listMaCN);
                        //2

                    }
                    else // moi load vao
                    {

                        var listMaCN = _unitOfWork.phanKhuCNRepository.GetById(user.RoleId).ChiNhanhs.Split(',').ToList();
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => listMaCN.Any(item2 => item1.Macn == item2));
                        BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Append(new Dmchinhanh() { Macn = "-- Select --" }).OrderBy(x => x.Macn);
                        //1                    
                        BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, listMaCN);
                        //1

                        //2
                        BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, listMaCN);
                        //2

                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(chiNhanh)) // moi load vao
                {
                    List<string> MaCNs = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == chiNhanh).Select(x => x.Macn).ToList();
                    //1
                    BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, MaCNs);
                    //1

                    //2
                    BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, MaCNs);
                    //2
                }
                else // da chon chinhanh
                {
                    //1
                    BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    //1

                    //2
                    BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    //2
                }
            }
            // moi load vao

            return View(BaoCaoVM);
        }
        public IActionResult DoanhSoTheoThangExcel(string tuThang1, string denThang1, string nam1,
                                              string tuThang2, string denThang2, string nam2, string chiNhanh)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            // moi load vao
            var currentYear = DateTime.Now.Year;
            var previousYear = currentYear - 1;

            BaoCaoVM = new BaoCaoViewModel()
            {
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                Thangs = Thangs()
            };

            if (string.IsNullOrEmpty(nam1))
            {
                nam1 = previousYear.ToString();
            }

            if (string.IsNullOrEmpty(nam2))
            {
                nam2 = currentYear.ToString();
            }

            tuThang1 ??= "1";
            denThang1 ??= "12";
            tuThang2 ??= "1";
            denThang2 ??= "12";

            ViewBag.tuThang1 = tuThang1;
            ViewBag.denThang1 = denThang1;
            ViewBag.nam1 = nam1;

            ViewBag.tuThang2 = tuThang2;
            ViewBag.denThang2 = denThang2;
            ViewBag.nam2 = nam2;

            ViewBag.chiNhanh = chiNhanh;

            // Error: bat dau phai nho hon ket thuc
            if ((int.Parse(tuThang1) > int.Parse(denThang1)) || (int.Parse(tuThang2) > int.Parse(denThang2)))
            {
                ModelState.AddModelError("", "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
                //1
                BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels("1", "12", nam1, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                //1

                //2
                BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels("1", "12", nam2, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                //2
                return View(BaoCaoVM);
            }
            // Error: bat dau phai nho hon ket thuc

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    List<string> MaCNs = new List<string>();
                    MaCNs.Add(user.MaCN);
                    //1
                    BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, MaCNs);
                    //1

                    //2
                    BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, MaCNs);
                    //2
                }
                else
                {

                    // chon chi nhanh
                    if (chiNhanh != "-- Select --")// da chon chinhanh
                    {
                        //List<string> MaCNs = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == chiNhanh).Select(x => x.Macn).ToList();
                        List<string> MaCNs = new List<string>();
                        MaCNs.Add(chiNhanh);
                        //1
                        BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, MaCNs);
                        //1

                        //2
                        BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, MaCNs);
                        //2
                    }
                    else  // list chinhanh theo role
                    {
                        //1
                        var listMaCN = _unitOfWork.phanKhuCNRepository.GetById(user.RoleId).ChiNhanhs.Split(',').ToList();

                        BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, listMaCN);
                        //1

                        //2
                        BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, listMaCN);
                        //2

                    }
                    // chon chi nhanh
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(chiNhanh)) // da chon chinhanh
                {
                    //List<string> MaCNs = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == chiNhanh).Select(x => x.Macn).ToList();
                    List<string> MaCNs = new List<string>();
                    MaCNs.Add(chiNhanh);
                    //1
                    BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, MaCNs);
                    //1

                    //2
                    BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, MaCNs);
                    //2
                }
                else  // moi load vao
                {
                    //1
                    BaoCaoVM.TourBaoCaoTheoThangs1 = TourBaoCaoTheoThangViewModels(tuThang1, denThang1, nam1, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    //1

                    //2
                    BaoCaoVM.TourBaoCaoTheoThangs2 = TourBaoCaoTheoThangViewModels(tuThang2, denThang2, nam2, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    //2
                }
            }
            // moi load vao

            byte[] fileContents;
            try
            {
                var thangNam1 = tuThang1 + "/" + denThang1 + "/" + nam1;
                var thangNam2 = tuThang2 + "/" + denThang2 + "/" + nam2;
                var excelPackage = DoanhThuTheoThangExcelResult(thangNam1, thangNam2, BaoCaoVM.TourBaoCaoTheoThangs1, BaoCaoVM.TourBaoCaoTheoThangs2);
                fileContents = excelPackage.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "DoanhSoTheoThang_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ExcelPackage DoanhThuTheoThangExcelResult(string thangNam1, string thangNam2,
                                                          IEnumerable<TourBaoCaoTheoThangViewModel> tourBaoCaoTheoThangViewModels1,
                                                          IEnumerable<TourBaoCaoTheoThangViewModel> tourBaoCaoTheoThangViewModels2)
        {
            var string1 = thangNam1.Split('/');
            var string2 = thangNam2.Split('/');
            string fromTo = "Từ tháng " + string1[0] + " đến tháng " + string1[1] + " năm " + string1[2] + " - " + "từ tháng " + string2[0] + " đến tháng " + string2[1] + " năm " + string2[2];
            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 10;// STT
            xlSheet.Column(2).Width = 12;// Tháng
            xlSheet.Column(3).Width = 25;// Số khách năm 1
            xlSheet.Column(4).Width = 20;// Doanh số năm 1
            xlSheet.Column(5).Width = 20;// Doanh thu năm 1
            xlSheet.Column(6).Width = 25;// Số khách năm 2
            xlSheet.Column(7).Width = 20;// Doanh số năm 2
            xlSheet.Column(8).Width = 20;// Doanh thu năm 2
            xlSheet.Column(9).Width = 10;// Tỉ lệ SK
            xlSheet.Column(10).Width = 10;// Tỉ lệ DT

            xlSheet.Cells[1, 1].Value = "CÔNG TY DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 10].Merge = true;

            xlSheet.Cells[2, 1].Value = "BÁO CÁO DOANH SỐ THEO THÁNG";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 10].Merge = true;
            setCenterAligment(2, 1, 2, 10, xlSheet);

            xlSheet.Cells[3, 1].Value = fromTo;
            xlSheet.Cells[3, 1, 3, 10].Merge = true;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            setCenterAligment(3, 1, 3, 10, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "STT";
            xlSheet.Cells[5, 2].Value = "Tháng ";
            xlSheet.Cells[5, 3].Value = "Số khách năm " + string1[2];
            xlSheet.Cells[5, 4].Value = "Doanh số năm " + string1[2];
            xlSheet.Cells[5, 5].Value = "Doanh thu năm " + string1[2];
            xlSheet.Cells[5, 6].Value = "Số khách năm " + string2[2];
            xlSheet.Cells[5, 7].Value = "Doanh số năm " + string2[2];
            xlSheet.Cells[5, 8].Value = "Doanh thu năm " + string2[2];
            xlSheet.Cells[5, 9].Value = "Tỷ lệ SK";
            xlSheet.Cells[5, 10].Value = "Tỷ lệ DT";

            xlSheet.Cells[5, 1, 5, 10].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 10, xlSheet);
            setCenterAligment(5, 1, 5, 10, xlSheet);
            // do du lieu tu table
            int dong = 6;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            //int idem = 1;

            var tour1Array = tourBaoCaoTheoThangViewModels1.OrderBy(x => int.Parse(x.Thang)).ToArray();
            var tour2Array = tourBaoCaoTheoThangViewModels2.OrderBy(x => int.Parse(x.Thang)).ToArray();

            decimal doanhThu1tong = 0, doanhThu2tong = 0;

            for (int i = 0; i <= 11; i++)
            {
                xlSheet.Cells[dong, 1].Value = (i + 1);
                TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                xlSheet.Cells[dong, 2].Value = "Tháng " + (i + 1);
                TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                // xlSheet.Cells[dong, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                xlSheet.Cells[dong, 3].Value = tour1Array[i].SoKhach;
                TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                xlSheet.Cells[dong, 4].Value = tour1Array[i].DoanhSo;
                TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                var doanhThu1 = tour1Array[i].DoanhSo * 10 / 11;
                doanhThu1tong += doanhThu1;

                xlSheet.Cells[dong, 5].Value = doanhThu1;
                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                xlSheet.Cells[dong, 6].Value = tour2Array[i].SoKhach;
                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                xlSheet.Cells[dong, 7].Value = tour2Array[i].DoanhSo;
                TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                var doanhThu2 = tour2Array[i].DoanhSo * 10 / 11;
                doanhThu2tong += doanhThu2;

                xlSheet.Cells[dong, 8].Value = doanhThu2;
                TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                // xlSheet.Cells[dong, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                int tyLeSK;
                if (tour1Array[i].SoKhach > 0)
                {
                    tyLeSK = tour2Array[i].SoKhach / tour1Array[i].SoKhach * 100;

                }
                else
                {
                    tyLeSK = 0;
                }
                xlSheet.Cells[dong, 9].Value = tyLeSK.ToString("N0") + "%";
                TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                decimal tyLeDoanhThu = 0;
                if (doanhThu1 > 0)
                {
                    tyLeDoanhThu = doanhThu2 / doanhThu1 * 100;
                }

                xlSheet.Cells[dong, 10].Value = tyLeDoanhThu.ToString("N0") + "%";
                TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                dong++;
                //idem++;

            }
            // 1
            xlSheet.Cells[dong, 2].Value = "Tổng Cộng: ";
            TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
            var tongSK1 = tourBaoCaoTheoThangViewModels1.Sum(x => x.SoKhach);
            xlSheet.Cells[dong, 3].Value = tongSK1;
            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
            xlSheet.Cells[dong, 4].Value = tourBaoCaoTheoThangViewModels1.Sum(x => x.DoanhSo);
            TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
            xlSheet.Cells[dong, 5].Value = doanhThu1tong;
            TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

            // 2
            var tongSK2 = tourBaoCaoTheoThangViewModels2.Sum(x => x.SoKhach);
            xlSheet.Cells[dong, 6].Value = tourBaoCaoTheoThangViewModels2.Sum(x => x.SoKhach);
            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
            xlSheet.Cells[dong, 7].Value = tourBaoCaoTheoThangViewModels2.Sum(x => x.DoanhSo);
            TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
            xlSheet.Cells[dong, 8].Value = doanhThu2tong;
            TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

            // tyle tyLeSKCuaTong = tongSK2 / tongSK1 * 100; tyLeDTCuaTong = doanhThu2Tong / doanhThu1Tong * 100
            decimal tongSK2ChiaTongSK1 = 0;
            if (tongSK1 > 0)
            {
                tongSK2ChiaTongSK1 = Convert.ToDecimal(tongSK2) / Convert.ToDecimal(tongSK1);
            }

            xlSheet.Cells[dong, 9].Value = (tongSK2ChiaTongSK1 * 100).ToString("N0") + "%";
            TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
            decimal doanhThu2tongChiadoanhThu1tong = 0;
            if (doanhThu1tong > 0)
            {
                doanhThu2tongChiadoanhThu1tong = doanhThu2tong / doanhThu1tong;
            }
            xlSheet.Cells[dong, 10].Value = (doanhThu2tongChiadoanhThu1tong * 100).ToString("N0") + "%";
            TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

            setFontBold(dong, 2, dong, 10, 12, xlSheet);
            NumberFormat(6, 3, 6 + dong, 8, xlSheet);

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            //setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            //setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            return ExcelApp;

            //byte[] fileContents;
            //try
            //{
            //    fileContents = ExcelApp.GetAsByteArray();
            //    return File(
            //    fileContents: fileContents,
            //    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //    fileDownloadName: "DoanhSoTheoSale_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private IEnumerable<TourBaoCaoTheoThangViewModel> TourBaoCaoTheoThangViewModels(string tuThang1, string denThang1, string nam1, List<string> chiNhanhs)
        {
            var searchFromDate = "01/" + tuThang1 + "/" + nam1;
            string searchToDate = "01/" + denThang1 + "/" + nam1;

            // thang co 31 ngay
            if (denThang1 == "1" || denThang1 == "3" || denThang1 == "5" || denThang1 == "7" || denThang1 == "8" || denThang1 == "10" || denThang1 == "12")
            {
                searchToDate = "31/" + denThang1 + "/" + nam1;
            }
            // thang co 30 ngay
            if (denThang1 == "4" || denThang1 == "6" || denThang1 == "9" || denThang1 == "11")
            {
                searchToDate = "30/" + denThang1 + "/" + nam1;
            }
            // kiem tra nam nhuan
            if ((denThang1 == "2") && (int.Parse(nam1) % 400 == 0)) // chia het 400 => nam nhuan
            {
                searchToDate = "29/" + denThang1 + "/" + nam1;
            }
            if ((denThang1 == "2") && (int.Parse(nam1) % 400 != 0)) // ko phai nam nhuan
            {
                searchToDate = "28/" + denThang1 + "/" + nam1;
            }
            //BaoCaoVM.TourBaoCaoDtos
            IEnumerable<TourBaoCaoDto> tourBaoCaos = _baoCaoService.DoanhSoTheoThang(searchFromDate, searchToDate, chiNhanhs);

            var tourBaoCaoDtos = tourBaoCaos.GroupBy(x => x.NgayTao.Month);
            IEnumerable<TourBaoCaoTheoThangViewModel> tourBaoCaoTheoThangViewModels = tourBaoCaoDtos.Select(x => new TourBaoCaoTheoThangViewModel()
            {
                Thang = x.First().NgayTao.Month.ToString(),
                SoKhach = x.Sum(x => x.SoKhachTT == 0 ? x.SoKhachDK : x.SoKhachTT),
                DoanhSo = x.Sum(x => x.DoanhThuTT == 0 ? x.DoanhThuDK : x.DoanhThuTT)
            });

            var TourBaoCaoTheoThangs1Array = tourBaoCaoTheoThangViewModels.ToArray();
            var count = 12 - TourBaoCaoTheoThangs1Array.Length;

            if (count != 0) // chua du 12 thang
            {
                // add list du 12 thang
                List<TourBaoCaoTheoThangViewModel> list = new List<TourBaoCaoTheoThangViewModel>();
                for (int i = 1; i <= 12; i++)
                {
                    list.Add(new TourBaoCaoTheoThangViewModel() { Thang = i.ToString(), SoKhach = 0, DoanhSo = 0 });
                }

                if (tourBaoCaoTheoThangViewModels.Count() != 0)
                {
                    // chi lay nhung item ma thang khong co' trong BaoCaoVM.TourBaoCaoTheoThangs1
                    foreach (var item in tourBaoCaoTheoThangViewModels)
                    {
                        var itemInList = list.Where(x => int.Parse(x.Thang) == int.Parse(item.Thang));
                        list.Remove(itemInList.FirstOrDefault());
                    }

                    tourBaoCaoTheoThangViewModels = tourBaoCaoTheoThangViewModels.Concat(list);

                }
                else
                {
                    tourBaoCaoTheoThangViewModels = list;
                }

                // add cho du 12 con vao BaoCaoVM.TourBaoCaoTheoThangs1

            }
            return tourBaoCaoTheoThangViewModels;

        }

        #endregion

        #region Doanh so theo ngay di
        public async Task<IActionResult> DoanhSoTheoNgayDi(string searchFromDate = null, string searchToDate = null, string loaiTour = null, string macn = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            //// moi load vao

            BaoCaoVM.Tourkinds = BaoCaoVM.Tourkinds.Append(new Tourkind() { TourkindInf = "" }).OrderBy(x => x.TourkindInf);
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                var currentTime = DateTime.Now;
                string TuNgayDenNgayString = LoadTuNgayDenNgay(currentTime.Month.ToString(), currentTime.Month.ToString(), currentTime.Year.ToString());
                searchFromDate = TuNgayDenNgayString.Split('-')[0];
                searchToDate = TuNgayDenNgayString.Split('-')[1];

            }
            else // da chon ngay thang - // check date correct
            {
                try
                {
                    Convert.ToDateTime(searchFromDate);
                    Convert.ToDateTime(searchToDate);
                }
                catch (Exception)
                {
                    BaoCaoVM = new BaoCaoViewModel()
                    {
                        Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                        Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                        TourBaoCaoDtosTheoNgay = new TourBaoCaoDtosTheoNgay()
                    };

                    ViewBag.macn = macn;
                    ViewBag.loaiTour = loaiTour;
                    ViewBag.searchFromDate = searchFromDate;
                    ViewBag.searchToDate = searchToDate;

                    ModelState.AddModelError("", "Lỗi định dạng ngày tháng.");
                    return View(BaoCaoVM);
                }
            }

            ViewBag.macn = macn;
            ViewBag.loaiTour = loaiTour;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    BaoCaoVM.Dmchinhanhs = new List<Dmchinhanh>() { new Dmchinhanh() { Macn = user.MaCN } };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoNgay(searchFromDate, searchToDate, loaiTour, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);
                    DoanhSoTheoNgay();
                }
                else
                {
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                    List<string> maCns = new List<string>();
                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }

                    BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoNgay(searchFromDate, searchToDate, loaiTour, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());

                    if (!string.IsNullOrEmpty(macn))
                    {
                        BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.MaCNTao == macn);
                    }
                    DoanhSoTheoNgay();
                }
            }
            else
            {
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoNgay(searchFromDate, searchToDate, loaiTour, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                if (!string.IsNullOrEmpty(macn))
                {
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.MaCNTao == macn);
                }
                DoanhSoTheoNgay();
            }



            return View(BaoCaoVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DoanhSoTheoNgayDiExcel(string searchFromDate = null, string searchToDate = null, string loaiTour = null, string macn = null)
        {
            BaoCaoVM.TourBaoCaoDtosTheoNgay = new TourBaoCaoDtosTheoNgay();
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll();

            ViewBag.macn = macn;
            ViewBag.loaiTour = loaiTour;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            string fromTo = "";
            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 10;// STT
            xlSheet.Column(2).Width = 20;// Code đoàn
            xlSheet.Column(3).Width = 15;// Ngày đi
            xlSheet.Column(4).Width = 15;// Ngày về
            xlSheet.Column(5).Width = 40;// Tuyến tham quan
            xlSheet.Column(6).Width = 10;// Số khách
            xlSheet.Column(7).Width = 15;// Doanh số 
            xlSheet.Column(8).Width = 15;// Sales
            xlSheet.Column(9).Width = 35;// Tên công ty/Khách hàng            
            xlSheet.Column(10).Width = 25;// Loại tour
            xlSheet.Column(11).Width = 15;// Nguồn tour
            xlSheet.Column(12).Width = 15;// Ngày tạo            

            xlSheet.Cells[1, 1].Value = "CÔNG TY DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 12].Merge = true;

            xlSheet.Cells[2, 1].Value = "BÁO CÁO DOANH SỐ THEO NGÀY ĐI";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 12].Merge = true;
            setCenterAligment(2, 1, 2, 12, xlSheet);

            // dinh dang tu ngay den ngay
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
                SetAlert("Từ ngày đến ngày không được để trống.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoNgayDi));
            }
            if (searchFromDate == searchToDate)
            {
                fromTo = "Ngày: " + searchFromDate;
            }
            else
            {
                fromTo = "Từ ngày: " + searchFromDate + " đến ngày: " + searchToDate;
            }
            xlSheet.Cells[3, 1].Value = fromTo;
            xlSheet.Cells[3, 1, 3, 12].Merge = true;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            setCenterAligment(3, 1, 3, 12, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "STT";
            xlSheet.Cells[5, 2].Value = "Code đoàn ";
            xlSheet.Cells[5, 3].Value = "Ngày đi";
            xlSheet.Cells[5, 4].Value = "Ngày về";
            xlSheet.Cells[5, 5].Value = "Tuyến tham quan";
            xlSheet.Cells[5, 6].Value = "Số khách";
            xlSheet.Cells[5, 7].Value = "Doanh số";
            xlSheet.Cells[5, 8].Value = "Sales";
            xlSheet.Cells[5, 9].Value = "Tên công ty/Khách hàng ";
            xlSheet.Cells[5, 10].Value = "Loại tour";
            xlSheet.Cells[5, 11].Value = "Nguồn tour";
            xlSheet.Cells[5, 12].Value = "Ngày tạo";

            xlSheet.Cells[5, 1, 5, 12].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 12, xlSheet);
            setCenterAligment(5, 1, 5, 12, xlSheet);

            // do du lieu tu table
            int dong = 6;

            //// moi load vao
            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    BaoCaoVM.Dmchinhanhs = new List<Dmchinhanh>() { new Dmchinhanh() { Macn = user.MaCN } };
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoNgay(searchFromDate, searchToDate, loaiTour, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);
                    DoanhSoTheoNgay();
                }
                else
                {
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                    List<string> maCns = new List<string>();
                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }
                    //BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.Find(x => x.KhuVuc == user.Role.RoleName);
                    BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoNgay(searchFromDate, searchToDate, loaiTour, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    if (!string.IsNullOrEmpty(macn))
                    {
                        BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.MaCNTao == macn);
                    }
                    DoanhSoTheoNgay();
                }
            }
            else
            {
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoNgay(searchFromDate, searchToDate, loaiTour, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                if (!string.IsNullOrEmpty(macn))
                {
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.MaCNTao == macn);
                }
                DoanhSoTheoNgay();
            }
            //return View(BaoCaoVM);

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (BaoCaoVM.TourBaoCaoDtosTheoNgay.TourBaoCaoDtos != null)
            {

                foreach (var item in BaoCaoVM.TourBaoCaoDtosTheoNgay.TourBaoCaoDtos)
                {
                    xlSheet.Cells[dong, 1].Value = idem;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.Sgtcode;
                    xlSheet.Cells[dong, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (item.TrangThai == "3")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(colorThanhLy);
                    }
                    else if (item.TrangThai == "2")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }
                    else if (item.TrangThai == "4")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Red);
                    }
                    else
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.White);
                    }

                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 3].Value = item.NgayDen.ToShortDateString();
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 4].Value = item.NgayDi.ToShortDateString();
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.TuyenTQ;
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 6].Value = (item.SoKhachTT == 0) ? 0 : item.SoKhachTT;
                    xlSheet.Cells[dong, 6].Style.Numberformat.Format = "#,##0";
                    TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 7].Value = (item.DoanhThuTT == 0) ? 0 : item.DoanhThuTT;
                    xlSheet.Cells[dong, 7].Style.Numberformat.Format = "#,##0";
                    TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 8].Value = item.NguoiTao;
                    TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 9].Value = item.CompanyName;
                    TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 10].Value = item.TenLoaiTour;
                    TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 11].Value = item.NguonTour;
                    TrSetCellBorder(xlSheet, dong, 11, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 12].Value = item.NgayTao;
                    TrSetCellBorder(xlSheet, dong, 12, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);

                    dong++;
                    idem++;
                }

                xlSheet.Cells[dong, 5].Value = "TỔNG CỘNG:";
                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSK;
                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDS;
                TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 1, 5].Value = "Các đoàn đã thanh lý:";
                TrSetCellBorder(xlSheet, dong + 1, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                xlSheet.Cells[dong + 1, 5].Style.Fill.BackgroundColor.SetColor(colorThanhLy);
                xlSheet.Cells[dong + 1, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanDaThanhLy;
                TrSetCellBorder(xlSheet, dong + 1, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 1, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanDaThanhLy;
                TrSetCellBorder(xlSheet, dong + 1, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 2, 5].Value = "Các đoàn chưa thanh lý:";
                TrSetCellBorder(xlSheet, dong + 2, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                xlSheet.Cells[dong + 2, 5].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                xlSheet.Cells[dong + 2, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanChuaThanhLy;
                TrSetCellBorder(xlSheet, dong + 2, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 2, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanChuaThanhLy;
                TrSetCellBorder(xlSheet, dong + 2, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 3, 5].Value = "Các đoàn chưa ký hợp đồng:";
                TrSetCellBorder(xlSheet, dong + 3, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 3, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanChuaKyHD;
                TrSetCellBorder(xlSheet, dong + 3, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 3, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanChuaKyHD;
                TrSetCellBorder(xlSheet, dong + 3, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                setBorder(dong, 1, dong + 3, 12, xlSheet);
                setFontBold(dong, 1, dong + 3, 12, 12, xlSheet);
                NumberFormat(6, 6, dong + 3, 7, xlSheet);

                DateFormat(6, 12, dong, 12, xlSheet);
                //xlSheet.Cells[dong, 1, dong, 12].Merge = true;
                //xlSheet.Cells[dong, 1].Value = vm.NguoiTao;
                //xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                ////TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 1].Style.Font.Bold = true;

                //NumberFormat(6, 8, dong + 1, 9, xlSheet);
                //dong = dong + 3;
                //idem = 1;

                //NumberFormat(dong, 2, dong, 5, xlSheet);
                //setFontBold(dong, 2, dong, 5, 12, xlSheet);
                //setBorder(dong, 2, dong, 5, xlSheet);
            }
            else
            {
                SetAlert("No sale.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoNgayDi));
            }

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "DoanhSoTheoNgayDi_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void DoanhSoTheoNgay()
        {

            BaoCaoVM.TourBaoCaoDtosTheoNgay.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos;
            //foreach (var item in BaoCaoVM.TourBaoCaoDtos)
            //{
            //var sk = (item.SoKhachTT == 0) ? item.SoKhachDK : item.SoKhachTT;
            var sk = BaoCaoVM.TourBaoCaoDtos.Sum(x => (x.SoKhachTT == 0) ? x.SoKhachDK : x.SoKhachTT);
            //var ds = BaoCaoVM.TourBaoCaoDtos.Sum(x => (x.DoanhThuTT == 0) ? x.DoanhThuDK : x.DoanhThuTT);
            var ds = BaoCaoVM.TourBaoCaoDtos.Sum(x => x.DoanhThuTT);

            //var skCacDoanDaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "3") // (3)
            //                                                .Sum(x => (x.SoKhachTT == 0) ? x.SoKhachDK : x.SoKhachTT);
            var skCacDoanDaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "3") // (3)
                                                            .Sum(x => x.SoKhachTT);

            //var dsCacDoanDaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "3")
            //                                                .Sum(x => (x.DoanhThuTT == 0) ? x.DoanhThuDK : x.DoanhThuTT);
            
            var dsCacDoanDaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "3")
                                                            .Sum(x => x.DoanhThuTT);

            //var skCacDoanChuaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai != "3") // gom moitao (0), da damphan (1), da ky Hd (2)
            //                                                  .Sum(x => (x.SoKhachTT == 0) ? x.SoKhachDK : x.SoKhachTT);

            var skCacDoanChuaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai != "3") // gom moitao (0), da damphan (1), da ky Hd (2)
                                                  .Sum(x => x.SoKhachTT);

            //var dsCacDoanChuaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai != "3")
            //                                                  .Sum(x => (x.DoanhThuTT == 0) ? x.DoanhThuDK : x.DoanhThuTT);

            var dsCacDoanChuaThanhLy = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai != "3")
                                                              .Sum(x => x.DoanhThuTT);

            //var skCacDoanChuaKyHD = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "0" || x.TrangThai == "1") // gom moitao (0), da damphan (1)
            //                                               .Sum(x => (x.SoKhachTT == 0) ? x.SoKhachDK : x.SoKhachTT);
            
            var skCacDoanChuaKyHD = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "0" || x.TrangThai == "1") // gom moitao (0), da damphan (1)
                                                           .Sum(x => x.SoKhachTT);

            //var dsCacDoanChuaKyHD = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "0" || x.TrangThai == "1")
            //                                               .Sum(x => (x.DoanhThuTT == 0) ? x.DoanhThuDK : x.DoanhThuTT);
            
            var dsCacDoanChuaKyHD = BaoCaoVM.TourBaoCaoDtos.Where(x => x.TrangThai == "0" || x.TrangThai == "1")
                                                           .Sum(x => x.DoanhThuTT);

            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSK += sk;
            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDS += ds;

            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanDaThanhLy += skCacDoanDaThanhLy;
            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanDaThanhLy += dsCacDoanDaThanhLy;

            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanChuaThanhLy += skCacDoanChuaThanhLy;
            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanChuaThanhLy += dsCacDoanChuaThanhLy;

            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanChuaKyHD += skCacDoanChuaKyHD;
            BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanChuaKyHD += dsCacDoanChuaKyHD;

            //}

            //foreach (var item1 in item.ChiTietHdViewModels)
            //{
            //    item1.TC = tongCong;
            //}


        }

        private string LoadTuNgayDenNgay(string tuThang1, string denThang1, string nam1)
        {
            string searchFromDate = "01/" + tuThang1 + "/" + nam1;
            string searchToDate = "01/" + denThang1 + "/" + nam1;

            // thang co 31 ngay
            if (denThang1 == "1" || denThang1 == "3" || denThang1 == "5" || denThang1 == "7" || denThang1 == "8" || denThang1 == "10" || denThang1 == "12")
            {
                searchToDate = "31/" + denThang1 + "/" + nam1;
            }
            // thang co 30 ngay
            if (denThang1 == "4" || denThang1 == "6" || denThang1 == "9" || denThang1 == "11")
            {
                searchToDate = "30/" + denThang1 + "/" + nam1;
            }
            // kiem tra nam nhuan
            if ((denThang1 == "2") && (int.Parse(nam1) % 400 == 0)) // chia het 400 => nam nhuan
            {
                searchToDate = "29/" + denThang1 + "/" + nam1;
            }
            if ((denThang1 == "2") && (int.Parse(nam1) % 400 != 0)) // ko phai nam nhuan
            {
                searchToDate = "28/" + denThang1 + "/" + nam1;
            }

            return searchFromDate + "-" + searchToDate;
        }

        #endregion

        #region Doanh so theo thi truong
        public async Task<IActionResult> DoanhSoTheoThiTruong(string searchFromDate = null, string searchToDate = null, string thiTruong = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            //// moi load vao
            BaoCaoVM.Phongbans = BaoCaoVM.Phongbans.Where(x => x.Maphong == "KDIB");
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                var currentTime = DateTime.Now;
                string TuNgayDenNgayString = LoadTuNgayDenNgay(currentTime.Month.ToString(), currentTime.Month.ToString(), currentTime.Year.ToString());
                searchFromDate = TuNgayDenNgayString.Split('-')[0];
                searchToDate = TuNgayDenNgayString.Split('-')[1];

            }
            else // da chon ngay thang - // check date correct
            {
                try
                {
                    Convert.ToDateTime(searchFromDate);
                    Convert.ToDateTime(searchToDate);
                }
                catch (Exception)
                {
                    BaoCaoVM = new BaoCaoViewModel()
                    {
                        Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                        Tourkinds = _unitOfWork.tourKindRepository.GetAll(),
                        TourBaoCaoDtosTheoNgay = new TourBaoCaoDtosTheoNgay()
                    };

                    //ViewBag.macn = macn;
                    ViewBag.thiTruong = thiTruong;
                    ViewBag.searchFromDate = searchFromDate;
                    ViewBag.searchToDate = searchToDate;

                    ModelState.AddModelError("", "Lỗi định dạng ngày tháng.");
                    return View(BaoCaoVM);
                }
            }

            //ViewBag.macn = macn;
            ViewBag.thiTruong = thiTruong;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            List<string> thiTruongs = new List<string>();
            List<string> maCns = new List<string>();

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {

                    BaoCaoVM.Phongbans = BaoCaoVM.Phongbans.Where(x => x.Maphong == user.PhongBanId);
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoThiTruong(searchFromDate, searchToDate, BaoCaoVM.Phongbans.Select(x => x.Maphong).ToList()); //, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());

                    // loc theo thi truong cua minh
                    //BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.ThiTruongByNguoiTao == user.PhongBanId);

                    // loc chi nhanh
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);

                    DoanhSoTheoNgay();
                }
                else
                {
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);

                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }
                    thiTruongs = new List<string>();
                    foreach (var item in maCns)
                    {
                        var users = await _unitOfWork.userRepository.FindIncludeOneAsync(x => x.Role, y => y.MaCN == item);
                        var phongBanIds = users.Select(x => x.PhongBanId).Distinct();
                        thiTruongs.AddRange(phongBanIds);
                    }
                    BaoCaoVM.Phongbans = BaoCaoVM.Phongbans.Where(item1 => thiTruongs.Any(item2 => item1.Maphong == item2));
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoThiTruong(searchFromDate, searchToDate, thiTruongs);
                    if (!string.IsNullOrEmpty(thiTruong))
                    {
                        BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.ThiTruongByNguoiTao == thiTruong);
                    }

                    // loc chi nhanh
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(item1 => maCns.Any(item2 => item1.MaCNTao == item2));

                    DoanhSoTheoNgay();
                }
            }
            else
            {
                thiTruongs = new List<string>();
                thiTruongs.AddRange(_unitOfWork.phongBanRepository.GetAll().Where(x => !string.IsNullOrEmpty(x.Macode)).Select(x => x.Maphong).Distinct());
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoThiTruong(searchFromDate, searchToDate, thiTruongs);
                if (!string.IsNullOrEmpty(thiTruong))
                {
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.ThiTruongByNguoiTao == thiTruong);
                }
                DoanhSoTheoNgay();
            }

            return View(BaoCaoVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DoanhSoTheoThiTruongExcel(string searchFromDate = null, string searchToDate = null, string thiTruong = null)
        {
            BaoCaoVM.TourBaoCaoDtosTheoNgay = new TourBaoCaoDtosTheoNgay();
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            //BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll();

            //ViewBag.macn = macn;
            ViewBag.thiTruong = thiTruong;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            string fromTo = "";
            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 10;// STT
            xlSheet.Column(2).Width = 20;// Code đoàn
            xlSheet.Column(3).Width = 15;// Ngày đi
            xlSheet.Column(4).Width = 15;// Ngày về
            xlSheet.Column(5).Width = 40;// Tuyến tham quan
            xlSheet.Column(6).Width = 10;// Số khách
            xlSheet.Column(7).Width = 15;// Doanh số 
            xlSheet.Column(8).Width = 15;// Sales
            xlSheet.Column(9).Width = 35;// Tên công ty/Khách hàng            
            xlSheet.Column(10).Width = 25;// Loại tour
            xlSheet.Column(11).Width = 15;// Nguồn tour
            xlSheet.Column(12).Width = 15;// Ngày tạo            
            xlSheet.Column(13).Width = 15;// Thị trường     

            xlSheet.Cells[1, 1].Value = "CÔNG TY DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 13].Merge = true;

            xlSheet.Cells[2, 1].Value = "BÁO CÁO DOANH SỐ THEO THỊ TRƯỜNG";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 13].Merge = true;
            setCenterAligment(2, 1, 2, 13, xlSheet);

            // dinh dang tu ngay den ngay
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
                SetAlert("Từ ngày đến ngày không được để trống.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoThiTruong));
            }
            if (searchFromDate == searchToDate)
            {
                fromTo = "Ngày: " + searchFromDate;
            }
            else
            {
                fromTo = "Từ ngày: " + searchFromDate + " đến ngày: " + searchToDate;
            }
            xlSheet.Cells[3, 1].Value = fromTo;
            xlSheet.Cells[3, 1, 3, 12].Merge = true;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            setCenterAligment(3, 1, 3, 12, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "STT";
            xlSheet.Cells[5, 2].Value = "Code đoàn ";
            xlSheet.Cells[5, 3].Value = "Ngày đi";
            xlSheet.Cells[5, 4].Value = "Ngày về";
            xlSheet.Cells[5, 5].Value = "Tuyến tham quan";
            xlSheet.Cells[5, 6].Value = "Số khách";
            xlSheet.Cells[5, 7].Value = "Doanh số";
            xlSheet.Cells[5, 8].Value = "Sales";
            xlSheet.Cells[5, 9].Value = "Tên công ty/Khách hàng ";
            xlSheet.Cells[5, 10].Value = "Loại tour";
            xlSheet.Cells[5, 11].Value = "Nguồn tour";
            xlSheet.Cells[5, 12].Value = "Ngày tạo";
            xlSheet.Cells[5, 13].Value = "Thị trường";

            xlSheet.Cells[5, 1, 5, 13].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 13, xlSheet);
            setCenterAligment(5, 1, 5, 13, xlSheet);

            // do du lieu tu table
            int dong = 6;

            //// moi load vao
            List<string> thiTruongs = new List<string>();
            List<string> maCns = new List<string>();

            if (user.Role.RoleName != "Admins")
            {
                if (user.Role.RoleName == "Users")
                {
                    //BaoCaoVM.Dmchinhanhs = new List<Dmchinhanh>() { new Dmchinhanh() { Macn = user.MaCN } };
                    BaoCaoVM.Phongbans = _unitOfWork.phongBanRepository.Find(x => x.Maphong == user.PhongBanId);
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoThiTruong(searchFromDate, searchToDate, BaoCaoVM.Phongbans.Select(x => x.Maphong).ToList()); //, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.NguoiTao == user.Username);
                    DoanhSoTheoNgay();
                }
                else
                {
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);

                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }
                    thiTruongs = new List<string>();
                    foreach (var item in maCns)
                    {
                        var users = await _unitOfWork.userRepository.FindIncludeOneAsync(x => x.Role, y => y.MaCN == item);
                        var phongBanIds = users.Select(x => x.PhongBanId).Distinct();
                        thiTruongs.AddRange(phongBanIds);
                    }

                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoThiTruong(searchFromDate, searchToDate, thiTruongs);
                    if (!string.IsNullOrEmpty(thiTruong))
                    {
                        BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.ThiTruongByNguoiTao == thiTruong);
                    }

                    // loc chi nhanh
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(item1 => maCns.Any(item2 => item1.MaCNTao == item2));

                    DoanhSoTheoNgay();
                }
            }
            else
            {
                thiTruongs = new List<string>();
                thiTruongs.AddRange(_unitOfWork.phongBanRepository.GetAll().Where(x => !string.IsNullOrEmpty(x.Macode)).Select(x => x.Maphong).Distinct());
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoThiTruong(searchFromDate, searchToDate, thiTruongs);
                if (!string.IsNullOrEmpty(thiTruong))
                {
                    BaoCaoVM.TourBaoCaoDtos = BaoCaoVM.TourBaoCaoDtos.Where(x => x.ThiTruongByNguoiTao == thiTruong);
                }
                DoanhSoTheoNgay();
            }

            //return View(BaoCaoVM);

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (BaoCaoVM.TourBaoCaoDtosTheoNgay.TourBaoCaoDtos != null)
            {

                foreach (var item in BaoCaoVM.TourBaoCaoDtosTheoNgay.TourBaoCaoDtos)
                {
                    xlSheet.Cells[dong, 1].Value = idem;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.Sgtcode;
                    xlSheet.Cells[dong, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (item.TrangThai == "3")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(colorThanhLy);
                    }
                    else if (item.TrangThai == "2")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }
                    else if (item.TrangThai == "4")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Red);
                    }
                    else
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.White);
                    }

                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 3].Value = item.NgayDen.ToShortDateString();
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 4].Value = item.NgayDi.ToShortDateString();
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.TuyenTQ;
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 6].Value = (item.SoKhachTT == 0) ? 0 : item.SoKhachTT;
                    xlSheet.Cells[dong, 6].Style.Numberformat.Format = "#,##0";
                    TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 7].Value = (item.DoanhThuTT == 0) ? 0 : item.DoanhThuTT;
                    xlSheet.Cells[dong, 7].Style.Numberformat.Format = "#,##0";
                    TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 8].Value = item.NguoiTao;
                    TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 9].Value = item.CompanyName;
                    TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 10].Value = item.TenLoaiTour;
                    TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 11].Value = item.NguonTour;
                    TrSetCellBorder(xlSheet, dong, 11, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 12].Value = item.NgayTao;
                    TrSetCellBorder(xlSheet, dong, 12, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 13].Value = item.ThiTruongByNguoiTao;
                    TrSetCellBorder(xlSheet, dong, 13, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);

                    dong++;
                    idem++;
                }

                xlSheet.Cells[dong, 5].Value = "TỔNG CỘNG:";
                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSK;
                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDS;
                TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 1, 5].Value = "Các đoàn đã thanh lý:";
                TrSetCellBorder(xlSheet, dong + 1, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                xlSheet.Cells[dong + 1, 5].Style.Fill.BackgroundColor.SetColor(colorThanhLy);
                xlSheet.Cells[dong + 1, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanDaThanhLy;
                TrSetCellBorder(xlSheet, dong + 1, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 1, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanDaThanhLy;
                TrSetCellBorder(xlSheet, dong + 1, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 2, 5].Value = "Các đoàn chưa thanh lý:";
                TrSetCellBorder(xlSheet, dong + 2, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                xlSheet.Cells[dong + 2, 5].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                xlSheet.Cells[dong + 2, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanChuaThanhLy;
                TrSetCellBorder(xlSheet, dong + 2, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 2, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanChuaThanhLy;
                TrSetCellBorder(xlSheet, dong + 2, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 3, 5].Value = "Các đoàn chưa ký hợp đồng:";
                TrSetCellBorder(xlSheet, dong + 3, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                xlSheet.Cells[dong + 3, 6].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongSKCacDoanChuaKyHD;
                TrSetCellBorder(xlSheet, dong + 3, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                xlSheet.Cells[dong + 3, 7].Value = BaoCaoVM.TourBaoCaoDtosTheoNgay.TongDSCacDoanChuaKyHD;
                TrSetCellBorder(xlSheet, dong + 3, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);

                setBorder(dong, 1, dong + 3, 13, xlSheet);
                setFontBold(dong, 1, dong + 3, 13, 12, xlSheet);
                NumberFormat(6, 6, dong + 3, 7, xlSheet);

                DateFormat(6, 12, dong, 12, xlSheet);
                //xlSheet.Cells[dong, 1, dong, 12].Merge = true;
                //xlSheet.Cells[dong, 1].Value = vm.NguoiTao;
                //xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                ////TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                //xlSheet.Cells[dong, 1].Style.Font.Bold = true;

                //NumberFormat(6, 8, dong + 1, 9, xlSheet);
                //dong = dong + 3;
                //idem = 1;

                //NumberFormat(dong, 2, dong, 5, xlSheet);
                //setFontBold(dong, 2, dong, 5, 12, xlSheet);
                //setBorder(dong, 2, dong, 5, xlSheet);
            }
            else
            {
                SetAlert("No sale.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoThiTruong));
            }

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "DoanhSoTheoThiTruong_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> BienNhanExcel(string searchFromDate = null, string searchToDate = null, string searchString = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            string fromTo = "";
            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 10;// STT
            xlSheet.Column(2).Width = 20;// CODE ĐOÀN
            xlSheet.Column(3).Width = 20;// SỐ BN
            xlSheet.Column(4).Width = 15;// NGÀY BN
            xlSheet.Column(5).Width = 50;// NỘI DUNG
            xlSheet.Column(6).Width = 15;// SỐ TIỀN
            xlSheet.Column(7).Width = 15;// NGƯỜI TẠO

            xlSheet.Cells[1, 1].Value = "CÔNG TY DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 7].Merge = true;

            xlSheet.Cells[2, 1].Value = "DANH SÁCH BIÊN NHẬN THEO NGÀY";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 7].Merge = true;
            setCenterAligment(2, 1, 2, 7, xlSheet);
            // dinh dang tu ngay den ngay
            //if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            //{
            //    ViewBag.searchFromDate = searchFromDate;
            //    ViewBag.searchToDate = searchToDate;
            //    SetAlert("Từ ngày đến ngày không được để trống.", "warning");
            //    //return RedirectToAction(nameof(DoanhSoTheoSale));
            //    return LocalRedirect("/BienNhans/Index");
            //}
            if (searchFromDate == searchToDate)
            {
                fromTo = "Ngày: " + searchFromDate;
            }
            else
            {
                fromTo = "Từ ngày: " + searchFromDate + " đến ngày: " + searchToDate;
            }
            xlSheet.Cells[3, 1].Value = fromTo;
            xlSheet.Cells[3, 1, 3, 7].Merge = true;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            setCenterAligment(3, 1, 3, 7, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "STT";
            xlSheet.Cells[5, 2].Value = "CODE ĐOÀN";
            xlSheet.Cells[5, 3].Value = "SỐ BN";
            xlSheet.Cells[5, 4].Value = "NGÀY BN";
            xlSheet.Cells[5, 5].Value = "NỘI DUNG";
            xlSheet.Cells[5, 6].Value = "SỐ TIỀN";
            xlSheet.Cells[5, 7].Value = "NGƯỜI TẠO";

            xlSheet.Cells[5, 1, 5, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 7, xlSheet);
            setCenterAligment(5, 1, 5, 7, xlSheet);

            // do du lieu tu table
            int dong = 6;

            BaoCaoVM.BienNhanDtos = await _baoCaoService.BienNhanExcel(searchFromDate, searchToDate, searchString);

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (BaoCaoVM.BienNhanDtos != null)
            {

                foreach (var item in BaoCaoVM.BienNhanDtos)
                {
                    xlSheet.Cells[dong, 1].Value = idem;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.Sgtcode;
                    xlSheet.Cells[dong, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (item.TrangThai == "3")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(colorThanhLy);
                    }
                    else if (item.TrangThai == "2")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }
                    else if (item.TrangThai == "4")
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.Red);
                    }
                    else
                    {
                        xlSheet.Cells[dong, 2].Style.Fill.BackgroundColor.SetColor(Color.White);
                    }

                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 3].Value = item.SoBN;
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    if (item.HuyBN == true)
                    {
                        xlSheet.Cells[dong, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        xlSheet.Cells[dong, 3].Style.Fill.BackgroundColor.SetColor(Color.Red);
                        
                    }
                    
                    xlSheet.Cells[dong, 4].Value = item.NgayBN.ToShortDateString();
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.NoiDung.Replace("*", "\n");
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    xlSheet.Cells[dong, 5].Style.WrapText = true;
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 6].Value = item.SoTien.ToString("N0");
                    TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 7].Value = item.NguoiTao;
                    TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);

                    dong++;
                    idem++;
                }

            }
            else
            {
                SetAlert("No sale.", "warning");
                return RedirectToAction(nameof(DoanhSoTheoSale));
            }

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            setCenterAligment(6, 1, 6 + dong, 1, xlSheet);
            xlSheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // canh giua code chinhanh
            //setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "DSBienNhan_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void NumberFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                range.Style.Numberformat.Format = "#,#0";
            }
        }

        private static void DateFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Numberformat.Format = "dd/MM/yyyy";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        private static void DateTimeFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        private static void setRightAligment(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        private static void setCenterAligment(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        private static void setFontSize(int fromRow, int fromColumn, int toRow, int toColumn, int fontSize, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Font.SetFromFont(new Font("Times New Roman", fontSize, FontStyle.Regular));
            }
        }

        private static void setFontBold(int fromRow, int fromColumn, int toRow, int toColumn, int fontSize, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Font.SetFromFont(new Font("Times New Roman", fontSize, FontStyle.Bold));
            }
        }

        private static void setBorder(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }

        private static void setBorderAround(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
        }

        private static void PhantramFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "0 %";
            }
        }

        private static void mergercell(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Merge = true;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                range.Style.WrapText = true;
            }
        }

        private static void numberMergercell(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                var a = sheet.Cells[fromRow, fromColumn].Value;
                range.Merge = true;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                range.Clear();
                sheet.Cells[fromRow, fromColumn].Value = a;
            }
        }

        private static void wrapText(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.WrapText = true;
            }
        }

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        ///////////////// new ///////////////////
        ///
        public void TrSetCellBorder(ExcelWorksheet xlSheet, int iRowIndex, int colIndex, ExcelBorderStyle excelBorderStyle, ExcelHorizontalAlignment excelHorizontalAlignment, Color borderColor, string fontName, int fontSize, FontStyle fontStyle)
        {
            xlSheet.Cells[iRowIndex, colIndex].Style.HorizontalAlignment = excelHorizontalAlignment;
            // Set Border
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Left.Style = excelBorderStyle;
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Top.Style = excelBorderStyle;
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Right.Style = excelBorderStyle;
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Bottom.Style = excelBorderStyle;
            // Set màu ch Border
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Left.Color.SetColor(borderColor);
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Top.Color.SetColor(borderColor);
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Right.Color.SetColor(borderColor);
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Bottom.Color.SetColor(borderColor);

            // Set Font cho text  trong Range hiện tại
            xlSheet.Cells[iRowIndex, colIndex].Style.Font.SetFromFont(new Font(fontName, fontSize, fontStyle));
        }
    }
}