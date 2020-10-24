using Data.Interfaces;
using Data.Models_HDDT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IDSDangKyHDRepository : IRepository<Dsdangkyhd>
    {

    }
    public class DSDangKyHDRepository : Repository_HDDT<Dsdangkyhd>, IDSDangKyHDRepository
    {
        public DSDangKyHDRepository(hoadondientuContext context) : base(context)
        {
        }
    }
}
