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
    public class ToursController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public TourViewModel TourVM { get; set; }

        public ToursController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            TourVM = new TourViewModel()
            {
                Tour = new Data.Models_IB.Tour(),
                Thanhphos = _unitOfWork.thanhPhoForTuyenTQRepository.GetAll(),
                Companies = _unitOfWork.khachHangRepository.GetAll()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            TourVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            var companies = TourVM.Companies;
            var loaiTours = _unitOfWork.loaiTourRepository.GetAll();
            var chiNhanhs = _unitOfWork.chiNhanhRepository.GetAll();
            var cacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll();

            TourVM.TourDtos = _unitOfWork.tourRepository.ListTour(searchString, companies, loaiTours, chiNhanhs, cacNoiDungHuyTours, page);
            if(TourVM.TourDtos == null)
            {

            }
            return View(TourVM);
        }

        public IActionResult Create(string strUrl)
        {
            TourVM.StrUrl = strUrl;
            return View(TourVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(TourVM);
            }

            //TourVM.Tour = new Data.Models_IB.Tour();
            TourVM.Tour.NgayTao = DateTime.Now;
            TourVM.Tour.NguoiTao = "Admin";
            try
            {
                _unitOfWork.tourRepository.Create(TourVM.Tour);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(TourVM);
            }

        }

        public async Task<IActionResult> Edit(int? id, string strUrl)
        {
            TourVM.StrUrl = strUrl;
            if (id == null)
                return NotFound();

            TourVM.Tour = await _unitOfWork.tourRepository.GetByIdAsync(id);

            if (TourVM.Tour == null)
                return NotFound();

            return View(TourVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, string strUrl)
        {
            if (id != TourVM.Tour.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                TourVM.Tour.NgaySua = DateTime.Now;
                TourVM.Tour.NguoiSua = "Admin";
                try
                {

                    _unitOfWork.tourRepository.Update(TourVM.Tour);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(TourVM);
                }
            }

            return View(TourVM);
        }

        public IActionResult Details(long id, string strUrl)
        {
            TourVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var tour = _unitOfWork.tourRepository.GetById(id);

            if (tour == null)
                return NotFound();

            TourVM.Tour = tour;

            return View(TourVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl)
        {
            var tour = _unitOfWork.tourRepository.GetById(id);
            if (tour == null)
                return NotFound();
            try
            {
                _unitOfWork.tourRepository.Delete(tour);
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

        private List<string> Visa()
        {
            return new List<string>()
            { "Khong visa", "Nuoc ngoai", "Cua khau" };
        }
    }
}