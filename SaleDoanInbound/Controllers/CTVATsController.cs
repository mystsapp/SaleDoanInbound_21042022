using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class CTVATsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public CTVATViewModel CTVATVM { get; set; }

        public CTVATsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CTVATVM = new CTVATViewModel()
            {
                CTVAT = new Data.Models_IB.CTVAT()
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(string invoiceId, string tabActive, string strUrl)
        {
            CTVATVM.StrUrl = strUrl + "&tabActive="+ tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
            return View(CTVATVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(CTVATVM);
            }

            try
            {
                CTVATVM.CTVAT.Descript = CTVATVM.CTVAT.Descript.ToUpper();
                CTVATVM.CTVAT.Unit = CTVATVM.CTVAT.Unit.ToUpper();
                _unitOfWork.cTVATRepository.Create(CTVATVM.CTVAT);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(CTVATVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(CTVATVM);
            }

        }

        public async Task<IActionResult> Edit(long id, string invoiceId, string tabActive, string strUrl)
        {
            CTVATVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = invoiceId;

            if (id == 0)
                return NotFound();

            CTVATVM.CTVAT = _unitOfWork.cTVATRepository.GetById(id);

            if (CTVATVM.CTVAT == null)
                return NotFound();

            return View(CTVATVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id/*, string strUrl*/)
        {
            if (id != CTVATVM.CTVAT.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    _unitOfWork.cTVATRepository.Update(CTVATVM.CTVAT);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(CTVATVM.StrUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(CTVATVM);
                }
            }

            return View(CTVATVM);
        }

        public async Task<IActionResult> Details(long id, string invoiceId, string tabActive, string strUrl)
        {
            CTVATVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);

            if (id == 0)
                return NotFound();

            var cTVAT = _unitOfWork.cTVATRepository.GetById(id);
            CTVATVM.CTVAT = cTVAT;
            if (cTVAT == null)
                return NotFound();

            return View(CTVATVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl, string tabActive)
        {
            CTVATVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab

            var cTVAT = _unitOfWork.cTVATRepository.GetById(id);
            if (cTVAT == null)
                return NotFound();
            try
            {
                _unitOfWork.cTVATRepository.Delete(cTVAT);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(CTVATVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(CTVATVM.StrUrl);
            }
        }
    }
}