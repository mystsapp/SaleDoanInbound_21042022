using Data.Interfaces;
using Data.Models;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IUserRepository : IRepository<Users>
    {

    }
    public class UserRepository : Repository_QLT<Users>, IUserRepository
    {
        public UserRepository(qltourContext context) : base(context)
        {
        }
    }
}
