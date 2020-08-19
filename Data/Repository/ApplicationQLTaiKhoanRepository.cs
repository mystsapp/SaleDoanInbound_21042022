using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IApplicationQLTaiKhoanRepository
    {
        Task<Application> GetByIdAsync(string id);

    }
    public class ApplicationQLTaiKhoanRepository : IApplicationQLTaiKhoanRepository
    {
        private readonly qltaikhoanContext _qltaikhoanContext;

        public ApplicationQLTaiKhoanRepository(qltaikhoanContext qltaikhoanContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
        }
        public async Task<Application> GetByIdAsync(string id)
        {
            return await _qltaikhoanContext.Application.FindAsync(id);
        }
    }
}
