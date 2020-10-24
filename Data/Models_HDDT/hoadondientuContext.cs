using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models_HDDT
{
    public partial class hoadondientuContext : DbContext
    {
        public hoadondientuContext()
        {
        }

        public hoadondientuContext(DbContextOptions<hoadondientuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Dschinhanh> Dschinhanh { get; set; }
        public virtual DbSet<Dsdangkyhd> Dsdangkyhd { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=118.68.170.128;Database=hoadondientu;Trusted_Connection=True;User Id=vanhong;Password=Hong@2019;Integrated Security=false;MultipleActiveResultSets=true;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.MaChiNhanh)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Dschinhanh>(entity =>
            {
                entity.HasKey(e => e.Machinhanh)
                    .HasName("PK_chinhanh");

                entity.ToTable("dschinhanh");

                entity.Property(e => e.Machinhanh)
                    .HasColumnName("machinhanh")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(250);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(50);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Masothue)
                    .HasColumnName("masothue")
                    .HasMaxLength(50);

                entity.Property(e => e.Maviettat)
                    .HasColumnName("maviettat")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tenchinhanh)
                    .HasColumnName("tenchinhanh")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Dsdangkyhd>(entity =>
            {
                entity.ToTable("dsdangkyhd");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activation).HasColumnName("activation");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasColumnName("chinhanh")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(120);

                entity.Property(e => e.Dienthoai)
                    .HasColumnName("dienthoai")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Kyhieuhd)
                    .IsRequired()
                    .HasColumnName("kyhieuhd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mainkey)
                    .HasColumnName("mainkey")
                    .HasColumnType("decimal(14, 0)");

                entity.Property(e => e.Masothue)
                    .IsRequired()
                    .HasColumnName("masothue")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mausohd)
                    .IsRequired()
                    .HasColumnName("mausohd")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaytaohd)
                    .HasColumnName("ngaytaohd")
                    .HasColumnType("datetime");

                entity.Property(e => e.Passdemo)
                    .HasColumnName("passdemo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Passsite)
                    .HasColumnName("passsite")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sitedemo)
                    .HasColumnName("sitedemo")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sitehddt)
                    .HasColumnName("sitehddt")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sohdden)
                    .IsRequired()
                    .HasColumnName("sohdden")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sohdtu)
                    .IsRequired()
                    .HasColumnName("sohdtu")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sohoadon)
                    .HasColumnName("sohoadon")
                    .HasColumnType("decimal(14, 0)");

                entity.Property(e => e.Stt).HasColumnName("stt");

                entity.Property(e => e.Sudungdenngay)
                    .HasColumnName("sudungdenngay")
                    .HasColumnType("date");

                entity.Property(e => e.Sudungtungay)
                    .HasColumnName("sudungtungay")
                    .HasColumnType("date");

                entity.Property(e => e.Userdemo)
                    .HasColumnName("userdemo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Usersite)
                    .HasColumnName("usersite")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
