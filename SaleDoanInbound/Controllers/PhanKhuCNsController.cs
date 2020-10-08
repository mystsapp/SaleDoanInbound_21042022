using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Repository;
using Data.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using X.PagedList;

namespace SaleDoanInbound.Controllers
{
    public class PhanKhuCNsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public PhanKhuCNViewModel PhanKhuCNVM { get; set; }
        public PhanKhuCNsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            PhanKhuCNVM = new PhanKhuCNViewModel()
            {
                PhanKhuCN = new Data.Models_IB.PhanKhuCN(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                PhanKhuCNs = _unitOfWork.phanKhuCNRepository.GetAll(),
                Roles = _unitOfWork.roleRepository.GetAll()
            };
        }
        public async Task<IActionResult> Index(string searchString = null, int page = 1)
        {
            PhanKhuCNVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            PhanKhuCNVM.PhanKhuCNs = await _unitOfWork.phanKhuCNRepository.GetAllIncludeOneAsync(x => x.Role);

            if (!string.IsNullOrEmpty(searchString))
            {
                PhanKhuCNVM.PhanKhuCNs = PhanKhuCNVM.PhanKhuCNs.Where(x => x.Role.RoleName.ToLower().Contains(searchString.ToLower()));
            }

            return View(PhanKhuCNVM);
        }

        public async Task<IActionResult> Create(string strUrl)
        {
            PhanKhuCNVM.StrUrl = strUrl;
            PhanKhuCNVM.PhanKhuCNs = await _unitOfWork.phanKhuCNRepository.GetAllIncludeOneAsync(x => x.Role);

            var phanKhuCNs = PhanKhuCNVM.PhanKhuCNs;
            
            // bo nhung chinhanh theo phankhu da ton tai
            //List<Dmchinhanh> chiNhanhsInPhanKhu = new List<Dmchinhanh>();
            //foreach (var item in phanKhuCNs.Select(x => x.ChiNhanhs))
            //{
            //    var chiNhanhItems = item.Split(',');
            //    for (int i = 0; i < chiNhanhItems.Length; i++)
            //    {
            //        var dmchinhanh = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == chiNhanhItems[i]).FirstOrDefault();
            //        chiNhanhsInPhanKhu.Add(dmchinhanh);
            //    }
            //}
            //PhanKhuCNVM.Dmchinhanhs = PhanKhuCNVM.Dmchinhanhs.Except(chiNhanhsInPhanKhu);
            // bo nhung chinhanh theo phankhu da ton tai

            // bo tenPhanKhu Admins va Users
            PhanKhuCNVM.Roles = PhanKhuCNVM.Roles.Where(x => x.RoleName != "Admins" && x.RoleName != "Users");
            // bo tenPhanKhu Admins va Users

            // bo nhung tenPhanKhu da ton tai
            List<Role> roles = new List<Role>();
            foreach (var item in phanKhuCNs)
            {
                roles.Add(_unitOfWork.roleRepository.GetById(item.RoleId));
            }
            if (roles.Count() > 0)
            {
                PhanKhuCNVM.Roles = PhanKhuCNVM.Roles.Except(roles);
            }
            // bo nhung tenPhanKhu da ton tai

            return View(PhanKhuCNVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                PhanKhuCNVM = new PhanKhuCNViewModel()
                {
                    PhanKhuCN = new Data.Models_IB.PhanKhuCN(),
                    Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                    PhanKhuCNs = _unitOfWork.phanKhuCNRepository.GetAll(),
                    Roles = _unitOfWork.roleRepository.GetAll()
                };

                // bo nhung tenPhanKhu da ton tai
                List<Role> roles = new List<Role>();
                foreach (var item in PhanKhuCNVM.PhanKhuCNs)
                {
                    roles.Add(_unitOfWork.roleRepository.GetById(item.RoleId));
                }
                if (roles.Count() > 0)
                {
                    PhanKhuCNVM.Roles = PhanKhuCNVM.Roles.Except(roles);
                }
                // bo nhung tenPhanKhu da ton tai
                ModelState.AddModelError("", "Bạn chưa chọn Tên.");
                return View(PhanKhuCNVM);
            }
            
            PhanKhuCNVM.PhanKhuCN.NgayTao = DateTime.Now;
            PhanKhuCNVM.PhanKhuCN.NguoiTao = user.Username;

            try
            {
                _unitOfWork.phanKhuCNRepository.Create(PhanKhuCNVM.PhanKhuCN);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(PhanKhuCNVM);
            }

        }

        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            PhanKhuCNVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            PhanKhuCNVM.PhanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(id);

