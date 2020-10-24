using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Models_QLTaiKhoan;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SaleDoanInbound.Models;
using Data.Utilities;
using Data.Dtos;

namespace SaleDoanInbound.Controllers
{
    public class KhachHangsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public KhachHangViewModel KhachHangVM { get; set; }
        public KhachHangsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            KhachHangVM = new KhachHangViewModel
            {
                Company = new Data.Models_QLT.Company(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                Quocgias = _unitOfWork.quocGiaRepository.GetAll()
            };
        }
        public IActionResult Index(decimal id = 0, string searchString = null, int page = 1)
        {
            KhachHangVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            ViewBag.searchString = searchString;

            // for delete
            //if (id != 0)
            //{

            //    var khachHang = _unitOfWork.khachHangRepository.GetById(id);
            //    if (khachHang == null)
            //    {

            //        var lastId = _unitOfWork.khachHangRepository
            //                                  .GetAll().OrderByDescending(x => x.Id)
            //                                  .FirstOrDefault().Id;
            //        id = lastId;

            //    }

            //}

            KhachHangVM.Companies = _unitOfWork.khachHangRepository.ListKhachHang(searchString, page);
            return View(KhachHangVM);
        }

        public IActionResult Create(string strUrl /*int maQuocGia = 0*/)
        {
            
            KhachHangVM.StrUrl = strUrl;

            //ViewBag.OSRddl = new SelectList(KhachHangVM.ThanhPhos, "Id", "TenThanhPho", "4");

            // get next Id
            //var lastId = _unitOfWork.khachHangRepository.GetAll()
            //                                            .OrderByDescending(x => x.CompanyId)
            //                                            .FirstOrDefault()
            //                                            .CompanyId;
            //KhachHangVM.Company.CompanyId = GetNextId.NextID(lastId, "");

            return View(KhachHangVM);
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(KhachHangVM);
            }

            //KhachHangVM.DMKhachHang = new Data.Models_IB.DMKhachHang();
            KhachHangVM.Company.Name = KhachHangVM.TenCreate;

            // get next Id
            var lastId = _unitOfWork.khachHangRepository.GetAll()
                                                        .OrderByDescending(x => x.CompanyId)
                                                        .FirstOrDefault()
                                                        .CompanyId;
            KhachHangVM.Company.CompanyId = GetNextId.NextKHId(lastId, "", "00001");

