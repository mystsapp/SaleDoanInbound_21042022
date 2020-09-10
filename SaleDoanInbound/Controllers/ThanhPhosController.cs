using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class ThanhPhosController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ThanhPhoViewModel ThanhPhoVM { get; set; }

        public ThanhPhosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ThanhPhoVM = new ThanhPhoViewModel()
            {
                Quocgias = _unitOfWork.quocGiaRepository.GetAll(),
                ThanhPho = new Data.Models_IB.ThanhPho(),
                ThanhPhoDto = new Data.Dtos.ThanhPhoDto(),
                ThanhPhoEditDto = new Data.Dtos.ThanhPhoEditDto()
            };
        }

        public IActionResult Index(string searchString = null, int page = 1)
        {
            ThanhPhoVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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
            var listQG = _unitOfWork.quocGiaRepository.GetAll();
            ThanhPhoVM.thanhPhoDtos = _unitOfWork.thanhPhoRepository.ListThanhPho(searchString, listQG, page);
            return View(ThanhPhoVM);
        }

        public IActionResult Create(string strUrl)
        {
            ThanhPhoVM.StrUrl = strUrl;
            return View(ThanhPhoVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(ThanhPhoVM);
            }

            ThanhPhoVM.ThanhPho.TenThanhPho = ThanhPhoVM.TenCreate;
            ThanhPhoVM.ThanhPho.NgayTao = DateTime.Now;
            ThanhPhoVM.ThanhPho.NguoiTao = "Admin";

            try
            {
                _unitOfWork.thanhPhoRepository.Create(ThanhPhoVM.ThanhPho);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(ThanhPhoVM);
            }

        }

        public async Task<IActionResult> Edit(int? id, string strUrl)
        {
            ThanhPhoVM.StrUrl = strUrl;
            if (id == null)
                return NotFound();

            var thanhPho = await _unitOfWork.thanhPhoRepository.GetByIdAsync(id);

            ThanhPhoVM.ThanhPhoEditDto.Id = thanhPho.Id;
            ThanhPhoVM.ThanhPhoEditDto.TenThanhPho = thanhPho.TenThanhPho;
            ThanhPhoVM.ThanhPhoEditDto.MaQuocGia = thanhPho.MaQuocGia;
            ThanhPhoVM.ThanhPhoEditDto.NgayTao = thanhPho.NgayTao;
            ThanhPhoVM.ThanhPhoEditDto.MaQuocGia = thanhPho.MaQuocGia;

            if (ThanhPhoVM.ThanhPho == null)
                return NotFound();

            return View(ThanhPhoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            if (id != ThanhPhoVM.ThanhPhoEditDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var thanhPho = new ThanhPho()
                {
                    Id = ThanhPhoVM.ThanhPhoEditDto.Id,
                    TenThanhPho = ThanhPhoVM.ThanhPhoEditDto.TenThanhPho,
                    NgayTao = ThanhPhoVM.ThanhPhoEditDto.NgayTao,
                    NguoiTao = ThanhPhoVM.ThanhPhoEditDto.NguoiTao,
                    MaQuocGia = ThanhPhoVM.ThanhPhoEditDto.MaQuocGia,
                    NgaySua = DateTime.Now,
                    NguoiSua = "Admin"
                };
                try
                {

                    _unitOfWork.thanhPhoRepository.Update(thanhPho);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(ThanhPhoVM);
                }
            }

            return View(ThanhPhoVM);
        }

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            ThanhPhoVM.StrUrl = strUrl;

            if (id == null)
                return NotFound();

            var thanhPho = await _unitOfWork.thanhPhoRepository.GetByIdAsync(id);
            ThanhPhoVM.ThanhPhoDto.Id = thanhPho.Id;
            ThanhPhoVM.ThanhPhoDto.TenThanhPho = thanhPho.TenThanhPho;
            ThanhPhoVM.ThanhPhoDto.QuocGia = _unitOfWork.quocGiaRepository.GetById(thanhPho.MaQuocGia).Nation;
            if (thanhPho == null)
                return NotFound();

            return View(ThanhPhoVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var thanhPho = await _unitOfWork.thanhPhoRepository.GetByIdAsync(id);
            if (thanhPho == null)
                return NotFound();
            try
            {
                _unitOfWork.thanhPhoRepository.Delete(thanhPho);
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
            var boolName = _unitOfWork.thanhPhoRepository.Find(x => x.TenThanhPho.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
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