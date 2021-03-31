using Data.Interfaces;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IThanhPho_QLT_Repository : IRepository<Thanhpho>
    {

    }
    public class ThanhPho_QLT_Repository : Repository_QLT<Thanhpho>, IThanhPho_QLT_Repository
    {
        public ThanhPho_QLT_Repository(qltourContext context) : base(context)
        {
        }
    }
}
