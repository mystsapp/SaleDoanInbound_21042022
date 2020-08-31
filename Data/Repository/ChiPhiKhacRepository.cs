using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IChiPhiKhacRepository
    {

        IEnumerable<Chiphikhac> GetAll();

        Task<Chiphikhac> GetByIdAsync(decimal id);

        IEnumerable<Chiphikhac> Find(Func<Chiphikhac, bool> predicate);
    }
    public class ChiPhiKhacRepository : IChiPhiKhacRepository
    {
        private readonly qltourContext _qltourContext;

        public ChiPhiKhacRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<Chiphikhac> Find(Func<Chiphikhac, bool> predicate)
        {
            return _qltourContext.Chiphikhac.Where(predicate);
        }

        public IEnumerable<Chiphikhac> GetAll()
        {
            return _qltourContext.Chiphikhac;
        }

        public async Task<Chiphikhac> GetByIdAsync(decimal id)
        {
            return await _qltourContext.Chiphikhac.FindAsync(id);
        }
    }
}