            try
            {
                _unitOfWork.khachHangRepository.Create(KhachHangVM.Company);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(KhachHangVM);
            }

        }

        public async Task<IActionResult> Edit(string id, string strUrl)
        {
            KhachHangVM.StrUrl = strUrl;
            if (id == null)
                return NotFound();

            KhachHangVM.Company = await _unitOfWork.khachHangRepository.GetByIdAsync(id);

            if (KhachHangVM.Company == null)
                return NotFound();

            return View(KhachHangVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string id, string strUrl)
        {
            KhachHangVM.StrUrl = strUrl;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            if (ModelState.IsValid)
            {
                //NganhNgheVM.DMNganhNghe.NgaySua = DateTime.Now;
                //NganhNgheVM.DMNganhNghe.NguoiSua = "Admin";
                if (!string.IsNullOrEmpty(KhachHangVM.Company.Nation))
                {
                    var quocgias = await _unitOfWork.quocGiaRepository.FindAsync(x => x.Nation == KhachHangVM.Company.Nation);
                    KhachHangVM.Company.Natione = quocgias.FirstOrDefault().Natione;
                }
                
                try
                {

                    _unitOfWork.khachHangRepository.Update(KhachHangVM.Company);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(KhachHangVM);
                }
            }

            return View(KhachHangVM);
        }

        public async Task<IActionResult> Details(string id, string strUrl)
        {
            KhachHangVM.StrUrl = strUrl;

            if (string.IsNullOrEmpty(id))
                return NotFound();

            var nganhNghe = await _unitOfWork.khachHangRepository.GetByIdAsync(id);
            
            if (nganhNghe == null)
                return NotFound();

            KhachHangVM.Company = nganhNghe;

            return View(KhachHangVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string strUrl)
        {
            var company = await _unitOfWork.khachHangRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound();
            try
            {
                _unitOfWork.khachHangRepository.Delete(company);
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


        #region Publish khách hàng lên hoá đơn điện tử VNPT
        [HttpPost]
        public async Task<IActionResult> TaoKhachhang(string id, string strUrl)
        {
            var company = await _unitOfWork.khachHangRepository.GetByIdAsync(id);
            //if (string.IsNullOrEmpty(company.Email))
            //{
            //    ModelState.AddModelError("", "Vui lòng cập nhật email, sau đó hãy tạo khách hàng trên VNPT");
            //    return RedirectToAction(nameof(Edit), new { id, strUrl });
            //}
            //string xmlCusData = "<Customers>";
            //xmlCusData += "<Customer>";
            //xmlCusData += "<Name>" + s.realname.Replace("&", "&amp;") + "</Name>";
            //xmlCusData += "<Code>" + s.code.Trim() + HttpContext.Session.GetString("maviettat") + "</Code>";
            //xmlCusData += "<TaxCode>" + s.taxcode + "</TaxCode>";
            //xmlCusData += "<Address>" + s.address.Replace("&", "&amp;") + "</Address>";
            //xmlCusData += "<BankAccountName></BankAccountName>";
            //xmlCusData += "<BankName></BankName>";
            //xmlCusData += "<BankNumber></BankNumber>";
            //xmlCusData += "<Email>" + s.email + "</Email>";
            //xmlCusData += "<Fax>" + s.fax + "</Fax>";
            //xmlCusData += "<Phone>" + s.telephone + "</Phone>";
            //xmlCusData += "<ContactPerson>" + s.contact + "</ContactPerson>";
            //xmlCusData += "<RepresentPerson></RepresentPerson>";
            //xmlCusData += "<CusType>1</CusType>";
            //xmlCusData += "</Customer>";
            //xmlCusData += "</Customers>";

            Customer customer = new Customer()
            {
                Name = company.Name,
                Code = company.CompanyId,
                TaxCode = company.Msthue,
                Address = company.Address,
                BankAccountName = "",
                BankName = "",
                BankNumber = "",
                Email = company.Email,
                Fax = company.Fax,
                Phone = company.Tel,
                ContactPerson = company.Nguoilienhe,
                RepresentPerson = company.Nguoidaidien,
                CusType = ""
            };

            List<Customer> customers = new List<Customer>();
            customers.Add(customer);

            var abc = XmlUtil.Serializer(typeof(Customer), customer);
            System.Diagnostics.Debug.WriteLine(abc);

            var inv = new PublishService.PublishServiceSoapClient(PublishService.PublishServiceSoapClient.EndpointConfiguration.PublishServiceSoap);

            //var dkhd = _dsdangkyhdRepository.listDangkyhoadon().Where(x => x.kyhieuhd == HttpContext.Session.GetString("kyhieuhd") && x.chinhanh == HttpContext.Session.GetString("maviettat")).SingleOrDefault();
            //var dkhd = _unitOfWork.dSDangKyHDRepository.FindAsync(x => x.Kyhieuhd == )
            //string sitehddt = dkhd.sitehddt.Trim() + "/PublishService.asmx";
            //string usersite = dkhd.usersite;
            //string passsite = dkhd.passsite;

            //// Hàm add webservice động
            //inv.ChannelFactory.Endpoint.Address = new EndpointAddress(sitehddt);

            //Task<PublishService.UpdateCusResponse> ketqua = inv.UpdateCusAsync(xmlCusData, usersite, passsite, 0);// HttpContext.Session.GetString("masohd"), HttpContext.Session.GetString("kyhieuhd"), 0);

            //var wait = await ketqua;
            //int result = Convert.ToInt32(wait.Body.UpdateCusResult.ToString());
            //if (result == -1)
            //{
            //    SetAlert("Tài khoản không có quyền", "error");
            //}
            //if (result == -2)
            //{
            //    SetAlert("Không thêm được khách hàng trên hoá đơn điện tử", "error");
            //}
            //if (result == -3)
            //{
            //    SetAlert("Dữ liệu không hợp lệ", "error");
            //}
            //if (result == -5)
            //{
            //    SetAlert("Khách hàng đã tồn tại", "error");
            //}
            //if (result > 0)
            //{
            //    SetAlert("Tạo / cập nhật thông tin khách hàng trên hoá đơn điện tử thành công", "success");
            //}

            return Redirect(strUrl);

        }
        #endregion


        //[HttpGet]
        //public JsonResult GetThanhPhosByQuocGia(int idQuocGia)
        //{
        //    var thanhPhos = _unitOfWork.thanhPhoRepository.Find(x => x.MaQuocGia == idQuocGia).ToList();
        //    if(thanhPhos.Count == 0)
        //    {
        //        thanhPhos = new List<ThanhPho>();
        //        thanhPhos.Add(new ThanhPho() { Id = 0, TenThanhPho = "-- Chưa có thành phố nào --" });
        //    }
        //    return Json(new
        //    {
        //        data = JsonConvert.SerializeObject(thanhPhos)
        //    });
        //}

        public IActionResult DetailsRedirect(string strUrl/*, string tabActive*/)
        {
            //if (!string.IsNullOrEmpty(tabActive))
            //{
            //    strUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            //}
            return Redirect(strUrl);
        }

        public JsonResult IsStringNameAvailable(string TenCreate)
        {
            var boolName = _unitOfWork.khachHangRepository.Find(x => x.Name.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
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