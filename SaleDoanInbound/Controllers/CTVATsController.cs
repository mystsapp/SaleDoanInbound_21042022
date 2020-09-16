using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Repository;
using Data.Utilities;
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
                CTVAT = new Data.Models_IB.CTVAT(),
                CTInvoice = new Data.Models_IB.CTVAT()
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        #region CTVAT
        public async Task<IActionResult> CreateCTVAT(string invoiceId, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
            return View(CTVATVM);
        }

        [HttpPost, ActionName("CreateCTVAT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCTVATPost(string invoiceId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
                return View(CTVATVM);
            }
            CTVATVM.CTVAT.NgayTao = DateTime.Now;
            CTVATVM.CTVAT.NguoiTao = user.Username;
            
            if (string.IsNullOrEmpty(CTVATVM.CTVAT.Descript))
            {
                CTVATVM.CTVAT.Descript = "";
            }
            CTVATVM.CTVAT.Descript = CTVATVM.CTVAT.Descript.ToUpper();
            if (string.IsNullOrEmpty(CTVATVM.CTVAT.Unit))
            {
                CTVATVM.CTVAT.Unit = "";
            }
            CTVATVM.CTVAT.Unit = CTVATVM.CTVAT.Unit.ToUpper();
            // ghi log
            CTVATVM.CTVAT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {
                
                _unitOfWork.cTVATRepository.Create(CTVATVM.CTVAT);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(CTVATVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
                return View(CTVATVM);
            }

        }

        public async Task<IActionResult> EditCTVAT(long id, string invoiceId/*, string tabActive*/, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = invoiceId;

            if (id == 0)
                return NotFound();

            CTVATVM.CTVAT = _unitOfWork.cTVATRepository.GetById(id);

            if (CTVATVM.CTVAT == null)
                return NotFound();

            return View(CTVATVM);
        }

        [HttpPost, ActionName("EditCTVAT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTVATPost(long id, string invoiceId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != CTVATVM.CTVAT.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                CTVATVM.CTVAT.NgaySua = DateTime.Now;
                CTVATVM.CTVAT.NguoiSua = user.Username;
                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.cTVATRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.Descript != CTVATVM.CTVAT.Descript)
                {
                    temp += String.Format("- Descript thay đổi: {0}->{1}", t.Descript, CTVATVM.CTVAT.Descript);
                }
                if (t.Quantity != CTVATVM.CTVAT.Quantity)
                {
                    temp += String.Format("- Số lượng thay đổi: {0}->{1}", t.Quantity, CTVATVM.CTVAT.Quantity);
                }
                if (t.Unit != CTVATVM.CTVAT.Unit)
                {
                    temp += String.Format("- DVT thay đổi: {0}->{1}", t.Unit, CTVATVM.CTVAT.Unit);
                }
                if (t.UnitPrice != CTVATVM.CTVAT.UnitPrice)
                {
                    temp += String.Format("- Đơn giá thay đổi: {0}->{1}", t.UnitPrice, CTVATVM.CTVAT.UnitPrice);
                }
                if (t.Percent != CTVATVM.CTVAT.Percent)
                {
                    temp += String.Format("- Phần trăm thay đổi: {0}->{1}", t.Percent, CTVATVM.CTVAT.Percent);
                }
                if (t.Amount != CTVATVM.CTVAT.Amount)
                {
                    temp += String.Format("- Tổng tiền thay đổi: {0:N0}->{1:N0}", t.Amount, CTVATVM.CTVAT.Amount);
                }
                if (t.ServiceFee != CTVATVM.CTVAT.ServiceFee)
                {
                    temp += String.Format("- PPV thay đổi: {0:N0}->{1:N0}", t.ServiceFee, CTVATVM.CTVAT.ServiceFee);
                }

                if (t.VAT != CTVATVM.CTVAT.VAT)
                {
                    temp += String.Format("- VAT thay đổi: {0}->{1}", t.VAT, CTVATVM.CTVAT.VAT);
                }
                
                // loai tien, ty gia mac dinh: vnd, 1
                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    CTVATVM.CTVAT.LogFile = t.LogFile;
                }

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
                    CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
                    CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    CTVATVM.CTVAT.InvoiceId = invoiceId;
                    return View(CTVATVM);
                }
            }
            // for not valid
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = invoiceId;
            return View(CTVATVM);
        }

        public async Task<IActionResult> DetailsCTVAT(long id, string invoiceId/*, string tabActive*/, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);

            if (id == 0)
                return NotFound();

            var cTVAT = _unitOfWork.cTVATRepository.GetById(id);
            
            if (cTVAT == null)
                return NotFound();

            CTVATVM.CTVAT = cTVAT;

            return View(CTVATVM);
        }

        [HttpPost, ActionName("DeleteCTVAT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCTVATConfirmed(long id, string invoiceId, string strUrl/*, string tabActive*/)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

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
#endregion

        // CTInvoice
        #region  CTInvoice
        public async Task<IActionResult> CreateCTInvoice(string invoiceId, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
            return View(CTVATVM);
        }

        [HttpPost, ActionName("CreateCTInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCTInvoicePost(string invoiceId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
                return View(CTVATVM);
            }
            CTVATVM.CTVAT.NgayTao = DateTime.Now;
            CTVATVM.CTVAT.NguoiTao = user.Username;
            CTVATVM.CTVAT.TiengAnh = true;

            if (string.IsNullOrEmpty(CTVATVM.CTVAT.Descript))
            {
                CTVATVM.CTVAT.Descript = "";
            }
            CTVATVM.CTVAT.Descript = CTVATVM.CTVAT.Descript.ToUpper();
            if (string.IsNullOrEmpty(CTVATVM.CTVAT.Unit))
            {
                CTVATVM.CTVAT.Unit = "";
            }
            CTVATVM.CTVAT.Unit = CTVATVM.CTVAT.Unit.ToUpper();
            // ghi log
            CTVATVM.CTVAT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {
                
                _unitOfWork.cTVATRepository.Create(CTVATVM.CTVAT);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(CTVATVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTVAT.InvoiceId = CTVATVM.Invoice.Id;
                return View(CTVATVM);
            }

        }

        public async Task<IActionResult> EditCTInvoice(long id, string invoiceId/*, string tabActive*/, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = invoiceId;

            if (id == 0)
                return NotFound();

            CTVATVM.CTVAT = _unitOfWork.cTVATRepository.GetById(id);

            if (CTVATVM.CTVAT == null)
                return NotFound();

            return View(CTVATVM);
        }

        [HttpPost, ActionName("EditCTInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTInvoicePost(long id, string invoiceId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != CTVATVM.CTVAT.Id)
                return NotFound();

            if (ModelState.IsValid)
            {

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.cTVATRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.Descript != CTVATVM.CTVAT.Descript)
                {
                    temp += String.Format("- Descript thay đổi: {0}->{1}", t.Descript, CTVATVM.CTVAT.Descript);
                }
                if (t.Quantity != CTVATVM.CTVAT.Quantity)
                {
                    temp += String.Format("- Số lượng thay đổi: {0}->{1}", t.Quantity, CTVATVM.CTVAT.Quantity);
                }
                if (t.Unit != CTVATVM.CTVAT.Unit)
                {
                    temp += String.Format("- DVT thay đổi: {0}->{1}", t.Unit, CTVATVM.CTVAT.Unit);
                }
                if (t.UnitPrice != CTVATVM.CTVAT.UnitPrice)
                {
                    temp += String.Format("- Đơn giá thay đổi: {0}->{1}", t.UnitPrice, CTVATVM.CTVAT.UnitPrice);
                }
                if (t.Percent != CTVATVM.CTVAT.Percent)
                {
                    temp += String.Format("- Phần trăm thay đổi: {0}->{1}", t.Percent, CTVATVM.CTVAT.Percent);
                }
                if (t.Amount != CTVATVM.CTVAT.Amount)
                {
                    temp += String.Format("- Tổng tiền thay đổi: {0:N0}->{1:N0}", t.Amount, CTVATVM.CTVAT.Amount);
                }
                if (t.ServiceFee != CTVATVM.CTVAT.ServiceFee)
                {
                    temp += String.Format("- PPV thay đổi: {0:N0}->{1:N0}", t.ServiceFee, CTVATVM.CTVAT.ServiceFee);
                }

                if (t.VAT != CTVATVM.CTVAT.VAT)
                {
                    temp += String.Format("- VAT thay đổi: {0}->{1}", t.VAT, CTVATVM.CTVAT.VAT);
                }
                
                // loai tien, ty gia mac dinh: vnd, 1
                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    CTVATVM.CTVAT.LogFile = t.LogFile;
                }

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
                    CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
                    CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    CTVATVM.CTVAT.InvoiceId = invoiceId;
                    return View(CTVATVM);
                }
            }
            // for not valid
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTVAT.InvoiceId = invoiceId;
            return View(CTVATVM);
        }

        public async Task<IActionResult> DetailsCTInvoice(long id, string invoiceId/*, string tabActive*/, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);

            if (id == 0)
                return NotFound();

            var cTVAT = _unitOfWork.cTVATRepository.GetById(id);
            
            if (cTVAT == null)
                return NotFound();

            CTVATVM.CTVAT = cTVAT;

            return View(CTVATVM);
        }

        [HttpPost, ActionName("DeleteCTInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCTInvoiceConfirmed(long id, string invoiceId, string strUrl/*, string tabActive*/)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

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
        #endregion
        // CTInvoice
    }
}