using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ITournoteRepository
    {
        IEnumerable<Tournode> GetAll();

        Task<Tournode> GetByIdAsync(string id);

        IEnumerable<Tournode> Find(Func<Tournode, bool> predicate);
    }
    public class TournoteRepository : ITournoteRepository
    {
        private readonly qltourContext _qltourContext;

        public TournoteRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<Tournode> Find(Func<Tournode, bool> predicate)
        {
            return _qltourContext.Tournode.Where(predicate);
        }

        public IEnumerable<Tournode> GetAll()
        {
            return _qltourContext.Tournode;
        }

        public async Task<Tournode> GetByIdAsync(string id)
        {
            return await _qltourContext.Tournode.FindAsync(id);
        }
    }
}
