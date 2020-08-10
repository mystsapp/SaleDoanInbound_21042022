using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using SaleDoanInbound.Utilities;

namespace SaleDoanInbound.Controllers
{
    public class BienNhansController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public BienNhanViewModel BienNhanVM { get; set; }

        public BienNhansController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            BienNhanVM = new BienNhanViewModel()
            {
                BienNhan = new Data.Models_IB.BienNhan(),
                CacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll()
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(string tourIBId, string tabActive, string strUrl)
        {
            BienNhanVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(tourIBId);
            BienNhanVM.BienNhan.TourIBId = tourIBId;
            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            //000099IB2020
            if (!ModelState.IsValid)
            {
                return View(BienNhanVM);
            }

            // next id (so bien nhan)
            var currentYear = DateTime.Now.Year;
            var prefix = "IB" + currentYear.ToString();
            var bienNhan = _unitOfWork.bienNhanRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (bienNhan == null)
            {
                BienNhanVM.BienNhan.Id = GetNextId.NextID("", prefix);
            }
            else
            {
                var oldYear = bienNhan.Id.Substring(2, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    BienNhanVM.BienNhan.Id = GetNextId.NextID(bienNhan.Id, prefix);
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    BienNhanVM.BienNhan.Id = GetNextId.NextID("", prefix);
                }

            }

            // next id (so bien nhan)

            try
            {
                _unitOfWork.bienNhanRepository.Create(BienNhanVM.BienNhan);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(BienNhanVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(BienNhanVM);
            }

        }

        public async Task<IActionResult> Edit(string id, string tourIBId, string tabActive, string strUrl)
        {
            BienNhanVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(tourIBId);
            BienNhanVM.BienNhan.TourIBId = tourIBId;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            BienNhanVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);

            if (BienNhanVM.BienNhan == null)
                return NotFound();

            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id/*, string strUrl*/)
        {
            if (id != BienNhanVM.BienNhan.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    _unitOfWork.bienNhanRepository.Update(BienNhanVM.BienNhan);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(BienNhanVM.StrUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(BienNhanVM);
                }
            }

            return View(BienNhanVM);
        }

        public async Task<IActionResult> Details(string id, string tourIBId, string tabActive, string strUrl)
        {
            BienNhanVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            BienNhanVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(id);

            if (string.IsNullOrEmpty(id))
                return NotFound();

            var bienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);
            
            if (bienNhan == null)
                return NotFound();

            BienNhanVM.BienNhan = bienNhan;

            return View(BienNhanVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl, string tabActive)
        {
            BienNhanVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab

            var bienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(id);
            if (bienNhan == null)
                return NotFound();
            try
            {
                _unitOfWork.bienNhanRepository.Delete(bienNhan);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(BienNhanVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(BienNhanVM.StrUrl);
            }
        }
    }
}