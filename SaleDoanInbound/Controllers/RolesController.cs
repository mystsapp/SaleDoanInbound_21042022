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
    public class RolesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public RoleViewModel RoleVM { get; set; }
        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RoleVM = new RoleViewModel()
            {
                Role = new Data.Models_IB.Role()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            RoleVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            RoleVM.Roles = _unitOfWork.roleRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                RoleVM.Roles = RoleVM.Roles.Where(x => x.RoleName.Trim().ToLower() == searchString.Trim().ToLower());
            }
            
            return View(RoleVM);
        }

        public IActionResult Create(string strUrl)
        {
            RoleVM.StrUrl = strUrl;
            return View(RoleVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(RoleVM);
            }

            try
            {
                _unitOfWork.roleRepository.Create(RoleVM.Role);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(RoleVM);
            }

        }

        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            RoleVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            RoleVM.Role = await _unitOfWork.roleRepository.GetByIdAsync(id);

            if (RoleVM.Role == null)
                return NotFound();

            return View(RoleVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, string strUrl)
        {
            if (id != RoleVM.Role.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.roleRepository.Update(RoleVM.Role);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(RoleVM);
                }
            }

            return View(RoleVM);
        }

        public async Task<IActionResult> Details(int id, string strUrl)
        {
            RoleVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var Role = await _unitOfWork.roleRepository.GetByIdAsync(id);

            if (Role == null)
                return NotFound();

            RoleVM.Role = Role;

            return View(RoleVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var Role = await _unitOfWork.roleRepository.GetByIdAsync(id);

            if (Role == null)
                return NotFound();

            try
            {
                _unitOfWork.roleRepository.Delete(Role);
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