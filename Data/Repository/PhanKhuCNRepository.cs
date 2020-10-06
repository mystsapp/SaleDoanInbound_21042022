using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IPhanKhuCNRepository : IRepository<PhanKhuCN>
    {

    }
    public class PhanKhuCNRepository : Repository<PhanKhuCN>, IPhanKhuCNRepository
    {
        public PhanKhuCNRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
    
}
