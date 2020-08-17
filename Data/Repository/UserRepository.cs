using Data.Dtos;
using Data.Interfaces;
using Data.Models_IB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        LoginModel login(string username, string mact);
        int changepass(string username, string newpass);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public int changepass(string username, string newpass)
        {
            var parammeter = new SqlParameter[]
             {
                    new SqlParameter("@username",username),
                    new SqlParameter("@newpass",newpass)
             };

            try
            {
                return _context.Database.ExecuteSqlRaw("spChangepass @username, @newpass", parammeter);
            }
            catch
            {
                throw;
            }
        }

        public LoginModel login(string username, string mact)
        {
            var parammeter = new SqlParameter[]
           {
                new SqlParameter("@username",username),
                new SqlParameter("@mact",mact)
           };

            var result = _context.LoginModels.FromSqlRaw("spLogin @username, @mact", parammeter).SingleOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
    }
}
