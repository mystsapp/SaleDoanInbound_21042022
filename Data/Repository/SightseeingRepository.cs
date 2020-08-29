using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ISightseeingRepository
    {
        IEnumerable<Sightseeing> GetAll();

        Task<Sightseeing> GetByIdAsync(decimal id);

        IEnumerable<Sightseeing> Find(Func<Sightseeing, bool> predicate);
    }
    public class SightseeingRepository : ISightseeingRepository
    {
        private readonly qltourContext _qltourContext;

        public SightseeingRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }
        public IEnumerable<Sightseeing> Find(Func<Sightseeing, bool> predicate)
        {
            return _qltourContext.Sightseeing.Where(predicate);
        }

        public IEnumerable<Sightseeing> GetAll()
        {
            return _qltourContext.Sightseeing;
        }

        public async Task<Sightseeing> GetByIdAsync(decimal id)
        {
            return await _qltourContext.Sightseeing.FindAsync(id);
        }
    }
}
