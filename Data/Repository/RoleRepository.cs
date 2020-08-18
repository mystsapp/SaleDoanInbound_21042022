using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IRoleRepository : IRepository<Role>
    {

    }
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
