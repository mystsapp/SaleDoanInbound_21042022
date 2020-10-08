using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiNhanh_PhanKhuCNs_IdPhanKhuCN",
                table: "ChiNhanh");

            migrationBuilder.DropTable(
                name: "TourLogs");

            migrationBuilder.DropTable(
                name: "UsrKhuCNs");

            migrationBuilder.DropTable(
                name: "DMKhachHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanKhuCNs",
                table: "PhanKhuCNs");

            migrationBuilder.DropColumn(
                name: "TenKhuCN",
                table: "PhanKhuCNs");

            migrationBuilder.RenameTable(
                name: "PhanKhuCNs",
                newName: "PhanKhuCN");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanKhuCN",
                table: "PhanKhuCN",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiNhanh_PhanKhuCN_IdPhanKhuCN",
                table: "ChiNhanh",
                column: "IdPhanKhuCN",
                principalTable: "PhanKhuCN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiNhanh_PhanKhuCN_IdPhanKhuCN",
                table: "ChiNhanh");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanKhuCN",
                table: "PhanKhuCN");

            migrationBuilder.RenameTable(
                name: "PhanKhuCN",
                newName: "PhanKhuCNs");

            migrationBuilder.AddColumn<string>(
                name: "TenKhuCN",
                table: "PhanKhuCNs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanKhuCNs",
                table: "PhanKhuCNs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DMKhachHang",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_DMKhachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsrKhuCNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKhuCN = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsrKhuCNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsrKhuCNs_PhanKhuCNs_IdKhuCN",
                        column: x => x.IdKhuCN,
                        principalTable: "PhanKhuCNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsrKhuCNs_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    IdKH = table.Column<long>(type: "bigint", nullable: false),
                    IdLoaiTour = table.Column<int>(type: "int", nullable: false),
                    IdTour = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_TourLogs_DMKhachHang_IdKH",
                        column: x => x.IdKH,
                        principalTable: "DMKhachHang",
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

            migrationBuilder.CreateIndex(
                name: "IX_UsrKhuCNs_IdKhuCN",
                table: "UsrKhuCNs",
                column: "IdKhuCN");

            migrationBuilder.CreateIndex(
                name: "IX_UsrKhuCNs_IdUser",
                table: "UsrKhuCNs",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiNhanh_PhanKhuCNs_IdPhanKhuCN",
                table: "ChiNhanh",
                column: "IdPhanKhuCN",
                principalTable: "PhanKhuCNs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
