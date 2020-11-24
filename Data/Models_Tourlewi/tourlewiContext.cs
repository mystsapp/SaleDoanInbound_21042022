using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models_Tourlewi
{
    public partial class tourlewiContext : DbContext
    {
        public tourlewiContext()
        {
        }

        public tourlewiContext(DbContextOptions<tourlewiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Changbay> Changbay { get; set; }
        public virtual DbSet<ChangbayLog> ChangbayLog { get; set; }
        public virtual DbSet<Chinhanh> Chinhanh { get; set; }
        public virtual DbSet<ChinhanhFolder> ChinhanhFolder { get; set; }
        public virtual DbSet<Chiphi> Chiphi { get; set; }
        public virtual DbSet<ChiphiLog> ChiphiLog { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Ctdatcoc> Ctdatcoc { get; set; }
        public virtual DbSet<CtdatcocLog> CtdatcocLog { get; set; }
        public virtual DbSet<Datcoc> Datcoc { get; set; }
        public virtual DbSet<DatcocLog> DatcocLog { get; set; }
        public virtual DbSet<Dichvu> Dichvu { get; set; }
        public virtual DbSet<Diemthamquan> Diemthamquan { get; set; }
        public virtual DbSet<Diemtqob> Diemtqob { get; set; }
        public virtual DbSet<Dmchinhanh> Dmchinhanh { get; set; }
        public virtual DbSet<Dmdaily> Dmdaily { get; set; }
        public virtual DbSet<Dmhuongdan> Dmhuongdan { get; set; }
        public virtual DbSet<Dmkhachhang> Dmkhachhang { get; set; }
        public virtual DbSet<Dmphong> Dmphong { get; set; }
        public virtual DbSet<DoanhthuQuay> DoanhthuQuay { get; set; }
        public virtual DbSet<DoanhthuSale> DoanhthuSale { get; set; }
        public virtual DbSet<Doitac> Doitac { get; set; }
        public virtual DbSet<Hangkhong> Hangkhong { get; set; }
        public virtual DbSet<Huongdan> Huongdan { get; set; }
        public virtual DbSet<HuongdanLog> HuongdanLog { get; set; }
        public virtual DbSet<Khachbaohiem> Khachbaohiem { get; set; }
        public virtual DbSet<Khachdoan> Khachdoan { get; set; }
        public virtual DbSet<KhachdoanLog> KhachdoanLog { get; set; }
        public virtual DbSet<Khachhang> Khachhang { get; set; }
        public virtual DbSet<Khachsan> Khachsan { get; set; }
        public virtual DbSet<KhachsanLog> KhachsanLog { get; set; }
        public virtual DbSet<Khachvetour> Khachvetour { get; set; }
        public virtual DbSet<KhachvetourLog> KhachvetourLog { get; set; }
        public virtual DbSet<Khachwaiting> Khachwaiting { get; set; }
        public virtual DbSet<Khachxe> Khachxe { get; set; }
        public virtual DbSet<Landtour> Landtour { get; set; }
        public virtual DbSet<LandtourLog> LandtourLog { get; set; }
        public virtual DbSet<Loaikhachsan> Loaikhachsan { get; set; }
        public virtual DbSet<Loaiphong> Loaiphong { get; set; }
        public virtual DbSet<Loaitour> Loaitour { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuType> MenuType { get; set; }
        public virtual DbSet<Nuoc> Nuoc { get; set; }
        public virtual DbSet<Phieuthamquan> Phieuthamquan { get; set; }
        public virtual DbSet<Quan> Quan { get; set; }
        public virtual DbSet<Rmlist> Rmlist { get; set; }
        public virtual DbSet<RmlistDetail> RmlistDetail { get; set; }
        public virtual DbSet<RmlistDetailLog> RmlistDetailLog { get; set; }
        public virtual DbSet<RmlistLog> RmlistLog { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Thanhpho> Thanhpho { get; set; }
        public virtual DbSet<Thecoupon> Thecoupon { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<TourLog> TourLog { get; set; }
        public virtual DbSet<Tourkhachdoan> Tourkhachdoan { get; set; }
        public virtual DbSet<Tuyendiem> Tuyendiem { get; set; }
        public virtual DbSet<Tuyentqob> Tuyentqob { get; set; }
        public virtual DbSet<Userfolder> Userfolder { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VChinhanh> VChinhanh { get; set; }
        public virtual DbSet<VDmtaixe> VDmtaixe { get; set; }
        public virtual DbSet<VDoanhthu> VDoanhthu { get; set; }
        public virtual DbSet<VDoitacChiphi> VDoitacChiphi { get; set; }
        public virtual DbSet<VKhachtourTour> VKhachtourTour { get; set; }
        public virtual DbSet<VLoaixe> VLoaixe { get; set; }
        public virtual DbSet<VThongtinHuongdan> VThongtinHuongdan { get; set; }
        public virtual DbSet<VTimVetourDatcoc> VTimVetourDatcoc { get; set; }
        public virtual DbSet<VTimdatcoc> VTimdatcoc { get; set; }
        public virtual DbSet<VTimdatcoc1> VTimdatcoc1 { get; set; }
        public virtual DbSet<VTimkhach> VTimkhach { get; set; }
        public virtual DbSet<VTimkhachBySales> VTimkhachBySales { get; set; }
        public virtual DbSet<VTimvetour> VTimvetour { get; set; }
        public virtual DbSet<VTourKhachhang> VTourKhachhang { get; set; }
        public virtual DbSet<VUser> VUser { get; set; }
        public virtual DbSet<VVetourDhtour> VVetourDhtour { get; set; }
        public virtual DbSet<Vanchuyen> Vanchuyen { get; set; }
        public virtual DbSet<Vethamquan> Vethamquan { get; set; }
        public virtual DbSet<Vetour> Vetour { get; set; }
        public virtual DbSet<VetourLog> VetourLog { get; set; }
        public virtual DbSet<View1> View1 { get; set; }
        public virtual DbSet<Yeucauxe> Yeucauxe { get; set; }
        public virtual DbSet<YeucauxeLog> YeucauxeLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=118.68.170.128;Database=tourlewi;Trusted_Connection=True;User Id=vanhong;Password=Hong@2019;Integrated Security=false;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Changbay>(entity =>
            {
                entity.HasKey(e => new { e.Sgtcode, e.Order })
                    .HasName("PK_changbay_1");

                entity.ToTable("changbay");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Choconlai).HasColumnName("choconlai");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Giaveeb)
                    .HasColumnName("giaveeb")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Giavenl)
                    .HasColumnName("giavenl")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Giavete)
                    .HasColumnName("giavete")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hanhtrinh)
                    .HasColumnName("hanhtrinh")
                    .HasMaxLength(200);

                entity.Property(e => e.Socho)
                    .HasColumnName("socho")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ChangbayLog>(entity =>
            {
                entity.ToTable("changbay_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Giaveeb)
                    .HasColumnName("giaveeb")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giavenl)
                    .HasColumnName("giavenl")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giavete)
                    .HasColumnName("giavete")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Hanhtrinh)
                    .HasColumnName("hanhtrinh")
                    .HasMaxLength(200);

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50);

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17);

                entity.Property(e => e.Socho).HasColumnName("socho");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Chinhanh>(entity =>
            {
                entity.ToTable("chinhanh");

                entity.Property(e => e.Chinhanh1)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Masothue)
                    .HasColumnName("masothue")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tencn)
                    .HasColumnName("tencn")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(70);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ChinhanhFolder>(entity =>
            {
                entity.HasKey(e => e.Chinhanh);

                entity.ToTable("chinhanhFolder");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DirPathName)
                    .HasColumnName("dirPathName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DomainNm)
                    .HasColumnName("domain_nm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlagUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Chiphi>(entity =>
            {
                entity.ToTable("chiphi");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Del)
                    .HasColumnName("del")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(200);

                entity.Property(e => e.Iddv)
                    .HasColumnName("iddv")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mancc)
                    .HasColumnName("mancc")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaynhap)
                    .HasColumnName("ngaynhap")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngoaite)
                    .HasColumnName("ngoaite")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Nguoinhap)
                    .HasColumnName("nguoinhap")
                    .HasMaxLength(50);

                entity.Property(e => e.Noidung)
                    .HasColumnName("noidung")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sokhach).HasColumnName("sokhach");

                entity.Property(e => e.Tenncc)
                    .HasColumnName("tenncc")
                    .HasMaxLength(50);

                entity.Property(e => e.Tienmat).HasColumnName("tienmat");

                entity.Property(e => e.Tienvnd)
                    .HasColumnName("tienvnd")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tigia)
                    .HasColumnName("tigia")
                    .HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<ChiphiLog>(entity =>
            {
                entity.ToTable("chiphi_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(200);

                entity.Property(e => e.Iddv)
                    .HasColumnName("iddv")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mancc)
                    .HasColumnName("mancc")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaynhap)
                    .HasColumnName("ngaynhap")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngoaite)
                    .HasColumnName("ngoaite")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Nguoinhap)
                    .HasColumnName("nguoinhap")
                    .HasMaxLength(50);

                entity.Property(e => e.Noidung)
                    .HasColumnName("noidung")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sokhach).HasColumnName("sokhach");

                entity.Property(e => e.Tenncc)
                    .HasColumnName("tenncc")
                    .HasMaxLength(50);

                entity.Property(e => e.Tienmat).HasColumnName("tienmat");

                entity.Property(e => e.Tienvnd)
                    .HasColumnName("tienvnd")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tigia)
                    .HasColumnName("tigia")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("company");

                entity.Property(e => e.Add)
                    .HasColumnName("add")
                    .HasMaxLength(80);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(40);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.Nation)
                    .HasColumnName("nation")
                    .HasMaxLength(50);

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Ctdatcoc>(entity =>
            {
                entity.HasKey(e => e.Idctdatcoc);

                entity.ToTable("ctdatcoc");

                entity.Property(e => e.Idctdatcoc)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chungtugoc)
                    .HasColumnName("chungtugoc")
                    .HasMaxLength(20);

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(30);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(50);

                entity.Property(e => e.Iddatcoc).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Idvetour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(25);

                entity.Property(e => e.Sotienct)
                    .HasColumnName("sotienct")
                    .HasColumnType("decimal(12, 0)");
            });

            modelBuilder.Entity<CtdatcocLog>(entity =>
            {
                entity.ToTable("ctdatcoc_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chungtugoc)
                    .HasColumnName("chungtugoc")
                    .HasMaxLength(20);

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(50);

                entity.Property(e => e.Iddatcoc).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Idvetour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(50);

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Datcoc>(entity =>
            {
                entity.HasKey(e => e.Iddatcoc);

                entity.ToTable("datcoc");

                entity.Property(e => e.Iddatcoc)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(30);

                entity.Property(e => e.Daily)
                    .HasColumnName("daily")
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Idvetour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Lydohuydc)
                    .HasColumnName("lydohuydc")
                    .HasMaxLength(100);

                entity.Property(e => e.Ngaydatcoc)
                    .HasColumnName("ngaydatcoc")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnName("ngayhuy")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaythu)
                    .HasColumnName("ngaythu")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoihuy)
                    .HasColumnName("nguoihuy")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoilambn)
                    .HasColumnName("nguoilambn")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoithu)
                    .HasColumnName("nguoithu")
                    .HasMaxLength(50);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DatcocLog>(entity =>
            {
                entity.ToTable("datcoc_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(30);

                entity.Property(e => e.Daily)
                    .HasColumnName("daily")
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idvetour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnName("ngaycapnhat")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaydatcoc)
                    .HasColumnName("ngaydatcoc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaythu)
                    .HasColumnName("ngaythu")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoilambn)
                    .HasColumnName("nguoilambn")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoithu)
                    .HasColumnName("nguoithu")
                    .HasMaxLength(50);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Dichvu>(entity =>
            {
                entity.HasKey(e => e.Iddv);

                entity.ToTable("dichvu");

                entity.Property(e => e.Iddv)
                    .HasColumnName("iddv")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tendv)
                    .HasColumnName("tendv")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Diemthamquan>(entity =>
            {
                entity.HasKey(e => e.Matq);

                entity.ToTable("diemthamquan");

                entity.Property(e => e.Matq)
                    .HasColumnName("matq")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diemtq)
                    .HasColumnName("diemtq")
                    .HasMaxLength(70);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Diemtqob>(entity =>
            {
                entity.ToTable("diemtqob");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Diemtq)
                    .IsRequired()
                    .HasColumnName("diemtq")
                    .HasMaxLength(100);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17);
            });

            modelBuilder.Entity<Dmchinhanh>(entity =>
            {
                entity.HasKey(e => e.Chinhanh);

                entity.ToTable("dmchinhanh");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dmdaily>(entity =>
            {
                entity.ToTable("dmdaily");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3);

                entity.Property(e => e.Daily)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi).HasMaxLength(100);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.TenDaily).HasMaxLength(100);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Dmhuongdan>(entity =>
            {
                entity.HasKey(e => e.Mahd);

                entity.ToTable("dmhuongdan");

                entity.Property(e => e.Mahd)
                    .HasColumnName("mahd")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Chucdanh)
                    .HasColumnName("chucdanh")
                    .HasMaxLength(50);

                entity.Property(e => e.Diachitamtru)
                    .HasColumnName("diachitamtru")
                    .HasMaxLength(100);

                entity.Property(e => e.Diachithuongtru)
                    .HasColumnName("diachithuongtru")
                    .HasMaxLength(70);

                entity.Property(e => e.Dienthoaidd)
                    .HasColumnName("dienthoaidd")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dienthoainha)
                    .HasColumnName("dienthoainha")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Dtquanhe)
                    .HasColumnName("dtquanhe")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichuvisa)
                    .HasColumnName("ghichuvisa")
                    .HasMaxLength(150);

                entity.Property(e => e.Hanthehdv)
                    .HasColumnName("hanthehdv")
                    .HasColumnType("date");

                entity.Property(e => e.Hanvisa)
                    .HasColumnName("hanvisa")
                    .HasColumnType("date");

                entity.Property(e => e.He1)
                    .HasColumnName("he1")
                    .HasMaxLength(50);

                entity.Property(e => e.He2)
                    .HasColumnName("he2")
                    .HasMaxLength(50);

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Honnhan).HasColumnName("honnhan");

                entity.Property(e => e.Lylich).HasColumnName("lylich");

                entity.Property(e => e.Nam1)
                    .HasColumnName("nam1")
                    .HasMaxLength(10);

                entity.Property(e => e.Nam2)
                    .HasColumnName("nam2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nganh1)
                    .HasColumnName("nganh1")
                    .HasMaxLength(50);

                entity.Property(e => e.Nganh2)
                    .HasColumnName("nganh2")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaycmnd)
                    .HasColumnName("ngaycmnd")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Ngoaingu)
                    .HasColumnName("ngoaingu")
                    .HasMaxLength(50);

                entity.Property(e => e.Noicapcmnd)
                    .HasColumnName("noicapcmnd")
                    .HasMaxLength(50);

                entity.Property(e => e.Noisinh)
                    .HasColumnName("noisinh")
                    .HasMaxLength(50);

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(50);

                entity.Property(e => e.Socmnd)
                    .HasColumnName("socmnd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sothehdv)
                    .HasColumnName("sothehdv")
                    .HasMaxLength(50);

                entity.Property(e => e.Tenhd)
                    .IsRequired()
                    .HasColumnName("tenhd")
                    .HasMaxLength(50);

                entity.Property(e => e.Tenthannhan)
                    .HasColumnName("tenthannhan")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Trinhdo)
                    .HasColumnName("trinhdo")
                    .HasMaxLength(50);

                entity.Property(e => e.Truong1)
                    .HasColumnName("truong1")
                    .HasMaxLength(50);

                entity.Property(e => e.Truong2)
                    .HasColumnName("truong2")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dmkhachhang>(entity =>
            {
                entity.HasKey(e => e.Makh);

                entity.ToTable("dmkhachhang");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3);

                entity.Property(e => e.Codecn)
                    .HasColumnName("codecn")
                    .HasMaxLength(20);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(80);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(40);

                entity.Property(e => e.Ghichukhigiaodich).HasColumnName("ghichukhigiaodich");

                entity.Property(e => e.Lsthamgiatour).HasColumnName("lsthamgiatour");

                entity.Property(e => e.Nganhnghe)
                    .HasColumnName("nganhnghe")
                    .HasMaxLength(150);

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoiqh)
                    .HasColumnName("nguoiqh")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Quocgia)
                    .HasColumnName("quocgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Tax)
                    .HasColumnName("tax")
                    .HasMaxLength(40);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(40);

                entity.Property(e => e.Tengiaodich)
                    .HasColumnName("tengiaodich")
                    .HasMaxLength(70);

                entity.Property(e => e.Tenthuongmai)
                    .HasColumnName("tenthuongmai")
                    .HasMaxLength(70);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dmphong>(entity =>
            {
                entity.HasKey(e => e.Phongban);

                entity.ToTable("dmphong");

                entity.Property(e => e.Phongban)
                    .HasColumnName("phongban")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tenphongban)
                    .HasColumnName("tenphongban")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DoanhthuQuay>(entity =>
            {
                entity.HasKey(e => e.Stt);

                entity.ToTable("doanhthuQuay");

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .ValueGeneratedNever();

                entity.Property(e => e.Giave)
                    .HasColumnName("giave")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Hanhtrinh)
                    .IsRequired()
                    .HasColumnName("hanhtrinh")
                    .HasMaxLength(150);

                entity.Property(e => e.Ngaydi)
                    .HasColumnName("ngaydi")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayve)
                    .HasColumnName("ngayve")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nguoiban)
                    .HasColumnName("nguoiban")
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DoanhthuSale>(entity =>
            {
                entity.HasKey(e => e.Stt);

                entity.ToTable("doanhthuSale");

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .ValueGeneratedNever();

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Doanhthu)
                    .HasColumnName("doanhthu")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Thucthu)
                    .HasColumnName("thucthu")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vetourid).HasColumnName("vetourid");
            });

            modelBuilder.Entity<Doitac>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("doitac");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CodeCn)
                    .HasColumnName("codeCN")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(150);

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dienthoailh)
                    .HasColumnName("dienthoailh")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Masothue)
                    .HasColumnName("masothue")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoilienhe)
                    .HasColumnName("nguoilienhe")
                    .HasMaxLength(50);

                entity.Property(e => e.Quocgia)
                    .HasColumnName("quocgia")
                    .HasMaxLength(30);

                entity.Property(e => e.Tenct)
                    .HasColumnName("tenct")
                    .HasMaxLength(70);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<Hangkhong>(entity =>
            {
                entity.ToTable("hangkhong");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Chococ1).HasColumnName("chococ1");

                entity.Property(e => e.Chococ2).HasColumnName("chococ2");

                entity.Property(e => e.Chococ3).HasColumnName("chococ3");

                entity.Property(e => e.Codebooking)
                    .HasColumnName("codebooking")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(300);

                entity.Property(e => e.Hanhtrinh)
                    .IsRequired()
                    .HasColumnName("hanhtrinh")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iddv)
                    .HasColumnName("iddv")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Loaibooking)
                    .HasColumnName("loaibooking")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfilehk).HasColumnName("logfilehk");

                entity.Property(e => e.Mancc)
                    .HasColumnName("mancc")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sochoxuatve).HasColumnName("sochoxuatve");

                entity.Property(e => e.Tenncc)
                    .HasColumnName("tenncc")
                    .HasMaxLength(50);

                entity.Property(e => e.Tiencoc1)
                    .HasColumnName("tiencoc1")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tiencoc2)
                    .HasColumnName("tiencoc2")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tiencoc3)
                    .HasColumnName("tiencoc3")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tiencochoan)
                    .HasColumnName("tiencochoan")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tiencocphat)
                    .HasColumnName("tiencocphat")
                    .HasColumnType("decimal(12, 0)");
            });

            modelBuilder.Entity<Huongdan>(entity =>
            {
                entity.HasKey(e => new { e.Sgtcode, e.Stt });

                entity.ToTable("huongdan");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(80);

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Phai)
                    .IsRequired()
                    .HasColumnName("phai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Phongks)
                    .HasColumnName("phongks")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(30);

                entity.Property(e => e.Tenhd)
                    .HasColumnName("tenhd")
                    .HasMaxLength(50);

                entity.Property(e => e.Vemaybay)
                    .IsRequired()
                    .HasColumnName("vemaybay")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<HuongdanLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("huongdan_log");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(80);

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(15);

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Loaicapnhat)
                    .HasColumnName("loaicapnhat")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnName("ngaycapnhat")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(50);

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Phongks)
                    .HasColumnName("phongks")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Tenhd)
                    .HasColumnName("tenhd")
                    .HasMaxLength(50);

                entity.Property(e => e.Vemaybay).HasColumnName("vemaybay");
            });

            modelBuilder.Entity<Khachbaohiem>(entity =>
            {
                entity.HasKey(e => e.Stt);

                entity.ToTable("khachbaohiem");

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .ValueGeneratedNever();

                entity.Property(e => e.Diemdon)
                    .IsRequired()
                    .HasColumnName("diemdon")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(150);

                entity.Property(e => e.Giave)
                    .HasColumnName("giave")
                    .HasMaxLength(50);

                entity.Property(e => e.Gioitinh)
                    .HasColumnName("gioitinh")
                    .HasMaxLength(50);

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(50);

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Vetourid).HasColumnName("vetourid");
            });

            modelBuilder.Entity<Khachdoan>(entity =>
            {
                entity.HasKey(e => e.Sgtcode)
                    .HasName("PK_tourkhachdoan");

                entity.ToTable("khachdoan");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Damphan)
                    .HasColumnName("damphan")
                    .HasColumnType("date");

                entity.Property(e => e.Datcocvmb)
                    .HasColumnName("datcocvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(250);

                entity.Property(e => e.Doitacnn)
                    .HasColumnName("doitacnn")
                    .HasMaxLength(50);

                entity.Property(e => e.Dtdukien)
                    .HasColumnName("dtdukien")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dtgiaodich)
                    .HasColumnName("dtgiaodich")
                    .HasMaxLength(50);

                entity.Property(e => e.Dtthucte)
                    .HasColumnName("dtthucte")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Giaodich)
                    .HasColumnName("giaodich")
                    .HasMaxLength(50);

                entity.Property(e => e.Hangkhong)
                    .HasColumnName("hangkhong")
                    .HasMaxLength(250);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasColumnType("date");

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhcdv)
                    .HasColumnName("ngayhcdv")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhctlhopdong)
                    .HasColumnName("ngayhctlhopdong")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhctour)
                    .HasColumnName("ngayhctour")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoikyhd)
                    .HasColumnName("nguoikyhd")
                    .HasMaxLength(40);

                entity.Property(e => e.Sales)
                    .HasColumnName("sales")
                    .HasMaxLength(50);

                entity.Property(e => e.Skdukien).HasColumnName("skdukien");

                entity.Property(e => e.Skthucte)
                    .HasColumnName("skthucte")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sohopdong)
                    .HasColumnName("sohopdong")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Thanhlyhd)
                    .HasColumnName("thanhlyhd")
                    .HasColumnType("date");

                entity.Property(e => e.Thongtin)
                    .HasColumnName("thongtin")
                    .HasMaxLength(250);

                entity.Property(e => e.Tlhopdong)
                    .HasColumnName("tlhopdong")
                    .HasMaxLength(250);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<KhachdoanLog>(entity =>
            {
                entity.ToTable("khachdoan_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Damphan)
                    .HasColumnName("damphan")
                    .HasColumnType("date");

                entity.Property(e => e.Datcocvmb)
                    .HasColumnName("datcocvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(250);

                entity.Property(e => e.Doitacnn)
                    .HasColumnName("doitacnn")
                    .HasMaxLength(50);

                entity.Property(e => e.Dtdukien)
                    .HasColumnName("dtdukien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dtgiaodich)
                    .HasColumnName("dtgiaodich")
                    .HasMaxLength(50);

                entity.Property(e => e.Dtthucte)
                    .HasColumnName("dtthucte")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giaodich)
                    .HasColumnName("giaodich")
                    .HasMaxLength(50);

                entity.Property(e => e.Hangkhong)
                    .HasColumnName("hangkhong")
                    .HasMaxLength(250);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasColumnType("date");

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhcdv)
                    .HasColumnName("ngayhcdv")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhctlhopdong)
                    .HasColumnName("ngayhctlhopdong")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhctour)
                    .HasColumnName("ngayhctour")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoikyhd)
                    .HasColumnName("nguoikyhd")
                    .HasMaxLength(40);

                entity.Property(e => e.Sales)
                    .HasColumnName("sales")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Skdukien).HasColumnName("skdukien");

                entity.Property(e => e.Skthucte).HasColumnName("skthucte");

                entity.Property(e => e.Sohopdong)
                    .HasColumnName("sohopdong")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Thanhlyhd)
                    .HasColumnName("thanhlyhd")
                    .HasColumnType("date");

                entity.Property(e => e.Thongtin)
                    .HasColumnName("thongtin")
                    .HasMaxLength(250);

                entity.Property(e => e.Tlhopdong)
                    .HasColumnName("tlhopdong")
                    .HasMaxLength(250);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(250);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.Makh);

                entity.ToTable("khachhang");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(70);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycaphc)
                    .HasColumnName("ngaycaphc")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Khachsan>(entity =>
            {
                entity.HasKey(e => e.Idks)
                    .HasName("PK_khachsan_1");

                entity.ToTable("khachsan");

                entity.Property(e => e.Idks)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Checkin)
                    .HasColumnName("checkin")
                    .HasColumnType("date");

                entity.Property(e => e.Checkout)
                    .HasColumnName("checkout")
                    .HasColumnType("date");

                entity.Property(e => e.Dieuhanh)
                    .HasColumnName("dieuhanh")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Loaigia)
                    .HasColumnName("loaigia")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Maks)
                    .HasColumnName("maks")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sophong)
                    .HasColumnName("sophong")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenks)
                    .HasColumnName("tenks")
                    .HasMaxLength(50);

                entity.Property(e => e.Tinhtp)
                    .HasColumnName("tinhtp")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<KhachsanLog>(entity =>
            {
                entity.ToTable("khachsan_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Checkin)
                    .HasColumnName("checkin")
                    .HasColumnType("date");

                entity.Property(e => e.Checkout)
                    .HasColumnName("checkout")
                    .HasColumnType("date");

                entity.Property(e => e.Dieuhanh)
                    .HasColumnName("dieuhanh")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Loaigia)
                    .HasColumnName("loaigia")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Maks)
                    .HasColumnName("maks")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sophong).HasColumnName("sophong");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenks)
                    .HasColumnName("tenks")
                    .HasMaxLength(40);

                entity.Property(e => e.Tinhtp)
                    .HasColumnName("tinhtp")
                    .HasMaxLength(30);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Khachvetour>(entity =>
            {
                entity.HasKey(e => e.Idkhach)
                    .HasName("PK_khachvetour_1");

                entity.ToTable("khachvetour");

                entity.Property(e => e.Idkhach)
                    .HasColumnName("idkhach")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Airticket)
                    .HasColumnName("airticket")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Baohiem)
                    .HasColumnName("baohiem")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("(host_name())");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Doanhthunn)
                    .HasColumnName("doanhthunn")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Dotuoi)
                    .HasColumnName("dotuoi")
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('ADL')");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ghichubaohiem)
                    .HasColumnName("ghichubaohiem")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichudtnn)
                    .HasColumnName("ghichudtnn")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichudvk)
                    .IsRequired()
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ghichugg)
                    .HasColumnName("ghichugg")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ghichushow)
                    .HasColumnName("ghichushow")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichuvisa)
                    .HasColumnName("ghichuvisa")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ghichuvmb)
                    .HasColumnName("ghichuvmb")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Gioitinh)
                    .HasColumnName("gioitinh")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Hanhtrinh)
                    .HasColumnName("hanhtrinh")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Hanxuatvmb)
                    .HasColumnName("hanxuatvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Landtour)
                    .HasColumnName("landtour")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Loaikhach)
                    .HasColumnName("loaikhach")
                    .HasMaxLength(2);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ngaycaphc)
                    .HasColumnName("ngaycaphc")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngayxuatvmb)
                    .HasColumnName("ngayxuatvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Noixuatvmb)
                    .HasColumnName("noixuatvmb")
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phongks)
                    .HasColumnName("phongks")
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Prn)
                    .HasColumnName("prn")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Quan)
                    .IsRequired()
                    .HasColumnName("quan")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Show)
                    .HasColumnName("show")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Thanhpho)
                    .IsRequired()
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Vanchuyen)
                    .HasColumnName("vanchuyen")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<KhachvetourLog>(entity =>
            {
                entity.ToTable("khachvetour_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Airticket)
                    .HasColumnName("airticket")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Baohiem)
                    .HasColumnName("baohiem")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chiemcho)
                    .IsRequired()
                    .HasColumnName("chiemcho")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(70);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Doanhthunn)
                    .HasColumnName("doanhthunn")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dotuoi)
                    .HasColumnName("dotuoi")
                    .HasMaxLength(3);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(250);

                entity.Property(e => e.Ghichubaohiem)
                    .HasColumnName("ghichubaohiem")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichudvk)
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichugg)
                    .HasColumnName("ghichugg")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichuvisa)
                    .HasColumnName("ghichuvisa")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvmb)
                    .HasColumnName("ghichuvmb")
                    .HasMaxLength(100);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Gioitinh)
                    .HasColumnName("gioitinh")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Hanhtrinh)
                    .HasColumnName("hanhtrinh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hanxuatvmb)
                    .HasColumnName("hanxuatvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(20);

                entity.Property(e => e.Landtour)
                    .HasColumnName("landtour")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Loaikhach)
                    .HasColumnName("loaikhach")
                    .HasMaxLength(2);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycaphc)
                    .HasColumnName("ngaycaphc")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(25);

                entity.Property(e => e.Noixuatvmb)
                    .HasColumnName("noixuatvmb")
                    .HasMaxLength(25);

                entity.Property(e => e.Phongks)
                    .HasColumnName("phongks")
                    .HasMaxLength(3);

                entity.Property(e => e.Prn)
                    .HasColumnName("prn")
                    .HasMaxLength(20);

                entity.Property(e => e.Quan)
                    .HasColumnName("quan")
                    .HasMaxLength(50);

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(30);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Vanchuyen)
                    .HasColumnName("vanchuyen")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<Khachwaiting>(entity =>
            {
                entity.HasKey(e => e.Idwaiting);

                entity.ToTable("khachwaiting");

                entity.Property(e => e.Idwaiting)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Baohiem)
                    .HasColumnName("baohiem")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(50);

                entity.Property(e => e.Dailytao)
                    .HasColumnName("dailytao")
                    .HasMaxLength(50);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Dotuoi)
                    .HasColumnName("dotuoi")
                    .HasMaxLength(3);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Emailnv)
                    .HasColumnName("emailnv")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(250);

                entity.Property(e => e.Ghichubaohiem)
                    .HasColumnName("ghichubaohiem")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichudvk)
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichugg)
                    .HasColumnName("ghichugg")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvisa)
                    .HasColumnName("ghichuvisa")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvmb)
                    .HasColumnName("ghichuvmb")
                    .HasMaxLength(100);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.Hanhtrinh).HasColumnName("hanhtrinh");

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(20);

                entity.Property(e => e.Landtour)
                    .HasColumnName("landtour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Loaikhach)
                    .HasColumnName("loaikhach")
                    .HasMaxLength(2);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycaphc)
                    .HasColumnName("ngaycaphc")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50);

                entity.Property(e => e.Phongks)
                    .HasColumnName("phongks")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Quan)
                    .HasColumnName("quan")
                    .HasMaxLength(50);

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(30);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<Khachxe>(entity =>
            {
                entity.HasKey(e => e.IdPhanxe);

                entity.ToTable("khachxe");

                entity.Property(e => e.IdPhanxe)
                    .HasColumnName("idPhanxe")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sttkhach).HasColumnName("sttkhach");

                entity.Property(e => e.Sttxe).HasColumnName("sttxe");
            });

            modelBuilder.Entity<Landtour>(entity =>
            {
                entity.HasKey(e => new { e.Sgtcode, e.Order });

                entity.ToTable("landtour");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Choconlai).HasColumnName("choconlai");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(1000);

                entity.Property(e => e.Giaveeb)
                    .HasColumnName("giaveeb")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giavenl)
                    .HasColumnName("giavenl")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giavete)
                    .HasColumnName("giavete")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Hanhtrinh)
                    .HasColumnName("hanhtrinh")
                    .HasMaxLength(200);

                entity.Property(e => e.Loai1)
                    .HasColumnName("loai1")
                    .HasMaxLength(50);

                entity.Property(e => e.Loai2)
                    .HasColumnName("loai2")
                    .HasMaxLength(50);

                entity.Property(e => e.Phongconlai)
                    .HasColumnName("phongconlai")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Socho).HasColumnName("socho");

                entity.Property(e => e.Sophong).HasColumnName("sophong");
            });

            modelBuilder.Entity<LandtourLog>(entity =>
            {
                entity.ToTable("landtour_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Choconlai).HasColumnName("choconlai");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(1000);

                entity.Property(e => e.Giaveeb)
                    .HasColumnName("giaveeb")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giavenl)
                    .HasColumnName("giavenl")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giavete)
                    .HasColumnName("giavete")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Hanhtrinh)
                    .HasColumnName("hanhtrinh")
                    .HasMaxLength(200);

                entity.Property(e => e.Loai1)
                    .HasColumnName("loai1")
                    .HasMaxLength(50);

                entity.Property(e => e.Loai2)
                    .HasColumnName("loai2")
                    .HasMaxLength(50);

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50);

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Phongconlai).HasColumnName("phongconlai");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Socho).HasColumnName("socho");

                entity.Property(e => e.Sophong).HasColumnName("sophong");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Loaikhachsan>(entity =>
            {
                entity.ToTable("loaikhachsan");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(50);

                entity.Property(e => e.Giatour)
                    .HasColumnName("giatour")
                    .HasColumnType("decimal(14, 0)");

                entity.Property(e => e.Hotel)
                    .IsRequired()
                    .HasColumnName("hotel")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17);

                entity.Property(e => e.Sophong).HasColumnName("sophong");
            });

            modelBuilder.Entity<Loaiphong>(entity =>
            {
                entity.HasKey(e => e.MaLoaiPhong);

                entity.ToTable("loaiphong");

                entity.Property(e => e.MaLoaiPhong)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TenLoaiPhong).HasMaxLength(50);
            });

            modelBuilder.Entity<Loaitour>(entity =>
            {
                entity.ToTable("loaitour");

                entity.Property(e => e.Loaitourid)
                    .HasColumnName("loaitourid")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tenloaitour)
                    .HasColumnName("tenloaitour")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Link).HasMaxLength(250);

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.Target)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            modelBuilder.Entity<MenuType>(entity =>
            {
                entity.ToTable("menuType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nuoc>(entity =>
            {
                entity.ToTable("nuoc");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TenNuoc)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Phieuthamquan>(entity =>
            {
                entity.ToTable("phieuthamquan");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Dichvunl).HasColumnName("dichvunl");

                entity.Property(e => e.Dichvute).HasColumnName("dichvute");

                entity.Property(e => e.Huongdan)
                    .HasColumnName("huongdan")
                    .HasMaxLength(50);

                entity.Property(e => e.Huy).HasColumnName("huy");

                entity.Property(e => e.Khachnl).HasColumnName("khachnl");

                entity.Property(e => e.Khachte).HasColumnName("khachte");

                entity.Property(e => e.Matq)
                    .HasColumnName("matq")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ngay)
                    .HasColumnName("ngay")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sokhach).HasColumnName("sokhach");

                entity.Property(e => e.Sokhachdv).HasColumnName("sokhachdv");

                entity.Property(e => e.Sophieu)
                    .HasColumnName("sophieu")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Quan>(entity =>
            {
                entity.ToTable("quan");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Tenquan)
                    .HasColumnName("tenquan")
                    .HasMaxLength(50);

                entity.Property(e => e.Tentp)
                    .HasColumnName("tentp")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rmlist>(entity =>
            {
                entity.HasKey(e => e.IdRmlist);

                entity.Property(e => e.IdRmlist)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Loaiphong)
                    .HasColumnName("loaiphong")
                    .HasMaxLength(20);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sophong).HasColumnName("sophong");

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Vetourid)
                    .HasColumnName("vetourid")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmlistDetail>(entity =>
            {
                entity.HasKey(e => e.IdRmlistDetail);

                entity.Property(e => e.IdRmlistDetail)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Loaiphong)
                    .HasColumnName("loaiphong")
                    .HasMaxLength(20);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sttkhach)
                    .HasColumnName("sttkhach")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Supplierid)
                    .IsRequired()
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RmlistDetailLog>(entity =>
            {
                entity.ToTable("RmlistDetail_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Loai)
                    .HasColumnName("loai")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnName("ngaycapnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sttkhach)
                    .HasColumnName("sttkhach")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RmlistLog>(entity =>
            {
                entity.ToTable("Rmlist_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Loai)
                    .HasColumnName("loai")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Loaiphong)
                    .HasColumnName("loaiphong")
                    .HasMaxLength(20);

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnName("ngaycapnhat")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sophong).HasColumnName("sophong");

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Vetourid)
                    .HasColumnName("vetourid")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("roleName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("supplier");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(30);

                entity.Property(e => e.Codecn)
                    .HasColumnName("codecn")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Field)
                    .HasColumnName("field")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Nation)
                    .HasColumnName("nation")
                    .HasMaxLength(50);

                entity.Property(e => e.Paymentcode)
                    .HasColumnName("paymentcode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Room)
                    .HasColumnName("room")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.Taxcode)
                    .HasColumnName("taxcode")
                    .HasMaxLength(50);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Thanhpho>(entity =>
            {
                entity.HasKey(e => e.Matp);

                entity.ToTable("thanhpho");

                entity.Property(e => e.Matp)
                    .HasColumnName("matp")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tentp)
                    .HasColumnName("tentp")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Thecoupon>(entity =>
            {
                entity.HasKey(e => e.Soseri);

                entity.ToTable("thecoupon");

                entity.Property(e => e.Soseri)
                    .HasColumnName("soseri")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Codedoan)
                    .HasColumnName("codedoan")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Daily)
                    .HasColumnName("daily")
                    .HasMaxLength(50);

                entity.Property(e => e.Gia)
                    .HasColumnName("gia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Ngaygd)
                    .HasColumnName("ngaygd")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayhethan)
                    .HasColumnName("ngayhethan")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhieuluc)
                    .HasColumnName("ngayhieuluc")
                    .HasColumnType("date");

                entity.Property(e => e.Nguoiban)
                    .HasColumnName("nguoiban")
                    .HasMaxLength(50);

                entity.Property(e => e.Noidung)
                    .HasColumnName("noidung")
                    .HasMaxLength(100);

                entity.Property(e => e.Quatang).HasColumnName("quatang");

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasMaxLength(50);

                entity.Property(e => e.Vetour)
                    .HasColumnName("vetour")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasKey(e => e.Idtour)
                    .HasName("PK_tour_1");

                entity.ToTable("tour");

                entity.HasIndex(e => e.Sgtcode)
                    .HasName("ck_unique_sgtcode")
                    .IsUnique();

                entity.Property(e => e.Idtour)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3);

                entity.Property(e => e.Choconlai)
                    .HasColumnName("choconlai")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Chudetour)
                    .HasColumnName("chudetour")
                    .HasMaxLength(150);

                entity.Property(e => e.Cttour).HasColumnName("cttour");

                entity.Property(e => e.Diemtq).HasColumnName("diemtq");

                entity.Property(e => e.Dienthoaihd)
                    .HasColumnName("dienthoaihd")
                    .HasMaxLength(100);

                entity.Property(e => e.Dongtour)
                    .HasColumnName("dongtour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dtnuocngoaieb)
                    .HasColumnName("dtnuocngoaieb")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dtnuocngoainl)
                    .HasColumnName("dtnuocngoainl")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dtnuocngoaite)
                    .HasColumnName("dtnuocngoaite")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(350);

                entity.Property(e => e.Ghichuonline)
                    .HasColumnName("ghichuonline")
                    .HasMaxLength(200);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Hanlamvisa)
                    .HasColumnName("hanlamvisa")
                    .HasColumnType("date");

                entity.Property(e => e.Hanxuatvmb)
                    .HasColumnName("hanxuatvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Huytour)
                    .HasColumnName("huytour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Khachle)
                    .IsRequired()
                    .HasColumnName("khachle")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Khachtoithieu).HasColumnName("khachtoithieu");

                entity.Property(e => e.Ksdukien)
                    .HasColumnName("ksdukien")
                    .HasMaxLength(120);

                entity.Property(e => e.Loaitour)
                    .HasColumnName("loaitour")
                    .HasMaxLength(100);

                entity.Property(e => e.Logfile)
                    .HasColumnName("logfile")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lydohuy)
                    .HasColumnName("lydohuy")
                    .HasMaxLength(50);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhopdoan)
                    .HasColumnName("ngayhopdoan")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoihuy)
                    .HasColumnName("nguoihuy")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoitaotour)
                    .HasColumnName("nguoitaotour")
                    .HasMaxLength(25);

                entity.Property(e => e.Nhdukien)
                    .HasColumnName("nhdukien")
                    .HasMaxLength(120);

                entity.Property(e => e.Noidungtinnhan)
                    .HasColumnName("noidungtinnhan")
                    .HasMaxLength(200);

                entity.Property(e => e.Noikhoihanh)
                    .IsRequired()
                    .HasColumnName("noikhoihanh")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Online).HasColumnName("online");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Socho).HasColumnName("socho");

                entity.Property(e => e.Tuyentq)
                    .HasColumnName("tuyentq")
                    .HasMaxLength(150);

                entity.Property(e => e.Vanchuyen)
                    .IsRequired()
                    .HasColumnName("vanchuyen")
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TourLog>(entity =>
            {
                entity.ToTable("tour_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3);

                entity.Property(e => e.Choconlai).HasColumnName("choconlai");

                entity.Property(e => e.Chudetour)
                    .HasColumnName("chudetour")
                    .HasMaxLength(150);

                entity.Property(e => e.Cttour).HasColumnName("cttour");

                entity.Property(e => e.Diemtq).HasColumnName("diemtq");

                entity.Property(e => e.Dienthoaihd)
                    .HasColumnName("dienthoaihd")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Dongtour)
                    .HasColumnName("dongtour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dtnuocngoaieb)
                    .HasColumnName("dtnuocngoaieb")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dtnuocngoainl)
                    .HasColumnName("dtnuocngoainl")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dtnuocngoaite)
                    .HasColumnName("dtnuocngoaite")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichuonline)
                    .HasColumnName("ghichuonline")
                    .HasMaxLength(200);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hanlamvisa)
                    .HasColumnName("hanlamvisa")
                    .HasColumnType("date");

                entity.Property(e => e.Hanxuatvmb)
                    .HasColumnName("hanxuatvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Huytour)
                    .HasColumnName("huytour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Khachle).HasColumnName("khachle");

                entity.Property(e => e.Ksdukien)
                    .HasColumnName("ksdukien")
                    .HasMaxLength(120);

                entity.Property(e => e.Loaitour)
                    .HasColumnName("loaitour")
                    .HasMaxLength(100);

                entity.Property(e => e.Lydohuy)
                    .HasColumnName("lydohuy")
                    .HasMaxLength(30);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(host_name())");

                entity.Property(e => e.Ngayhopdoan)
                    .HasColumnName("ngayhopdoan")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoihuy)
                    .HasColumnName("nguoihuy")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoitaotour)
                    .HasColumnName("nguoitaotour")
                    .HasMaxLength(50);

                entity.Property(e => e.Nhdukien)
                    .HasColumnName("nhdukien")
                    .HasMaxLength(120);

                entity.Property(e => e.Noidungtinnhan)
                    .HasColumnName("noidungtinnhan")
                    .HasMaxLength(200);

                entity.Property(e => e.Noikhoihanh)
                    .HasColumnName("noikhoihanh")
                    .HasMaxLength(100);

                entity.Property(e => e.Online)
                    .HasColumnName("online")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17);

                entity.Property(e => e.Socho)
                    .HasColumnName("socho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tuyentq)
                    .HasColumnName("tuyentq")
                    .HasMaxLength(150);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Vanchuyen)
                    .HasColumnName("vanchuyen")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<Tourkhachdoan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("tourkhachdoan");

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3);

                entity.Property(e => e.Chudetour)
                    .HasColumnName("chudetour")
                    .HasMaxLength(150);

                entity.Property(e => e.Damphan)
                    .HasColumnName("damphan")
                    .HasColumnType("date");

                entity.Property(e => e.Datcocvmb)
                    .HasColumnName("datcocvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(80);

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(250);

                entity.Property(e => e.Diemtq).HasColumnName("diemtq");

                entity.Property(e => e.Doitacnn)
                    .HasColumnName("doitacnn")
                    .HasMaxLength(50);

                entity.Property(e => e.Dongtour)
                    .HasColumnName("dongtour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dtdukien)
                    .HasColumnName("dtdukien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dtgiaodich)
                    .HasColumnName("dtgiaodich")
                    .HasMaxLength(50);

                entity.Property(e => e.Dtthucte)
                    .HasColumnName("dtthucte")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(40);

                entity.Property(e => e.Giaodich)
                    .HasColumnName("giaodich")
                    .HasMaxLength(50);

                entity.Property(e => e.Hangkhong)
                    .HasColumnName("hangkhong")
                    .HasMaxLength(250);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasColumnType("date");

                entity.Property(e => e.Huytour)
                    .HasColumnName("huytour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Loaitour)
                    .HasColumnName("loaitour")
                    .HasMaxLength(100);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhcdv)
                    .HasColumnName("ngayhcdv")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhctlhopdong)
                    .HasColumnName("ngayhctlhopdong")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayhctour)
                    .HasColumnName("ngayhctour")
                    .HasColumnType("date");

                entity.Property(e => e.Nguoikyhd)
                    .HasColumnName("nguoikyhd")
                    .HasMaxLength(40);

                entity.Property(e => e.Sales)
                    .HasColumnName("sales")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Skdukien).HasColumnName("skdukien");

                entity.Property(e => e.Skthucte).HasColumnName("skthucte");

                entity.Property(e => e.Sohopdong)
                    .HasColumnName("sohopdong")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(40);

                entity.Property(e => e.Tengiaodich)
                    .HasColumnName("tengiaodich")
                    .HasMaxLength(70);

                entity.Property(e => e.Thanhlyhd)
                    .HasColumnName("thanhlyhd")
                    .HasColumnType("date");

                entity.Property(e => e.Thongtin)
                    .HasColumnName("thongtin")
                    .HasMaxLength(250);

                entity.Property(e => e.Tlhopdong)
                    .HasColumnName("tlhopdong")
                    .HasMaxLength(250);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(250);

                entity.Property(e => e.Tuyentq)
                    .HasColumnName("tuyentq")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Tuyendiem>(entity =>
            {
                entity.ToTable("tuyendiem");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Tentuyendiem)
                    .HasColumnName("tentuyendiem")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tuyentqob>(entity =>
            {
                entity.ToTable("tuyentqob");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Diemtq)
                    .IsRequired()
                    .HasColumnName("diemtq")
                    .HasMaxLength(70);

                entity.Property(e => e.Nuoc)
                    .IsRequired()
                    .HasColumnName("nuoc")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Userfolder>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("userfolder");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Phongban)
                    .HasColumnName("phongban")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Show)
                    .HasColumnName("show")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Upload)
                    .HasColumnName("upload")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users");

                entity.HasIndex(e => e.Username)
                    .HasName("unique_username")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Adminkd).HasColumnName("adminkd");

                entity.Property(e => e.Adminkl).HasColumnName("adminkl");

                entity.Property(e => e.Bantour)
                    .HasColumnName("bantour")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Banve)
                    .HasColumnName("banve")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Daily)
                    .HasColumnName("daily")
                    .HasMaxLength(50);

                entity.Property(e => e.Dcdanhmuc)
                    .HasColumnName("dcdanhmuc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Doimk)
                    .HasColumnName("doimk")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dongtour)
                    .HasColumnName("dongtour")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Emailcc)
                    .HasColumnName("emailcc")
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .HasColumnName("fullName")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnName("ngaycapnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaydoimk)
                    .HasColumnName("ngaydoimk")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Phongban)
                    .HasColumnName("phongban")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("dieu hanh tour nuoc ngoai (campuchia)");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('sales')");

                entity.Property(e => e.Suatour)
                    .HasColumnName("suatour")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Suave)
                    .HasColumnName("suave")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Taotour)
                    .HasColumnName("taotour")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Trangthai)
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VChinhanh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vChinhanh");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DirPathName)
                    .HasColumnName("dirPathName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DomainNm)
                    .HasColumnName("domain_nm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FlagUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Masothue)
                    .HasColumnName("masothue")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tencn)
                    .HasColumnName("tencn")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(70);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<VDmtaixe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDmtaixe");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.LoaiXe).HasMaxLength(30);

                entity.Property(e => e.SoXe)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Tentaixe)
                    .HasColumnName("tentaixe")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VDoanhthu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDoanhthu");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giatour)
                    .HasColumnName("giatour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Ngayhuy)
                    .HasColumnName("ngayhuy")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoilambn)
                    .HasColumnName("nguoilambn")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");
            });

            modelBuilder.Entity<VDoitacChiphi>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDoitac_chiphi");

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hanghk)
                    .HasColumnName("hanghk")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Iddv)
                    .HasColumnName("iddv")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mancc)
                    .HasColumnName("mancc")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ngoaite)
                    .HasColumnName("ngoaite")
                    .HasColumnType("decimal(38, 2)");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenncc)
                    .HasColumnName("tenncc")
                    .HasMaxLength(50);

                entity.Property(e => e.Tienvnd)
                    .HasColumnName("tienvnd")
                    .HasColumnType("decimal(38, 0)");
            });

            modelBuilder.Entity<VKhachtourTour>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vKhachtour_tour");

                entity.Property(e => e.Baohiem)
                    .HasColumnName("baohiem")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(204);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Dotuoi)
                    .HasColumnName("dotuoi")
                    .HasMaxLength(3);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichu)
                    .IsRequired()
                    .HasColumnName("ghichu")
                    .HasMaxLength(250);

                entity.Property(e => e.Ghichubaohiem)
                    .HasColumnName("ghichubaohiem")
                    .HasMaxLength(150);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");

                entity.Property(e => e.Hanhtrinh).HasColumnName("hanhtrinh");

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(20);

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Idkhach)
                    .HasColumnName("idkhach")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Landtour)
                    .HasColumnName("landtour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Loaikhach)
                    .HasColumnName("loaikhach")
                    .HasMaxLength(2);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycaphc)
                    .HasColumnName("ngaycaphc")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Phongks)
                    .IsRequired()
                    .HasColumnName("phongks")
                    .HasMaxLength(3);

                entity.Property(e => e.Quoctich)
                    .IsRequired()
                    .HasColumnName("quoctich")
                    .HasMaxLength(30);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(12);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<VLoaixe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vLoaixe");

                entity.Property(e => e.Loaixe)
                    .HasColumnName("loaixe")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<VThongtinHuongdan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vThongtinHuongdan");

                entity.Property(e => e.Dienthoaidd)
                    .HasColumnName("dienthoaidd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hieuluchochieu)
                    .HasColumnName("hieuluchochieu")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Tenhd)
                    .HasColumnName("tenhd")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VTimVetourDatcoc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimVetourDatcoc");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Dailyxuatve)
                    .HasColumnName("dailyxuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Diemdon)
                    .HasColumnName("diemdon")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichudvk)
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvetour).HasColumnName("ghichuvetour");

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giatour)
                    .HasColumnName("giatour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kenhgd)
                    .HasColumnName("kenhgd")
                    .HasMaxLength(30);

                entity.Property(e => e.Lydogiamgia)
                    .HasColumnName("lydogiamgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnName("ngayhuyve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnName("ngaythutien")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnName("ngayxuatve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoihuyve)
                    .HasColumnName("nguoihuyve")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoithu)
                    .HasColumnName("nguoithu")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(12);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(38, 0)");

                entity.Property(e => e.Tencoquan)
                    .HasColumnName("tencoquan")
                    .HasMaxLength(60);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Tuhuy).HasColumnName("tuhuy");

                entity.Property(e => e.VetourId).HasColumnName("vetourId");

                entity.Property(e => e.Yeucauks)
                    .HasColumnName("yeucauks")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<VTimdatcoc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimdatcoc");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Idvetour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Ngaydatcoc)
                    .HasColumnName("ngaydatcoc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoilambn)
                    .HasColumnName("nguoilambn")
                    .HasMaxLength(25);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<VTimdatcoc1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimdatcoc1");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Ghichuvetour).HasColumnName("ghichuvetour");

                entity.Property(e => e.Idvetour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Ngaydatcoc)
                    .HasColumnName("ngaydatcoc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoilambn)
                    .HasColumnName("nguoilambn")
                    .HasMaxLength(25);

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<VTimkhach>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimkhach");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Tuyentq)
                    .HasColumnName("tuyentq")
                    .HasMaxLength(150);

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<VTimkhachBySales>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimkhachBySales");

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("datetime");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<VTimvetour>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTimvetour");

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("datetime");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Nguoilambn)
                    .HasColumnName("nguoilambn")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(12);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Tuyentq)
                    .HasColumnName("tuyentq")
                    .HasMaxLength(150);

                entity.Property(e => e.VetourId).HasColumnName("vetourId");
            });

            modelBuilder.Entity<VTourKhachhang>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTourKhachhang");

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3);

                entity.Property(e => e.Choconlai).HasColumnName("choconlai");

                entity.Property(e => e.Chudetour)
                    .HasColumnName("chudetour")
                    .HasMaxLength(150);

                entity.Property(e => e.Cttour).HasColumnName("cttour");

                entity.Property(e => e.Diemtq).HasColumnName("diemtq");

                entity.Property(e => e.Dienthoaihd)
                    .HasColumnName("dienthoaihd")
                    .HasMaxLength(100);

                entity.Property(e => e.Dongtour)
                    .HasColumnName("dongtour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dtnuocngoaieb)
                    .HasColumnName("dtnuocngoaieb")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dtnuocngoainl)
                    .HasColumnName("dtnuocngoainl")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Dtnuocngoaite)
                    .HasColumnName("dtnuocngoaite")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(350);

                entity.Property(e => e.Ghichuonline)
                    .HasColumnName("ghichuonline")
                    .HasMaxLength(200);

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Hanlamvisa)
                    .HasColumnName("hanlamvisa")
                    .HasColumnType("date");

                entity.Property(e => e.Hanxuatvmb)
                    .HasColumnName("hanxuatvmb")
                    .HasColumnType("date");

                entity.Property(e => e.Huytour)
                    .HasColumnName("huytour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idtour).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Khachle).HasColumnName("khachle");

                entity.Property(e => e.Ksdukien)
                    .HasColumnName("ksdukien")
                    .HasMaxLength(120);

                entity.Property(e => e.Loaitour)
                    .HasColumnName("loaitour")
                    .HasMaxLength(100);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Lydohuy)
                    .HasColumnName("lydohuy")
                    .HasMaxLength(50);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhopdoan)
                    .HasColumnName("ngayhopdoan")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoihuy)
                    .HasColumnName("nguoihuy")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoitaotour)
                    .HasColumnName("nguoitaotour")
                    .HasMaxLength(25);

                entity.Property(e => e.Nhdukien)
                    .HasColumnName("nhdukien")
                    .HasMaxLength(120);

                entity.Property(e => e.Noidungtinnhan)
                    .HasColumnName("noidungtinnhan")
                    .HasMaxLength(200);

                entity.Property(e => e.Noikhoihanh)
                    .IsRequired()
                    .HasColumnName("noikhoihanh")
                    .HasMaxLength(100);

                entity.Property(e => e.Online).HasColumnName("online");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Socho).HasColumnName("socho");

                entity.Property(e => e.Tengiaodich)
                    .HasColumnName("tengiaodich")
                    .HasMaxLength(70);

                entity.Property(e => e.Tuyentq)
                    .HasColumnName("tuyentq")
                    .HasMaxLength(150);

                entity.Property(e => e.Vanchuyen)
                    .IsRequired()
                    .HasColumnName("vanchuyen")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<VUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUser");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Adminkd).HasColumnName("adminkd");

                entity.Property(e => e.Adminkl).HasColumnName("adminkl");

                entity.Property(e => e.Bantour)
                    .HasColumnName("bantour")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Banve).HasColumnName("banve");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Daily)
                    .HasColumnName("daily")
                    .HasMaxLength(50);

                entity.Property(e => e.Dcdanhmuc).HasColumnName("dcdanhmuc");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DirPathName)
                    .HasColumnName("dirPathName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Doimk).HasColumnName("doimk");

                entity.Property(e => e.DomainNm)
                    .HasColumnName("domain_nm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dongtour).HasColumnName("dongtour");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Emailcc)
                    .HasColumnName("emailcc")
                    .HasMaxLength(50);

                entity.Property(e => e.FlagUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasColumnName("fullName")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaydoimk)
                    .HasColumnName("ngaydoimk")
                    .HasColumnType("date");

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Phongban)
                    .HasColumnName("phongban")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Show).HasColumnName("show");

                entity.Property(e => e.Suatour).HasColumnName("suatour");

                entity.Property(e => e.Suave).HasColumnName("suave");

                entity.Property(e => e.Taotour).HasColumnName("taotour");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.Property(e => e.Upload).HasColumnName("upload");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VVetourDhtour>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vVetour_dhtour");

                entity.Property(e => e.Biennhan)
                    .HasColumnName("biennhan")
                    .HasMaxLength(12);

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Dailyxuatve)
                    .HasColumnName("dailyxuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Diemdon)
                    .HasColumnName("diemdon")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichudvk)
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvetour).HasColumnName("ghichuvetour");

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giatour)
                    .HasColumnName("giatour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kenhgd)
                    .HasColumnName("kenhgd")
                    .HasMaxLength(30);

                entity.Property(e => e.Kenhtt)
                    .HasColumnName("kenhtt")
                    .HasMaxLength(30);

                entity.Property(e => e.Lydogiamgia)
                    .HasColumnName("lydogiamgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnName("ngayhuyve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnName("ngaythutien")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnName("ngayxuatve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoihuyve)
                    .HasColumnName("nguoihuyve")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoithu)
                    .HasColumnName("nguoithu")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(12);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(38, 0)");

                entity.Property(e => e.Tencoquan)
                    .HasColumnName("tencoquan")
                    .HasMaxLength(60);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Tuhuy).HasColumnName("tuhuy");

                entity.Property(e => e.VetourId).HasColumnName("vetourId");

                entity.Property(e => e.Voucher)
                    .HasColumnName("voucher")
                    .HasMaxLength(50);

                entity.Property(e => e.Yeucauks)
                    .HasColumnName("yeucauks")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Vanchuyen>(entity =>
            {
                entity.ToTable("vanchuyen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tenvanchuyen)
                    .HasColumnName("tenvanchuyen")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vethamquan>(entity =>
            {
                entity.ToTable("vethamquan");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Dichvunl).HasColumnName("dichvunl");

                entity.Property(e => e.Dichvute).HasColumnName("dichvute");

                entity.Property(e => e.Diemtq)
                    .HasColumnName("diemtq")
                    .HasMaxLength(70);

                entity.Property(e => e.Huongdan)
                    .HasColumnName("huongdan")
                    .HasMaxLength(50);

                entity.Property(e => e.Khachnl).HasColumnName("khachnl");

                entity.Property(e => e.Khachte).HasColumnName("khachte");

                entity.Property(e => e.Matq)
                    .HasColumnName("matq")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ngay)
                    .HasColumnName("ngay")
                    .HasColumnType("date");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sokhach).HasColumnName("sokhach");

                entity.Property(e => e.Sokhachdv).HasColumnName("sokhachdv");

                entity.Property(e => e.Sophieu)
                    .HasColumnName("sophieu")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vetour>(entity =>
            {
                entity.ToTable("vetour");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cachtinhhh)
                    .HasColumnName("cachtinhhh")
                    .HasMaxLength(120);

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chiemcho)
                    .HasColumnName("chiemcho")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cmndnguoinhanhh)
                    .HasColumnName("cmndnguoinhanhh")
                    .HasMaxLength(50);

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(30);

                entity.Property(e => e.Dailyhuyve)
                    .HasColumnName("dailyhuyve")
                    .HasMaxLength(25);

                entity.Property(e => e.Dailyxuatve)
                    .HasColumnName("dailyxuatve")
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Diachinguoinhanhh)
                    .HasColumnName("diachinguoinhanhh")
                    .HasMaxLength(50);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Diemdon)
                    .HasColumnName("diemdon")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Emailnv)
                    .HasColumnName("emailnv")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichudvk)
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvetour).HasColumnName("ghichuvetour");

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Giatour)
                    .HasColumnName("giatour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Handongtien)
                    .HasColumnName("handongtien")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Hoahongct).HasColumnName("hoahongct");

                entity.Property(e => e.Hoahongnv).HasColumnName("hoahongnv");

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Idchuyenve)
                    .HasColumnName("idchuyenve")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kenhgd)
                    .HasColumnName("kenhgd")
                    .HasMaxLength(30);

                entity.Property(e => e.Kenhtt)
                    .HasColumnName("kenhtt")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')")
                    .HasComment("vourcher:  'HDT' hoặc số vourcher");

                entity.Property(e => e.Lephihuy)
                    .HasColumnName("lephihuy")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Lydogiamgia)
                    .HasColumnName("lydogiamgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Magdonline)
                    .HasColumnName("magdonline")
                    .HasMaxLength(20);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mstnguoinhanhh)
                    .HasColumnName("mstnguoinhanhh")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaychihh)
                    .HasColumnName("ngaychihh")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnName("ngayhuyve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnName("ngaythutien")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnName("ngayxuatve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoichihh)
                    .HasColumnName("nguoichihh")
                    .HasMaxLength(40);

                entity.Property(e => e.Nguoihuyve)
                    .HasColumnName("nguoihuyve")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoinhanhh)
                    .HasColumnName("nguoinhanhh")
                    .HasMaxLength(40);

                entity.Property(e => e.Nguoithu)
                    .HasColumnName("nguoithu")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Noidunghuyve).HasColumnName("noidunghuyve");

                entity.Property(e => e.Phieuchihh)
                    .HasColumnName("phieuchihh")
                    .HasMaxLength(50);

                entity.Property(e => e.Quan)
                    .HasColumnName("quan")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(12);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tencoquan)
                    .HasColumnName("tencoquan")
                    .HasMaxLength(60);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Thetindung)
                    .HasColumnName("thetindung")
                    .HasMaxLength(20);

                entity.Property(e => e.Thuetncn)
                    .HasColumnName("thuetncn")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tienhoahong)
                    .HasColumnName("tienhoahong")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tienhoan)
                    .HasColumnName("tienhoan")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tigia)
                    .HasColumnName("tigia")
                    .HasColumnType("decimal(12, 2)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Tuhuy).HasColumnName("tuhuy");

                entity.Property(e => e.VetourId).HasColumnName("vetourId");

                entity.Property(e => e.Voucher)
                    .HasColumnName("voucher")
                    .HasMaxLength(50);

                entity.Property(e => e.Yeucauks)
                    .HasColumnName("yeucauks")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<VetourLog>(entity =>
            {
                entity.ToTable("vetour_log");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cachtinhhh)
                    .HasColumnName("cachtinhhh")
                    .HasMaxLength(120);

                entity.Property(e => e.Chiemcho).HasColumnName("chiemcho");

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(50);

                entity.Property(e => e.Dailyhuyve)
                    .HasColumnName("dailyhuyve")
                    .HasMaxLength(25);

                entity.Property(e => e.Dailyxuatve)
                    .HasColumnName("dailyxuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dichvukhac)
                    .HasColumnName("dichvukhac")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Diemdon)
                    .HasColumnName("diemdon")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(60);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichudvk)
                    .HasColumnName("ghichudvk")
                    .HasMaxLength(100);

                entity.Property(e => e.Ghichuvetour).HasColumnName("ghichuvetour");

                entity.Property(e => e.Giamgia)
                    .HasColumnName("giamgia")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Giatour)
                    .HasColumnName("giatour")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Hoahong)
                    .HasColumnName("hoahong")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Huyve)
                    .HasColumnName("huyve")
                    .HasMaxLength(12);

                entity.Property(e => e.Idchuyenve)
                    .HasColumnName("idchuyenve")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kenhgd)
                    .HasColumnName("kenhgd")
                    .HasMaxLength(30);

                entity.Property(e => e.Kenhtt)
                    .HasColumnName("kenhtt")
                    .HasMaxLength(30);

                entity.Property(e => e.Lephihuy)
                    .HasColumnName("lephihuy")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Lydogiamgia)
                    .HasColumnName("lydogiamgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Magdonline)
                    .HasColumnName("magdonline")
                    .HasMaxLength(20);

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaychihh)
                    .HasColumnName("ngaychihh")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnName("ngayhuyve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaysua)
                    .HasColumnName("ngaysua")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnName("ngaythutien")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnName("ngayxuatve")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoichihh)
                    .HasColumnName("nguoichihh")
                    .HasMaxLength(40);

                entity.Property(e => e.Nguoihuyve)
                    .HasColumnName("nguoihuyve")
                    .HasMaxLength(25);

                entity.Property(e => e.Nguoinhanhh)
                    .HasColumnName("nguoinhanhh")
                    .HasMaxLength(40);

                entity.Property(e => e.Nguoisua)
                    .HasColumnName("nguoisua")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoithu)
                    .HasColumnName("nguoithu")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoixuatve)
                    .HasColumnName("nguoixuatve")
                    .HasMaxLength(25);

                entity.Property(e => e.Noidunghuyve).HasColumnName("noidunghuyve");

                entity.Property(e => e.Phieuchihh)
                    .HasColumnName("phieuchihh")
                    .HasMaxLength(12);

                entity.Property(e => e.Quan)
                    .HasColumnName("quan")
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(12);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tencoquan)
                    .HasColumnName("tencoquan")
                    .HasMaxLength(60);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(50);

                entity.Property(e => e.Thetindung)
                    .HasColumnName("thetindung")
                    .HasMaxLength(20);

                entity.Property(e => e.Tienhoan)
                    .HasColumnName("tienhoan")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tigia).HasColumnName("tigia");

                entity.Property(e => e.Tuhuy).HasColumnName("tuhuy");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VetourId).HasColumnName("vetourId");

                entity.Property(e => e.Yeucauks)
                    .HasColumnName("yeucauks")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_1");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.LaiXe).HasMaxLength(50);

                entity.Property(e => e.LoaiXe).HasMaxLength(30);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.SoXe)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Yeucauxe>(entity =>
            {
                entity.HasKey(e => new { e.Sgtcode, e.Stt });

                entity.ToTable("yeucauxe");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ghichuycx)
                    .HasColumnName("ghichuycx")
                    .HasMaxLength(150);

                entity.Property(e => e.Laixe)
                    .HasColumnName("laixe")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Loaixe)
                    .HasColumnName("loaixe")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Nguonxe)
                    .HasColumnName("nguonxe")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Soxe)
                    .HasColumnName("soxe")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<YeucauxeLog>(entity =>
            {
                entity.ToTable("yeucauxe_log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Computer)
                    .HasColumnName("computer")
                    .HasMaxLength(50);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichuycx)
                    .HasColumnName("ghichuycx")
                    .HasMaxLength(150);

                entity.Property(e => e.Laixe)
                    .HasColumnName("laixe")
                    .HasMaxLength(50);

                entity.Property(e => e.Loaixe)
                    .HasColumnName("loaixe")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnName("ngaycapnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoicapnhat)
                    .HasColumnName("nguoicapnhat")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguonxe)
                    .HasColumnName("nguonxe")
                    .HasMaxLength(70);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Soxe)
                    .HasColumnName("soxe")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('I')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
