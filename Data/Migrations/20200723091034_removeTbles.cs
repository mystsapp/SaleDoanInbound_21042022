using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeTbles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatCocLogs");

            migrationBuilder.DropTable(
                name: "DMHoaHongs");

            migrationBuilder.DropTable(
                name: "DMKhachTours");

            migrationBuilder.DropTable(
                name: "RoomingListDs");

            migrationBuilder.DropTable(
                name: "ThongTinTours");

            migrationBuilder.DropTable(
                name: "TourTMPs");

            migrationBuilder.DropTable(
                name: "VeMayBays");

            migrationBuilder.DropTable(
                name: "DatCocs");

            migrationBuilder.DropTable(
                name: "RoomingLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatCocs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChungTuGoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DaiLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LoaiTien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NgayDatCoc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SoBienNhan = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenMay = table.Column<string>(type: "nvarchar(3000)", maxLength: 300, nullable: true),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatCocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMHoaHongs",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Id_DMKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sales = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SalesNM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoCMNN = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMHoaHongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMKhachTours",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienThoai = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    HieuLucHoChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoChieu = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgayCMND = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiCapCMND = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuocTich = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    SoCMND = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMKhachTours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomingLists",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayCheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenKhachSan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinTours",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoaiTin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoiDungTin = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinTours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourTMPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChoConLai = table.Column<int>(type: "int", nullable: false),
                    ChuDeTour = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    KetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KhachLe = table.Column<bool>(type: "bit", nullable: false),
                    MaCN = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTaoTour = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Sgtcode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    SoCho = table.Column<int>(type: "int", nullable: false),
                    TenGiaoDich = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TuyenTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTMPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeMayBays",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChuyenBay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiemDen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiemDi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GioDen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GioDi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LuotDiVe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayBay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SoVe_ADL_D = table.Column<int>(type: "int", nullable: false),
                    SoVe_ADL_V = table.Column<int>(type: "int", nullable: false),
                    SoVe_CHL_D = table.Column<int>(type: "int", nullable: false),
                    SoVe_CHL_V = table.Column<int>(type: "int", nullable: false),
                    SoVe_INF_D = table.Column<int>(type: "int", nullable: false),
                    SoVe_INF_V = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeMayBays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatCocLogs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChungTuGoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DaiLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdDatCoc = table.Column<long>(type: "bigint", nullable: false),
                    IdTour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoaiTienn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayDatCoc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiLamBN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiThaoTac = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SoBienNhan = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenMay = table.Column<string>(type: "nvarchar(3000)", maxLength: 300, nullable: true),
                    ThoiGianThaoTac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatCocLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DatCocLogs_DatCocs_IdDatCoc",
                        column: x => x.IdDatCoc,
                        principalTable: "DatCocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomingListDs",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdRoomingList = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KhachTour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LoaiPhong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomingListDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomingListDs_RoomingLists_IdRoomingList",
                        column: x => x.IdRoomingList,
                        principalTable: "RoomingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatCocLogs_IdDatCoc",
                table: "DatCocLogs",
                column: "IdDatCoc");

            migrationBuilder.CreateIndex(
                name: "IX_RoomingListDs_IdRoomingList",
                table: "RoomingListDs",
                column: "IdRoomingList");
        }
    }
}
