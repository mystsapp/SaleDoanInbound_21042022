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
    public class PhanKhuCNsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public PhanKhuCNViewModel PhanKhuCNVM { get; set; }
        public PhanKhuCNsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            PhanKhuCNVM = new PhanKhuCNViewModel()
            {
                PhanKhuCN = new Data.Models_IB.PhanKhuCN(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            PhanKhuCNVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            PhanKhuCNVM.PhanKhuCNs = _unitOfWork.phanKhuCNRepository.Find(x => x.TenKhuCN.ToLower().Contains(searchString.ToLower()));
            return View(PhanKhuCNVM);
        }

        public IActionResult Create(string strUrl)
        {
            PhanKhuCNVM.StrUrl = strUrl;
            return View(PhanKhuCNVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(PhanKhuCNVM);
            }

            if (string.IsNullOrEmpty(PhanKhuCNVM.PhanKhuCN.TenKhuCN))
            {
                PhanKhuCNVM.PhanKhuCN.TenKhuCN = "";
            }

            try
            {
                _unitOfWork.phanKhuCNRepository.Create(PhanKhuCNVM.PhanKhuCN);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(PhanKhuCNVM);
            }

        }

        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            PhanKhuCNVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            PhanKhuCNVM.PhanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(id);

            if (PhanKhuCNVM.PhanKhuCN == null)
                return NotFound();

            return View(PhanKhuCNVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, string strUrl)
        {
            if (id != PhanKhuCNVM.PhanKhuCN.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.phanKhuCNRepository.Update(PhanKhuCNVM.PhanKhuCN);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(PhanKhuCNVM);
                }
            }

            return View(PhanKhuCNVM);
        }

        public async Task<IActionResult> Details(int id, string strUrl)
        {
            PhanKhuCNVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(id);

            if (phanKhuCN == null)
                return NotFound();

            PhanKhuCNVM.PhanKhuCN = phanKhuCN;

            return View(PhanKhuCNVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(id);

            if (phanKhuCN == null)
                return NotFound();

            try
            {
                _unitOfWork.phanKhuCNRepository.Delete(phanKhuCN);
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

    }
}