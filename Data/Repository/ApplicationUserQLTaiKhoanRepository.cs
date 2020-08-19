using Data.Interfaces;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IApplicationUserQLTaiKhoanRepository : IRepository<ApplicationUser>
    {
         Task<ApplicationUser> GetByIdTwoKeyAsync(string username, string mct);
    }
    public class ApplicationUserQLTaiKhoanRepository : Repository_QLTaiKhoan<ApplicationUser>, IApplicationUserQLTaiKhoanRepository
    {
        public ApplicationUserQLTaiKhoanRepository(qltaikhoanContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> GetByIdTwoKeyAsync(string username, string mct)
        {
            return await _context.ApplicationUser.FindAsync(username, mct);
        }
    }
}
