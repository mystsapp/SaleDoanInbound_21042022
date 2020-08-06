using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IChiTietBNRepository : IRepository<ChiTietBN>
    {

    }
    public class ChiTietBNRepository : Repository<ChiTietBN>, IChiTietBNRepository
    {
        public ChiTietBNRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
