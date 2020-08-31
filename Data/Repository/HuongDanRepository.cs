using Data.Models_QLT;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IHuongDanRepository
    {
        IEnumerable<Huongdan> GetAll();

        Task<Huongdan> GetByIdAsync(decimal id);

        IEnumerable<Huongdan> Find(Func<Huongdan, bool> predicate);
    }

    public class HuongDanRepository : IHuongDanRepository
    {
        private readonly qltourContext _qltourContext;

        public HuongDanRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }
        public IEnumerable<Huongdan> Find(Func<Huongdan, bool> predicate)
        {
            return _qltourContext.Huongdan.Where(predicate);
        }

        public IEnumerable<Huongdan> GetAll()
        {
            return _qltourContext.Huongdan;
        }

        public async Task<Huongdan> GetByIdAsync(decimal id)
        {
            return await _qltourContext.Huongdan.FindAsync(id);
        }
    }
}