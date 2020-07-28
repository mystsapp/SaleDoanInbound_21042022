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
    public class LoaiToursController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public LoaiTourViewModel LoaiTourVM { get; set; }

        public LoaiToursController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoaiTourVM = new LoaiTourViewModel()
            {
                LoaiTour = new Data.Models_IB.LoaiTour()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            LoaiTourVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            LoaiTourVM.LoaiTours = _unitOfWork.loaiTourRepository.ListLoaiTour(searchString, page);
            return View(LoaiTourVM);
        }

        public IActionResult Create(string strUrl)
        {
            LoaiTourVM.StrUrl = strUrl;
            return View(LoaiTourVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(LoaiTourVM);
            }

            //LoaiTourVM.LoaiTour = new Data.Models_IB.LoaiTour();
            LoaiTourVM.LoaiTour.TenLoaiTour = LoaiTourVM.TenCreate;
            try
            {
                _unitOfWork.loaiTourRepository.Create(LoaiTourVM.LoaiTour);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(LoaiTourVM);
            }

        }

        public async Task<IActionResult> Edit(int? id, string strUrl)
        {
            LoaiTourVM.StrUrl = strUrl;
            if (id == null)
                return NotFound();

            LoaiTourVM.LoaiTour = await _unitOfWork.loaiTourRepository.GetByIdAsync(id);

            if (LoaiTourVM.LoaiTour == null)
                return NotFound();

            return View(LoaiTourVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            if (id != LoaiTourVM.LoaiTour.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    _unitOfWork.loaiTourRepository.Update(LoaiTourVM.LoaiTour);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(LoaiTourVM);
                }
            }

            return View(LoaiTourVM);
        }

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            LoaiTourVM.StrUrl = strUrl;

            if (id == null)
                return NotFound();

            var loaiTour = await _unitOfWork.loaiTourRepository.GetByIdAsync(id);
            LoaiTourVM.LoaiTour = loaiTour;
            if (loaiTour == null)
                return NotFound();

            return View(LoaiTourVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var loaiTour = await _unitOfWork.loaiTourRepository.GetByIdAsync(id);
            if (loaiTour == null)
                return NotFound();
            try
            {
                _unitOfWork.loaiTourRepository.Delete(loaiTour);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(strUrl);
            }
        }

        public JsonResult IsStringNameAvailable(string TenCreate)
        {
            var boolName = _unitOfWork.loaiTourRepository.Find(x => x.TenLoaiTour.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
            if (boolName == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}