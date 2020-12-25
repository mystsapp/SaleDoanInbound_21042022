using Data.Dtos;
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
    public interface IBienNhanService
    {
        Task<IPagedList<BienNhanDto>> BienNhanPagedList(string searchString, string searchFromDate, string searchToDate, int? page, List<User> users);
        
    }
    public class BienNhanService : IBienNhanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BienNhanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<BienNhanDto>> BienNhanPagedList(string searchString, string searchFromDate, string searchToDate, int? page, List<User> users)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            var bienNhans = await _unitOfWork.bienNhanRepository.GetAllIncludeOneAsync(x => x.Tour);

            // phan quyen
            if (users != null)// ko phai admin
            {

                bienNhans = bienNhans.Where(item1 => users.Any(item2 => item1.NguoiTao == item2.Username)).ToList(); // chi lay nhung item (list) co user trong users

            }
            // phan quyen

            List<BienNhanDto> listBNDto = new List<BienNhanDto>();
            foreach (var item in bienNhans)
            {
                listBNDto.Add(new BienNhanDto()
                {
                    DiaChi = item.DiaChi,
                    DienThoai = item.DienThoai,
                    GhiChu = item.GhiChu,
                    HuyBN = item.HuyBN,
                    Id = item.Id,
                    SoBN = item.SoBN,
                    KhachLe = item.KhachLe,
                    LoaiTien = item.LoaiTien,
                    LogFile = item.LogFile,
                    NgayBN = item.NgayBN,
                    NgayHuy = item.NgayHuy,
                    NgaySua = item.NgaySua,
                    NgayTao = item.NgayTao,
                    NguoiSua = item.NguoiSua,
                    NguoiTao = item.NguoiTao,
                    NoiDung = item.NoiDung,
                    NoiDungHuy = (item.NDHuyBNId == 0) ? "0" : _unitOfWork.cacNoiDungHuyTourRepository.GetById(item.NDHuyBNId).NoiDung,
                    TourId = item.TourId,
                    Sgtcode = item.Tour.Sgtcode,
                    SK = item.SK,
                    SoTien = item.SoTien,
                    TenKhach = item.TenKhach,
                    TyGia = item.TyGia

                });
            }

            var list = listBNDto.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.SoBN.ToLower().Contains(searchString.ToLower()) ||
                                       (!string.IsNullOrEmpty(x.TenKhach) && x.TenKhach.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.DienThoai) && x.DienThoai.ToLower().Contains(searchString.ToLower())) ||
                                       (!string.IsNullOrEmpty(x.Sgtcode) && x.Sgtcode.ToLower().Contains(searchString.ToLower())));
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
                    list = list.Where(x => x.NgayBN >= fromDate &&
                                       x.NgayBN < toDate.AddDays(1));
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
                        list = list.Where(x => x.NgayBN >= fromDate);
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
                        list = list.Where(x => x.NgayBN < toDate.AddDays(1));

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
