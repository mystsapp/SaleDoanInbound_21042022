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
    public interface ITourRepository : IRepository<Tour>
    {
        IPagedList<TourDto> ListTour(string searchString, IEnumerable<Company> companies, IEnumerable<LoaiTour> loaiTours, IEnumerable<Dmchinhanh> chiNhanhs, IEnumerable<CacNoiDungHuyTour> cacNoiDungHuyTours, int? page);
    }
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IPagedList<TourDto> ListTour(string searchString, IEnumerable<Company> companies, IEnumerable<LoaiTour> loaiTours, IEnumerable<Dmchinhanh> chiNhanhs, IEnumerable<CacNoiDungHuyTour> cacNoiDungHuyTours, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = new List<TourDto>();
            var tours = _context.Tours;
            if(tours != null)
            {
                return null;
            }
            foreach (var item in tours)
            {
                list.Add(new TourDto()
                {
                    Id = item.Id,
                    Sgtcode = item.Sgtcode,
                    KhachLe = item.KhachLe,
                    ChuDeTour = item.ChuDeTour,
                    ThiTruong = item.ThiTruong,
                    NgayKhoa = item.NgayKhoa,
                    NguoiKhoa = item.NguoiKhoa,
                    NgayTao = item.NgayTao,
                    NguoiTao = item.NguoiTao,
                    NgayDen = item.NgayDen,
                    NgayDi = item.NgayDi,
                    TuyenTQ = item.TuyenTQ,
                    SoKhachDK = item.SoKhachDK,
                    DoanhThuDK = item.DoanhThuDK,
                    CompanyName = companies.Where(x => x.CompanyId == item.CompanyId).FirstOrDefault().Name,
                    NgayDamPhan = item.NgayDamPhan,
                    HinhThucGiaoDich = item.HinhThucGiaoDich,
                    NgayKyHopDong = item.NgayKyHopDong,
                    NguoiKyHopDong = item.NguoiKyHopDong,
                    HanXuatVe = item.HanXuatVe,
                    NgayThanhLyHD = item.NgayThanhLyHD,
                    SoKhachTT = item.SoKhachTT,
                    SKTreEm = item.SKTreEm,
                    DoanhThuTT = item.DoanhThuTT,
                    ChuongTrinhTour = item.ChuongTrinhTour,
                    NoiDungThanhLyHD = item.NoiDungThanhLyHD,
                    DichVu = item.DichVu,
                    DaiLy = item.DaiLy,
                    TrangThai = item.TrangThai,
                    NgaySua = item.NgaySua,
                    NguoiSua = item.NguoiSua,
                    TenLoaiTour = loaiTours.Where(x => x.Id == item.LoaiTourId).FirstOrDefault().TenLoaiTour,
                    MaCNTao = chiNhanhs.Where(x => x.Id == item.ChiNhanhTaoId).FirstOrDefault().Macn,
                    NgayNhanDuTien = item.NgayNhanDuTien,
                    LyDoNhanDu = item.LyDoNhanDu,
                    SoHopDong = item.SoHopDong,
                    LaiChuaVe = item.LaiChuaVe,
                    LaiGomVe = item.LaiGomVe,
                    LaiThucTeGomVe = item.LaiThucTeGomVe,
                    NguonTour = item.NguonTour,
                    FileKhachDiTour = item.FileKhachDiTour,
                    FileVeMayBay = item.FileVeMayBay,
                    FileBienNhan = item.FileBienNhan,
                    NguoiDaiDien = item.NguoiDaiDien,
                    DoiTacNuocNgoai = item.DoiTacNuocNgoai,
                    MaCNDH = chiNhanhs.Where(x => x.Id == item.ChiNhanhDHId).FirstOrDefault().Macn,
                    NgayHuyTour = item.NgayHuyTour,
                    NDHuyTour = cacNoiDungHuyTours.Where(x => x.Id == item.NDHuyTourId).FirstOrDefault().NoiDung,
                    GhiChu = item.GhiChu,
                    LoaiTien = item.LoaiTien,
                    TyGia = item.TyGia,
                    LogFile = item.LogFile
                });
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Sgtcode.ToLower().Contains(searchString.ToLower()) ||
                                       x.SoHopDong.ToLower().Contains(searchString.ToLower()) ||
                                       x.ChuDeTour.ToLower().Contains(searchString.ToLower())||
                                       x.TuyenTQ.ToLower().Contains(searchString.ToLower()) ).ToList();
            }

            var count = list.Count();

            // page the list
            const int pageSize = 2;
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
