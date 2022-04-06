using Data.Dtos;
using Data.Models_QLT;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface IUserQLTourRepository
    {
        UserInfo getSaleByUsername(string username, string loaikhach, bool khachle);
    }

    public class UserQLTourRepository : IUserQLTourRepository
    {
        private readonly qltourContext _qltourContext;

        public UserQLTourRepository(qltourContext qltourContext)
        {
            _qltourContext = qltourContext;
        }

        public UserInfo getSaleByUsername(string username, string loaikhach, bool khachle)
        {
            var parammeter = new SqlParameter[]
            {
                    new SqlParameter("@username",username),
                    new SqlParameter("@passtypId",loaikhach),
                    new SqlParameter("@khachle",khachle)
            };

            var result = _qltourContext.UserInfo.FromSqlRaw("spGetSaleByUsername @username, @passtypId,@khachle", parammeter).ToList()/*.SingleOrDefault()*/;
            if (result == null)
            {
                return null;
            }
            else
            {
                var userInfo = result.FirstOrDefault();
                return userInfo;
            }
        }
    }
}