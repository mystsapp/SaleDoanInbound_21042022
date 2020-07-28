using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addTourLogsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourLogs",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    IdTour = table.Column<decimal>(nullable: false),
                    SgtCode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    ChuDeTour = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ThiTruong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    BatDau = table.Column<DateTime>(nullable: false),
                    KetThuc = table.Column<DateTime>(nullable: false),
                    TuyenTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiemTQ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SoKhachDK = table.Column<int>(nullable: false),
                    DoanhThuDK = table.Column<decimal>(nullable: false),
                    IdKH = table.Column<decimal>(nullable: false),
                    NgayDamPhan = table.Column<DateTime>(nullable: false),
                    HinhThucGiaoDich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayKyHopDong = table.Column<DateTime>(nullable: false),
                    NguoiKyHopDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HanXuatVMB = table.Column<DateTime>(nullable: false),
                    NgayThanhLyHD = table.Column<DateTime>(nullable: false),
                    SoKhachTT = table.Column<int>(nullable: false),
                    DoanhThuTT = table.Column<decimal>(nullable: false),
                    ChuongTrinhTour = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NoiDungThanhLyHD = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DichVu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DaiLy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdLoaiTour = table.Column<int>(nullable: false),
                    TrangThai = table.Column<string>(nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MaCN = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    NgayNhanDuTien = table.Column<DateTime>(nullable: false),
                    LyDoNhanDu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SoHopDong = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LaiChuaVe = table.Column<decimal>(nullable: false),
                    LaiGomVe = table.Column<decimal>(nullable: false),
                    LaiThucTeGomVe = table.Column<decimal>(nullable: false),
                    NguyenNhanHuyThau = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NguonTour = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileKhachDiTour = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    FileVeMayBay = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    fileBienNhan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NguoiDaiDien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DoiTacNuocNgoai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NgayHuyTour = table.Column<DateTime>(nullable: false),
                    NguoiThaoTac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThoiGianThaoTac = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_TourLogs_IdKH",
                table: "TourLogs",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_TourLogs_IdLoaiTour",
                table: "TourLogs",
                column: "IdLoaiTour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourLogs");
        }
    }
}
