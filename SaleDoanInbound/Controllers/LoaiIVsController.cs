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
    public class LoaiIVsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public LoaiIVViewModel LoaiIVVM { get; set; }

        public LoaiIVsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoaiIVVM = new LoaiIVViewModel()
            {
                LoaiIV = new Data.Models_IB.LoaiIV()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            LoaiIVVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            LoaiIVVM.LoaiIVs = _unitOfWork.loaiIVRepository.ListLoaiIV(searchString, page);
            return View(LoaiIVVM);
        }

        public IActionResult Create(string strUrl)
        {
            LoaiIVVM.StrUrl = strUrl;
            return View(LoaiIVVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(LoaiIVVM);
            }

            if (string.IsNullOrEmpty(LoaiIVVM.LoaiIV.TenLoaiIV))
            {
                LoaiIVVM.LoaiIV.TenLoaiIV = "";
            }
            
            if (string.IsNullOrEmpty(LoaiIVVM.LoaiIV.GhiChu))
            {
                LoaiIVVM.LoaiIV.GhiChu = "";
            }

            try
            {
                _unitOfWork.loaiIVRepository.Create(LoaiIVVM.LoaiIV);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(LoaiIVVM);
            }

        }

        public async Task<IActionResult> Edit(string id, string strUrl)
        {
            LoaiIVVM.StrUrl = strUrl;
            if (string.IsNullOrEmpty(id))
                return NotFound();

            LoaiIVVM.LoaiIV = await _unitOfWork.loaiIVRepository.GetByIdAsync(id);

            if (LoaiIVVM.LoaiIV == null)
                return NotFound();

            return View(LoaiIVVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id, string strUrl)
        {
            if (id != LoaiIVVM.LoaiIV.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.loaiIVRepository.Update(LoaiIVVM.LoaiIV);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(LoaiIVVM);
                }
            }

            return View(LoaiIVVM);
        }

        public async Task<IActionResult> Details(string id, string strUrl)
        {
            LoaiIVVM.StrUrl = strUrl;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            var loaiIV = await _unitOfWork.loaiIVRepository.GetByIdAsync(id);
            
            if (loaiIV == null)
                return NotFound();

            LoaiIVVM.LoaiIV = loaiIV;

            return View(LoaiIVVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl)
        {
            var loaiIV = await _unitOfWork.loaiIVRepository.GetByIdAsync(id);

            if (loaiIV == null)
                return NotFound();

            try
            {
                _unitOfWork.loaiIVRepository.Delete(loaiIV);
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

        //public JsonResult IsStringNameAvailable(string TenCreate)
        //{
        //    var boolName = _unitOfWork.dMNganhNgheRepository.Find(x => x.TenNganhNghe.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
        //    if (boolName == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json(false);
        //    }
        //}

    }
}