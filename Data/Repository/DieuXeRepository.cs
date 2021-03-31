using Data.Models_QLT;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDieuXeRepository
    {
        IEnumerable<Dieuxe> GetAll();

        Task<Dieuxe> GetByIdAsync(decimal id);

        IEnumerable<Dieuxe> Find(Func<Dieuxe, bool> predicate);

        IEnumerable<Dieuxe> ListXe(string code);
    }

    public class DieuXeRepository : IDieuXeRepository
    {
        private readonly qltourContext _qltourContext;

        public DieuXeRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }
        public IEnumerable<Dieuxe> Find(Func<Dieuxe, bool> predicate)
        {
            return _qltourContext.Dieuxe.Where(predicate);
        }

        public IEnumerable<Dieuxe> GetAll()
        {
            return _qltourContext.Dieuxe;
        }

        public async Task<Dieuxe> GetByIdAsync(decimal id)
        {
            return await _qltourContext.Dieuxe.FindAsync(id);
        }

        public IEnumerable<Dieuxe> ListXe(string code)
        {
            return _qltourContext.Dieuxe.Where(x => x.Sgtcode == code && x.Del == false).OrderBy(x => x.Sttxe);
        }
    }
}