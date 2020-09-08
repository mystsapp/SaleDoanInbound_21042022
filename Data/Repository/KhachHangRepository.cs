using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Data.Repository
{
    public interface IKhachHangRepository : IRepository<Company>
    {
        IPagedList<Company> ListKhachHang(string searchName, int? page);
    }
    public class KhachHangRepository : Repository_QLT<Company>, IKhachHangRepository
    {
        public KhachHangRepository(qltourContext context) : base(context)
        {
        }

        public IPagedList<Company> ListKhachHang(string searchName, int? page)
        {

            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = GetAll().OrderByDescending(x => x.CompanyId).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                list = list.Where(x => x.CompanyId.ToLower().Contains(searchName.ToLower()) ||
                                       x.Name.ToLower().Contains(searchName.ToLower()) ||
                                       x.Nation.ToLower().Contains(searchName.ToLower()) ||
                                       x.Natione.ToLower().Contains(searchName.ToLower())||
                                       x.Fullname.ToLower().Contains(searchName.ToLower()));
            }

            var count = list.Count();

            // page the list
            const int pageSize = 200;
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
