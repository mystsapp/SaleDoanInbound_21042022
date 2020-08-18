using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_QLTaiKhoan;
using Data.Repository;
using Data.Utilities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public UserViewModel UserVM { get; set; }

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            UserVM = new UserViewModel()
            {
                User = new Data.Models_IB.User(),
                Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                Roles = _unitOfWork.roleRepository.GetAll()
            };
        }
        public IActionResult Index(string searchString = null, int page = 1)
        {
            UserVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            UserVM.Users = _unitOfWork.userRepository.ListUser(searchString, page);
            return View(UserVM);
        }

        public IActionResult Create(string strUrl)
        {
            UserVM.StrUrl = strUrl;
            return View(UserVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            
            if (!ModelState.IsValid)
            {
                return View(UserVM);
            }

            //NganhNgheVM.DMNganhNghe = new Data.Models_IB.DMNganhNghe();
            UserVM.User.Username = UserVM.TenCreate;
            UserVM.User.Password = MaHoaSHA1.EncodeSHA1(UserVM.User.Password); // ma hoa password
            UserVM.User.NgayTao = DateTime.Now;
            UserVM.User.NguoiTao = "Admin";

            // INSERT INTO qltaikhoan.dbo.Users(username,[password],hoten,macn, trangthai,doimk,ngaydoimk,ngaytao,nguoitao)
            var qltkUser = new Users()
            {
                Username = UserVM.User.Username,
                Password = UserVM.User.Password,
                Hoten = UserVM.User.HoTen,
                Macn = UserVM.User.MaCN,
                Trangthai = UserVM.User.TrangThai,
                Doimk = UserVM.User.DoiMK,
                Ngaydoimk = UserVM.User.NgayDoiMK,
                Ngaytao = UserVM.User.NgayTao,
                Nguoitao = UserVM.User.NguoiTao
            };
            // INSERT INTO qltaikhoan.dbo.Users(username,[password],hoten,macn, trangthai,doimk,ngaydoimk,ngaytao,nguoitao)

            // 	insert into qltaikhoan.dbo.ApplicationUser(username, mact)
            var applicationUser = new ApplicationUser()
            {
                Username = UserVM.User.Username,
                Mact = "015"
            };
            // 	insert into qltaikhoan.dbo.ApplicationUser(username, mact)

            try
            {
                _unitOfWork.userQLTaiKhoanRepository.Create(qltkUser);
                _unitOfWork.applicationUserQLTaiKhoanRepository.Create(applicationUser);
                _unitOfWork.userRepository.Create(UserVM.User);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(UserVM);
            }

        }

        public async Task<IActionResult> Edit(int? id, string strUrl)
        {
            UserVM.StrUrl = strUrl;
            
            if (id == null)
                return NotFound();

            UserVM.User = await _unitOfWork.userRepository.GetByIdAsync(id);

            if (UserVM.User == null)
                return NotFound();

            UserVM.OldPassword = UserVM.User.Password; // gang' lai pass cu

            return View(UserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string strUrl)
        {
            if (id != UserVM.User.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                UserVM.User.NgayCapNhat = DateTime.Now;
                UserVM.User.NguoiCapNhat = "Admin";

                if (!string.IsNullOrEmpty(UserVM.User.Password)) // co' password moi
                {
                    UserVM.User.Password = MaHoaSHA1.EncodeSHA1(UserVM.User.Password);
                }
                else // ko thay doi pass --> UserVM.User.Password == ""
                {
                    UserVM.User.Password = UserVM.OldPassword;
                }
                // update qltaikhoan.dbo.users 
                // set[password] = i.[password],hoten = i.[Name],trangthai = i.TrangThai,doimk = i.Doimk,ngaydoimk = i.ngaydoimk
                var userQLTaiKhoan = new Users()
                {
                    Username = UserVM.User.Username,
                    Password = UserVM.User.Password,
                    Hoten = UserVM.User.HoTen,
                    Trangthai = UserVM.User.TrangThai,
                    Doimk = UserVM.User.DoiMK,
                    Ngaydoimk = UserVM.User.NgayDoiMK
                };
                // update qltaikhoan.dbo.users set[password] = i.[password],hoten = i.[Name],trangthai = i.TrangThai,doimk = i.Doimk,ngaydoimk = i.ngaydoimk
                try
                {
                    var userQLTaiKhoanDb = _unitOfWork.userQLTaiKhoanRepository.GetByIdAsNoTracking(x => x.Username == UserVM.User.Username);
                    if (userQLTaiKhoanDb != null)
                    {
                        _unitOfWork.userQLTaiKhoanRepository.Update(userQLTaiKhoan);
                    }
                    else
                    {
                        SetAlert("User không tồn tại trên QLTK", "warning");
                    }
                    _unitOfWork.userRepository.Update(UserVM.User);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(UserVM);
                }
            }

            return View(UserVM);
        }

        public async Task<IActionResult> Details(int? id, string strUrl)
        {
            UserVM.StrUrl = strUrl;

            if (id == null)
                return NotFound();

            var user = await _unitOfWork.userRepository.GetByIdAsync(id);
            UserVM.User = user;
            if (user == null)
                return NotFound();

            return View(UserVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string strUrl)
        {
            var user = await _unitOfWork.userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            try
            {
                _unitOfWork.userRepository.Delete(user);
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

        public JsonResult IsStringNameAvailable(string TenCreate)
        {
            var boolName = _unitOfWork.userRepository.Find(x => x.Username.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
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