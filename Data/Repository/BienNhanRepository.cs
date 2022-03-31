﻿using Data.Dtos;
using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLTaiKhoan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IBienNhanRepository : IRepository<BienNhan>
    {
        IEnumerable<BienNhanDto> ListBienNhan(string searchString, long tourId, string searchFromDate, string searchToDate);

        Task<BienNhan> GetByIdIncludeOneAsync(long id);
    }

    public class BienNhanRepository : Repository<BienNhan>, IBienNhanRepository
    {
        public BienNhanRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task<BienNhan> GetByIdIncludeOneAsync(long id)
        {
            return await _context.BienNhans.Include(x => x.Tour).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<BienNhanDto> ListBienNhan(string searchString, long tourId, string searchFromDate, string searchToDate)
        {
            var bienNhans = Find(x => x.TourId == tourId);
            List<BienNhanDto> list = new List<BienNhanDto>();
            foreach (var item in bienNhans)
            {
                list.Add(new BienNhanDto()
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
                    NoiDungHuy = (item.NDHuyBNId == 0) ? "0" : _context.CacNoiDungHuyTours.Find(item.NDHuyBNId).NoiDung,
                    TourId = item.TourId,
                    Sgtcode = item.Tour.Sgtcode,
                    SK = item.SK,
                    SoTien = item.SoTien,
                    STNguyenTe = item.STNguyenTe,
                    TenKhach = item.TenKhach,
                    TyGia = item.TyGia,
                    HTTT = item.HTTT
                });
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.SoBN.ToLower().Contains(searchString.ToLower()) ||
                                       x.TenKhach.ToLower().Contains(searchString.ToLower()) ||
                                       x.DienThoai.ToLower().Contains(searchString.ToLower()) ||
                                       x.Sgtcode.ToLower().Contains(searchString.ToLower())).ToList();
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

            return list.OrderByDescending(x => x.NgayTao);
        }
    }
}