using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IBienNhanRepository : IRepository<BienNhan>
    {

    }
    public class BienNhanRepository : Repository<BienNhan>, IBienNhanRepository
    {
        public BienNhanRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
