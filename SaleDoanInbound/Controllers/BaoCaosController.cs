using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            }

            return View(BaoCaoVM);
        }
    }
}