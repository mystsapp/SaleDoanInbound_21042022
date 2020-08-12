using Data.Interfaces;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {

    }
    public class CompanyRepository : Repository_QLT<Company>, ICompanyRepository
    {
        public CompanyRepository(qltourContext context) : base(context)
        {
        }
    }
}
