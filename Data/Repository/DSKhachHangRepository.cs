using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IDSKhachHangRepository : IRepository<KhachHang>
    {
        void CreateRange(IEnumerable<KhachHang> khachHangs);
        void DeleteRange(IEnumerable<KhachHang> khachHangs);
    }
    public class DSKhachHangRepository : Repository<KhachHang>, IDSKhachHangRepository
    {
        public DSKhachHangRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public void CreateRange(IEnumerable<KhachHang> khachHangs)
        {
            _context.AddRange(khachHangs);
            _context.SaveChanges();
        }
        
        public void DeleteRange(IEnumerable<KhachHang> khachHangs)
        {
            _context.RemoveRange(khachHangs);
            _context.SaveChanges();
        }

    }
}
