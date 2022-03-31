using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class Supplier
    {
        public Supplier()
        {
            DanhGiaCamLao = new HashSet<DanhGiaCamLao>();
            DanhGiaCruise = new HashSet<DanhGiaCruise>();
            DanhGiaDiemThamQuan = new HashSet<DanhGiaDiemThamQuan>();
            DanhGiaGolf = new HashSet<DanhGiaGolf>();
            DanhGiaKhachSan = new HashSet<DanhGiaKhachSan>();
            DanhGiaLandtour = new HashSet<DanhGiaLandtour>();
            DanhGiaNhaHang = new HashSet<DanhGiaNhaHang>();
            DanhGiaVanChuyen = new HashSet<DanhGiaVanChuyen>();
            DichVus = new HashSet<DichVus>();
        }

        public string Code { get; set; }
        public string Tapdoan { get; set; }
        public string Tengiaodich { get; set; }
        public string Tenthuongmai { get; set; }
        public string Tinhtp { get; set; }
        public string Thanhpho { get; set; }
        public string Quocgia { get; set; }
        public string Diachi { get; set; }
        public string Dienthoai { get; set; }
        public string Fax { get; set; }
        public string Masothue { get; set; }
        public string Nganhnghe { get; set; }
        public string Nguoilienhe { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public bool Trangthai { get; set; }
        public DateTime Ngaytao { get; set; }
        public string Nguoitao { get; set; }
        public string Chinhanh { get; set; }
        public DateTime? Ngayhethan { get; set; }
        public string Logfile { get; set; }
        public string Tknganhang { get; set; }
        public string Tennganhang { get; set; }
        public string Tour { get; set; }
        public string NguoiTrinhKyHd { get; set; }
        public string ThoiGianDongMo { get; set; }
        public string NoiDungDongMo { get; set; }
        public bool KhuyenNghi { get; set; }
        public string LoaiSao { get; set; }
        public int? TapDoanId { get; set; }

        public virtual TapDoan TapDoan { get; set; }
        public virtual ICollection<DanhGiaCamLao> DanhGiaCamLao { get; set; }
        public virtual ICollection<DanhGiaCruise> DanhGiaCruise { get; set; }
        public virtual ICollection<DanhGiaDiemThamQuan> DanhGiaDiemThamQuan { get; set; }
        public virtual ICollection<DanhGiaGolf> DanhGiaGolf { get; set; }
        public virtual ICollection<DanhGiaKhachSan> DanhGiaKhachSan { get; set; }
        public virtual ICollection<DanhGiaLandtour> DanhGiaLandtour { get; set; }
        public virtual ICollection<DanhGiaNhaHang> DanhGiaNhaHang { get; set; }
        public virtual ICollection<DanhGiaVanChuyen> DanhGiaVanChuyen { get; set; }
        public virtual ICollection<DichVus> DichVus { get; set; }
    }
}
