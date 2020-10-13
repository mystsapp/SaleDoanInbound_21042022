﻿using Data.Dtos;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Services
{
    public interface IBaoCaoService
    {
        IEnumerable<TourBaoCaoDto> DoanhSoTheoSale(string searchFromDate, string searchToDate, List<string> MaCNs);
    }
    public class BaoCaoService : IBaoCaoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaoCaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TourBaoCaoDto> DoanhSoTheoSale(string searchFromDate, string searchToDate, List<string> MaCNs)
        {
            var list = new List<TourBaoCaoDto>();
            var tours = _unitOfWork.tourRepository.GetAll();
            var companies = _unitOfWork.khachHangRepository.GetAll();
            var chiNhanhs = _unitOfWork.dmChiNhanhRepository.GetAll();
            var loaiTours = _unitOfWork.tourKindRepository.GetAll();
            var cacNoiDungHuyTours = _unitOfWork.cacNoiDungHuyTourRepository.GetAll();

            if (tours == null)
            {
                return null;
            }
            foreach (var item in tours)
            {
                var tourDto = new TourBaoCaoDto();

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
                tourDto.MaCNTao = (item.ChiNhanhTaoId == 0) ? "" : _unitOfWork.dmChiNhanhRepository.Find(x => x.Id == item.ChiNhanhTaoId).FirstOrDefault().Macn;// chiNhanhs.Where(x => x.Id == item.ChiNhanhTaoId).FirstOrDefault().Macn;
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
                tourDto.HuyTour = item.HuyTour;
                tourDto.NDHuyTour = (item.NDHuyTourId == 0) ? "" : cacNoiDungHuyTours.Where(x => x.Id == item.NDHuyTourId).FirstOrDefault().NoiDung;
                tourDto.GhiChu = item.GhiChu;
                tourDto.LoaiTien = item.LoaiTien;
                tourDto.TyGia = item.TyGia;
                tourDto.LogFile = item.LogFile;

                //tourDto.Invoices = _context.Invoices.Where(x => x.TourId == item.Id).Count();

                list.Add(tourDto);
            }

            if (MaCNs.Count > 0)
            {
                //list = list.Where(x => x.MaCNTao == macn).ToList();
                list = list.Where(item1 => MaCNs.Any(item2 => item1.MaCNTao == item2)).ToList();
            }
            list = list.Where(x => x.HuyTour != true).OrderByDescending(x => x.NgayTao).ToList();
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
                    list = list.Where(x => x.NgayTao >= fromDate &&
                                       x.NgayTao < toDate.AddDays(1)).ToList();
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
                        list = list.Where(x => x.NgayTao >= fromDate).ToList();
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
                        list = list.Where(x => x.NgayTao < toDate.AddDays(1)).ToList();

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