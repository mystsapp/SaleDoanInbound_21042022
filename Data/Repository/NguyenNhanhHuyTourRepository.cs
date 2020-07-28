using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface INguyenNhanHuyTour : IRepository<CacNoiDungHuyTour>
    {

    }
    public class NguyenNhanhHuyTourRepository : Repository<CacNoiDungHuyTour>, INguyenNhanHuyTour
    {
        public NguyenNhanhHuyTourRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
