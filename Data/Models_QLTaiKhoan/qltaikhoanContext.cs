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
        public virtual DbSet<Dichvu> Dichvu { get; set; }
        public virtual DbSet<Dmchinhanh> Dmchinhanh { get; set; }
        public virtual DbSet<Dmdaily> Dmdaily { get; set; }
        public virtual DbSet<Mien> Mien { get; set; }
        public virtual DbSet<Phongban> Phongban { get; set; }
        public virtual DbSet<Quan> Quan { get; set; }
        public virtual DbSet<Quocgia> Quocgia { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Temp> Temp { get; set; }
        public virtual DbSet<Thanhpho> Thanhpho { get; set; }
        public virtual DbSet<Thanhpho1> Thanhpho1 { get; set; }
        public virtual DbSet<Tinh> Tinh { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VTinh> VTinh { get; set; }
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

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUser_users");
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
                    .HasMaxLength(100);

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
                    .HasMaxLength(50);

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

            modelBuilder.Entity<Mien>(entity =>
            {
                entity.Property(e => e.MienId)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TenMien).HasMaxLength(50);
            });

            modelBuilder.Entity<Phongban>(entity =>
            {
                entity.HasKey(e => e.Maphong);

                entity.ToTable("phongban");

                entity.Property(e => e.Maphong)
                    .HasColumnName("maphong")
                    .HasMaxLength(5)
                    .IsUnicode(false);

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

                entity.Property(e => e.Id).HasColumnName("id");

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
                entity.HasKey(e => e.Code);

                entity.ToTable("supplier");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Codecn)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi).HasMaxLength(150);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Masothue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nganhnghe).HasMaxLength(150);

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoilienhe).HasMaxLength(150);

                entity.Property(e => e.Nguoitao).HasMaxLength(50);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(70);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(70);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Website).HasMaxLength(60);
            });

            modelBuilder.Entity<Temp>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("temp");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Field)
                    .HasColumnName("field")
                    .HasMaxLength(50);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Paymentcode)
                    .HasColumnName("paymentcode")
                    .HasMaxLength(50);

                entity.Property(e => e.Room).HasColumnName("room");

                entity.Property(e => e.Supplierid)
                    .HasColumnName("supplierid")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(50);

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

                entity.Property(e => e.Matp)
                    .HasColumnName("matp")
                    .HasMaxLength(6);

                entity.Property(e => e.Matinh)
                    .IsRequired()
                    .HasColumnName("matinh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tentp)
                    .HasColumnName("tentp")
                    .HasMaxLength(50);
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
