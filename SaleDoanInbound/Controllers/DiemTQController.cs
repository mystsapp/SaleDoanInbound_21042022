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
    public class DiemTQController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public DiemTQViewModel DiemTQVM { get; set; }

        public DiemTQController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            DiemTQVM = new DiemTQViewModel
            {
                Dmdiemtq = new Data.Models.Dmdiemtq()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            DiemTQVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            DiemTQVM.Dmdiemtqs = _unitOfWork.diemTQRepository.ListDiemTQ(searchString, page);
            return View(DiemTQVM);
        }

        public async Task<IActionResult> Details(string code, string strUrl)
        {
            DiemTQVM.StrUrl = strUrl;

            if (string.IsNullOrEmpty(code))
                return NotFound();

            var diemTQ = await _unitOfWork.diemTQRepository.GetByIdAsync(code);
            DiemTQVM.Dmdiemtq = diemTQ;
            if (diemTQ == null)
                return NotFound();

            return View(DiemTQVM);
        }
    }
}