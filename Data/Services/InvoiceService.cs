using Data.Models_IB;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.Services
{
    public interface IInvoiceService
    {
        Task<IPagedList<Invoice>> InvoicePagedList(string searchString, string searchFromDate, string searchToDate, int? page, List<User> users);
    }
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<Invoice>> InvoicePagedList(string searchString, string searchFromDate, string searchToDate, int? page, List<User> users)
        {

            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            var list = await _unitOfWork.invoiceRepository.GetAllIncludeOneAsync(x => x.Tour);
            
            // phan quyen
            if(users != null)// ko phai admin
            {
                
                list = list.Where(item1 => users.Any(item2 => item1.NguoiTao == item2.Username)).ToList(); // chi lay nhung item (list) co user trong users
                
            }
            // phan quyen

            list = list.OrderByDescending(x => x.Date).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Id.ToLower().Contains(searchString.ToLower()) ||
                                    (!string.IsNullOrEmpty(x.Replace) && x.Replace.ToLower().Contains(searchString.ToLower())) ||
                                    (!string.IsNullOrEmpty(x.MaKH) && x.MaKH.ToLower().Contains(searchString.ToLower())) ||
                                    (!string.IsNullOrEmpty(x.TenKhach) && x.TenKhach.ToLower().Contains(searchString.ToLower())) ||
                                    (!string.IsNullOrEmpty(x.Ref) && x.Ref.ToLower().Contains(searchString.ToLower())) ||
                                    (!string.IsNullOrEmpty(x.HopDong) && x.HopDong.ToLower().Contains(searchString.ToLower())) ||
                                    (!string.IsNullOrEmpty(x.Tour.Sgtcode) && x.Tour.Sgtcode.ToLower().Contains(searchString.ToLower())));
            }

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
                    list = list.Where(x => x.Date >= fromDate &&
                                       x.Date < toDate.AddDays(1));
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
                        list = list.Where(x => x.Date >= fromDate);
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
                        list = list.Where(x => x.Date < toDate.AddDays(1));

                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            // search date


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
