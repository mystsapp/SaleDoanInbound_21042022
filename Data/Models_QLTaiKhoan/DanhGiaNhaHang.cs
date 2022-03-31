﻿using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class DanhGiaNhaHang
    {
        public long Id { get; set; }
        public string TenNcu { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool? CoGpkd { get; set; }
        public bool? CoHdvat { get; set; }
        public string ViTri { get; set; }
        public string SucChuaToiDa { get; set; }
        public string DinhLuong { get; set; }
        public string ChatLuong { get; set; }
        public string NhaVeSinh { get; set; }
        public string ThaiDoPvcuaNv { get; set; }
        public string CoPvmienPhiNoiBo { get; set; }
        public string CoBaiDoXe { get; set; }
        public bool? DaCoKhaoSatThucTe { get; set; }
        public bool? KqDat { get; set; }
        public bool? KqKhaoSatThem { get; set; }
        public bool? DongYduaVaoDsncu { get; set; }
        public int LoaiDvid { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
        public string SupplierId { get; set; }
        public bool? TiemNang { get; set; }
        public bool? TaiKy { get; set; }
        public string NguoiDanhGia { get; set; }
        public string LogFile { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
