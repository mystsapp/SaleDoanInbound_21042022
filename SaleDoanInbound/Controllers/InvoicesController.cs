using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using Data.Utilities;
using Data.Models_IB;

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
                CTVAT = new CTVAT(),
                CTInvoice = new CTVAT(),
                TourIB = new Data.Models_IB.TourIB(),
                Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                Tour = new Data.Models_IB.Tour()
            };
        }
        public async Task<IActionResult> Index(long tourId, string id, string searchString = null)
        {
            InvoiceVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            InvoiceVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
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

            InvoiceVM.Invoices = _unitOfWork.invoiceRepository.ListInvoice(searchString, tourId).OrderByDescending(x => x.Date);

            // invoice click
            if (!string.IsNullOrEmpty(id))
            {
                InvoiceVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);
                InvoiceVM.CTVATs = await _unitOfWork.cTVATRepository.FindIncludeOneAsync(x => x.Invoice, y => y.InvoiceId == id && !y.TiengAnh);
                InvoiceVM.CTInvoices = await _unitOfWork.cTVATRepository.FindIncludeOneAsync(x => x.Invoice, y => y.InvoiceId == id && y.TiengAnh);
            }

            // invoice click

            return View(InvoiceVM);
        }

        public async Task<IActionResult> Create(long tourId, /*string tabActive,*/ string strUrl)
        {
            InvoiceVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

            InvoiceVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            InvoiceVM.Invoice.Arr = InvoiceVM.Tour.NgayDen;
            InvoiceVM.Invoice.Dep = InvoiceVM.Tour.NgayDi;
            InvoiceVM.Invoice.Pax = (InvoiceVM.Tour.SoKhachTT == 0)? InvoiceVM.Tour.SoKhachDK: InvoiceVM.Tour.SoKhachTT;
            InvoiceVM.Invoice.HopDong = InvoiceVM.Tour.SoHopDong;
            InvoiceVM.Invoice.MaKH = InvoiceVM.Tour.MaKH;
            InvoiceVM.Invoice.TenKhach = InvoiceVM.Tour.TenKH;
            InvoiceVM.Invoice.Ref = "";
            InvoiceVM.Invoice.MOFP = "TM/CK";
            InvoiceVM.Invoice.TourId = InvoiceVM.Tour.Id;

            InvoiceVM.LoaiIVs = _unitOfWork.loaiIVRepository.GetAll();
            InvoiceVM.Invoices = await _unitOfWork.invoiceRepository.FindAsync(x => x.TourId == tourId);

            return View(InvoiceVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(long tourId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                return View(InvoiceVM);
            }

            //InvoiceVM.Invoice = new Data.Models_IB.Invoice();
            InvoiceVM.Invoice.Date = DateTime.Now;
            InvoiceVM.Invoice.NgayVAT = DateTime.Now;
            InvoiceVM.Invoice.NguoiTao = user.Username;

            #region next id
            ////// next id
            var currentYear = DateTime.Now.Year;
            var invoice = _unitOfWork.invoiceRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            if (invoice == null)
            {
                var id = GetNextId.NextID("", "");
                InvoiceVM.Invoice.Id = id + currentYear.ToString();
            }
            else
            {
                var oldYear = invoice.Id.Substring(6, 4);
                // cung nam
                if (oldYear == currentYear.ToString())
                {
                    var oldId = invoice.Id.Substring(0, 6);                        
                    InvoiceVM.Invoice.Id = GetNextId.NextID(oldId, "") + currentYear.ToString();
                }
                else
                {
                    // sang nam khac' chay lai tu dau
                    var id = GetNextId.NextID("", "");
                    InvoiceVM.Invoice.Id = id + currentYear.ToString();
                }

            }
            ////// next id
            #endregion
            ///
            // ghi log
            InvoiceVM.Invoice.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
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
                ModelState.AddModelError("", ex.Message);
                ////
                InvoiceVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

                InvoiceVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
                InvoiceVM.Invoice.Arr = InvoiceVM.Tour.NgayDen;
                InvoiceVM.Invoice.Dep = InvoiceVM.Tour.NgayDi;
                InvoiceVM.Invoice.Pax = (InvoiceVM.Tour.SoKhachTT == 0) ? InvoiceVM.Tour.SoKhachDK : InvoiceVM.Tour.SoKhachTT;
                InvoiceVM.Invoice.HopDong = InvoiceVM.Tour.SoHopDong;
                InvoiceVM.Invoice.MaKH = InvoiceVM.Tour.MaKH;
                InvoiceVM.Invoice.TenKhach = InvoiceVM.Tour.TenKH;
                InvoiceVM.Invoice.Ref = "";
                InvoiceVM.Invoice.TourId = InvoiceVM.Tour.Id;

                InvoiceVM.LoaiIVs = _unitOfWork.loaiIVRepository.GetAll();
                InvoiceVM.Invoices = await _unitOfWork.invoiceRepository.FindAsync(x => x.TourId == tourId);
                ////

                return View(InvoiceVM);
            }

        }

        public async Task<IActionResult> Edit(string id, long tourId/*, string tabActive*/, string strUrl)
        {
            InvoiceVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            //if (!string.IsNullOrEmpty(tabActive))
            //{
            //    // reset url -> cut tabActive
            //    var newStrUrl = InvoiceVM.StrUrl.Split("&tabActive");
            //    if (newStrUrl.Length > 1)
            //    {
            //        InvoiceVM.StrUrl = newStrUrl[0];
            //    }

            //}
            InvoiceVM.Tour = _unitOfWork.tourRepository.GetById(tourId);
            InvoiceVM.Invoices = await _unitOfWork.invoiceRepository.FindAsync(x => x.TourId == tourId);

            if (string.IsNullOrEmpty(id))
                return NotFound();

            InvoiceVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);

            if (InvoiceVM.Invoice == null)
                return NotFound();

            InvoiceVM.LoaiIVs = _unitOfWork.loaiIVRepository.GetAll();

            return View(InvoiceVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != InvoiceVM.Invoice.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                InvoiceVM.Invoice.NgaySua = DateTime.Now;
                InvoiceVM.Invoice.NguoiSua = user.Username;

                if (string.IsNullOrEmpty(InvoiceVM.Invoice.Replace))
                {
                    InvoiceVM.Invoice.Replace = "";
                }
                if (string.IsNullOrEmpty(InvoiceVM.Invoice.Ref))
                {
                    InvoiceVM.Invoice.Ref = "";
                }
                if (string.IsNullOrEmpty(InvoiceVM.Invoice.HopDong))
                {
                    InvoiceVM.Invoice.HopDong = "";
                }

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.invoiceRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.Replace != InvoiceVM.Invoice.Replace)
                {
                    temp += String.Format("- Replace thay đổi: {0}->{1}", t.Replace, InvoiceVM.Invoice.Replace);
                }
                if (t.Type != InvoiceVM.Invoice.Type)
                {
                    temp += String.Format("- Loại invoice thay đổi: {0}->{1}", t.Type, InvoiceVM.Invoice.Type);
                }
                if (t.TenKhach != InvoiceVM.Invoice.TenKhach)
                {
                    temp += String.Format("- Tên khách thay đổi: {0}->{1}", t.TenKhach, InvoiceVM.Invoice.TenKhach);
                }
                if (t.GhiChu != InvoiceVM.Invoice.GhiChu)
                {
                    temp += String.Format("- Ghi chú thay đổi: {0}->{1}", t.GhiChu, InvoiceVM.Invoice.GhiChu);
                }
                if (t.Arr != InvoiceVM.Invoice.Arr)
                {
                    temp += String.Format("- Bắt đầu thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.Arr, InvoiceVM.Invoice.Arr);
                }
                if (t.Dep != InvoiceVM.Invoice.Dep)
                {
                    temp += String.Format("- Kết thúc thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.Dep, InvoiceVM.Invoice.Dep);
                }
                if (t.Pax != InvoiceVM.Invoice.Pax)
                {
                    temp += String.Format("- Số khách thay đổi: {0}->{1}", t.Pax, InvoiceVM.Invoice.Pax);
                }

                if (t.Currency != InvoiceVM.Invoice.Currency)
                {
                    temp += String.Format("- Loại tiền thay đổi: {0}->{1}", t.Currency, InvoiceVM.Invoice.Currency);
                }
                if (t.Rate != InvoiceVM.Invoice.Rate)
                {
                    temp += String.Format("- Tỷ giá thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.Rate, InvoiceVM.Invoice.Rate);
                }
                if (t.SGL != InvoiceVM.Invoice.SGL)
                {
                    temp += String.Format("- SGL thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.SGL, InvoiceVM.Invoice.SGL);
                }
                if (t.DBL != InvoiceVM.Invoice.DBL)
                {
                    temp += String.Format("- DBL thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.DBL, InvoiceVM.Invoice.DBL);
                }
                if (t.TPL != InvoiceVM.Invoice.TPL)
                {
                    temp += String.Format("- TPL thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.TPL, InvoiceVM.Invoice.TPL);
                }
                if (t.MOFP != InvoiceVM.Invoice.MOFP)
                {
                    temp += String.Format("- MOFP thay đổi: {0:#,##0.0}->{1:#,##0.0}", t.MOFP, InvoiceVM.Invoice.MOFP);
                }
                if (t.DOFP != InvoiceVM.Invoice.DOFP)
                {
                    temp += String.Format("- DOFP thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.DOFP, InvoiceVM.Invoice.DOFP);
                }
                if (t.Ref != InvoiceVM.Invoice.Ref)
                {
                    temp += String.Format("- Ref thay đổi: {0}->{1}", t.Ref, InvoiceVM.Invoice.Ref);
                }
                if (t.HopDong != InvoiceVM.Invoice.HopDong)
                {
                    temp += String.Format("- Hộp đồng thay đổi: {0}->{1}", t.HopDong, InvoiceVM.Invoice.HopDong);
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
                    InvoiceVM.Invoice.LogFile = t.LogFile;
                }

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
                    InvoiceVM = new InvoiceViewModel()
                    {
                        Invoice = new Data.Models_IB.Invoice(),
                        TourIB = new Data.Models_IB.TourIB(),
                        Ngoaites = _unitOfWork.ngoaiTeRepository.GetAll(),
                        LoaiIVs = _unitOfWork.loaiIVRepository.GetAll(),
                        Tour = new Data.Models_IB.Tour()
                    };
                    return View(InvoiceVM);
                }
            }

            return View(InvoiceVM);
        }

        public async Task<IActionResult> Details(string id, long tourId/*, string tabActive*/, string strUrl)
        {
            InvoiceVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            InvoiceVM.Tour = _unitOfWork.tourRepository.GetById(tourId);

            if (id == null)
                return NotFound();

            var invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);

            if (invoice == null)
                return NotFound();

            InvoiceVM.Invoice = invoice;
            return View(InvoiceVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl/*, string tabActive*/)
        {
            InvoiceVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

            var invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();
            try
            {
                _unitOfWork.invoiceRepository.Delete(invoice);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(InvoiceVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(InvoiceVM.StrUrl);
            }
        }


        //-----------HuyInvoice------------
        public async Task<IActionResult> HuyInvoicePartial(string id, string strUrl)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            InvoiceVM.StrUrl = strUrl;
            InvoiceVM.Invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(id);
            InvoiceVM.CacNoiDungHuyTours = await _unitOfWork.cacNoiDungHuyTourRepository.FindAsync(x => x.Xoa == false);

            return PartialView(InvoiceVM);
        }

        [HttpPost]
        public async Task<IActionResult> HuyInvoice()
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log;

            // BN
            var invoice = await _unitOfWork.invoiceRepository.GetByIdAsync(InvoiceVM.Invoice.Id);
            if (InvoiceVM.Invoice.NgayHuy.HasValue)
            {
                invoice.NgayHuy = InvoiceVM.Invoice.NgayHuy;
            }
            else
            {
                invoice.NgayHuy = DateTime.Now;
            }
            invoice.NDHuyBNId = InvoiceVM.Invoice.NDHuyBNId;
            invoice.HuyInvoice = true;

            // kiem tra thay doi

            if (InvoiceVM.Invoice.NDHuyBNId > 0)
            {
                var cacNoiDungHuyTour = _unitOfWork.cacNoiDungHuyTourRepository.GetById(InvoiceVM.Invoice.NDHuyBNId);
                temp += String.Format("- Nội dung huy: {0}", cacNoiDungHuyTour.NoiDung);
            }

            //if (!string.IsNullOrEmpty(BienNhanVM.BienNhan.GhiChu))
            //{
            //    temp += String.Format("- Ghi chú: {0}", BienNhanVM.BienNhan.GhiChu);
            //}

            if (InvoiceVM.Invoice.NgayHuy.HasValue)
            {
                temp += String.Format("- Ngày hủy: {0:dd/MM/yyyy} - Người hủy: {1}", InvoiceVM.Invoice.NgayHuy, user.Username); // username
            }

            if (temp.Length > 0)
            {

                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp;
                invoice.LogFile = invoice.LogFile + log;
            }

            try
            {
                _unitOfWork.invoiceRepository.Update(invoice);
                await _unitOfWork.Complete();
                SetAlert("Hủy thành công.", "success");
                return Redirect(InvoiceVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert("Error: " + ex.Message, "error");
                ModelState.AddModelError("", ex.Message);
                return Redirect(InvoiceVM.StrUrl);

            }
        }
        //-----------HuyInvoice------------

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