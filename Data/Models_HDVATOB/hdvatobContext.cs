using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models_HDVATOB
{
    public partial class hdvatobContext : DbContext
    {
        public hdvatobContext()
        {
        }

        public hdvatobContext(DbContextOptions<hdvatobContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cthdvat> Cthdvat { get; set; }
        public virtual DbSet<CthdvatDel> CthdvatDel { get; set; }
        public virtual DbSet<Cttachve> Cttachve { get; set; }
        public virtual DbSet<Dmhttc> Dmhttc { get; set; }
        public virtual DbSet<Dmtk> Dmtk { get; set; }
        public virtual DbSet<Dvphi> Dvphi { get; set; }
        public virtual DbSet<Hoadon> Hoadon { get; set; }
        public virtual DbSet<HoadonDel> HoadonDel { get; set; }
        public virtual DbSet<HoadonHuy> HoadonHuy { get; set; }
        public virtual DbSet<Huycthdvat> Huycthdvat { get; set; }
        public virtual DbSet<Huyhdvat> Huyhdvat { get; set; }
        public virtual DbSet<Nguonhd> Nguonhd { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Tachve> Tachve { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=118.68.170.128;Database=hdvatob;Trusted_Connection=True;User Id=vanhong;Password=Hong@2019;Integrated Security=false;MultipleActiveResultSets=true;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cthdvat>(entity =>
            {
                entity.ToTable("cthdvat");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon)
                    .HasColumnName("coupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diengiai)
                    .HasColumnName("diengiai")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(160);

                entity.Property(e => e.Hoadonhuy)
                    .HasColumnName("hoadonhuy")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Httc)
                    .HasColumnName("httc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnName("ngayhuy")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Ppv)
                    .HasColumnName("ppv")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Slve).HasColumnName("slve");

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Sotiennt)
                    .HasColumnName("sotiennt")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Sttdong).HasColumnName("sttdong");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(140);

                entity.Property(e => e.Tiencoupon)
                    .HasColumnName("tiencoupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tkco)
                    .HasColumnName("tkco")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tkno)
                    .HasColumnName("tkno")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tygia)
                    .HasColumnName("tygia")
                    .HasColumnType("decimal(8, 1)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnName("xuatve")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CthdvatDel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cthdvat_del");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon)
                    .HasColumnName("coupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diengiai)
                    .HasColumnName("diengiai")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(60);

                entity.Property(e => e.Hoadonhuy)
                    .HasColumnName("hoadonhuy")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Httc)
                    .HasColumnName("httc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnName("ngayhuy")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Ppv)
                    .HasColumnName("ppv")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Slve).HasColumnName("slve");

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Sotiennt)
                    .HasColumnName("sotiennt")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Sttdong).HasColumnName("sttdong");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(40);

                entity.Property(e => e.Tiencoupon)
                    .HasColumnName("tiencoupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tkco)
                    .HasColumnName("tkco")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tkno)
                    .HasColumnName("tkno")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tygia)
                    .HasColumnName("tygia")
                    .HasColumnType("decimal(8, 1)");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnName("xuatve")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Cttachve>(entity =>
            {
                entity.ToTable("cttachve");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon)
                    .HasColumnName("coupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Diengiai)
                    .HasColumnName("diengiai")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(60);

                entity.Property(e => e.Httc)
                    .HasColumnName("httc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnName("ngayhuy")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ppv)
                    .HasColumnName("ppv")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Sotiennt)
                    .HasColumnName("sotiennt")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(140);

                entity.Property(e => e.Tiencoupon)
                    .HasColumnName("tiencoupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tkco)
                    .HasColumnName("tkco")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tkno)
                    .HasColumnName("tkno")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tygia)
                    .HasColumnName("tygia")
                    .HasColumnType("decimal(8, 1)");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnName("xuatve")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Dmhttc>(entity =>
            {
                entity.HasKey(e => e.Httc);

                entity.ToTable("dmhttc");

                entity.Property(e => e.Httc)
                    .HasColumnName("httc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diengiai)
                    .HasColumnName("diengiai")
                    .HasMaxLength(40);

                entity.Property(e => e.MaIn)
                    .HasColumnName("ma_in")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dmtk>(entity =>
            {
                entity.ToTable("dmtk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Sudung).HasColumnName("sudung");

                entity.Property(e => e.Tentk)
                    .HasColumnName("tentk")
                    .HasMaxLength(100);

                entity.Property(e => e.Tkhoan)
                    .IsRequired()
                    .HasColumnName("tkhoan")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tkhoan1)
                    .HasColumnName("tkhoan1")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tkhoan2)
                    .HasColumnName("tkhoan2")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Tkhoancu)
                    .HasColumnName("tkhoancu")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dvphi>(entity =>
            {
                entity.HasKey(e => e.Srvtype)
                    .HasName("PK_dvphi_1");

                entity.ToTable("dvphi");

                entity.Property(e => e.Srvtype)
                    .HasColumnName("srvtype")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dvphi1)
                    .HasColumnName("dvphi")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Httc)
                    .HasColumnName("httc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Vatra).HasColumnName("vatra");

                entity.Property(e => e.Vatvao).HasColumnName("vatvao");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => new { e.Idhoadon, e.Chinhanh })
                    .HasName("PK_hoadon_1");

                entity.ToTable("hoadon");

                entity.Property(e => e.Idhoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("date");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(150);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(80);

                entity.Property(e => e.Hdvat)
                    .HasColumnName("hdvat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasMaxLength(120);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("date");

                entity.Property(e => e.Keyhddt)
                    .HasColumnName("keyhddt")
                    .HasMaxLength(120);

                entity.Property(e => e.Kyhieu)
                    .HasColumnName("kyhieu")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Mausohd)
                    .HasColumnName("mausohd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Msthue)
                    .HasColumnName("msthue")
                    .HasMaxLength(16);

                entity.Property(e => e.Ngayct)
                    .HasColumnName("ngayct")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayin)
                    .HasColumnName("ngayin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnName("ngayxoa")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitaohd)
                    .HasColumnName("nguoitaohd")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguonhd)
                    .HasColumnName("nguonhd")
                    .HasMaxLength(100);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkh)
                    .HasColumnName("tenkh")
                    .HasMaxLength(200);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<HoadonDel>(entity =>
            {
                entity.ToTable("hoadon_del");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("date");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(150);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(80);

                entity.Property(e => e.Hdvat)
                    .HasColumnName("hdvat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasMaxLength(12);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("date");

                entity.Property(e => e.Keyhddt)
                    .HasColumnName("keyhddt")
                    .HasMaxLength(120);

                entity.Property(e => e.Kyhieu)
                    .HasColumnName("kyhieu")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Mausohd)
                    .HasColumnName("mausohd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Msthue)
                    .HasColumnName("msthue")
                    .HasMaxLength(16);

                entity.Property(e => e.Ngayct)
                    .HasColumnName("ngayct")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayin)
                    .HasColumnName("ngayin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnName("ngayxoa")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitaohd)
                    .HasColumnName("nguoitaohd")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguonhd)
                    .HasColumnName("nguonhd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkh)
                    .HasColumnName("tenkh")
                    .HasMaxLength(200);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<HoadonHuy>(entity =>
            {
                entity.ToTable("hoadon_huy");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Batdau)
                    .HasColumnName("batdau")
                    .HasColumnType("date");

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(150);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(80);

                entity.Property(e => e.Hdvat)
                    .HasColumnName("hdvat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasMaxLength(12);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ketthuc)
                    .HasColumnName("ketthuc")
                    .HasColumnType("date");

                entity.Property(e => e.Keyhddt)
                    .HasColumnName("keyhddt")
                    .HasMaxLength(120);

                entity.Property(e => e.Kyhieu)
                    .HasColumnName("kyhieu")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Mausohd)
                    .HasColumnName("mausohd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Msthue)
                    .HasColumnName("msthue")
                    .HasMaxLength(16);

                entity.Property(e => e.Ngayct)
                    .HasColumnName("ngayct")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayin)
                    .HasColumnName("ngayin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnName("ngayxoa")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitaohd)
                    .HasColumnName("nguoitaohd")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguonhd)
                    .HasColumnName("nguonhd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkh)
                    .HasColumnName("tenkh")
                    .HasMaxLength(200);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Huycthdvat>(entity =>
            {
                entity.ToTable("huycthdvat");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Coupon)
                    .HasColumnName("coupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dichvu)
                    .HasColumnName("dichvu")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diengiai)
                    .HasColumnName("diengiai")
                    .HasMaxLength(150);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Hoadonhuy)
                    .HasColumnName("hoadonhuy")
                    .HasMaxLength(20);

                entity.Property(e => e.Httc)
                    .HasColumnName("httc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Loaitien)
                    .HasColumnName("loaitien")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnName("ngayhuy")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Ppv)
                    .HasColumnName("ppv")
                    .HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Slve).HasColumnName("slve");

                entity.Property(e => e.Sotien)
                    .HasColumnName("sotien")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Sotiennt)
                    .HasColumnName("sotiennt")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(40);

                entity.Property(e => e.Tiencoupon)
                    .HasColumnName("tiencoupon")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Tkco)
                    .HasColumnName("tkco")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tkno)
                    .HasColumnName("tkno")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tygia)
                    .HasColumnName("tygia")
                    .HasColumnType("decimal(8, 1)");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnName("xuatve")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Huyhdvat>(entity =>
            {
                entity.HasKey(e => new { e.Idhoadon, e.Chinhanh });

                entity.ToTable("huyhdvat");

                entity.Property(e => e.Idhoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnName("datelock")
                    .HasColumnType("datetime");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(200);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(180);

                entity.Property(e => e.Hdvat)
                    .HasColumnName("hdvat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasMaxLength(50);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Keyhddt)
                    .HasColumnName("keyhddt")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Kyhieu)
                    .HasColumnName("kyhieu")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Locker)
                    .HasColumnName("locker")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Mausohd)
                    .HasColumnName("mausohd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Maytinh)
                    .HasColumnName("maytinh")
                    .HasMaxLength(50);

                entity.Property(e => e.Msthue)
                    .HasColumnName("msthue")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngayct)
                    .HasColumnName("ngayct")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayin)
                    .HasColumnName("ngayin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnName("ngayxoa")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitaohd)
                    .HasColumnName("nguoitaohd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nguonhd)
                    .HasColumnName("nguonhd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Noidunghuy)
                    .HasColumnName("noidunghuy")
                    .HasMaxLength(100);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkh)
                    .HasColumnName("tenkh")
                    .HasMaxLength(200);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nguonhd>(entity =>
            {
                entity.HasKey(e => e.IdNguonhd)
                    .HasName("PK_Nguonhd");

                entity.ToTable("nguonhd");

                entity.Property(e => e.IdNguonhd)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Diengiai).HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.Chinhanh })
                    .HasName("PK_supplier_1");

                entity.ToTable("supplier");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(15);

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(50);

                entity.Property(e => e.Daily).HasColumnName("daily");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Field)
                    .HasColumnName("field")
                    .HasMaxLength(50);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Muave).HasColumnName("muave");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.Property(e => e.Nation)
                    .HasColumnName("nation")
                    .HasMaxLength(50);

                entity.Property(e => e.Paymentcode)
                    .HasColumnName("paymentcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Realname)
                    .HasColumnName("realname")
                    .HasMaxLength(200);

                entity.Property(e => e.Room).HasColumnName("room");

                entity.Property(e => e.Suppliercode)
                    .HasColumnName("suppliercode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Taxcode)
                    .HasColumnName("taxcode")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Taxform)
                    .HasColumnName("taxform")
                    .HasMaxLength(50);

                entity.Property(e => e.Taxsign)
                    .HasColumnName("taxsign")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(50);

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Tachve>(entity =>
            {
                entity.HasKey(e => new { e.Idhoadon, e.Chinhanh });

                entity.ToTable("tachve");

                entity.Property(e => e.Idhoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Capnhat)
                    .HasColumnName("capnhat")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chuyenvat)
                    .HasColumnName("chuyenvat")
                    .HasMaxLength(50);

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(200);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(100);

                entity.Property(e => e.Hdvat)
                    .HasColumnName("hdvat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hopdong)
                    .HasColumnName("hopdong")
                    .HasMaxLength(12);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Msthue)
                    .HasColumnName("msthue")
                    .HasMaxLength(16);

                entity.Property(e => e.Ngaychuyen)
                    .HasColumnName("ngaychuyen")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayct)
                    .HasColumnName("ngayct")
                    .HasColumnType("date");

                entity.Property(e => e.Ngayin)
                    .HasColumnName("ngayin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnName("ngayxoa")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitach)
                    .HasColumnName("nguoitach")
                    .HasMaxLength(50);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasColumnName("stt")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Tenkh)
                    .HasColumnName("tenkh")
                    .HasMaxLength(100);

                entity.Property(e => e.Tenkhach)
                    .HasColumnName("tenkhach")
                    .HasMaxLength(50);

                entity.Property(e => e.Tienve)
                    .HasColumnName("tienve")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tour)
                    .HasColumnName("tour")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_users_1");

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Accounthddt)
                    .HasColumnName("accounthddt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(50);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Maviettat)
                    .HasColumnName("maviettat")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytao)
                    .HasColumnName("ngaytao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Passwordhddt)
                    .HasColumnName("passwordhddt")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
