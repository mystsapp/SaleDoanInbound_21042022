using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeKHTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatCocs_Tours_IdTour",
                table: "DatCocs");

            migrationBuilder.DropForeignKey(
                name: "FK_DMHoaHongs_Tours_IdTour",
                table: "DMHoaHongs");

            migrationBuilder.DropForeignKey(
                name: "FK_DMKhachTours_Tours_IdTour",
                table: "DMKhachTours");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomingLists_Tours_IdTour",
                table: "RoomingLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinTours_Tours_IdTour",
                table: "ThongTinTours");

            migrationBuilder.DropForeignKey(
                name: "FK_VeMayBays_Tours_IdTour",
                table: "VeMayBays");

            migrationBuilder.DropTable(
                name: "TourLogs");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "DMKhachHangs");

            migrationBuilder.DropIndex(
                name: "IX_VeMayBays_IdTour",
                table: "VeMayBays");

            migrationBuilder.DropIndex(
                name: "IX_ThongTinTours_IdTour",
                table: "ThongTinTours");

            migrationBuilder.DropIndex(
                name: "IX_RoomingLists_IdTour",
                table: "RoomingLists");

            migrationBuilder.DropIndex(
                name: "IX_DMKhachTours_IdTour",
                table: "DMKhachTours");

            migrationBuilder.DropIndex(
                name: "IX_DMHoaHongs_IdTour",
                table: "DMHoaHongs");

            migrationBuilder.DropIndex(
                name: "IX_DatCocs_IdTour",
                table: "DatCocs");

            migrationBuilder.DropColumn(
                name: "IdTour",
                table: "VeMayBays");

            migrationBuilder.DropColumn(
                name: "IdTour",
                table: "ThongTinTours");

            migrationBuilder.DropColumn(
                name: "IdTour",
                table: "RoomingLists");

            migrationBuilder.DropColumn(
                name: "IdTour",
                table: "DMKhachTours");

            migrationBuilder.DropColumn(
                name: "IdTour",
                table: "DMHoaHongs");

            migrationBuilder.DropColumn(
                name: "IdTour",
                table: "DatCocs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IdTour",
                table: "VeMayBays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdTour",
                table: "ThongTinTours",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdTour",
                table: "RoomingLists",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdTour",
                table: "DMKhachTours",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdTour",
                table: "DMHoaHongs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdTour",
                table: "DatCocs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "DMKhachHangs",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Fax = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    GhiChuKhiGiaoDich = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MaCN = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    MaNganhNghe = table.Column<int>(type: "int", nullable: false),
                    MaQuocGia = table.Column<int>(type: "int", nullable: false),
                    MaThanhPho = table.Column<int>(type: "int", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiLH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Tax = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Telephone = table.Column<string>(type: "varchar(20)", maxLength: 15, nullable: true),
                    TenGiaoDich = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TenThuongMai = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMKhachHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourLogs",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChuDeTour = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChuongTrinhTour = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DaiLy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DichVu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DiemTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DoanhThuDK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoanhThuTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoiTacNuocNgoai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FileKhachDiTour = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    FileVeMayBay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HanXuatVMB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanhDong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HinhThucGiaoDich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdLoaiTour = table.Column<int>(type: "int", nullable: false),
                    IdTour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaiChuaVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaiGomVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaiThucTeGomVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LyDoNhanDu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MaCN = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    NgayDamPhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHuyTour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKyHopDong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhanDuTien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThanhLyHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiDaiDien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NguoiKyHopDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiThaoTac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguonTour = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NguyenNhanHuyThau = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NoiDungThanhLyHD = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SgtCode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    SoHopDong = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SoKhachDK = table.Column<int>(type: "int", nullable: false),
                    SoKhachTT = table.Column<int>(type: "int", nullable: false),
                    ThiTruong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThoiGianThaoTac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuyenTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    fileBienNhan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourLogs_DMKhachHangs_IdKH",
                        column: x => x.IdKH,
                        principalTable: "DMKhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourLogs_LoaiTours_IdLoaiTour",
                        column: x => x.IdLoaiTour,
                        principalTable: "LoaiTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChuDeTour = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChuongTrinhTour = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DaiLy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DichVu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DiemTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DoanhThuTT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoanhtTuDK = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoiTacNuocNgoai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FileBienNhan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    FileKhachDiTour = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    FileVeMayBay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HanXuatVe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhThucGiaoDich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdKH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdLoaiTour = table.Column<int>(type: "int", nullable: false),
                    KetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaiChuaVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaiGomVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaiThucTeGomVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    LyDoNhanDu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MaCN = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    NgayDamPhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHuytTour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKyHopDong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhanDuTien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThanhLyHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiDaiDien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NguoiKyHopDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguonTour = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NguyenNhanHuyThau = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NoiDungThanhLyHD = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Sgtcode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    SoHopDong = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SoKhachDK = table.Column<int>(type: "int", nullable: false),
                    SoKhachTT = table.Column<int>(type: "int", nullable: false),
                    ThiTruong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TuyenTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_DMKhachHangs_IdKH",
                        column: x => x.IdKH,
                        principalTable: "DMKhachHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_LoaiTours_IdLoaiTour",
                        column: x => x.IdLoaiTour,
                        principalTable: "LoaiTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeMayBays_IdTour",
                table: "VeMayBays",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinTours_IdTour",
                table: "ThongTinTours",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_RoomingLists_IdTour",
                table: "RoomingLists",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_DMKhachTours_IdTour",
                table: "DMKhachTours",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_DMHoaHongs_IdTour",
                table: "DMHoaHongs",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_DatCocs_IdTour",
                table: "DatCocs",
                column: "IdTour");

            migrationBuilder.CreateIndex(
                name: "IX_TourLogs_IdKH",
                table: "TourLogs",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_TourLogs_IdLoaiTour",
                table: "TourLogs",
                column: "IdLoaiTour");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_IdKH",
                table: "Tours",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_IdLoaiTour",
                table: "Tours",
                column: "IdLoaiTour");

            migrationBuilder.AddForeignKey(
                name: "FK_DatCocs_Tours_IdTour",
                table: "DatCocs",
                column: "IdTour",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DMHoaHongs_Tours_IdTour",
                table: "DMHoaHongs",
                column: "IdTour",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DMKhachTours_Tours_IdTour",
                table: "DMKhachTours",
                column: "IdTour",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomingLists_Tours_IdTour",
                table: "RoomingLists",
                column: "IdTour",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinTours_Tours_IdTour",
                table: "ThongTinTours",
                column: "IdTour",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VeMayBays_Tours_IdTour",
                table: "VeMayBays",
                column: "IdTour",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
