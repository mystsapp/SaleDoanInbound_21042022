using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IKhachTourRepository : IRepository<Khachtour>
    {
        void CreateRange(IEnumerable<Khachtour> khachtours);
    }
    public class KhachTourRepository : Repository_QLT<Khachtour>, IKhachTourRepository
    {
        public KhachTourRepository(qltourContext context) : base(context)
        {
        }

        public void CreateRange(IEnumerable<Khachtour> khachtours)
        {
            _context.AddRange(khachtours);
            _context.SaveChanges();
        }
    }
}
