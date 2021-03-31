using Data.Dtos;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface ITraHauCanRepository
    {
        IEnumerable<VChiphiHaucan> ListChiphiHaucan(string code);
    }
    public class TraHauCanRepository : ITraHauCanRepository
    {
        private readonly qltourContext _qltourContext;

        public TraHauCanRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public IEnumerable<VChiphiHaucan> ListChiphiHaucan(string code)
        {
            return _qltourContext.VChiphiHaucan.Where(x => x.Sgtcode == code);
        }
    }
}
