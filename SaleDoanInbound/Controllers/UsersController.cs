using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Models_QLT;
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
                Roles = _unitOfWork.roleRepository.GetAll(),
                PhongBans = PhongBan()
            };
        }
        public async Task<IActionResult> Index(string searchString = null, int page = 1)
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

            UserVM.Users = await _unitOfWork.userRepository.ListUser(searchString, page);
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
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (!ModelState.IsValid)
            {
                return View(UserVM);
            }

            UserVM.User.Username = UserVM.TenCreate;
            UserVM.User.Password = MaHoaSHA1.EncodeSHA1(UserVM.User.Password); // ma hoa password
            UserVM.User.NgayTao = DateTime.Now;
            UserVM.User.NguoiTao = user.Username;
            
            UserVM.User.HoTen = string.IsNullOrEmpty(UserVM.User.HoTen) ? "" : UserVM.User.HoTen;
            UserVM.User.MaCN = string.IsNullOrEmpty(UserVM.User.MaCN) ? "" : UserVM.User.MaCN;
            UserVM.User.Email = string.IsNullOrEmpty(UserVM.User.Email) ? "" : UserVM.User.Email;
            UserVM.User.DienThoai = string.IsNullOrEmpty(UserVM.User.DienThoai) ? "" : UserVM.User.DienThoai;
            try
            {


                // kiem tra ton tai user trong qltk
                var qltkUserExist = await _unitOfWork.userQLTaiKhoanRepository.GetByIdAsync(UserVM.User.Username);
                // kiem tra ton tai user trong qltk
                if (qltkUserExist == null) // user trong qltk chua co
                {
                    // INSERT INTO qltaikhoan.dbo.Users(username,[password],hoten,macn, trangthai,doimk,ngaydoimk,ngaytao,nguoitao)
                    var qltkUser = new Data.Models_QLTaiKhoan.Users()
                    {
                        Username = UserVM.User.Username,
                        Password = UserVM.User.Password,
                        Hoten = UserVM.User.HoTen,
                        Macn = UserVM.User.MaCN,
                        Trangthai = UserVM.User.TrangThai,
                        Doimk = UserVM.User.DoiMK,
                        Ngaydoimk = UserVM.User.NgayDoiMK,
                        Ngaytao = UserVM.User.NgayTao,
                        Nguoitao = UserVM.User.NguoiTao,
                        Maphong = UserVM.User.PhongBanId
                    };

                    _unitOfWork.userQLTaiKhoanRepository.Create(qltkUser);
                    // INSERT INTO qltaikhoan.dbo.Users(username,[password],hoten,macn, trangthai,doimk,ngaydoimk,ngaytao,nguoitao)

                }

                // kiem tra ton tai user trong applicationuser qltk
                var qltkApplicationUser = await _unitOfWork.applicationUserQLTaiKhoanRepository.GetByIdTwoKeyAsync(UserVM.User.Username, "015");
                if (qltkApplicationUser == null)
                {

                    // 	insert into qltaikhoan.dbo.ApplicationUser(username, mact)
                    var applicationUser = new ApplicationUser()
                    {
                        Username = UserVM.User.Username,
                        Mact = "015"
                    };
                    _unitOfWork.applicationUserQLTaiKhoanRepository.Create(applicationUser);
                    // 	insert into qltaikhoan.dbo.ApplicationUser(username, mact)

                }
                // kiem tra ton tai user trong applicationuser qltk

                //if (qltkUserExist != null && qltkApplicationUser != null)
                //{
                //    SetAlert("User này đã tồn tại trên QLTK và trên ứng dụng này", "warning");
                //    UserVM = new UserViewModel()
                //    {
                //        User = new Data.Models_IB.User(),
                //        Dmchinhanhs = _unitOfWork.dmChiNhanhRepository.GetAll(),
                //        Roles = _unitOfWork.roleRepository.GetAll()
                //    };
                //    return View(UserVM);
                //}
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
            // from session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            if (id != UserVM.User.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                UserVM.User.NgayCapNhat = DateTime.Now;
                UserVM.User.NguoiCapNhat = user.Username;

                if (!string.IsNullOrEmpty(UserVM.User.Password)) // co' password moi
                {
                    UserVM.User.Password = MaHoaSHA1.EncodeSHA1(UserVM.User.Password);
                }
                else // ko thay doi pass --> UserVM.User.Password == ""
                {
                    UserVM.User.Password = UserVM.OldPassword;
                }

                try
                {

                    // update qltaikhoan.dbo.users 
                    // set[password] = i.[password],hoten = i.[Name],trangthai = i.TrangThai,doimk = i.Doimk,ngaydoimk = i.ngaydoimk
                    var userQLTaiKhoan = new Data.Models_QLTaiKhoan.Users()
                    {
                        Username = UserVM.User.Username,
                        Password = UserVM.User.Password,
                        Hoten = UserVM.User.HoTen,
                        Macn = UserVM.User.MaCN,
                        Trangthai = UserVM.User.TrangThai,
                        Doimk = UserVM.User.DoiMK,
                        Ngaydoimk = UserVM.User.NgayDoiMK,
                        Ngaytao = UserVM.User.NgayTao,
                        Nguoitao = UserVM.User.NguoiTao,
                        Maphong = UserVM.User.PhongBanId
                    };
                    var userQLTaiKhoanDb = _unitOfWork.userQLTaiKhoanRepository.GetByIdAsNoTracking(x => x.Username == UserVM.User.Username);
                    if (userQLTaiKhoanDb != null)
                    {
                        _unitOfWork.userQLTaiKhoanRepository.Update(userQLTaiKhoan);
                    }
                    else
                    {
                        SetAlert("User không tồn tại trên QLTK", "warning");
                    }
                    // update qltaikhoan.dbo.users set[password] = i.[password],hoten = i.[Name],trangthai = i.TrangThai,doimk = i.Doimk,ngaydoimk = i.ngaydoimk

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

            var user = await _unitOfWork.userRepository.FindIncludeOneAsync(x => x.Role, y => y.Id == id);
            UserVM.User = user.FirstOrDefault();
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

            //        delete qltaikhoan.dbo.ApplicationUser where username = (select username from deleted)
            var applicationUserQLTaiKhoan = await _unitOfWork.applicationUserQLTaiKhoanRepository.GetByIdTwoKeyAsync(user.Username, "015");
            if (applicationUserQLTaiKhoan != null)
            {
                _unitOfWork.applicationUserQLTaiKhoanRepository.Delete(applicationUserQLTaiKhoan);
            }
            //        delete qltaikhoan.dbo.ApplicationUser where username = (select username from deleted)

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


        private List<Data.Models_QLT.Phongban> PhongBan()
        {
            //return _unitOfWork.phongBanRepository.GetAll()
            //                                     .Where(x => !string.IsNullOrEmpty(x.Macode))
            //                                     .ToList();
            return _unitOfWork.phongBanRepository.GetAll().Where(x => !string.IsNullOrEmpty(x.Macode)).ToList();
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