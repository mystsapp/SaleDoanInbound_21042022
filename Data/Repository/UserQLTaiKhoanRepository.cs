using Data.Interfaces;
using Data.Models_QLTaiKhoan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUserQLTaiKhoanRepository : IRepository<Users>
    {
        Task<IEnumerable<Httt>> GetAllHTTT();
    }

    public class UserQLTaiKhoanRepository : Repository_QLTaiKhoan<Users>, IUserQLTaiKhoanRepository
    {
        public UserQLTaiKhoanRepository(qltaikhoanContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Httt>> GetAllHTTT()
        {
            return await _context.Httt.ToListAsync();
        }
    }
}