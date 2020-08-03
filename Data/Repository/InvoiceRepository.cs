using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {

    }
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(SaleDoanIBDbContext context) : base(context)
        {
        }
    }
}
