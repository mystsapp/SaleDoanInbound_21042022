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
    public interface IThanhPhoRepository : IRepository<ThanhPho>
    {
        IPagedList<ThanhPhoDto> ListThanhPho(string searchString, IEnumerable<Quocgia> listQG, int? page);
    }
    public class ThanhPhoRepository : Repository<ThanhPho>, IThanhPhoRepository
    {
        public ThanhPhoRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        //public ThanhPhoRepository(SaleDoanIBDbContext context) : base(context)
        //{
        //}

        public IPagedList<ThanhPhoDto> ListThanhPho(string searchString, IEnumerable<Quocgia> listQG, int? page)
        {
            
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = GetAll().AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.TenThanhPho.ToLower().Contains(searchString.ToLower()));
            }

            var count = list.Count();
            List<ThanhPhoDto> listDto = new List<ThanhPhoDto>();
            if(count != 0)
            {
                foreach (var item in list)
                {
                    listDto.Add(new ThanhPhoDto
                    {
                        Id = item.Id,
                        NgaySua = item.NgaySua,
                        NgayTao = item.NgayTao,
                        NguoiSua = item.NguoiSua,
                        NguoiTao = item.NguoiTao,
                        QuocGia = listQG.Where(x => x.Code == item.MaQuocGia.ToString()).FirstOrDefault().Nation,
                        TenThanhPho = item.TenThanhPho
                    });
                }
            }
            
            // page the list
            const int pageSize = 2;
            decimal aa = (decimal)listDto.Count() / (decimal)pageSize;
            var bb = Math.Ceiling(aa);
            if (page > bb)
            {
                page--;
            }
            page = (page == 0) ? 1 : page;
            var listPaged = listDto.ToPagedList(page ?? 1, pageSize);
            //if (page > listPaged.PageCount)
            //    page--;
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;


            return listPaged;

        }
    }
}
