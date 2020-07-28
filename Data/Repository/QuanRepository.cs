using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IQuanRepository : IRepository<ThanhPho>
    {

    }
    public class QuanRepository : Repository<ThanhPho>, IQuanRepository
    {
        public QuanRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
