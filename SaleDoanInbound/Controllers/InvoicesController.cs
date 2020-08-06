using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using SaleDoanInbound.Utilities;

namespace SaleDoanInbound.Controllers
{
    public class InvoicesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public InvoiceViewModel InvoiceVM { get; set; }
        public InvoicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InvoiceVM = new InvoiceViewModel()
            {
                Invoice = new Data.Models_IB.Invoice(),
                TourIB = new Data.Models_IB.TourIB()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            InvoiceVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            InvoiceVM.Invoices = _unitOfWork.invoiceRepository.ListInvoice(searchString, page);
            return View(InvoiceVM);
        }

        public async Task<IActionResult> Create(string tourIBId, string strUrl)
        {
            InvoiceVM.StrUrl = strUrl;
            InvoiceVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(tourIBId);
            InvoiceVM.Invoice.Arr = InvoiceVM.TourIB.Arr;
            InvoiceVM.Invoice.Dep = InvoiceVM.TourIB.Dep;
            InvoiceVM.Invoice.Pax = InvoiceVM.TourIB.Pax;
            InvoiceVM.Invoice.TourIBId = InvoiceVM.TourIB.Id;
            return View(InvoiceVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(InvoiceVM);
            }

            //InvoiceVM.Invoice = new Data.Models_IB.Invoice();
            InvoiceVM.Invoice.Date = DateTime.Now;

            ////// next id
            var currentYear = DateTime.Now.Year;
            var invoice = _unitOfWork.invoiceRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (invoice == null)
            {
                InvoiceVM.Invoice.Id = GetNextId.NextID("", currentYear.ToString());
            }
            else
            {
                var oldYear = invoice.Id.Substring(0, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    InvoiceVM.Invoice.Id = GetNextId.NextID(invoice.Id, currentYear.ToString());
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    InvoiceVM.Invoice.Id = GetNextId.NextID("", currentYear.ToString());
                }

            }
            ////// next id

            try
            {
                _unitOfWork.invoiceRepository.Create(InvoiceVM.Invoice);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(InvoiceVM);
            }

        }

        public async Task<IActionResult> Edit(string id, string tourIBId, string strUrl)
        {
            InvoiceVM.StrUrl = strUrl;
            InvoiceVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(tourIBId);

            if (string.IsNullOrEmpty(id))
                return NotFound();

            InvoiceVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);

            if (InvoiceVM.Invoice == null)
                return NotFound();

            return View(InvoiceVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id, string strUrl)
        {
            if (id != InvoiceVM.Invoice.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                //NganhNgheVM.DMNganhNghe.NgaySua = DateTime.Now;
                //NganhNgheVM.DMNganhNghe.NguoiSua = "Admin";
                try
                {

                    _unitOfWork.invoiceRepository.Update(InvoiceVM.Invoice);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(InvoiceVM);
                }
            }

            return View(InvoiceVM);
        }

        public async Task<IActionResult> Details(string id, string tourIBId, string strUrl)
        {
            InvoiceVM.StrUrl = strUrl;
            InvoiceVM.TourIB = await _unitOfWork.tourIBRepository.GetByIdAsync(tourIBId);

            if (id == null)
                return NotFound();

            var invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);
            InvoiceVM.Invoice = invoice;
            if (invoice == null)
                return NotFound();

            return View(InvoiceVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl)
        {
            var invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();
            try
            {
                _unitOfWork.invoiceRepository.Delete(invoice);
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