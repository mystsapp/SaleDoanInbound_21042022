using Data.Models_HDVATOB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUserHDVATOBRepository
    {
        IEnumerable<Users> GetAll();

        Task<Users> GetByIdAsync(string id);
        Users GetById(string id);

        IEnumerable<Users> Find(Func<Users, bool> predicate);
    }
    public class UserHDVATOBRepository : IUserHDVATOBRepository
    {
        private readonly hdvatobContext _hdvatobContext;

        public UserHDVATOBRepository(hdvatobContext hdvatobContext)
        {
            _hdvatobContext = hdvatobContext;
        }

        public IEnumerable<Users> Find(Func<Users, bool> predicate)
        {
            return _hdvatobContext.Users.Where(predicate);
        }

        public IEnumerable<Users> GetAll()
        {
            return _hdvatobContext.Users;
        }

        public Users GetById(string id)
        {
            return _hdvatobContext.Users.Find(id);
        }

        public async Task<Users> GetByIdAsync(string id)
        {
            return await _hdvatobContext.Users.FindAsync(id);
        }

    }
}
