using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ICTVATRepository : IRepository<CTVAT>
    {

    }
    public class CTVATRepository : Repository<CTVAT>, ICTVATRepository
    {
        public CTVATRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
