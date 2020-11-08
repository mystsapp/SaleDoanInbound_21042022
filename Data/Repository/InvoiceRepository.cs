using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Repository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IEnumerable<Invoice> ListInvoice(string searchString, long tourId);
    }
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IEnumerable<Invoice> ListInvoice(string searchString, long tourId)
        {

            var list = Find(x => x.TourId == tourId);
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Id.ToLower().Contains(searchString.ToLower()) ||
                                       (!string.IsNullOrEmpty(x.Replace) && x.Replace.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Type) && x.Type.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Ref) && x.Ref.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.HopDong) && x.HopDong.ToLower().Contains(searchString.ToLower()))||
                                       (!string.IsNullOrEmpty(x.Tour.Sgtcode) && x.Tour.Sgtcode.ToLower().Contains(searchString.ToLower())));
            }

            var count = list.Count();
            return list;

        }
    }
}
