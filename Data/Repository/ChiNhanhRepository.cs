using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Repository
{
    public interface IChiNhanhRepository : IRepository<Dmchinhanh>
    {
        IPagedList<Dmchinhanh> ListChiNhanh(string searchString, int? page);
    }
    public class ChiNhanhRepository : Repository_QLT<Dmchinhanh>, IChiNhanhRepository
    {
        public ChiNhanhRepository(qltourContext qltourContext) : base(qltourContext)
        {
        }

        public IPagedList<Dmchinhanh> ListChiNhanh(string searchString, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = GetAll().AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Tencn.ToLower().Contains(searchString.ToLower()) ||
                                       x.Macn.ToLower().Contains(searchString.ToLower())||
                                       x.Thanhpho.ToLower().Contains(searchString.ToLower())||
                                       x.Diachi.ToLower().Contains(searchString.ToLower()));
            }

            var count = list.Count();

            // page the list
            const int pageSize = 15;
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