            if (PhanKhuCNVM.PhanKhuCN == null)
                return NotFound();

            var phanKhuCNs = PhanKhuCNVM.PhanKhuCNs;

            // Macn mac dinh
            //var Macns = PhanKhuCNVM.PhanKhuCN.ChiNhanhs.Split(',');
            // Macn mac dinh

            // chinhanh theo phankhu da ton tai
            //var phanKhuCNs = PhanKhuCNVM.PhanKhuCNs;
            //var phanKhusHienTai = await _unitOfWork.phanKhuCNRepository.FindAsync(x => x.RoleId == id);
            //phanKhuCNs = phanKhuCNs.Except(phanKhusHienTai);
            //List<Dmchinhanh> chiNhanhsInPhanKhu = new List<Dmchinhanh>();
            //foreach (var item in phanKhuCNs.Select(x => x.ChiNhanhs))
            //{
            //    var chiNhanhItems = item.Split(',');
            //    // chiNhanhItems = chiNhanhItems.Concat(Macns).ToArray(); noi 2 mang
            //    for (int i = 0; i < chiNhanhItems.Length; i++)
            //    {
            //        var dmchinhanh = _unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == chiNhanhItems[i]).FirstOrDefault();
            //        chiNhanhsInPhanKhu.Add(dmchinhanh);
            //    }
            //}
            //PhanKhuCNVM.Dmchinhanhs = PhanKhuCNVM.Dmchinhanhs.Except(chiNhanhsInPhanKhu);
            // chinhanh theo phankhu da ton tai

            // add item default
            //List<Dmchinhanh> dmchinhanhs = new List<Dmchinhanh>();
            //for (int i = 0; i < Macns.Length; i++)
            //{
            //    dmchinhanhs.Add(_unitOfWork.dmChiNhanhRepository.Find(x => x.Macn == Macns[i]).FirstOrDefault());
            //}
            //PhanKhuCNVM.Dmchinhanhs = PhanKhuCNVM.Dmchinhanhs.Concat(dmchinhanhs);

            // bo tenPhanKhu Admins va Users
            PhanKhuCNVM.Roles = PhanKhuCNVM.Roles.Where(x => x.RoleName != "Admins" && x.RoleName != "Users");
            // bo tenPhanKhu Admins va Users

            // bo nhung tenPhanKhu da ton tai
            List<Role> roles = new List<Role>();
            foreach (var item in phanKhuCNs)
            {
                roles.Add(_unitOfWork.roleRepository.GetById(item.RoleId));
            }
            if (roles.Count() > 0)
            {
                PhanKhuCNVM.Roles = PhanKhuCNVM.Roles.Except(roles);
            }
            // bo nhung tenPhanKhu da ton tai

            return View(PhanKhuCNVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, string strUrl)
        {
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (id != PhanKhuCNVM.PhanKhuCN.RoleId)
                return NotFound();

            PhanKhuCNVM.PhanKhuCN.NgaySua = DateTime.Now;
            PhanKhuCNVM.PhanKhuCN.NguoiSua = user.Username;

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.phanKhuCNRepository.Update(PhanKhuCNVM.PhanKhuCN);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(PhanKhuCNVM);
                }
            }

            return View(PhanKhuCNVM);
        }

        public async Task<IActionResult> Details(int id, string strUrl)
        {
            PhanKhuCNVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(id);

            if (phanKhuCN == null)
                return NotFound();

            PhanKhuCNVM.PhanKhuCN = phanKhuCN;

            return View(PhanKhuCNVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var phanKhuCN = await _unitOfWork.phanKhuCNRepository.GetByIdAsync(id);

            if (phanKhuCN == null)
                return NotFound();

            try
            {
                _unitOfWork.phanKhuCNRepository.Delete(phanKhuCN);
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

    }
}