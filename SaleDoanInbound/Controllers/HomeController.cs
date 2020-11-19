using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Models_QLT;
using Data.Repository;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class HomeController : BaseController
    {
        [BindProperty]
        public HomeViewModel HomeVM { get; set; }
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaoCaoService _baoCaoService;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IBaoCaoService baoCaoService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _baoCaoService = baoCaoService;

            HomeVM = new HomeViewModel();
        }

        public async Task<IActionResult> Index()
        {
            var currentDateTime = DateTime.Now;
            var previousDateTime = currentDateTime.AddMonths(-1);

            var currentTours = await _baoCaoService.ChartDoanhSoTheoThiTruongs(currentDateTime); // current month
            var previousTours = await _baoCaoService.ChartDoanhSoTheoThiTruongs(previousDateTime); // previous month

            HomeVM.CurrentTourTheoThiTruongViewModels = TourBaoCaoTheoThangViewModels(currentTours);
            HomeVM.PreviousTourTheoThiTruongViewModels = TourBaoCaoTheoThangViewModels(previousTours);
            
            return View(HomeVM);
        }


        private IEnumerable<TourTheoThiTruongViewModel> TourBaoCaoTheoThangViewModels(IEnumerable<TourBaoCaoDto> tourBaoCaoDtos)
        {
            
            var tourBaoCaoDtosGroupBy = tourBaoCaoDtos.GroupBy(x => x.ThiTruongByNguoiTao);
            IEnumerable<TourTheoThiTruongViewModel> tourBaoCaoDtosGroupBys = tourBaoCaoDtosGroupBy.Select(x => new TourTheoThiTruongViewModel()
            {
                TenThiTruong = x.First().ThiTruongByNguoiTao,
                SoKhach = x.Sum(x => x.SoKhachTT == 0 ? x.SoKhachDK : x.SoKhachTT),
                DoanhSo = x.Sum(x => x.DoanhThuTT == 0 ? x.DoanhThuDK : x.DoanhThuTT)
            });
            var phongbans = _unitOfWork.phongBanRepository.Find(x => !string.IsNullOrEmpty(x.Macode));
            IEnumerable<string> listMaPhongBan = phongbans.Select(x => x.Maphong);
            listMaPhongBan = listMaPhongBan.Except(tourBaoCaoDtosGroupBys.Select(x => x.TenThiTruong));

            List<TourTheoThiTruongViewModel> tourTheoThiTruongNull = new List<TourTheoThiTruongViewModel>();
            foreach (var item in listMaPhongBan)
            {
                if(tourBaoCaoDtosGroupBys.Any(x => x.TenThiTruong != item)) // chua co ten thi truong trong list
                {
                    tourTheoThiTruongNull.Add(new TourTheoThiTruongViewModel()
                    {
                        TenThiTruong = item,
                        DoanhSo = 0,
                        SoKhach = 0
                    });
                }
                
            }
            if(tourTheoThiTruongNull.Count != 0)
            {
                tourBaoCaoDtosGroupBys = tourBaoCaoDtosGroupBys.Concat(tourTheoThiTruongNull);
            }
            return tourBaoCaoDtosGroupBys.OrderBy(x => x.TenThiTruong);

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
