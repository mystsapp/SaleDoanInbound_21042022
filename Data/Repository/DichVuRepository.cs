using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDichVuRepository
    {
        IEnumerable<Dichvu> GetAll();

        Task<Dichvu> GetByIdAsync(string id);

        IEnumerable<Dichvu> Find(Func<Dichvu, bool> predicate);
    }
    public class DichVuRepository : IDichVuRepository
    {
        private readonly qltourContext _qltourContext;

        public DichVuRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<Dichvu> Find(Func<Dichvu, bool> predicate)
        {
            return _qltourContext.Dichvu.Where(predicate);
        }

        public IEnumerable<Dichvu> GetAll()
        {
            return _qltourContext.Dichvu;
        }

        public async Task<Dichvu> GetByIdAsync(string id)
        {
            return await _qltourContext.Dichvu.FindAsync();
        }
    }
}
