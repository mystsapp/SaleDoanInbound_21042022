using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models_QLTaiKhoan
{
    public partial class qltaikhoanContext : DbContext
    {
        public qltaikhoanContext()
        {
        }

        public qltaikhoanContext(DbContextOptions<qltaikhoanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Citys> Citys { get; set; }
        public virtual DbSet<DanhGiaCamLao> DanhGiaCamLao { get; set; }
        public virtual DbSet<DanhGiaCruise> DanhGiaCruise { get; set; }
        public virtual DbSet<DanhGiaDiemThamQuan> DanhGiaDiemThamQuan { get; set; }
        public virtual DbSet<DanhGiaGolf> DanhGiaGolf { get; set; }
        public virtual DbSet<DanhGiaKhachSan> DanhGiaKhachSan { get; set; }
        public virtual DbSet<DanhGiaLandtour> DanhGiaLandtour { get; set; }
        public virtual DbSet<DanhGiaNhaHang> DanhGiaNhaHang { get; set; }
        public virtual DbSet<DanhGiaVanChuyen> DanhGiaVanChuyen { get; set; }
        public virtual DbSet<DichVus> DichVus { get; set; }
        public virtual DbSet<Dichvu> Dichvu { get; set; }
        public virtual DbSet<Dmchinhanh> Dmchinhanh { get; set; }
        public virtual DbSet<Dmdaily> Dmdaily { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<FolderUser> FolderUser { get; set; }
        public virtual DbSet<HinhAnhs> HinhAnhs { get; set; }
        public virtual DbSet<Httt> Httt { get; set; }
        public virtual DbSet<LoaiDvs> LoaiDvs { get; set; }
        public virtual DbSet<LoginModel> LoginModel { get; set; }
        public virtual DbSet<Mien> Mien { get; set; }
        public virtual DbSet<Nationals> Nationals { get; set; }
        public virtual DbSet<Phongban> Phongban { get; set; }
        public virtual DbSet<Quan> Quan { get; set; }
        public virtual DbSet<Quocgia> Quocgia { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Supplierob> Supplierob { get; set; }
        public virtual DbSet<TapDoan> TapDoan { get; set; }
        public virtual DbSet<Thanhpho> Thanhpho { get; set; }
        public virtual DbSet<Thanhpho1> Thanhpho1 { get; set; }
        public virtual DbSet<Tinh> Tinh { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VPhongban> VPhongban { get; set; }
        public virtual DbSet<VSupplierob> VSupplierob { get; set; }
        public virtual DbSet<VSupplierob1> VSupplierob1 { get; set; }
        public virtual DbSet<VTinh> VTinh { get; set; }
        public virtual DbSet<VUserHoadon> VUserHoadon { get; set; }
        public virtual DbSet<Vungmien> Vungmien { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //                optionsBuilder.UseSqlServer("Server=118.68.170.128;database=qltaikhoan;Trusted_Connection=true;User Id=vanhong;Password=Hong@2019;Integrated security=false;MultipleActiveResultSets=true");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Chuongtrinh)
                    .HasColumnName("chuongtrinh")
                    .HasMaxLength(70);

                entity.Property(e => e.Mact)
                    .HasColumnName("mact")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mota)
                    .HasColumnName("mota")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.Mact)
                    .HasName("PK_chuongtrinh");

                entity.Property(e => e.Mact)
                    .HasColumnName("mact")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Chuongtrinh)
                    .IsRequired()
                    .HasColumnName("chuongtrinh")
                    .HasMaxLength(50);

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(50);

                entity.Property(e => e.Mota)
                    .HasColumnName("mota")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Mact });

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mact)
                    .HasColumnName("mact")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.MactNavigation)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.Mact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUser_Application");
            });

            modelBuilder.Entity<Citys>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.Property(e => e.CityCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.NationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DanhGiaCamLao>(entity =>
            {
                entity.Property(e => e.CldvvaHdvtiengViet)
                    .HasColumnName("CLDVVaHDVTiengViet")
                    .HasMaxLength(150);

                entity.Property(e => e.DaCoKhaoSatThucTe).HasMaxLength(250);

                entity.Property(e => e.DongYduaVaoDsncu).HasColumnName("DongYDuaVaoDSNCU");

                entity.Property(e => e.GiaCa).HasMaxLength(150);

                entity.Property(e => e.KnngheNghiep)
                    .HasColumnName("KNNgheNghiep")
                    .HasMaxLength(250);

                entity.Property(e => e.KntaiThiTruongVn)
                    .HasColumnName("KNTaiThiTruongVN")
                    .HasMaxLength(150);

                entity.Property(e => e.Kqdat).HasColumnName("KQDat");

                entity.Property(e => e.KqkhaoSatThem).HasColumnName("KQKhaoSatThem");

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.MucDoHtxuLySuCo)
                    .HasColumnName("MucDoHTXuLySuCo")
                    .HasMaxLength(150);

                entity.Property(e => e.MucDoKipThoiTrongGd)
                    .HasColumnName("MucDoKipThoiTrongGD")
                    .HasMaxLength(150);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NlkhaiThacDvtaiDiaPhuong)
                    .HasColumnName("NLKhaiThacDVTaiDiaPhuong")
                    .HasMaxLength(150);

                entity.Property(e => e.SanPham).HasMaxLength(150);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .IsRequired()
                    .HasColumnName("TenNCU")
                    .HasMaxLength(250);

                entity.Property(e => e.TiemNang).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaCamLao)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaCamLao_supplier");
            });

            modelBuilder.Entity<DanhGiaCruise>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CangDonKhach).HasMaxLength(150);

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gpkd).HasColumnName("GPKD");

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.LoaiTau).HasMaxLength(150);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoLuongTauTqngay).HasColumnName("SoLuongTauTQNgay");

                entity.Property(e => e.SucChuaTauNguDem).HasMaxLength(150);

                entity.Property(e => e.SucChuaTauTqngay)
                    .HasColumnName("SucChuaTauTQNgay")
                    .HasMaxLength(150);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.TieuChuanSao).HasMaxLength(50);

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaCruise)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaCruise_supplier");
            });

            modelBuilder.Entity<DanhGiaDiemThamQuan>(entity =>
            {
                entity.Property(e => e.CoBaiDoXe).HasMaxLength(150);

                entity.Property(e => e.CoGpkd).HasColumnName("CoGPKD");

                entity.Property(e => e.CoHdvat).HasColumnName("CoHDVAT");

                entity.Property(e => e.CoNhaHang).HasMaxLength(50);

                entity.Property(e => e.CoNhaVeSinh).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DongYduaVaoDsncu).HasColumnName("DongYDuaVaoDSNCU");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.MucDoHapDan).HasMaxLength(50);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhuongTienPvvuiChoi)
                    .HasColumnName("PhuongTienPVVuiChoi")
                    .HasMaxLength(50);

                entity.Property(e => e.SucChuaToiDa).HasMaxLength(150);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.ThaiDoPvcuaNv)
                    .HasColumnName("ThaiDoPVCuaNV")
                    .HasMaxLength(50);

                entity.Property(e => e.ViTri).HasMaxLength(150);

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaDiemThamQuan)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaDiemThamQuan_supplier");
            });

            modelBuilder.Entity<DanhGiaGolf>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DienTichSanGolf)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gpkd).HasColumnName("GPKD");

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.MucGiaPhi).HasMaxLength(50);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.Property(e => e.ViTri).HasMaxLength(150);

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaGolf)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaGolf_supplier");
            });

            modelBuilder.Entity<DanhGiaKhachSan>(entity =>
            {
                entity.Property(e => e.CoBaiDoXe).HasMaxLength(150);

                entity.Property(e => e.CoBoTriPhongChoNb)
                    .HasColumnName("CoBoTriPhongChoNB")
                    .HasMaxLength(150);

                entity.Property(e => e.CoNhaHang).HasMaxLength(150);

                entity.Property(e => e.CoPhongHop).HasMaxLength(150);

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DongYduaVaoDsncu).HasColumnName("DongYDuaVaoDSNCU");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gpkd).HasColumnName("GPKD");

                entity.Property(e => e.KqKhaoSatThem).HasMaxLength(150);

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SucChuaToiDa).HasMaxLength(150);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.ThaiDoPvcuaNv)
                    .HasColumnName("ThaiDoPVCuaNV")
                    .HasMaxLength(150);

                entity.Property(e => e.TieuChuanSao).HasMaxLength(50);

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.Property(e => e.ViTri).HasMaxLength(150);

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaKhachSan)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaKhachSan_supplier");
            });

            modelBuilder.Entity<DanhGiaLandtour>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CldvvaHdv)
                    .HasColumnName("CLDVVaHDV")
                    .HasMaxLength(150);

                entity.Property(e => e.CoGpkd).HasColumnName("CoGPKD");

                entity.Property(e => e.CoHdvat).HasColumnName("CoHDVAT");

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DongYduaVaoDsncu).HasColumnName("DongYDuaVaoDSNCU");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiaCa).HasMaxLength(50);

                entity.Property(e => e.KinhNghiemThiTruongNd)
                    .HasColumnName("KinhNghiemThiTruongND")
                    .HasMaxLength(150);

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.MucDoHoTroXuLySuCo).HasMaxLength(50);

                entity.Property(e => e.MucDoKipThoiTrongGd)
                    .HasColumnName("MucDoKipThoiTrongGD")
                    .HasMaxLength(50);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NlkhaiThacDvdiaPhuong)
                    .HasColumnName("NLKhaiThacDVDiaPhuong")
                    .HasMaxLength(150);

                entity.Property(e => e.SanPham).HasMaxLength(50);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaLandtour)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaLandtour_supplier");
            });

            modelBuilder.Entity<DanhGiaNhaHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChatLuong).HasMaxLength(50);

                entity.Property(e => e.CoBaiDoXe).HasMaxLength(150);

                entity.Property(e => e.CoGpkd).HasColumnName("CoGPKD");

                entity.Property(e => e.CoHdvat).HasColumnName("CoHDVAT");

                entity.Property(e => e.CoPvmienPhiNoiBo)
                    .HasColumnName("CoPVMienPhiNoiBo")
                    .HasMaxLength(150);

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DinhLuong).HasMaxLength(50);

                entity.Property(e => e.DongYduaVaoDsncu).HasColumnName("DongYDuaVaoDSNCU");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NhaVeSinh).HasMaxLength(50);

                entity.Property(e => e.SucChuaToiDa).HasMaxLength(150);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.ThaiDoPvcuaNv)
                    .HasColumnName("ThaiDoPVCuaNV")
                    .HasMaxLength(50);

                entity.Property(e => e.ViTri).HasMaxLength(150);

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaNhaHang)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaNhaHang_supplier");
            });

            modelBuilder.Entity<DanhGiaVanChuyen>(entity =>
            {
                entity.Property(e => e.DanhSachDoiTac).HasMaxLength(250);

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoiXeCuNhatMoiNhat).HasMaxLength(150);

                entity.Property(e => e.DoiXeOrLoaiXe).HasMaxLength(150);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Gpkd).HasColumnName("GPKD");

                entity.Property(e => e.KhaNangHuyDong).HasMaxLength(150);

                entity.Property(e => e.KinhNghiem).HasMaxLength(50);

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.LoaiXeCoNhieuNhat).HasMaxLength(150);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiDanhGia).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhapNhan).HasMaxLength(150);

                entity.Property(e => e.SoXeChinhThuc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplierId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcu)
                    .HasColumnName("TenNCU")
                    .HasMaxLength(150);

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.Property(e => e.Website)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DanhGiaVanChuyen)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DanhGiaVanChuyen_supplier");
            });

            modelBuilder.Entity<DichVus>(entity =>
            {
                entity.HasKey(e => e.MaDv);

                entity.Property(e => e.MaDv)
                    .HasColumnName("MaDV")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(250);

                entity.Property(e => e.DienThoai).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GhiChu).HasMaxLength(250);

                entity.Property(e => e.GiaHd)
                    .HasColumnName("GiaHD")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LoaiDvid).HasColumnName("LoaiDVId");

                entity.Property(e => e.LoaiHd)
                    .HasColumnName("LoaiHD")
                    .HasMaxLength(50);

                entity.Property(e => e.LoaiSao).HasMaxLength(50);

                entity.Property(e => e.LoaiTau).HasMaxLength(150);

                entity.Property(e => e.LoaiXe).HasMaxLength(150);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NgayTrinhKy).HasColumnType("date");

                entity.Property(e => e.NguoiLienHe).HasMaxLength(150);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTrinhKy).HasMaxLength(150);

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenHd)
                    .HasColumnName("TenHD")
                    .HasMaxLength(150);

                entity.Property(e => e.ThoiGianHd)
                    .HasColumnName("ThoiGianHD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tuyen).HasMaxLength(250);

                entity.Property(e => e.Website).HasMaxLength(150);

                entity.HasOne(d => d.LoaiDv)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.LoaiDvid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DichVus_LoaiDVs");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DichVus)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DichVus_supplier");
            });

            modelBuilder.Entity<Dichvu>(entity =>
            {
                entity.HasKey(e => e.Iddichvu);

                entity.ToTable("dichvu");

                entity.Property(e => e.Iddichvu)
                    .HasColumnName("iddichvu")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tendv)
                    .HasColumnName("tendv")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Dmchinhanh>(entity =>
            {
                entity.ToTable("dmchinhanh");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(200);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Macn)
                    .IsRequired()
                    .HasColumnName("macn")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Masothue)
                    .HasColumnName("masothue")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tencn)
                    .HasColumnName("tencn")
                    .HasMaxLength(100);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Dmdaily>(entity =>
            {
                entity.ToTable("dmdaily");

                entity.Property(e => e.Daily)
                    .HasColumnName("daily")
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Macn)
                    .HasColumnName("macn")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tendaily)
                    .HasColumnName("tendaily")
                    .HasMaxLength(100);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.InnerMessage).HasMaxLength(300);

                entity.Property(e => e.MaCn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasMaxLength(300);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FolderUser>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Path)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HinhAnhs>(entity =>
            {
                entity.Property(e => e.DichVuId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.DichVu)
                    .WithMany(p => p.HinhAnhs)
                    .HasForeignKey(d => d.DichVuId)
                    .HasConstraintName("FK_HinhAnhs_DichVus");
            });

            modelBuilder.Entity<Httt>(entity =>
            {
                entity.HasKey(e => e.Idhttt);

                entity.ToTable("httt");

                entity.Property(e => e.Idhttt)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Diengiai).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiDvs>(entity =>
            {
                entity.ToTable("LoaiDVs");

                entity.Property(e => e.GhiChu).HasMaxLength(150);

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<LoginModel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LoginModel");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(200);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Doimk).HasColumnName("doimk");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(50);

                entity.Property(e => e.Macn)
                    .HasColumnName("macn")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Macode)
                    .HasColumnName("macode")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mact)
                    .IsRequired()
                    .HasColumnName("mact")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Maphong)
                    .HasColumnName("maphong")
                    .HasMaxLength(50);

                entity.Property(e => e.Masothue)
                    .HasColumnName("masothue")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaydoimk)
                    .HasColumnName("ngaydoimk")
                    .HasColumnType("date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tencn)
                    .HasColumnName("tencn")
                    .HasMaxLength(100);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mien>(entity =>
            {
                entity.Property(e => e.MienId)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TenMien).HasMaxLength(50);
            });

            modelBuilder.Entity<Nationals>(entity =>
            {
                entity.HasKey(e => e.NationCode);

                entity.Property(e => e.NationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Continent).HasMaxLength(50);

                entity.Property(e => e.NationName).HasMaxLength(50);

                entity.Property(e => e.Territory).HasMaxLength(50);
            });

            modelBuilder.Entity<Phongban>(entity =>
            {
                entity.HasKey(e => e.Maphong);

                entity.ToTable("phongban");

                entity.Property(e => e.Maphong)
                    .HasColumnName("maphong")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Hdvat).HasColumnName("hdvat");

                entity.Property(e => e.Macode)
                    .HasColumnName("macode")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tenphong)
                    .HasColumnName("tenphong")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<Quan>(entity =>
            {
                entity.ToTable("quan");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Tenquan)
                    .HasColumnName("tenquan")
                    .HasMaxLength(50);

                entity.Property(e => e.Tentp)
                    .HasColumnName("tentp")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Quocgia>(entity =>
            {
                entity.Property(e => e.TenNuoc).HasMaxLength(50);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_role");

                entity.ToTable("roles");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasColumnName("roleName")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_supplier_1");

                entity.ToTable("supplier");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi).HasMaxLength(250);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.LoaiSao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Masothue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nganhnghe).HasMaxLength(300);

                entity.Property(e => e.Ngayhethan).HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NguoiTrinhKyHd)
                    .HasColumnName("NguoiTrinhKyHD")
                    .HasMaxLength(150);

                entity.Property(e => e.Nguoilienhe).HasMaxLength(200);

                entity.Property(e => e.Nguoitao).HasMaxLength(50);

                entity.Property(e => e.NoiDungDongMo).HasMaxLength(250);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tapdoan).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(100);

                entity.Property(e => e.Tennganhang).HasMaxLength(50);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(100);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.ThoiGianDongMo)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Tinhtp).HasMaxLength(50);

                entity.Property(e => e.Tknganhang).HasMaxLength(50);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Website).HasMaxLength(200);

                entity.HasOne(d => d.TapDoan)
                    .WithMany(p => p.Supplier)
                    .HasForeignKey(d => d.TapDoanId)
                    .HasConstraintName("FK_supplier_TapDoan");
            });

            modelBuilder.Entity<Supplierob>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("supplierob");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi).HasMaxLength(250);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Masothue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nganhnghe).HasMaxLength(300);

                entity.Property(e => e.Ngayhethan).HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoilienhe).HasMaxLength(100);

                entity.Property(e => e.Nguoitao).HasMaxLength(50);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tapdoan).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(100);

                entity.Property(e => e.Tennganhang).HasMaxLength(50);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(100);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.Tinhtp).HasMaxLength(50);

                entity.Property(e => e.Tknganhang).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(150);
            });

            modelBuilder.Entity<TapDoan>(entity =>
            {
                entity.Property(e => e.Chuoi).HasMaxLength(250);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Thanhpho>(entity =>
            {
                entity.HasKey(e => e.Matp);

                entity.ToTable("thanhpho");

                entity.Property(e => e.Matp)
                    .HasColumnName("matp")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mien)
                    .HasColumnName("mien")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tentp)
                    .HasColumnName("tentp")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Thanhpho1>(entity =>
            {
                entity.HasKey(e => e.Matp)
                    .HasName("PK_thanhpho1_1");

                entity.ToTable("thanhpho1");

                entity.Property(e => e.Matp).HasMaxLength(6);

                entity.Property(e => e.Matinh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tentp).HasMaxLength(50);
            });

            modelBuilder.Entity<Tinh>(entity =>
            {
                entity.HasKey(e => e.Matinh);

                entity.Property(e => e.Matinh).HasMaxLength(3);

                entity.Property(e => e.MienId)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tentinh).HasMaxLength(50);

                entity.Property(e => e.VungId)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Doimk).HasColumnName("doimk");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(50);

                entity.Property(e => e.Macn)
                    .HasColumnName("macn")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Maphong)
                    .HasColumnName("maphong")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaydoimk)
                    .HasColumnName("ngaydoimk")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<VPhongban>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPhongban");

                entity.Property(e => e.Maphong)
                    .IsRequired()
                    .HasColumnName("maphong")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tenphong)
                    .HasColumnName("tenphong")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VSupplierob>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vSupplierob");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Tengiaodich)
                    .HasColumnName("tengiaodich")
                    .HasMaxLength(403);

                entity.Property(e => e.Tour)
                    .IsRequired()
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VSupplierob1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vSupplierob1");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tengiaodich)
                    .HasColumnName("tengiaodich")
                    .HasMaxLength(166);

                entity.Property(e => e.Tour)
                    .IsRequired()
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VTinh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTinh");

                entity.Property(e => e.Matinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Mien)
                    .HasColumnName("mien")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TenMien).HasMaxLength(50);

                entity.Property(e => e.TenVung).HasMaxLength(50);

                entity.Property(e => e.Tentinh).HasMaxLength(50);

                entity.Property(e => e.VungId)
                    .IsRequired()
                    .HasColumnName("vungId")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VUserHoadon>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserHoadon");

                entity.Property(e => e.Accounthddt)
                    .HasColumnName("accounthddt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Kyhieuhd)
                    .IsRequired()
                    .HasColumnName("kyhieuhd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mausohd)
                    .IsRequired()
                    .HasColumnName("mausohd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Maviettat)
                    .HasColumnName("maviettat")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Passwordhddt)
                    .HasColumnName("passwordhddt")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vungmien>(entity =>
            {
                entity.HasKey(e => e.VungId);

                entity.ToTable("vungmien");

                entity.Property(e => e.VungId)
                    .HasColumnName("vungId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Mien)
                    .HasColumnName("mien")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TenVung).HasMaxLength(50);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}