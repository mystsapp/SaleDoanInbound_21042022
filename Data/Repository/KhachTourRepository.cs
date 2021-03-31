using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface IKhachTourRepository : IRepository<Khachtour>
    {
        void CreateRange(IEnumerable<Khachtour> khachtours);
        List<Khachtour> ListKhachTour(string code);
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

        public List<Khachtour> ListKhachTour(string code)
        {
            return _context.Khachtour.Where(x => x.Sgtcode == code && x.Del == false).OrderBy(x => x.Stt).ToList();
        }
    }
}
