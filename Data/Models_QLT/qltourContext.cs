using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models_QLT
{
    public partial class qltourContext : DbContext
    {
        public qltourContext()
        {
        }

        public qltourContext(DbContextOptions<qltourContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booked> Booked { get; set; }
        public virtual DbSet<Chiphikhac> Chiphikhac { get; set; }
        public virtual DbSet<CodeSupplier> CodeSupplier { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Dichvu> Dichvu { get; set; }
        public virtual DbSet<Dieuxe> Dieuxe { get; set; }
        public virtual DbSet<DmLoaiphong> DmLoaiphong { get; set; }
        public virtual DbSet<Dmchinhanh> Dmchinhanh { get; set; }
        public virtual DbSet<Dmdaily> Dmdaily { get; set; }
        public virtual DbSet<Dmdiemtq> Dmdiemtq { get; set; }
        public virtual DbSet<Dmdiemtq1> Dmdiemtq1 { get; set; }
        public virtual DbSet<DsLoaixe> DsLoaixe { get; set; }
        public virtual DbSet<Haucan> Haucan { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<HotelDel> HotelDel { get; set; }
        public virtual DbSet<Hoteltemp> Hoteltemp { get; set; }
        public virtual DbSet<Huongdan> Huongdan { get; set; }
        public virtual DbSet<Khachtour> Khachtour { get; set; }
        public virtual DbSet<Loaikhach> Loaikhach { get; set; }
        public virtual DbSet<Loaivisa> Loaivisa { get; set; }
        public virtual DbSet<LoginModel> LoginModel { get; set; }
        public virtual DbSet<Ngoaite> Ngoaite { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Passtype> Passtype { get; set; }
        public virtual DbSet<Phongban> Phongban { get; set; }
        public virtual DbSet<Quan> Quan { get; set; }
        public virtual DbSet<Quocgia> Quocgia { get; set; }
        public virtual DbSet<Sightseeing> Sightseeing { get; set; }
        public virtual DbSet<SightseeingDel> SightseeingDel { get; set; }
        public virtual DbSet<SightseeingTemp> SightseeingTemp { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Thanhpho> Thanhpho { get; set; }
        public virtual DbSet<Thongbao> Thongbao { get; set; }
        public virtual DbSet<TourTempNote> TourTempNote { get; set; }
        public virtual DbSet<Tourinf> Tourinf { get; set; }
        public virtual DbSet<Tourkind> Tourkind { get; set; }
        public virtual DbSet<Tournode> Tournode { get; set; }
        public virtual DbSet<Tourprog> Tourprog { get; set; }
        public virtual DbSet<TourprogDel> TourprogDel { get; set; }
        public virtual DbSet<Tourprogtemp> Tourprogtemp { get; set; }
        public virtual DbSet<Tourtemplate> Tourtemplate { get; set; }
        public virtual DbSet<Tuyentq> Tuyentq { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VDmdiemtq> VDmdiemtq { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=118.68.170.128;database=qltour;Trusted_Connection=true;User Id=vanhong;Password=Hong@2019;Integrated security=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booked>(entity =>
            {
                entity.HasKey(e => e.Idbooking)
                    .HasName("PK_Booked_1");

                entity.Property(e => e.Idbooking)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Booking).HasMaxLength(10);

                entity.Property(e => e.ConfirmDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17);

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Chiphikhac>(entity =>
            {
                entity.HasKey(e => e.Idorthercost)
                    .HasName("PK_chiphikhac");

                entity.Property(e => e.Idorthercost)
                    .HasColumnName("idorthercost")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Debit).HasColumnName("debit");

                entity.Property(e => e.Del).HasColumnName("del");

                entity.Property(e => e.Fromdate).HasColumnName("fromdate");

                entity.Property(e => e.Guidedays).HasColumnName("guidedays");

                entity.Property(e => e.Km).HasColumnName("km");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Srvcode)
                    .HasColumnName("srvcode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Srvnode)
                    .HasColumnName("srvnode")
                    .HasMaxLength(250);

                entity.Property(e => e.Srvprofit)
                    .HasColumnName("srvprofit")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Srvtype)
                    .HasColumnName("srvtype")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Todate).HasColumnName("todate");

                entity.Property(e => e.TourItem)
                    .HasColumnName("tour_item")
                    .HasMaxLength(250);

                entity.Property(e => e.Unitprice)
                    .HasColumnName("unitprice")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vatin).HasColumnName("vatin");

                entity.Property(e => e.Vatout).HasColumnName("vatout");
            });

            modelBuilder.Entity<CodeSupplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi).HasMaxLength(150);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Ghichu).HasMaxLength(250);

                entity.Property(e => e.Lydo).HasMaxLength(250);

                entity.Property(e => e.Masothue).HasMaxLength(50);

                entity.Property(e => e.Nganhnghe).HasMaxLength(50);

                entity.Property(e => e.Ngayyeucau)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoilienhe).HasMaxLength(150);

                entity.Property(e => e.Nguoiyeucau).HasMaxLength(50);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tapdoan).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(50);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(100);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.Tinhtp).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(200);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("companyId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(250);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(80);

                entity.Property(e => e.Headoffice)
                    .HasColumnName("headoffice")
                    .HasMaxLength(50);

                entity.Property(e => e.Msthue)
                    .HasColumnName("msthue")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(80);

                entity.Property(e => e.Nation)
                    .HasColumnName("nation")
                    .HasMaxLength(50);

                entity.Property(e => e.Natione)
                    .HasColumnName("natione")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoidaidien)
                    .HasColumnName("nguoidaidien")
                    .HasMaxLength(50);

                entity.Property(e => e.Nguoilienhe)
                    .HasColumnName("nguoilienhe")
                    .HasMaxLength(50);

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Codealpha).HasMaxLength(10);

                entity.Property(e => e.Continent).HasMaxLength(50);

                entity.Property(e => e.Nation).HasMaxLength(50);

                entity.Property(e => e.Natione).HasMaxLength(50);

                entity.Property(e => e.TelCode)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Territory).HasMaxLength(50);
            });

            modelBuilder.Entity<Dichvu>(entity =>
            {
                entity.HasKey(e => e.Iddichvu);

                entity.Property(e => e.Iddichvu)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tendv).HasMaxLength(50);
            });

            modelBuilder.Entity<Dieuxe>(entity =>
            {
                entity.HasKey(e => e.Idxe);

                entity.Property(e => e.Idxe)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Chiphi).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Del).HasColumnName("del");

                entity.Property(e => e.Denngay).HasColumnType("datetime");

                entity.Property(e => e.Diemdon).HasMaxLength(50);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Dongiakm).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Giodon)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Kmnl).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Laixe).HasMaxLength(50);

                entity.Property(e => e.Loaixe).HasMaxLength(50);

                entity.Property(e => e.Lotrinh).HasMaxLength(50);

                entity.Property(e => e.Ngaydon).HasColumnType("date");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Soxe)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Supplierid)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.YeuCauXe)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DmLoaiphong>(entity =>
            {
                entity.HasKey(e => e.Loaiphong);

                entity.ToTable("dmLoaiphong");

                entity.Property(e => e.Loaiphong)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dmchinhanh>(entity =>
            {
                entity.Property(e => e.Diachi).HasMaxLength(100);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.Macn)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Masothue).HasMaxLength(20);

                entity.Property(e => e.Tencn).HasMaxLength(50);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);
            });

            modelBuilder.Entity<Dmdaily>(entity =>
            {
                entity.Property(e => e.Daily).HasMaxLength(50);

                entity.Property(e => e.Diachi).HasMaxLength(100);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Macn)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tendaily).HasMaxLength(100);
            });

            modelBuilder.Entity<Dmdiemtq>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_Dmdiemtq_1");

                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Congno)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Diemtq).HasMaxLength(150);

                entity.Property(e => e.Giatreem).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Giave).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(10);

                entity.Property(e => e.Tilelai).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Tinhtp).HasMaxLength(15);
            });

            modelBuilder.Entity<Dmdiemtq1>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_Dmdiemtq");

                entity.ToTable("Dmdiemtq_");

                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Congno)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Diemtq).HasMaxLength(150);

                entity.Property(e => e.Giatreem).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Giave).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Thanhpho)
                    .HasColumnName("thanhpho")
                    .HasMaxLength(10);

                entity.Property(e => e.Tilelai).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Tinhtp).HasMaxLength(15);
            });

            modelBuilder.Entity<DsLoaixe>(entity =>
            {
                entity.HasKey(e => e.Loaixe);
            });

            modelBuilder.Entity<Haucan>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dongia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Donvitinh).HasMaxLength(10);

                entity.Property(e => e.Ghichu).HasMaxLength(50);

                entity.Property(e => e.Mahh).HasMaxLength(15);

                entity.Property(e => e.Ngayyeucau).HasColumnType("datetime");

                entity.Property(e => e.Nguoiyeucau).HasMaxLength(50);

                entity.Property(e => e.Sgtcode).HasMaxLength(17);

                entity.Property(e => e.Thanhtien).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dbl).HasColumnName("dbl");

                entity.Property(e => e.Dblcost)
                    .HasColumnName("dblcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Dblpax).HasColumnName("dblpax");

                entity.Property(e => e.Extdbl).HasColumnName("extdbl");

                entity.Property(e => e.Extdblcost)
                    .HasColumnName("extdblcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Extsgl).HasColumnName("extsgl");

                entity.Property(e => e.Extsglcost)
                    .HasColumnName("extsglcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Exttwn).HasColumnName("exttwn");

                entity.Property(e => e.Exttwncost)
                    .HasColumnName("exttwncost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Homestay).HasColumnName("homestay");

                entity.Property(e => e.Homestaycost)
                    .HasColumnName("homestaycost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Homestaynote)
                    .IsRequired()
                    .HasColumnName("homestaynote")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Homestaypax).HasColumnName("homestaypax");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(150);

                entity.Property(e => e.Oth).HasColumnName("oth");

                entity.Property(e => e.Othcost)
                    .HasColumnName("othcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Othpax).HasColumnName("othpax");

                entity.Property(e => e.Othtype)
                    .HasColumnName("othtype")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgl).HasColumnName("sgl");

                entity.Property(e => e.Sglcost)
                    .HasColumnName("sglcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Sglpax).HasColumnName("sglpax");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Twn).HasColumnName("twn");

                entity.Property(e => e.Twncost)
                    .HasColumnName("twncost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Twnpax).HasColumnName("twnpax");
            });

            modelBuilder.Entity<HotelDel>(entity =>
            {
                entity.ToTable("Hotel_del");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dbl).HasColumnName("dbl");

                entity.Property(e => e.Dblcost)
                    .HasColumnName("dblcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Dblpax).HasColumnName("dblpax");

                entity.Property(e => e.Extdbl).HasColumnName("extdbl");

                entity.Property(e => e.Extdblcost)
                    .HasColumnName("extdblcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Extsgl).HasColumnName("extsgl");

                entity.Property(e => e.Extsglcost)
                    .HasColumnName("extsglcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Exttwn).HasColumnName("exttwn");

                entity.Property(e => e.Exttwncost)
                    .HasColumnName("exttwncost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Homestay).HasColumnName("homestay");

                entity.Property(e => e.Homestaycost)
                    .HasColumnName("homestaycost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Homestaynote)
                    .IsRequired()
                    .HasColumnName("homestaynote")
                    .HasMaxLength(200);

                entity.Property(e => e.Homestaypax).HasColumnName("homestaypax");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(150);

                entity.Property(e => e.Oth).HasColumnName("oth");

                entity.Property(e => e.Othcost)
                    .HasColumnName("othcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Othpax).HasColumnName("othpax");

                entity.Property(e => e.Othtype)
                    .HasColumnName("othtype")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgl).HasColumnName("sgl");

                entity.Property(e => e.Sglcost)
                    .HasColumnName("sglcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Sglpax).HasColumnName("sglpax");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Twn).HasColumnName("twn");

                entity.Property(e => e.Twncost)
                    .HasColumnName("twncost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Twnpax).HasColumnName("twnpax");
            });

            modelBuilder.Entity<Hoteltemp>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dbl).HasColumnName("dbl");

                entity.Property(e => e.Dblcost)
                    .HasColumnName("dblcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Dblpax).HasColumnName("dblpax");

                entity.Property(e => e.Extdbl).HasColumnName("extdbl");

                entity.Property(e => e.Extdblcost)
                    .HasColumnName("extdblcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Extsgl).HasColumnName("extsgl");

                entity.Property(e => e.Extsglcost)
                    .HasColumnName("extsglcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Exttwn).HasColumnName("exttwn");

                entity.Property(e => e.Exttwncost)
                    .HasColumnName("exttwncost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Homestay).HasColumnName("homestay");

                entity.Property(e => e.Homestaycost)
                    .HasColumnName("homestaycost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Homestaynote)
                    .IsRequired()
                    .HasColumnName("homestaynote")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Homestaypax).HasColumnName("homestaypax");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(150);

                entity.Property(e => e.Oth).HasColumnName("oth");

                entity.Property(e => e.Othcost)
                    .HasColumnName("othcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Othpax).HasColumnName("othpax");

                entity.Property(e => e.Othtype)
                    .HasColumnName("othtype")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgl).HasColumnName("sgl");

                entity.Property(e => e.Sglcost)
                    .HasColumnName("sglcost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Sglpax).HasColumnName("sglpax");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Twn).HasColumnName("twn");

                entity.Property(e => e.Twncost)
                    .HasColumnName("twncost")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Twnpax).HasColumnName("twnpax");
            });

            modelBuilder.Entity<Huongdan>(entity =>
            {
                entity.HasKey(e => e.IdHuongdan);

                entity.Property(e => e.IdHuongdan)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Batdau).HasColumnType("datetime");

                entity.Property(e => e.Batdautai).HasMaxLength(20);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Del).HasColumnName("del");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ghichu).HasMaxLength(100);

                entity.Property(e => e.Hopdongcty)
                    .HasColumnName("hopdongcty")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ketthuc).HasColumnType("datetime");

                entity.Property(e => e.Ketthuctai).HasMaxLength(20);

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ndcongviec).HasMaxLength(100);

                entity.Property(e => e.Ngayyeucau)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngoaingu).HasMaxLength(50);

                entity.Property(e => e.Phididoan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Phidontien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Tenhd).HasMaxLength(50);

                entity.Property(e => e.Traphi).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Khachtour>(entity =>
            {
                entity.HasKey(e => e.Idkhach)
                    .HasName("PK_khachtour");

                entity.Property(e => e.Idkhach)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cmnd)
                    .HasColumnName("cmnd")
                    .HasMaxLength(50);

                entity.Property(e => e.Del).HasColumnName("del");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(100);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("ghichu")
                    .HasMaxLength(200);

                entity.Property(e => e.Hieuluchc)
                    .HasColumnName("hieuluchc")
                    .HasColumnType("date");

                entity.Property(e => e.Hochieu)
                    .HasColumnName("hochieu")
                    .HasMaxLength(50);

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(50);

                entity.Property(e => e.Loaiphong)
                    .HasColumnName("loaiphong")
                    .HasMaxLength(50);

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasColumnName("makh")
                    .HasMaxLength(50);

                entity.Property(e => e.Ngaycaphc)
                    .HasColumnName("ngaycaphc")
                    .HasColumnType("date");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasColumnType("date");

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Prn)
                    .HasColumnName("prn")
                    .HasMaxLength(20);

                entity.Property(e => e.Quoctich)
                    .HasColumnName("quoctich")
                    .HasMaxLength(50);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Vmb).HasColumnName("vmb");
            });

            modelBuilder.Entity<Loaikhach>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Tenloaikhach)
                    .HasColumnName("tenloaikhach")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Loaivisa>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Visa)
                    .HasColumnName("visa")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoginModel>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Ngoaite>(entity =>
            {
                entity.HasKey(e => e.MaNt);

                entity.Property(e => e.MaNt)
                    .HasColumnName("MaNT")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TenNt)
                    .HasColumnName("TenNT")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => new { e.Sgtcode, e.Stt });

                entity.Property(e => e.Sgtcode).HasMaxLength(17);

                entity.Property(e => e.Stt).HasMaxLength(4);

                entity.Property(e => e.HieulucHc).HasColumnName("HieulucHC");

                entity.Property(e => e.Hochieu).HasMaxLength(20);

                entity.Property(e => e.Hoten).HasMaxLength(50);

                entity.Property(e => e.Loginname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NgaycapHc).HasColumnName("NgaycapHC");

                entity.Property(e => e.Quoctich).HasMaxLength(20);
            });

            modelBuilder.Entity<Passtype>(entity =>
            {
                entity.ToTable("passtype");

                entity.Property(e => e.PasstypeId)
                    .HasColumnName("passtypeId")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Phongban>(entity =>
            {
                entity.HasKey(e => e.Maphong);

                entity.Property(e => e.Maphong)
                    .HasColumnName("maphong")
                    .HasMaxLength(5);

                entity.Property(e => e.Macode)
                    .HasColumnName("macode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tenphong)
                    .HasColumnName("tenphong")
                    .HasMaxLength(100);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<Quocgia>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Nation).HasMaxLength(50);

                entity.Property(e => e.Natione).HasMaxLength(50);

                entity.Property(e => e.Telcode)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sightseeing>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount).HasColumnType("decimal(12, 1)");

                entity.Property(e => e.ChildernPrice).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Chinhanh).HasMaxLength(3);

                entity.Property(e => e.Codedtq)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Httt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaxPrice).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Serial)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SightseeingDel>(entity =>
            {
                entity.ToTable("Sightseeing_del");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Amount).HasColumnType("decimal(12, 1)");

                entity.Property(e => e.ChildernPrice).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Chinhanh).HasMaxLength(3);

                entity.Property(e => e.Codedtq)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Httt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PaxPrice).HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Serial)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SightseeingTemp>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Childernprice)
                    .HasColumnName("childernprice")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Codedtq)
                    .HasColumnName("codedtq")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .HasColumnName("debit")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Httt)
                    .HasColumnName("httt")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Paxprice)
                    .HasColumnName("paxprice")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(50);

                entity.Property(e => e.Srvprofit)
                    .HasColumnName("srvprofit")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Vatin).HasColumnName("vatin");

                entity.Property(e => e.Vatout).HasColumnName("vatout");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Codecn).HasMaxLength(5);

                entity.Property(e => e.Diachi).HasMaxLength(150);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Masothue).HasMaxLength(50);

                entity.Property(e => e.Nganhnghe).HasMaxLength(150);

                entity.Property(e => e.Nguoilienhe).HasMaxLength(70);

                entity.Property(e => e.Nguoitao).HasMaxLength(50);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(70);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(70);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<Thanhpho>(entity =>
            {
                entity.HasKey(e => e.Matp);

                entity.Property(e => e.Matp).HasMaxLength(10);

                entity.Property(e => e.Mien)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tentp).HasMaxLength(50);
            });

            modelBuilder.Entity<Thongbao>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Diengiai).HasMaxLength(500);

                entity.Property(e => e.IdTourProg).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Iddichvu)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaydv).HasColumnType("datetime");

                entity.Property(e => e.Nguoinhan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nguoinhap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.SrvcodeNew)
                    .HasColumnName("srvcodeNew")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SrvcodeOld)
                    .HasColumnName("srvcodeOld")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierIdNew)
                    .HasColumnName("supplierIdNew")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierIdOld)
                    .HasColumnName("supplierIdOld")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TourTempNote>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_TourTempNote_1");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tourinf>(entity =>
            {
                entity.HasKey(e => e.Sgtcode);

                entity.ToTable("tourinf");

                entity.HasIndex(e => e.Chinhanh)
                    .HasName("tourinfIndex_chinhanh");

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Arr)
                    .HasColumnName("arr")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cancel)
                    .HasColumnName("cancel")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cancelnote)
                    .HasColumnName("cancelnote")
                    .HasMaxLength(150);

                entity.Property(e => e.Childern).HasColumnName("childern");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanhtao)
                    .IsRequired()
                    .HasColumnName("chinhanhtao")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("companyId")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Concernto)
                    .HasColumnName("concernto")
                    .HasMaxLength(50);

                entity.Property(e => e.Createtour)
                    .HasColumnName("createtour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dep)
                    .HasColumnName("dep")
                    .HasColumnType("datetime");

                entity.Property(e => e.Departcreate)
                    .HasColumnName("departcreate")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Departoperator)
                    .HasColumnName("departoperator")
                    .HasMaxLength(50);

                entity.Property(e => e.Entryby)
                    .HasColumnName("entryby")
                    .HasMaxLength(50);

                entity.Property(e => e.Entryport)
                    .HasColumnName("entryport")
                    .HasMaxLength(50);

                entity.Property(e => e.Khachle).HasColumnName("khachle");

                entity.Property(e => e.Locktour)
                    .HasColumnName("locktour")
                    .HasColumnType("datetime");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(250);

                entity.Property(e => e.Operators)
                    .HasColumnName("operators")
                    .HasMaxLength(50);

                entity.Property(e => e.PasstypeId)
                    .HasColumnName("passtypeId")
                    .HasMaxLength(50);

                entity.Property(e => e.Pax).HasColumnName("pax");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasMaxLength(150);

                entity.Property(e => e.Revenue)
                    .HasColumnName("revenue")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Routing)
                    .HasColumnName("routing")
                    .HasMaxLength(100);

                entity.Property(e => e.TourkindId).HasColumnName("tourkindId");

                entity.Property(e => e.Userlock)
                    .HasColumnName("userlock")
                    .HasMaxLength(50);

                entity.Property(e => e.Visa)
                    .HasColumnName("visa")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Tourkind>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TourkindInf).HasMaxLength(50);
            });

            modelBuilder.Entity<Tournode>(entity =>
            {
                entity.HasKey(e => e.Sgtcode);

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tourprog>(entity =>
            {
                entity.HasIndex(e => new { e.Sgtcode, e.Dieuhanh })
                    .HasName("tourProgIndex_sgtcode");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Airtype)
                    .HasColumnName("airtype")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(14, 1)");

                entity.Property(e => e.Arr)
                    .HasColumnName("arr")
                    .HasMaxLength(100);

                entity.Property(e => e.Carguide)
                    .HasColumnName("carguide")
                    .HasMaxLength(100);

                entity.Property(e => e.Carrier)
                    .HasColumnName("carrier")
                    .HasMaxLength(50);

                entity.Property(e => e.Childern).HasColumnName("childern");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Debit).HasColumnName("debit");

                entity.Property(e => e.Dep)
                    .HasColumnName("dep")
                    .HasMaxLength(100);

                entity.Property(e => e.Deposit)
                    .HasColumnName("deposit")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Dieuhanh)
                    .HasColumnName("dieuhanh")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Foc).HasColumnName("foc");

                entity.Property(e => e.Infant).HasColumnName("infant");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Lydohuydv)
                    .HasColumnName("lydohuydv")
                    .HasMaxLength(250)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngayhuydv)
                    .HasColumnName("ngayhuydv")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaythang)
                    .HasColumnName("ngaythang")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoihuydv)
                    .HasColumnName("nguoihuydv")
                    .HasMaxLength(50);

                entity.Property(e => e.Pax).HasColumnName("pax");

                entity.Property(e => e.Pickuptime)
                    .HasColumnName("pickuptime")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Srvcode)
                    .HasColumnName("srvcode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Srvnode)
                    .HasColumnName("srvnode")
                    .HasMaxLength(500);

                entity.Property(e => e.Srvtype)
                    .HasColumnName("srvtype")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TourItem)
                    .HasColumnName("tour_item")
                    .HasMaxLength(200);

                entity.Property(e => e.Unitpricea)
                    .HasColumnName("unitpricea")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Unitpricec)
                    .HasColumnName("unitpricec")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Unitpricei)
                    .HasColumnName("unitpricei")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Vatin).HasColumnName("vatin");

                entity.Property(e => e.Vatout).HasColumnName("vatout");

                entity.HasOne(d => d.SgtcodeNavigation)
                    .WithMany(p => p.Tourprog)
                    .HasForeignKey(d => d.Sgtcode)
                    .HasConstraintName("FK_Tourprog_tourinf");
            });

            modelBuilder.Entity<TourprogDel>(entity =>
            {
                entity.ToTable("Tourprog_del");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Airtype)
                    .HasColumnName("airtype")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(14, 1)");

                entity.Property(e => e.Arr)
                    .HasColumnName("arr")
                    .HasMaxLength(100);

                entity.Property(e => e.Carguide)
                    .HasColumnName("carguide")
                    .HasMaxLength(100);

                entity.Property(e => e.Carrier)
                    .HasColumnName("carrier")
                    .HasMaxLength(50);

                entity.Property(e => e.Childern).HasColumnName("childern");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Debit).HasColumnName("debit");

                entity.Property(e => e.Dep)
                    .HasColumnName("dep")
                    .HasMaxLength(100);

                entity.Property(e => e.Deposit)
                    .HasColumnName("deposit")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Dieuhanh)
                    .HasColumnName("dieuhanh")
                    .HasMaxLength(50);

                entity.Property(e => e.Foc).HasColumnName("foc");

                entity.Property(e => e.Infant).HasColumnName("infant");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Lydohuydv)
                    .HasColumnName("lydohuydv")
                    .HasMaxLength(250);

                entity.Property(e => e.Ngayhuydv)
                    .HasColumnName("ngayhuydv")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaythang)
                    .HasColumnName("ngaythang")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nguoihuydv)
                    .HasColumnName("nguoihuydv")
                    .HasMaxLength(50);

                entity.Property(e => e.Pax).HasColumnName("pax");

                entity.Property(e => e.Pickuptime)
                    .HasColumnName("pickuptime")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Sgtcode)
                    .HasColumnName("sgtcode")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Srvcode)
                    .HasColumnName("srvcode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Srvnode)
                    .HasColumnName("srvnode")
                    .HasMaxLength(500);

                entity.Property(e => e.Srvtype)
                    .HasColumnName("srvtype")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TourItem)
                    .HasColumnName("tour_item")
                    .HasMaxLength(200);

                entity.Property(e => e.Unitpricea)
                    .HasColumnName("unitpricea")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Unitpricec)
                    .HasColumnName("unitpricec")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Unitpricei)
                    .HasColumnName("unitpricei")
                    .HasColumnType("decimal(12, 1)");

                entity.Property(e => e.Vatin).HasColumnName("vatin");

                entity.Property(e => e.Vatout).HasColumnName("vatout");
            });

            modelBuilder.Entity<Tourprogtemp>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Airtype)
                    .HasColumnName("airtype")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(14, 1)");

                entity.Property(e => e.Arr)
                    .HasColumnName("arr")
                    .HasMaxLength(100);

                entity.Property(e => e.Carguide)
                    .HasColumnName("carguide")
                    .HasMaxLength(100);

                entity.Property(e => e.Carrier)
                    .HasColumnName("carrier")
                    .HasMaxLength(50);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Debit).HasColumnName("debit");

                entity.Property(e => e.Dep)
                    .HasColumnName("dep")
                    .HasMaxLength(100);

                entity.Property(e => e.Foc).HasColumnName("foc");

                entity.Property(e => e.Pickuptime)
                    .HasColumnName("pickuptime")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Srvcode)
                    .HasColumnName("srvcode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Srvnode)
                    .HasColumnName("srvnode")
                    .HasMaxLength(300);

                entity.Property(e => e.Srvtype)
                    .HasColumnName("srvtype")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TourItem)
                    .HasColumnName("tour_item")
                    .HasMaxLength(200);

                entity.Property(e => e.Unitpricea)
                    .HasColumnName("unitpricea")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Unitpricec)
                    .HasColumnName("unitpricec")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Unitpricei)
                    .HasColumnName("unitpricei")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.Vatin).HasColumnName("vatin");

                entity.Property(e => e.Vatout).HasColumnName("vatout");

                entity.HasOne(d => d.CodeNavigation)
                    .WithMany(p => p.Tourprogtemp)
                    .HasForeignKey(d => d.Code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tourprogtemp_Tourtemplate");
            });

            modelBuilder.Entity<Tourtemplate>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Chudetour).HasMaxLength(50);

                entity.Property(e => e.Nguoitao)
                    .HasColumnName("nguoitao")
                    .HasMaxLength(50);

                entity.Property(e => e.Songay).HasDefaultValueSql("((1))");

                entity.Property(e => e.Tentour).HasMaxLength(150);

                entity.Property(e => e.Tourkind)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tuyentq).HasMaxLength(500);
            });

            modelBuilder.Entity<Tuyentq>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tentuyen).HasMaxLength(150);

                entity.Property(e => e.Tuyen).HasMaxLength(120);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Hoten).HasMaxLength(50);

                entity.Property(e => e.Khachdoan).HasColumnName("khachdoan");

                entity.Property(e => e.Khachle).HasColumnName("khachle");

                entity.Property(e => e.Maphong)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<VDmdiemtq>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDmdiemtq");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Diemtq).HasMaxLength(150);

                entity.Property(e => e.Tinhtp)
                    .HasColumnName("tinhtp")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
