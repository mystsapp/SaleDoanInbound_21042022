using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class ChiNhanhsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ChiNhanhViewModel ChiNhanhVM { get; set; }

        public ChiNhanhsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ChiNhanhVM = new ChiNhanhViewModel()
            {
                Dmchinhanh = new Data.Models_QLT.Dmchinhanh()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            ChiNhanhVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            ChiNhanhVM.Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.ListChiNhanh(searchString, page);
            return View(ChiNhanhVM);
        }

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            ChiNhanhVM.StrUrl = strUrl;

            if (id == null)
                return NotFound();

            var chiNhanh = await _unitOfWork.dmChiNhanhRepository.GetByIdAsync(id.Value);
            ChiNhanhVM.Dmchinhanh = chiNhanh;
            if (chiNhanh == null)
                return NotFound();

            return View(ChiNhanhVM);
        }
    }
}