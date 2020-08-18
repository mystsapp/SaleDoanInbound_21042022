using Data.Interfaces;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IApplicationUserQLTaiKhoanRepository : IRepository<ApplicationUser>
    {

    }
    public class ApplicationUserQLTaiKhoanRepository : Repository_QLTaiKhoan<ApplicationUser>, IApplicationUserQLTaiKhoanRepository
    {
        public ApplicationUserQLTaiKhoanRepository(qltaikhoanContext context) : base(context)
        {
        }
    }
}
