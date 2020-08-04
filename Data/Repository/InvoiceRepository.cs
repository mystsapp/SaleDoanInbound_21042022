using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Data.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IPagedList<Invoice> ListInvoice(string searchString, int page);
    }
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IPagedList<Invoice> ListInvoice(string searchString, int page)
        {
            throw new NotImplementedException();
        }
    }
}
