using Data.Dtos;
using Data.Interfaces;
using Data.Models_IB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        //LoginModel login(string username, string mact);
        //int changepass(string username, string newpass);
        Task <IPagedList<User>> ListUser(string searchString, int? page, long roleId);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        //public int changepass(string username, string newpass)
        //{
        //    var parammeter = new SqlParameter[]
        //     {
        //            new SqlParameter("@username",username),
        //            new SqlParameter("@newpass",newpass)
        //     };

        //    try
        //    {
        //        return _context.Database.ExecuteSqlRaw("spChangepass @username, @newpass", parammeter);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public async System.Threading.Tasks.Task<IPagedList<User>> ListUser(string searchString, int? page, long roleId)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = await GetAllIncludeOneAsync(x => x.Role);
            
            if(roleId != 0)
            {
                list = list.Where(x => x.RoleId == roleId);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Username.ToLower().Contains(searchString.ToLower()) ||
                                       (!string.IsNullOrEmpty(x.HoTen) && x.HoTen.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.MaCN) && x.MaCN.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Email) && x.Email.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.DienThoai) && x.DienThoai.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.PhongBanId) && x.PhongBanId.ToLower().Contains(searchString.ToLower())));
            }

            var count = list.Count();

            // page the list
            const int pageSize = 10;
            decimal aa = (decimal)list.Count() / (decimal)pageSize;
            var bb = Math.Ceiling(aa);
            if (page > bb)
            {
                page--;
            }
            page = (page == 0) ? 1 : page;
            var listPaged = list.ToPagedList(page ?? 1, pageSize);
            //if (page > listPaged.PageCount)
            //    page--;
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;


            return listPaged;

        }

        //public LoginModel login(string username, string mact)
        //{
        //    var parammeter = new SqlParameter[]
        //   {
        //        new SqlParameter("@username",username),
        //        new SqlParameter("@mact",mact)
        //   };

        //    var result = _context.LoginModels.FromSqlRaw("spLogin @username, @mact", parammeter).SingleOrDefault();
        //    if (result == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //}
    }
}
