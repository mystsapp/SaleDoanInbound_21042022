
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models_IB
{
    public class SaleDoanIBDbContext : DbContext
    {
        public SaleDoanIBDbContext(DbContextOptions<SaleDoanIBDbContext> options) : base(options)
        {

        }

        // SaleDoanIB
        public DbSet<CacNoiDungHuyTour> CacNoiDungHuyTours { get; set; }
        //public DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public DbSet<DatCoc> DatCocs { get; set; }
        public DbSet<DatCocLog> DatCocLogs { get; set; }
        public DbSet<DMDaiLy> DMDaiLies { get; set; }
        public DbSet<DMHoaHong> DMHoaHongs { get; set; }
        //public DbSet<DMKhachHang> DMKhachHangs { get; set; }
        public DbSet<DMKhachTour> DMKhachTours { get; set; }
        public DbSet<DMNganhNghe> DMNganhNghes { get; set; }
        public DbSet<KhuVuc> KhuVucs { get; set; }
        public DbSet<LoaiTour> LoaiTours { get; set; }

        //public DbSet<Nuoc> Nuocs { get; set; }  => qlTaiKhoan
        public DbSet<ThanhPho> ThanhPhos { get; set; }
        public DbSet<PhanKhuCN> PhanKhuCNs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoomingList> RoomingLists { get; set; }
        public DbSet<RoomingListD> RoomingListDs { get; set; }
        public DbSet<ThongTinTour> ThongTinTours { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }
        public DbSet<TourTMP> TourTMPs { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<UsrKhuCN> UsrKhuCNs { get; set; }
        public DbSet<VeMayBay> VeMayBays { get; set; }

        // SaleDoanIB --> Mr.Son
        public DbSet<TourIB> TourIBs { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<CTVAT> CTVATs { get; set; }
        public DbSet<BienNhan> BienNhans { get; set; }
        public DbSet<ChiTietBN> ChiTietBNs { get; set; }


    }
}
