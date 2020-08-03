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
    public class KhuVucTGsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public KhuVucTGViewModel KhuVucTGVM { get; set; }
        public KhuVucTGsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            KhuVucTGVM = new KhuVucTGViewModel()
            {
                KhuVucTG = new Data.Models_IB.KhuVuc()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            KhuVucTGVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            KhuVucTGVM.KhuVucTGs = _unitOfWork.khuVucTGRepository.ListKhuVucTG(searchString, page);
            return View(KhuVucTGVM);
        }

        public IActionResult Create(string strUrl)
        {
            KhuVucTGVM.StrUrl = strUrl;
            return View(KhuVucTGVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(KhuVucTGVM);
            }

            KhuVucTGVM.KhuVucTG = new Data.Models_IB.KhuVuc();
            KhuVucTGVM.KhuVucTG.TenKhu = KhuVucTGVM.TenCreate;
            KhuVucTGVM.KhuVucTG.NgayTao = DateTime.Now;
            KhuVucTGVM.KhuVucTG.NguoiTao = "Admin";
            try
            {
                _unitOfWork.khuVucTGRepository.Create(KhuVucTGVM.KhuVucTG);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(KhuVucTGVM);
            }

        }


        public async Task<IActionResult> Edit(int? id, string strUrl)
        {
            KhuVucTGVM.StrUrl = strUrl;
            if (id == null)
                return NotFound();

            KhuVucTGVM.KhuVucTG = await _unitOfWork.khuVucTGRepository.GetByIdAsync(id);

            if (KhuVucTGVM.KhuVucTG == null)
                return NotFound();

            return View(KhuVucTGVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            if (id != KhuVucTGVM.KhuVucTG.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                KhuVucTGVM.KhuVucTG.NguoiSua = "Admin";
                KhuVucTGVM.KhuVucTG.NgaySua = DateTime.Now;

                try
                {

                    _unitOfWork.khuVucTGRepository.Update(KhuVucTGVM.KhuVucTG);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(KhuVucTGVM);
                }
            }

            return View(KhuVucTGVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var khuVucTG = _unitOfWork.khuVucTGRepository.GetById(id);
            if (khuVucTG == null)
                return NotFound();
            try
            {
                _unitOfWork.khuVucTGRepository.Delete(khuVucTG);
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

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            KhuVucTGVM.StrUrl = strUrl;

            if (id == null)
                return NotFound();

            var khuVucTG = await _unitOfWork.khuVucTGRepository.GetByIdAsync(id);
            KhuVucTGVM.KhuVucTG = khuVucTG;
            if (khuVucTG == null)
                return NotFound();

            return View(KhuVucTGVM);
        }


        public JsonResult IsStringNameAvailable(string TenCreate)
        {
            var boolName = _unitOfWork.khuVucTGRepository.Find(x => x.TenKhu.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
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