﻿using System;
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
        
        [HttpPost, ActionName("DeleteCTVATPost")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCTVATPost(long ctvatId, string invoiceId)
        {

            var cTVAT = _unitOfWork.cTVATRepository.GetById(ctvatId);
            if (cTVAT == null)
                return NotFound();
            try
            {
                //if (cTVAT.TiengAnh) // CTInvoice : co the da co' cai copy
                //{
                //    _unitOfWork.cTVATRepository.Find(x => x.Descript)
                //}
                _unitOfWork.cTVATRepository.Delete(cTVAT);
                await _unitOfWork.Complete();
                //SetAlert("Xóa thành công.", "success");
                //return Redirect(CTVATVM.StrUrl);

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                //SetAlert(ex.Message, "error");
                //return Redirect(CTVATVM.StrUrl);

                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CopyCTInvoice_DS_To_CTVAT(string invoiceId, string strUrl)
        //{
        //    // from login session
        //    var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

        //    CTVATVM.StrUrl = strUrl;
        //    CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
        //    //find CTinvoice co DS (doanh so) == true
        //    var CTInvoices = await _unitOfWork.cTVATRepository.FindAsync(x => x.InvoiceId == invoiceId && x.DS && x.TiengAnh);

        //    foreach (var item in CTInvoices)
        //    {
        //        item.TiengAnh = false;

        //        // ghi log
        //        item.LogFile = "-User copy: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString() + " copy từ CTInvoice " + item.Id.ToString(); // user.Username
        //        item.Id = 0;

        //        _unitOfWork.cTVATRepository.Create(item);
        //    }
        //    try
        //    {
        //        await _unitOfWork.Complete();
        //        SetAlert("Copy thành công.", "success");

        //    }
        //    catch (Exception ex)
        //    {
        //        SetAlert("Copy không thành công.", "error");
        //    }

        //    return Redirect(strUrl);
        //}
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyCTInvoice_DS_To_CTVATPost(string invoiceId)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            //find CTinvoice co DS (doanh so) == true
            var CTInvoices = await _unitOfWork.cTVATRepository.FindAsync(x => x.InvoiceId == invoiceId && x.DS && x.TiengAnh);

            foreach (var item in CTInvoices)
            {
                item.TiengAnh = false;

                // ghi log
                item.LogFile = "-User copy: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString() + " copy từ CTInvoice " + item.Id.ToString(); // user.Username
                item.Id = 0;

                _unitOfWork.cTVATRepository.Create(item);
            }
            try
            {
                await _unitOfWork.Complete();
                //SetAlert("Copy thành công.", "success");
                return Json(new
                {
                    status = true
                });

            }
            catch (Exception ex)
            {
                //SetAlert("Copy không thành công.", "error");
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

            //return Redirect(strUrl);
        }

        #endregion
        public async Task<IActionResult> CheckExist(string invoiceId)
        {
            // tim nhung CTInvoice xem co1 trong CTVAT chua
            var CTInvoices = await _unitOfWork.cTVATRepository.FindAsync(x => x.InvoiceId == invoiceId && x.DS);
            // gang CTVAT
            var cTVATs = await _unitOfWork.cTVATRepository.FindAsync(x => x.InvoiceId == invoiceId && !x.TiengAnh);
            foreach (var item in CTInvoices)
            {
                foreach (var item1 in cTVATs)
                {
                    if (item.Descript == item1.Descript && item.Quantity == item1.Quantity &&
                                           item.Unit == item1.Unit && item.UnitPrice == item1.UnitPrice &&
                                           item.Percent == item1.Percent && item.Amount == item1.Amount &&
                                           item.ServiceFee == item1.ServiceFee && item.VAT == item1.VAT)
                    {

                        return Json(new
                        {
                            status = true
                        });
                    }
                }

            }
            return Json(new
            {
                status = false
            });

        }
        // CTInvoice
        #region  CTInvoice
        public async Task<IActionResult> CreateCTInvoice(string invoiceId, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
            CTVATVM.CTInvoice.Percent = 100;
            CTVATVM.CTInvoice.ServiceFee = 3;
            CTVATVM.CTInvoice.VAT = 10;
            //CTVATVM.ListTrueFalse = ListTrueFalse();
            //CTVATVM.CTInvoice.DS = false;
            //CTVATVM.CTInvoice.DLHH = false;
            return View(CTVATVM);
        }
        public async Task<IActionResult> CreateCTInvoicePartial(string invoiceId)
        {
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
            CTVATVM.CTInvoice.Percent = 100;
            CTVATVM.CTInvoice.ServiceFee = 3;
            CTVATVM.CTInvoice.VAT = 10;
            
            return PartialView(CTVATVM);
        }

        [HttpPost, ActionName("CreateCTInvoice")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> CreateCTInvoicePost(string invoiceId, string strUrl, string r1)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                return View(CTVATVM);
            }
            if (CTVATVM.CTInvoice.DLHH == false && CTVATVM.CTInvoice.DS == false)
            {
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                ModelState.AddModelError("", "Ds Or DLHH ?");
                return View(CTVATVM);
            }
            CTVATVM.CTInvoice.NgayTao = DateTime.Now;
            CTVATVM.CTInvoice.NguoiTao = user.Username;
            CTVATVM.CTInvoice.TiengAnh = true;

            if (string.IsNullOrEmpty(CTVATVM.CTInvoice.Descript))
            {
                CTVATVM.CTInvoice.Descript = "";
            }
            CTVATVM.CTInvoice.Descript = CTVATVM.CTInvoice.Descript.ToUpper();
            if (string.IsNullOrEmpty(CTVATVM.CTInvoice.Unit))
            {
                CTVATVM.CTInvoice.Unit = "";
            }
            CTVATVM.CTInvoice.Unit = CTVATVM.CTInvoice.Unit.ToUpper();
            // ghi log
            CTVATVM.CTInvoice.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {

                _unitOfWork.cTVATRepository.Create(CTVATVM.CTInvoice);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(CTVATVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                CTVATVM.StrUrl = strUrl;
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                return View(CTVATVM);
            }

        }
        
        [HttpPost, ActionName("CreateCTInvoicePartial")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> CreateCTInvoicePartialPost()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(CTVATVM.Invoice.Id);
                //CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                return Json(new
                {
                    status = false,
                    message = "Not valid?"
                });
            }
            if (CTVATVM.CTInvoice.DLHH == false && CTVATVM.CTInvoice.DS == false)
            {
                //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(CTVATVM.Invoice.Id);
                //CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                //ModelState.AddModelError("", "Ds Or DLHH ?");
                return Json(new
                {
                    status = false,
                    message = "Ds Or DLHH ?"
                });
            }
            CTVATVM.CTInvoice.NgayTao = DateTime.Now;
            CTVATVM.CTInvoice.NguoiTao = user.Username;
            CTVATVM.CTInvoice.TiengAnh = true;

            if (string.IsNullOrEmpty(CTVATVM.CTInvoice.Descript))
            {
                CTVATVM.CTInvoice.Descript = "";
            }
            CTVATVM.CTInvoice.Descript = CTVATVM.CTInvoice.Descript.ToUpper();
            if (string.IsNullOrEmpty(CTVATVM.CTInvoice.Unit))
            {
                CTVATVM.CTInvoice.Unit = "";
            }
            CTVATVM.CTInvoice.Unit = CTVATVM.CTInvoice.Unit.ToUpper();
            // ghi log
            CTVATVM.CTInvoice.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {

                _unitOfWork.cTVATRepository.Create(CTVATVM.CTInvoice);
                await _unitOfWork.Complete();
                //SetAlert("Thêm mới thành công.", "success");
                //return Redirect(CTVATVM.StrUrl);

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                //SetAlert(ex.Message, "error");
                //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                //CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }

        public async Task<IActionResult> EditCTInvoice(long id, string invoiceId/*, string tabActive*/, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTInvoice.InvoiceId = invoiceId;

            if (id == 0)
                return NotFound();

            CTVATVM.CTInvoice = _unitOfWork.cTVATRepository.GetById(id);

            if (CTVATVM.CTInvoice == null)
                return NotFound();

            return View(CTVATVM);
        }
        
        public async Task<IActionResult> EditCTInvoicePartial(long ctInvoiceId, string invoiceId)
        {
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTInvoice.InvoiceId = invoiceId;

            if (ctInvoiceId == 0)
                return NotFound();

            CTVATVM.CTInvoice = _unitOfWork.cTVATRepository.GetById(ctInvoiceId);

            if (CTVATVM.CTInvoice == null)
                return NotFound();

            return PartialView(CTVATVM);
        }

        [HttpPost, ActionName("EditCTInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTInvoicePost(long id, string invoiceId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != CTVATVM.CTInvoice.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                CTVATVM.CTInvoice.NgaySua = DateTime.Now;
                CTVATVM.CTInvoice.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.cTVATRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.Descript != CTVATVM.CTInvoice.Descript)
                {
                    temp += String.Format("- Descript thay đổi: {0}->{1}", t.Descript, CTVATVM.CTInvoice.Descript);
                }
                if (t.Quantity != CTVATVM.CTInvoice.Quantity)
                {
                    temp += String.Format("- Số lượng thay đổi: {0}->{1}", t.Quantity, CTVATVM.CTInvoice.Quantity);
                }
                if (t.Unit != CTVATVM.CTInvoice.Unit)
                {
                    temp += String.Format("- DVT thay đổi: {0}->{1}", t.Unit, CTVATVM.CTInvoice.Unit);
                }
                if (t.UnitPrice != CTVATVM.CTInvoice.UnitPrice)
                {
                    temp += String.Format("- Đơn giá thay đổi: {0}->{1}", t.UnitPrice, CTVATVM.CTInvoice.UnitPrice);
                }
                if (t.Percent != CTVATVM.CTInvoice.Percent)
                {
                    temp += String.Format("- Phần trăm thay đổi: {0}->{1}", t.Percent, CTVATVM.CTInvoice.Percent);
                }
                if (t.Amount != CTVATVM.CTInvoice.Amount)
                {
                    temp += String.Format("- Tổng tiền thay đổi: {0:N0}->{1:N0}", t.Amount, CTVATVM.CTInvoice.Amount);
                }
                if (t.ServiceFee != CTVATVM.CTInvoice.ServiceFee)
                {
                    temp += String.Format("- PPV thay đổi: {0:N0}->{1:N0}", t.ServiceFee, CTVATVM.CTInvoice.ServiceFee);
                }

                if (t.VAT != CTVATVM.CTInvoice.VAT)
                {
                    temp += String.Format("- VAT thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.VAT);
                }
                // muc, tenkhoanmuc, ds, dlhh
                if (t.Muc != CTVATVM.CTInvoice.Muc)
                {
                    temp += String.Format("- Muc thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.Muc);
                }
                if (t.TenKhoanMuc != CTVATVM.CTInvoice.TenKhoanMuc)
                {
                    temp += String.Format("- TenKhoanMuc thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.TenKhoanMuc);
                }
                if (t.DS != CTVATVM.CTInvoice.DS)
                {
                    temp += String.Format("- Ds thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.DS);
                }
                if (t.DLHH != CTVATVM.CTInvoice.DLHH)
                {
                    temp += String.Format("- DLHH thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.DLHH);
                }

                // loai tien, ty gia mac dinh: vnd, 1
                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật CTInvoice: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    CTVATVM.CTInvoice.LogFile = t.LogFile;
                }

                if (CTVATVM.CTInvoice.DLHH == false && CTVATVM.CTInvoice.DS == false)
                {
                    CTVATVM.StrUrl = strUrl;
                    CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                    ModelState.AddModelError("", "Ds Or DLHH ?");
                    return View(CTVATVM);
                }

                try
                {
                    _unitOfWork.cTVATRepository.Update(CTVATVM.CTInvoice);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(CTVATVM.StrUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
                    CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    CTVATVM.CTInvoice.InvoiceId = invoiceId;
                    return View(CTVATVM);
                }
            }
            // for not valid
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            CTVATVM.CTInvoice.InvoiceId = invoiceId;
            return View(CTVATVM);
        }
        
        [HttpPost, ActionName("EditCTInvoicePartial")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTInvoicePartialPost(string invoiceId)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (ModelState.IsValid)
            {
                CTVATVM.CTInvoice.NgaySua = DateTime.Now;
                CTVATVM.CTInvoice.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.cTVATRepository.GetByIdAsNoTracking(x => x.Id == CTVATVM.CTInvoice.Id);
                if (t.Descript != CTVATVM.CTInvoice.Descript)
                {
                    temp += String.Format("- Descript thay đổi: {0}->{1}", t.Descript, CTVATVM.CTInvoice.Descript);
                }
                if (t.Quantity != CTVATVM.CTInvoice.Quantity)
                {
                    temp += String.Format("- Số lượng thay đổi: {0}->{1}", t.Quantity, CTVATVM.CTInvoice.Quantity);
                }
                if (t.Unit != CTVATVM.CTInvoice.Unit)
                {
                    temp += String.Format("- DVT thay đổi: {0}->{1}", t.Unit, CTVATVM.CTInvoice.Unit);
                }
                if (t.UnitPrice != CTVATVM.CTInvoice.UnitPrice)
                {
                    temp += String.Format("- Đơn giá thay đổi: {0}->{1}", t.UnitPrice, CTVATVM.CTInvoice.UnitPrice);
                }
                if (t.Percent != CTVATVM.CTInvoice.Percent)
                {
                    temp += String.Format("- Phần trăm thay đổi: {0}->{1}", t.Percent, CTVATVM.CTInvoice.Percent);
                }
                if (t.Amount != CTVATVM.CTInvoice.Amount)
                {
                    temp += String.Format("- Tổng tiền thay đổi: {0:N0}->{1:N0}", t.Amount, CTVATVM.CTInvoice.Amount);
                }
                if (t.ServiceFee != CTVATVM.CTInvoice.ServiceFee)
                {
                    temp += String.Format("- PPV thay đổi: {0:N0}->{1:N0}", t.ServiceFee, CTVATVM.CTInvoice.ServiceFee);
                }

                if (t.VAT != CTVATVM.CTInvoice.VAT)
                {
                    temp += String.Format("- VAT thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.VAT);
                }
                // muc, tenkhoanmuc, ds, dlhh
                if (t.Muc != CTVATVM.CTInvoice.Muc)
                {
                    temp += String.Format("- Muc thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.Muc);
                }
                if (t.TenKhoanMuc != CTVATVM.CTInvoice.TenKhoanMuc)
                {
                    temp += String.Format("- TenKhoanMuc thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.TenKhoanMuc);
                }
                if (t.DS != CTVATVM.CTInvoice.DS)
                {
                    temp += String.Format("- Ds thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.DS);
                }
                if (t.DLHH != CTVATVM.CTInvoice.DLHH)
                {
                    temp += String.Format("- DLHH thay đổi: {0}->{1}", t.VAT, CTVATVM.CTInvoice.DLHH);
                }

                // loai tien, ty gia mac dinh: vnd, 1
                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật CTInvoice: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    CTVATVM.CTInvoice.LogFile = t.LogFile;
                }

                if (CTVATVM.CTInvoice.DLHH == false && CTVATVM.CTInvoice.DS == false)
                {
                    //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    //CTVATVM.CTInvoice.InvoiceId = CTVATVM.Invoice.Id;
                    //ModelState.AddModelError("", "Ds Or DLHH ?");
                    //return View(CTVATVM);

                    return Json(new
                    {
                        status = false,
                        message = "Ds Or DLHH ?"
                    });
                }

                try
                {
                    _unitOfWork.cTVATRepository.Update(CTVATVM.CTInvoice);
                    await _unitOfWork.Complete();
                    //SetAlert("Cập nhật thành công", "success");
                    //return Redirect(CTVATVM.StrUrl);

                    return Json(new
                    {
                        status = true
                    });
                }
                catch (Exception ex)
                {
                    //SetAlert(ex.Message, "error");
                    //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                    //CTVATVM.CTInvoice.InvoiceId = invoiceId;
                    //return View(CTVATVM);

                    return Json(new
                    {
                        status = false,
                        message = ex.Message
                    });
                }
            }
            // for not valid
            //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
            //CTVATVM.CTInvoice.InvoiceId = invoiceId;
            //return View(CTVATVM);

            return Json(new
            {
                status = false,
                message = "Not valid!"
            });
        }

        public async Task<IActionResult> DetailsCTInvoice(long id, string invoiceId/*, string tabActive*/, string strUrl)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);

            if (id == 0)
                return NotFound();

            var cTInvoice = _unitOfWork.cTVATRepository.GetById(id);

            if (cTInvoice == null)
                return NotFound();

            CTVATVM.CTInvoice = cTInvoice;

            return View(CTVATVM);
        }

        [HttpPost, ActionName("DeleteCTInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCTInvoiceConfirmed(long id, string invoiceId, string strUrl/*, string tabActive*/)
        {
            CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

            var cTInvoice = _unitOfWork.cTVATRepository.GetById(id);
            if (cTInvoice == null)
                return NotFound();
            try
            {
                _unitOfWork.cTVATRepository.Delete(cTInvoice);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(CTVATVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                // for not valid
                CTVATVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
                CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                CTVATVM.CTInvoice.InvoiceId = invoiceId;
                return Redirect(CTVATVM.StrUrl);
            }
        }
        
        [HttpPost, ActionName("DeleteCTInvoiceInCTInvoicesCTVATsPartial")]
        public async Task<IActionResult> DeleteCTInvoiceInCTInvoicesCTVATsPartialPost(long ctInvoiceId, string invoiceId)
        {

            var cTInvoice = _unitOfWork.cTVATRepository.GetById(ctInvoiceId);
            if (cTInvoice == null)
                return NotFound();
            try
            {
                _unitOfWork.cTVATRepository.Delete(cTInvoice);
                await _unitOfWork.Complete();
                //SetAlert("Xóa thành công.", "success");
                //return Redirect(CTVATVM.StrUrl);

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                //SetAlert(ex.Message, "error");
                //// for not valid
                //CTVATVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(invoiceId);
                //CTVATVM.CTInvoice.InvoiceId = invoiceId;
                //return Redirect(CTVATVM.StrUrl);

                return Json(new
                {
                    status = false,
                    message = "Xóa không thành công!"
                });
            }
        }
        #endregion
        // CTInvoice

        public JsonResult GetAount(decimal quantity, decimal unitPrice)
        {
            if (quantity != 0 && unitPrice != 0)
            {
                return Json(new
                {
                    status = true,
                    data = quantity * unitPrice
                });
            }
            return Json(new
            {
                status = false
            });
        }

        private IEnumerable<ListViewModel> ListTrueFalse()
        {
            return new List<ListViewModel>
            {
                new ListViewModel(){id = 1, Name = "aa"},
                new ListViewModel(){id = 2, Name = "bb"}
            };
        }
    }
}