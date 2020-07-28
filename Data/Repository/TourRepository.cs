using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ITourRepository : IRepository<Tour>
    {

    }
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
