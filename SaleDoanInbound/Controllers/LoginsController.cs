using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLTaiKhoan;
using Data.Repository;
using Data.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Controllers
{
    public class LoginsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = _unitOfWork.userRepository.login(model.Username, "015");

                // login
                var applications = _unitOfWork.applicationQLTaiKhoanRepository.GetAll();
                var applicationUsers = _unitOfWork.applicationUserQLTaiKhoanRepository.GetAll();
                var users = _unitOfWork.userQLTaiKhoanRepository.GetAll();
                var usersIB = _unitOfWork.userRepository.GetAll();

                var result = (from a in applications
                              join au in applicationUsers on a.Mact equals au.Mact
                              join u in users on au.Username equals u.Username
                              join u1 in usersIB on u.Username equals u1.Username
                              where au.Mact == "015" && u.Username.ToLower() == model.Username.ToLower()
                              select new LoginModel()
                              {
                                  Username = u.Username,
                                  Mact = a.Mact,
                                  Password = u1.Password,
                                  Hoten = u.Hoten,
                                  Dienthoai = u.Dienthoai,
                                  Email = u.Email,
                                  Macn = u1.MaCN,
                                  RoleId = u1.RoleId,
                                  Trangthai = u.Trangthai,
                                  Doimk = u.Doimk,
                                  Ngaydoimk = u.Ngaydoimk
                              }).FirstOrDefault();
                // login

                if (result == null)
                {
                    ModelState.AddModelError("", "Tài khoản này không tồn tại");
                }
                else
                {
                    if (!result.Trangthai)
                    {
                        ModelState.AddModelError("", "Tài khoản này đã bị khóa");
                        return View();
                    }
                    string modelPass = MaHoaSHA1.EncodeSHA1(model.Password);
                    if (result.Password != modelPass)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");

                    }
                    if (result.Password == modelPass)
                    {
                        //var user = _userRepository.GetById(model.Username);
                        var user = await _unitOfWork.userRepository.FindIncludeOneAsync(x => x.Role, x => x.Username == model.Username);
                        HttpContext.Session.Set("loginUser", user);

                        //HttpContext.Session.SetString("username", model.Username);
                        HttpContext.Session.SetString("password", model.Password);
                        //HttpContext.Session.SetString("hoten", result.Hoten);
                        //HttpContext.Session.SetString("phong", result.Maphong);
                        HttpContext.Session.SetString("chinhanh", user.SingleOrDefault().MaCN);
                        HttpContext.Session.SetString("userId", user.SingleOrDefault().Id.ToString());
                        //HttpContext.Session.SetString("dienthoai", String.IsNullOrEmpty(result.Dienthoai) ? "" : result.Dienthoai);
                        //HttpContext.Session.SetString("macode", result.Macode);
                        //HttpContext.Session.SetString("userRole", user.FirstOrDefault().Role.RoleName);
                        //HttpContext.Session.SetString("Newtour", user.Newtour.ToString());
                        //HttpContext.Session.SetString("Dongtour", user.Dongtour.ToString());
                        //HttpContext.Session.SetString("Danhmuc", user.Catalogue.ToString());
                        //HttpContext.Session.SetString("Booking", user.Booking.ToString());
                        //HttpContext.Session.SetString("Report", user.Report.ToString());
                        //HttpContext.Session.SetString("Showprice", user.Showprice.ToString());
                        //HttpContext.Session.SetString("Print", user.Print.ToString());
                        //HttpContext.Session.SetString("Doixe", user.Doixe.ToString());
                        //HttpContext.Session.SetString("Maybay", user.Maybay.ToString());
                        //HttpContext.Session.SetString("Huongdan", user.Huongdan.ToString());
                        //HttpContext.Session.SetString("Sales", user.Sales.ToString());
                        //HttpContext.Session.SetString("Vetq", user.Vetq.ToString());
                        //HttpContext.Session.SetString("Admin", user.Admin.ToString());
                        //HttpContext.Session.SetString("khachle", user.khachle.ToString());
                        //HttpContext.Session.SetString("khachdoan", user.khachdoan.ToString());

                        //if (!string.IsNullOrEmpty(user.Email))
                        //{
                        //    HttpContext.Session.SetString("Email", user.Email.ToString());
                        //}

                        //DateTime ngaydoimk = Convert.ToDateTime(result.Ngaydoimk);
                        //int kq = (DateTime.Now.Month - ngaydoimk.Month) + 12 * (DateTime.Now.Year - ngaydoimk.Year);
                        //if (kq >= 3)
                        //{
                        //    return View("changepass");
                        //}

                        //if (result.Doimk == true)
                        //{
                        //    return View("changepass");
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public IActionResult changepass(string strUrl)
        {
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            changepassModel changpassmodel = new changepassModel
            {
                Username = user.Username,
                StrUrl = strUrl
            };
            return View(changpassmodel);
        }
        [HttpPost]
        public async Task<IActionResult> changepass(changepassModel model, string strUrl)
        {
            if (ModelState.IsValid)
            {
                string oldpass = HttpContext.Session.GetString("password");
                if (MaHoaSHA1.EncodeSHA1(oldpass) != MaHoaSHA1.EncodeSHA1(model.Password))
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                }
                else if (model.Newpassword != model.Confirmpassword)
                {
                    ModelState.AddModelError("", "Mật khẩu nhập lại không đúng.");
                }
                else
                {
                    //int result = _unitOfWork.userRepository.changepass(model.Username, MaHoaSHA1.EncodeSHA1(model.Newpassword));

                    // change pass

                    //for local user
                    var user = _unitOfWork.userRepository.Find(x => x.Username == model.Username).FirstOrDefault();
                    user.Password = MaHoaSHA1.EncodeSHA1(model.Newpassword);
                    user.NgayDoiMK = DateTime.Now;
                    _unitOfWork.userRepository.Update(user);
                    // for local user

                    // for qltk user
                    var qltkUser = await _unitOfWork.userQLTaiKhoanRepository.GetByIdAsync(model.Username);
                    qltkUser.Password = MaHoaSHA1.EncodeSHA1(model.Newpassword);
                    qltkUser.Ngaydoimk = DateTime.Now;
                    qltkUser.Doimk = false;
                    _unitOfWork.userQLTaiKhoanRepository.Update(qltkUser);
                    // for qltk user

                    var result = await _unitOfWork.Complete();
                    // change pass

                    if (result > 0)
                    {
                        SetAlert("Đổi mật khẩu thành công", "success");
                        return LocalRedirect(strUrl); // /Users/Create : effect with local url
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không thể đổi mật khẩu.");
                    }
                }

            }
            return View();
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

    }
}