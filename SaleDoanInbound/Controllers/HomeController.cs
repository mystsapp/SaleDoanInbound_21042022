using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            var currentTours = await _baoCaoService.ChartDoanhSoTheoThiTruongs(currentDateTime);
            var previousTours = await _baoCaoService.ChartDoanhSoTheoThiTruongs(previousDateTime);
            return View(HomeVM);
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
