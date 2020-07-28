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
using SaleDoanInbound.Utilities;

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
                Dmchinhanhs = _unitOfWork.chiNhanhRepository.GetAll(),
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

        public async Task<IActionResult> Create(string strUrl /*int maQuocGia = 0*/)
        {
            
            KhachHangVM.StrUrl = strUrl;

            //ViewBag.OSRddl = new SelectList(KhachHangVM.ThanhPhos, "Id", "TenThanhPho", "4");

            // get next Id
            var lastId = _unitOfWork.khachHangRepository.GetAll()
                                                        .OrderByDescending(x => x.CompanyId)
                                                        .FirstOrDefault()
                                                        .CompanyId;
            KhachHangVM.Company.CompanyId = GetNextId.NextID(lastId, "");

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

        [HttpGet]
        public JsonResult GetThanhPhosByQuocGia(int idQuocGia)
        {
            var thanhPhos = _unitOfWork.thanhPhoRepository.Find(x => x.MaQuocGia == idQuocGia).ToList();
            if(thanhPhos.Count == 0)
            {
                thanhPhos = new List<ThanhPho>();
                thanhPhos.Add(new ThanhPho() { Id = 0, TenThanhPho = "-- Chưa có thành phố nào --" });
            }
            return Json(new
            {
                data = JsonConvert.SerializeObject(thanhPhos)
            });
        }

        public IActionResult DetailsRedirect(string strUrl)
        {
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