﻿using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class DanhGiaCruise
    {
        public long Id { get; set; }
        public string TenNcu { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TieuChuanSao { get; set; }
        public bool Gpkd { get; set; }
        public bool Vat { get; set; }
        public int SoLuongTauNguDem { get; set; }
        public string SucChuaTauNguDem { get; set; }
        public string LoaiTau { get; set; }
        public int SoLuongTauTqngay { get; set; }
        public string SucChuaTauTqngay { get; set; }
        public bool GiaCaHopLy { get; set; }
        public bool CabineCoBanCong { get; set; }
        public string CangDonKhach { get; set; }
        public bool CoHoTroTot { get; set; }
        public bool KhaoSatThucTe { get; set; }
        public bool KqDat { get; set; }
        public bool KqKhaoSatThem { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
        public int LoaiDvid { get; set; }
        public string SupplierId { get; set; }
        public bool TiemNang { get; set; }
        public bool TaiKy { get; set; }
        public string NguoiDanhGia { get; set; }
        public string LogFile { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
