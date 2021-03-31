using Data.Dtos;
using Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    public interface IUserQLTourService
    {
        UserInfo GetUserByUsername(string username);
    }

    public class UserQLTourService : IUserQLTourService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserQLTourService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserInfo GetUserByUsername(string username)
        {
            var users = _unitOfWork.userQLTaiKhoanRepository.GetAll();
            var phongBans = _unitOfWork.phongBanRepository.GetAll();

            var result = from u in users
                         join p in phongBans on u.Maphong equals p.Maphong
                         where u.Username.ToLower() == username.ToLower()
                         select new UserInfo()
                         {
                             username = u.Username,
                             hoten = u.Hoten,
                             dienthoai = u.Dienthoai,
                             email = u.Email,
                             tenphong = p.Tenphong,
                             macn = u.Macn,
                             roleId = u.RoleId
                         };

            return result.FirstOrDefault();
        }
    }
}