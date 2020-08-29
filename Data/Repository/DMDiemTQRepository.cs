using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDMDiemTQRepository
    {

        IEnumerable<Dmdiemtq> GetAll();

        Task<Dmdiemtq> GetByIdAsync(string id);

        IEnumerable<Dmdiemtq> Find(Func<Dmdiemtq, bool> predicate);
    }
    public class DMDiemTQRepository : IDMDiemTQRepository
    {
        private readonly qltourContext _qltourContext;

        public DMDiemTQRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<Dmdiemtq> Find(Func<Dmdiemtq, bool> predicate)
        {
            return _qltourContext.Dmdiemtq.Where(predicate);
        }

        public IEnumerable<Dmdiemtq> GetAll()
        {
            return _qltourContext.Dmdiemtq;
        }

        public async Task<Dmdiemtq> GetByIdAsync(string id)
        {
            return await _qltourContext.Dmdiemtq.FindAsync(id);
        }
    }
}
