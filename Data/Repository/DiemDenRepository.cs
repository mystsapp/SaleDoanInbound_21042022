using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IDiemDenRepository : IRepository<ThanhPho> { }
    public class DiemDenRepository : Repository<ThanhPho>, IDiemDenRepository
    {
        public DiemDenRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
