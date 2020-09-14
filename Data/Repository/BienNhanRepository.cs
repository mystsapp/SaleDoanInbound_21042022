using Data.Interfaces;
using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface IBienNhanRepository : IRepository<BienNhan>
    {
        IEnumerable<BienNhan> ListBienNhan(string searchString, long tourId, string searchFromDate, string searchToDate);
    }
    public class BienNhanRepository : Repository<BienNhan>, IBienNhanRepository
    {
        public BienNhanRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IEnumerable<BienNhan> ListBienNhan(string searchString, long tourId, string searchFromDate, string searchToDate)
        {

            var list = Find(x => x.TourId == tourId);
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Id.ToLower().Contains(searchString.ToLower()) ||
                                       x.TenKhach.ToLower().Contains(searchString.ToLower()) ||
                                       x.Tour.Sgtcode.ToLower().Contains(searchString.ToLower()));
            }

            var count = list.Count();

            // search date
            DateTime fromDate, toDate;
            if (!string.IsNullOrEmpty(searchFromDate) && !string.IsNullOrEmpty(searchToDate))
            {

                try
                {
                    fromDate = DateTime.Parse(searchFromDate);
                    toDate = DateTime.Parse(searchToDate);

                    if (fromDate > toDate)
                    {
                        return null;
                    }
                    list = list.Where(x => x.NgayBN >= fromDate &&
                                       x.NgayBN < toDate.AddDays(1)).ToList();
                }
                catch (Exception)
                {

                    return null;
                }


                //list.Where(x => x.NgayTao >= fromDate && x.NgayTao < (toDate.AddDays(1))/*.ToPagedList(page, pageSize)*/;



            }
            else
            {
                if (!string.IsNullOrEmpty(searchFromDate))
                {
                    try
                    {
                        fromDate = DateTime.Parse(searchFromDate);
                        list = list.Where(x => x.NgayBN >= fromDate).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
                if (!string.IsNullOrEmpty(searchToDate))
                {
                    try
                    {
                        toDate = DateTime.Parse(searchToDate);
                        list = list.Where(x => x.NgayBN < toDate.AddDays(1)).ToList();

                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            // search date

            return list;

        }
    }
}
