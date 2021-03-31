using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ITourprogRepository
    {
        IEnumerable<Tourprog> GetAll();

        Task<Tourprog> GetByIdAsync(string id);

        IEnumerable<Tourprog> Find(Func<Tourprog, bool> predicate);

        IEnumerable<Tourprog> ListTourProg(string code);
    }
    public class TourprogRepository : ITourprogRepository
    {
        private readonly qltourContext _qltourContext;

        public TourprogRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }
        public IEnumerable<Tourprog> Find(Func<Tourprog, bool> predicate)
        {
            return _qltourContext.Tourprog.Where(predicate);
        }

        public IEnumerable<Tourprog> GetAll()
        {
            return _qltourContext.Tourprog;
        }

        public async Task<Tourprog> GetByIdAsync(string id)
        {
            return await _qltourContext.Tourprog.FindAsync(id);
        }

        public IEnumerable<Tourprog> ListTourProg(string code)
        {
            return _qltourContext.Tourprog.Where(x => x.Sgtcode == code).OrderBy(x => x.Stt);
        }

    }
}
