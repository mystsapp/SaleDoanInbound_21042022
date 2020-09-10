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
    public class QuocGiasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public QuocGiaViewModel quocGiaVM { get; set; }
        public QuocGiasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            quocGiaVM = new QuocGiaViewModel()
            {
                Quocgia = new Data.Models_QLT.Quocgia()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            quocGiaVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            quocGiaVM.Quocgias = _unitOfWork.quocGiaRepository.ListQuocGia(searchString, page);
            return View(quocGiaVM);
        }

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            quocGiaVM.StrUrl = strUrl;

            if (id == null)
                return NotFound();

            var quocGia = await _unitOfWork.quocGiaRepository.GetByIdAsync(id);
            quocGiaVM.Quocgia = quocGia;
            if (quocGia == null)
                return NotFound();

            return View(quocGiaVM);
        }

    }
}