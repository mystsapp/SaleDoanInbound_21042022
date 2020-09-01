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
    public class CacNoiDungHuyToursController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CacNoiDungHuyTourViewModel CacNoiDungHuyTourVM { get; set; }
        public CacNoiDungHuyToursController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CacNoiDungHuyTourVM = new CacNoiDungHuyTourViewModel()
            {
                CacNoiDungHuyTour = new Data.Models_IB.CacNoiDungHuyTour()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            CacNoiDungHuyTourVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            CacNoiDungHuyTourVM.CacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.ListNoiDungHuy(searchString, page);
            return View(CacNoiDungHuyTourVM);
        }


        public IActionResult Create(string strUrl)
        {
            CacNoiDungHuyTourVM.StrUrl = strUrl;
            return View(CacNoiDungHuyTourVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(CacNoiDungHuyTourVM);
            }

            CacNoiDungHuyTourVM.CacNoiDungHuyTour = new Data.Models_IB.CacNoiDungHuyTour();
            CacNoiDungHuyTourVM.CacNoiDungHuyTour.NoiDung = CacNoiDungHuyTourVM.TenCreate;
            CacNoiDungHuyTourVM.CacNoiDungHuyTour.NgayTao = DateTime.Now;
            CacNoiDungHuyTourVM.CacNoiDungHuyTour.NguoiTao = "Admin";
            try
            {
                _unitOfWork.cacNoiDungHuyTourRepository.Create(CacNoiDungHuyTourVM.CacNoiDungHuyTour);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(CacNoiDungHuyTourVM);
            }

        }

        public IActionResult Edit(long id, string strUrl)
        {
            CacNoiDungHuyTourVM.StrUrl = strUrl;
            if (string.IsNullOrEmpty(id.ToString()))
                return NotFound();

            CacNoiDungHuyTourVM.CacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(id);

            if (CacNoiDungHuyTourVM.CacNoiDungHuyTour == null)
                return NotFound();

            return View(CacNoiDungHuyTourVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id, string strUrl)
        {
            if (id != CacNoiDungHuyTourVM.CacNoiDungHuyTour.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                CacNoiDungHuyTourVM.CacNoiDungHuyTour.NgaySua = DateTime.Now;
                CacNoiDungHuyTourVM.CacNoiDungHuyTour.NguoiSua = "Admin";
                try
                {

                    _unitOfWork.cacNoiDungHuyTourRepository.Update(CacNoiDungHuyTourVM.CacNoiDungHuyTour);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(CacNoiDungHuyTourVM);
                }
            }

            return View(CacNoiDungHuyTourVM);
        }

        public async Task<IActionResult> Details(long id, string strUrl)
        {
            CacNoiDungHuyTourVM.StrUrl = strUrl;

            if (string.IsNullOrEmpty(id.ToString()))
                return NotFound();

            var cacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(id);
            CacNoiDungHuyTourVM.CacNoiDungHuyTour = cacNoiDungHuyTour;
            if (CacNoiDungHuyTourVM.CacNoiDungHuyTour == null)
                return NotFound();

            return View(CacNoiDungHuyTourVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl)
        {
            var cacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(id);
            if (cacNoiDungHuyTour == null)
                return NotFound();
            try
            {
                _unitOfWork.cacNoiDungHuyTourRepository.Delete(cacNoiDungHuyTour);
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
            var boolName = _unitOfWork.cacNoiDungHuyTourRepository.Find(x => x.NoiDung.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
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