using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Controllers
{
    public class NganhNghesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public NganhNgheViewModel NganhNgheVM { get; set; }
        public NganhNghesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            NganhNgheVM = new NganhNgheViewModel()
            {
                DMNganhNghe = new Data.Models_IB.DMNganhNghe()
            };
        }

        public IActionResult Index(string searchString = null, int page = 1)
        {
            NganhNgheVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            NganhNgheVM.DMNganhNghes = _unitOfWork.dMNganhNgheRepository.ListNganhNghe(searchString, page);
            return View(NganhNgheVM);
        }

        public IActionResult Create(string strUrl)
        {
            NganhNgheVM.StrUrl = strUrl;
            return View(NganhNgheVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(NganhNgheVM);
            }

            NganhNgheVM.DMNganhNghe = new Data.Models_IB.DMNganhNghe();
            NganhNgheVM.DMNganhNghe.TenNganhNghe = NganhNgheVM.TenCreate;
            NganhNgheVM.DMNganhNghe.NgayTao = DateTime.Now;
            NganhNgheVM.DMNganhNghe.NguoiTao = "Admin";
            try
            {
                _unitOfWork.dMNganhNgheRepository.Create(NganhNgheVM.DMNganhNghe);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success"); 
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(NganhNgheVM);
            }
            
        }

        public async Task<IActionResult> Edit(int? id, string strUrl)
        {
            NganhNgheVM.StrUrl = strUrl;
            if (id == null)
                return NotFound();

            NganhNgheVM.DMNganhNghe = await _unitOfWork.dMNganhNgheRepository.GetByIdAsync(id);

            if (NganhNgheVM.DMNganhNghe == null)
                return NotFound();

            return View(NganhNgheVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            if (id != NganhNgheVM.DMNganhNghe.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                NganhNgheVM.DMNganhNghe.NgaySua = DateTime.Now;
                NganhNgheVM.DMNganhNghe.NguoiSua = "Admin";
                try
                {

                    _unitOfWork.dMNganhNgheRepository.Update(NganhNgheVM.DMNganhNghe);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(NganhNgheVM);
                }
            }

            return View(NganhNgheVM);
        }

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            NganhNgheVM.StrUrl = strUrl;
            
            if (id == null)
                return NotFound();

            var nganhNghe = await _unitOfWork.dMNganhNgheRepository.GetByIdAsync(id);
            NganhNgheVM.DMNganhNghe = nganhNghe;
            if (nganhNghe == null)
                return NotFound();

            return View(NganhNgheVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var nganhNghe = await _unitOfWork.dMNganhNgheRepository.GetByIdAsync(id);
            if (nganhNghe == null)
                return NotFound();
            try
            {
                _unitOfWork.dMNganhNgheRepository.Delete(nganhNghe);
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
            var boolName = _unitOfWork.dMNganhNgheRepository.Find(x => x.TenNganhNghe.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
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