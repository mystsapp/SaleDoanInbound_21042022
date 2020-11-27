using Data.Models_HDDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IdsChiNhanh_HDDTRepository
    {
        IEnumerable<Dschinhanh> GetAll();

        Task<Dschinhanh> GetByIdAsync(string id);
        Dschinhanh GetById(string id);

        IEnumerable<Dschinhanh> Find(Func<Dschinhanh, bool> predicate);
    }
    public class dsChiNhanh_HDDTRepository : IdsChiNhanh_HDDTRepository
    {
        private readonly hoadondientuContext _hoadondientuContext;

        public dsChiNhanh_HDDTRepository(hoadondientuContext hoadondientuContext)
        {
            _hoadondientuContext = hoadondientuContext;
        }

        public IEnumerable<Dschinhanh> Find(Func<Dschinhanh, bool> predicate)
        {
            return _hoadondientuContext.Dschinhanh.Where(predicate);
        }

        public IEnumerable<Dschinhanh> GetAll()
        {
            return _hoadondientuContext.Dschinhanh;
        }

        public Dschinhanh GetById(string id)
        {
            return _hoadondientuContext.Dschinhanh.Find(id);
        }

        public async Task<Dschinhanh> GetByIdAsync(string id)
        {
            return await _hoadondientuContext.Dschinhanh.FindAsync(id);
        }

    }
}
