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
        IPagedList<TourDto> ListTour(string searchString, IEnumerable<Company> companies, IEnumerable<Tourkind> loaiTours, IEnumerable<Dmchinhanh> chiNhanhs, IEnumerable<CacNoiDungHuyTour> cacNoiDungHuyTours, int? page);
    }
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(SaleDoanIBDbContext context) : base(context)
        {
        }

        public IPagedList<TourDto> ListTour(string searchString, IEnumerable<Company> companies, IEnumerable<Tourkind> loaiTours, IEnumerable<Dmchinhanh> chiNhanhs, IEnumerable<CacNoiDungHuyTour> cacNoiDungHuyTours, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = new List<TourDto>();
            var tours = _context.Tours;
            if (tours == null)
            {
                return null;
            }
            foreach (var item in tours)
            {
                var tourDto = new TourDto();

                tourDto.Id = item.Id;
                tourDto.Sgtcode = item.Sgtcode;
                tourDto.KhachLe = item.KhachLe;
                tourDto.ChuDeTour = item.ChuDeTour;
                tourDto.ThiTruong = item.PhongDH;
                tourDto.NgayKhoa = item.NgayKhoa;
                tourDto.NguoiKhoa = item.NguoiKhoa;
                tourDto.NgayTao = item.NgayTao;
                tourDto.NguoiTao = item.NguoiTao;
                tourDto.NgayDen = item.NgayDen;
                tourDto.NgayDi = item.NgayDi;
                tourDto.TuyenTQ = item.TuyenTQ;
                tourDto.SoKhachDK = item.SoKhachDK;
                tourDto.DoanhThuDK = item.DoanhThuDK;
                tourDto.CompanyName = companies.Where(x => x.CompanyId == item.MaKH).FirstOrDefault().Name;
                if (item.NgayDamPhan.HasValue)
                {
                    tourDto.NgayDamPhan = item.NgayDamPhan.Value;
                }
                
                tourDto.HinhThucGiaoDich = item.HinhThucGiaoDich;
                if (item.NgayKyHopDong.HasValue)
                {
                    tourDto.NgayKyHopDong = item.NgayKyHopDong.Value;
                }
                
                tourDto.NguoiKyHopDong = item.NguoiKyHopDong;
                if (item.HanXuatVe.HasValue)
                {
                    tourDto.HanXuatVe = item.HanXuatVe.Value;
                }
                if (item.NgayThanhLyHD.HasValue)
                {
                    tourDto.NgayThanhLyHD = item.NgayThanhLyHD.Value;
                }
                
                tourDto.SoKhachTT = item.SoKhachTT;
                tourDto.SKTreEm = item.SKTreEm;
                tourDto.DoanhThuTT = item.DoanhThuTT;
                tourDto.ChuongTrinhTour = item.ChuongTrinhTour;
                tourDto.NoiDungThanhLyHD = item.NoiDungThanhLyHD;
                tourDto.DichVu = item.DichVu;
                tourDto.DaiLy = item.DaiLy;
                tourDto.TrangThai = item.TrangThai;
                tourDto.NgaySua = item.NgaySua;
                tourDto.NguoiSua = item.NguoiSua;
                tourDto.TenLoaiTour = loaiTours.Where(x => x.Id == item.LoaiTourId).FirstOrDefault().TourkindInf;
                tourDto.MaCNTao = (item.ChiNhanhTaoId == 0) ? "" : chiNhanhs.Where(x => x.Id == item.ChiNhanhTaoId).FirstOrDefault().Macn;
                if (item.NgayNhanDuTien.HasValue)
                {
                    tourDto.NgayNhanDuTien = item.NgayNhanDuTien.Value;
                }
                
                tourDto.LyDoNhanDu = item.LyDoNhanDu;
                tourDto.SoHopDong = item.SoHopDong;
                tourDto.LaiChuaVe = item.LaiChuaVe;
                tourDto.LaiGomVe = item.LaiGomVe;
                tourDto.LaiThucTeGomVe = item.LaiThucTeGomVe;
                tourDto.NguonTour = item.NguonTour;
                tourDto.FileKhachDiTour = item.FileKhachDiTour;
                tourDto.FileVeMayBay = item.FileVeMayBay;
                tourDto.FileBienNhan = item.FileBienNhan;
                tourDto.NguoiDaiDien = item.NguoiDaiDien;
                tourDto.DoiTacNuocNgoai = item.DoiTacNuocNgoai;
                tourDto.MaCNDH = chiNhanhs.Where(x => x.Id == item.ChiNhanhDHId).FirstOrDefault().Macn;
                if (item.NgayHuyTour.HasValue)
                {
                    tourDto.NgayHuyTour = item.NgayHuyTour.Value;
                }
                
                tourDto.NDHuyTour = (item.NDHuyTourId == 0) ? "" : cacNoiDungHuyTours.Where(x => x.Id == item.NDHuyTourId).FirstOrDefault().NoiDung;
                tourDto.GhiChu = item.GhiChu;
                tourDto.LoaiTien = item.LoaiTien;
                tourDto.TyGia = item.TyGia;
                tourDto.LogFile = item.LogFile;

                list.Add(tourDto);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Sgtcode.ToLower().Contains(searchString.ToLower()) ||
                                       x.SoHopDong.ToLower().Contains(searchString.ToLower()) ||
                                       x.ChuDeTour.ToLower().Contains(searchString.ToLower()) ||
                                       x.TuyenTQ.ToLower().Contains(searchString.ToLower())).ToList();
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
