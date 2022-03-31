﻿using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class DichVus
    {
        public DichVus()
        {
            HinhAnhs = new HashSet<HinhAnhs>();
        }

        public string MaDv { get; set; }
        public string TenHd { get; set; }
        public string LoaiSao { get; set; }
        public string ThongTinLienHe { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string NoiDung { get; set; }
        public string NguoiLienHe { get; set; }
        public string SupplierId { get; set; }
        public string Website { get; set; }
        public bool DaKy { get; set; }
        public bool HoatDong { get; set; }
        public string Tuyen { get; set; }
        public string LoaiTau { get; set; }
        public string LoaiXe { get; set; }
        public bool DauXe { get; set; }
        public string GhiChu { get; set; }
        public decimal GiaHd { get; set; }
        public string LoaiHd { get; set; }
        public int LoaiDvid { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
        public string LogFile { get; set; }
        public string ThoiGianHd { get; set; }
        public bool HotDeal { get; set; }
        public string NguoiTrinhKy { get; set; }
        public DateTime? NgayTrinhKy { get; set; }

        public virtual LoaiDvs LoaiDv { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<HinhAnhs> HinhAnhs { get; set; }
    }
}
