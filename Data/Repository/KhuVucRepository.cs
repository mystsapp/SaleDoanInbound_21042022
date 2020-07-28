using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IKhuvucRepository : IRepository<KhuVuc>
    {

    }
    public class KhuVucRepository : Repository<KhuVuc>, IKhuvucRepository
    {
        public KhuVucRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
