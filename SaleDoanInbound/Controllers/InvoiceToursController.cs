using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using SaleDoanInbound.Utilities;

namespace SaleDoanInbound.Controllers
{
    public class InvoiceToursController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public InvoiceTourViewModel InvoiceTourVM { get; set; }

        public InvoiceToursController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InvoiceTourVM = new InvoiceTourViewModel()
            {
                CacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll(),
                Companies = _unitOfWork.khachHangRepository.GetAll(),
                TourIB = new Data.Models_IB.TourIB(),
                TourIBDto = new TourIBDto()
            };
        }
        public IActionResult Index(string id = null, string searchString = null, int page = 1)
        {
            InvoiceTourVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            List<TourIB> tourIBs = _unitOfWork.tourIBRepository.GetAll().ToList();
            InvoiceTourVM.TourIBs = _unitOfWork.tourIBRepository.ListTourIB(searchString, tourIBs,
                                                                            InvoiceTourVM.Companies.ToList(),
                                                                            InvoiceTourVM.CacNoiDungHuyTours.ToList(), page);

            // when we click --> get list invoice
            if (!string.IsNullOrEmpty(id))
            {
                InvoiceTourVM.Invoices = _unitOfWork.invoiceRepository.Find(x => x.TourIBId == id);
                // get detail touribDto for invoice view index
                var tourIB = _unitOfWork.tourIBRepository.Find(x => x.Id == id).FirstOrDefault();
                var tourIBDto = new TourIBDto()
                {
                    Id = tourIB.Id,
                    Name = tourIB.Name,
                    Address = tourIB.Address,
                    Arr = tourIB.Arr,
                    CompanyName = InvoiceTourVM.Companies.Where(x => x.CompanyId == tourIB.CompanyId).FirstOrDefault().Name,
                    Dep = tourIB.Dep,
                    Deposit = tourIB.Deposit,
                    NoiDungHuy = InvoiceTourVM.CacNoiDungHuyTours.Where(x => x.Id == tourIB.NoiDungHuy).Count() == 0 ? "" : InvoiceTourVM.CacNoiDungHuyTours.Where(x => x.Id == tourIB.NoiDungHuy).FirstOrDefault().NoiDung,
                    Note = tourIB.Note,
                    Order = tourIB.Order,
                    Pax = tourIB.Pax,
                    Ref = tourIB.Ref,
                    SGTCode = tourIB.SGTCode
                };
                InvoiceTourVM.TourIBDto = tourIBDto;
            }
            
            return View(InvoiceTourVM);
        }

        public IActionResult Create(string strUrl)
        {
            InvoiceTourVM.StrUrl = strUrl;

            return View(InvoiceTourVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(InvoiceTourVM);
            }

            //InvoiceTourVM.TourIB = new Data.Models_IB.TourIB();

            // next id
            var currentYear = DateTime.Now.Year;
            var tourIB = _unitOfWork.tourIBRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (tourIB == null)
            {
                InvoiceTourVM.TourIB.Id = GetNextId.NextID("", currentYear.ToString());
            }
            else
            {
                var oldYear = tourIB.Id.Substring(0, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    InvoiceTourVM.TourIB.Id = GetNextId.NextID(tourIB.Id, currentYear.ToString());
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    InvoiceTourVM.TourIB.Id = GetNextId.NextID("", currentYear.ToString());
                }

            }

            // next id
            try
            {
                _unitOfWork.tourIBRepository.Create(InvoiceTourVM.TourIB);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(InvoiceTourVM);
            }

        }

        public async Task<IActionResult> Edit(string id, string strUrl)
        {
            InvoiceTourVM.StrUrl = strUrl;
            if (string.IsNullOrEmpty(id))
                return NotFound();

            InvoiceTourVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(id);

            if (InvoiceTourVM.TourIB == null)
                return NotFound();

            return View(InvoiceTourVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id, string strUrl)
        {
            if (id != InvoiceTourVM.TourIB.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    _unitOfWork.tourIBRepository.Update(InvoiceTourVM.TourIB);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(InvoiceTourVM);
                }
            }

            return View(InvoiceTourVM);
        }

        public async Task<IActionResult> Details(string id, string strUrl)
        {
            InvoiceTourVM.StrUrl = strUrl;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            var tourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(id);
            InvoiceTourVM.TourIB = tourIB;
            if (tourIB == null)
                return NotFound();

            return View(InvoiceTourVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl)
        {
            var tourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(id);
            if (tourIB == null)
                return NotFound();
            try
            {
                _unitOfWork.tourIBRepository.Delete(tourIB);
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