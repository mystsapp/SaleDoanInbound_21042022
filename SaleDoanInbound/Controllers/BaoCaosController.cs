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
                    foreach(var item in phanKhuCNs)
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
                List<ChiTietHdViewModel> chiTietHdViewModels = new List<ChiTietHdViewModel>();
                foreach (var item in d)
                {
                    chiTietHdViewModels.Add(new ChiTietHdViewModel
                    {
                        HoTen = item.HoaDon.NhanVien.HoTen,
                        VPName = item.HoaDon.VanPhong.Name,
                        KVName = item.HoaDon.NhanVien.KhuVuc.Name,
                        NgayTao = item.HoaDon.NgayTao,
                        TenMon = item.ThucDon.TenMon,
                        SoLuong = item.SoLuong,
                        DonGia = item.DonGia,
                        ThanhTien = item.SoLuong * item.DonGia,
                        TC = 0,
                        TenBan = item.HoaDon.Ban.TenBan,
                        NoiLamViec = item.ThucDon.LoaiThucDon.NoiLamViec
                    });
                }

                List<TourBaoCaoDto> tourBaoCaoDtos = new List<TourBaoCaoDto>()
                {

                }

                //With Query Syntax

                List<ChiTietHDGroupByResultViewModel> results1 = (
                    from p in chiTietHdViewModels
                    group p by p.NoiLamViec into g
                    select new ChiTietHDGroupByResultViewModel()
                    {
                        NoiLamViec = g.Key,
                        ChiTietHdViewModels = g.ToList()
                    }
                    ).ToList();

                ////////////// tinh TC /////////////////////

                foreach (var item in results1)
                {
                    decimal? tongCong = 0;
                    foreach (var item1 in item.ChiTietHdViewModels)
                    {
                        tongCong += item1.ThanhTien;
                    }

                    foreach (var item1 in item.ChiTietHdViewModels)
                    {
                        item1.TC = tongCong;
                    }

                }

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