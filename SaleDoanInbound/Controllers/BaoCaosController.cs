using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Services;
using Data.Utilities;
using Microsoft.AspNetCore.Mvc;
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
                }
            }
            else
            {
                BaoCaoVM.TourBaoCaoDtos = _baoCaoService.DoanhSoTheoSale(searchFromDate, searchToDate, BaoCaoVM.Dmchinhanhs.Select(x => x.Macn).ToList());
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

            return View(BaoCaoVM);
        }
    }
}