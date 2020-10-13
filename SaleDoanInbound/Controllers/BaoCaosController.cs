using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Services;
using Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SaleDoanInbound.Models;

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
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll()
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DoanhSoTheoSale(string searchFromDate = null, string searchToDate = null, string Macn = null)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            ViewBag.Macn = Macn;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            //// moi load vao
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
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                    List<string> maCns = new List<string>();
                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }
                    //BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.Find(x => x.KhuVuc == user.Role.RoleName);
                    BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
            }
            else
            {
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());

                DoanhSoTheoSaleGroupbyNguoiTao();

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
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                    List<string> maCns = new List<string>();
                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }
                    //BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.Find(x => x.KhuVuc == user.Role.RoleName);
                    BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
            }
            else
            {
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());

                DoanhSoTheoSaleGroupbyNguoiTao();
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

                        xlSheet.Cells[dong, 9].Value = item.DoanhThuDK.ToString("N0");
                        TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 10].Value = item.SoKhachTT;
                        TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 11].Value = item.DoanhThuTT.ToString("N0");
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
                fileDownloadName: "TheoNgayBay_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");

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
            xlSheet.Cells[5, 1].Value = "Sale";
            xlSheet.Cells[5, 2].Value = "Doanh số ";

            xlSheet.Cells[5, 1, 5, 2].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 2, xlSheet);
            setCenterAligment(5, 1, 5, 12, xlSheet);
            // do du lieu tu table
            int dong = 6;

            //// moi load vao
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
                    var phanKhuCNs = await _unitOfWork.phanKhuCNRepository.FindIncludeOneAsync(x => x.Role, y => y.RoleId == user.RoleId);
                    List<string> maCns = new List<string>();
                    foreach (var item in phanKhuCNs)
                    {
                        maCns.AddRange(item.ChiNhanhs.Split(',').ToList());
                    }
                    //BaoCaoVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.Find(x => x.KhuVuc == user.Role.RoleName);
                    BaoCaoVM.Dmchinhanhs = BaoCaoVM.Dmchinhanhs.Where(item1 => maCns.Any(item2 => item1.Macn == item2));
                    BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
                    DoanhSoTheoSaleGroupbyNguoiTao();
                }
            }
            else
            {
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());

                DoanhSoTheoSaleGroupbyNguoiTao();
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

            // chart

            var lineChart = workSheetChart.Drawings.AddChart("lineChart", eChartType.ColumnClustered);
            //workSheetChart.Cells["A1"].LoadFromDataTable(dt1, false);
            //set the title
            lineChart.Title.Font.LatinFont = "Times New Roman";
            lineChart.Title.Font.Size = 16;
            lineChart.Title.Font.Bold = true;
            lineChart.Title.Text = "Đoàn đi tour từ ngày " + d1.ToString("dd/MM/yyyy") + " đến ngày " + d2.ToString("dd/MM/yyyy");
            //create the ranges for the chart
            iTotalRow1 = iTotalRow1 + 1;//+1 do bat dau tu row a2,b2
            var rangeLabel = workSheetChart.Cells["A2:A" + iTotalRow1];
            var range1 = workSheetChart.Cells["B2:B" + iTotalRow1];
            //var range2 = workSheetChart.Cells["B3:K3"];
            //add the ranges to the chart
            var lineSerires = (ExcelBarChartSerie)lineChart.Series.Add(range1, rangeLabel);
            //lineChart.Series.Add(range2, rangeLabel);

            lineSerires.DataLabel.Font.LatinFont = "Times New Roman";
            lineSerires.DataLabel.Font.Size = 13;
            //set the names of the legend
            lineChart.Series[0].Header = "Doanh số";
            //lineChart.Series[1].Header = workSheetChart.Cells["A3"].Value.ToString();
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
            lineChart.SetPosition(1, 0, 4, 0);

            workSheetChart.Cells.AutoFitColumns();
#endregion


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