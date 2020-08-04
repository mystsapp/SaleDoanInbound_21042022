using Data.Dtos;
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
    public interface ITourIBRepository : IRepository<TourIB>
    {
        IPagedList<TourIBDto> ListTourIB(string searchString, List<TourIB> tourIBs, List<Company> companies, List<CacNoiDungHuyTour> cacNoiDungHuyTours, int? page);
    }
    public class TourIBRepository : Repository<TourIB>, ITourIBRepository
    {
        public TourIBRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IPagedList<TourIBDto> ListTourIB(string searchString, List<TourIB> tourIBs, List<Company> companies, List<CacNoiDungHuyTour> cacNoiDungHuyTours, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = new List<TourIBDto>();

            foreach (var item in tourIBs)
            {
                list.Add(new TourIBDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Arr = item.Arr,
                    CompanyName = companies.Where(x => x.CompanyId == item.CompanyId).FirstOrDefault().Name,
                    Dep = item.Dep,
                    Deposit = item.Deposit,
                    NoiDungHuy = cacNoiDungHuyTours.Where(x => x.Id == item.NoiDungHuy).Count() == 0 ? "" : cacNoiDungHuyTours.Where(x => x.Id == item.NoiDungHuy).FirstOrDefault().NoiDung,
                    Note = item.Note,
                    Order = item.Order,
                    Pax = item.Pax,
                    Ref = item.Ref,
                    SGTCode = item.SGTCode
                });
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Name.ToLower().Contains(searchString.ToLower()) ||
                                       x.NoiDungHuy.ToLower().Contains(searchString.ToLower()) ||
                                       x.Address.ToLower().Contains(searchString.ToLower()) ||
                                       x.CompanyName.ToLower().Contains(searchString.ToLower())||
                                       x.SGTCode.ToLower().Contains(searchString.ToLower())).ToList();
            }
            
            var count = list.Count();

            // page the list
            const int pageSize = 3;
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
