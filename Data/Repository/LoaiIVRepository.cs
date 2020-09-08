using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Repository
{
    public interface ILoaiIVRepository : IRepository<LoaiIV>
    {
        IPagedList<LoaiIV> ListLoaiIV(string searchString, int? page);
    }
    public class LoaiIVRepository : Repository<LoaiIV>, ILoaiIVRepository
    {
        public LoaiIVRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IPagedList<LoaiIV> ListLoaiIV(string searchString, int? page)
        {
            
                // return a 404 if user browses to before the first page
                if (page.HasValue && page < 1)
                    return null;

                // retrieve list from database/whereverand

                var list = GetAll().AsQueryable();
                if (!string.IsNullOrEmpty(searchString))
                {
                    list = list.Where(x => x.Id.ToLower().Contains(searchString.ToLower()) || 
                                           x.TenLoaiIV.ToLower().Contains(searchString.ToLower()) ||
                                           x.GhiChu.ToLower().Contains(searchString.ToLower()));
                }

                var count = list.Count();

                // page the list
                const int pageSize = 10;
                decimal aa = (decimal)list.Count() / (decimal)pageSize;
                var bb = Math.Ceiling(aa);
                if (page > bb)
                {
                    page--;
                }
                page = (page == 0) ? 1 : page;
                var listPaged = list.ToPagedList(page ?? 1, pageSize);
                //if (page > listPaged.PageCount)
                //    page--;
                // return a 404 if user browses to pages beyond last page. special case first page if no items exist
                if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                    return null;


                return listPaged;
            
        }
    }
}
