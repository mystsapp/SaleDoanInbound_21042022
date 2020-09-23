using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeBNCTBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBNs");

            migrationBuilder.DropTable(
                name: "BienNhans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BienNhans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DienThoai = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HuyBN = table.Column<bool>(type: "bit", nullable: true),
                    KhachLe = table.Column<bool>(type: "bit", nullable: false),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NDHuyBNId = table.Column<long>(type: "bigint", nullable: false),
                    NgayBN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHuy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SK = table.Column<int>(type: "int", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienNhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BienNhans_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBNs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BienNhanId = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Descript = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HTTT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SoTienCT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBNs_BienNhans_BienNhanId",
                        column: x => x.BienNhanId,
                        principalTable: "BienNhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BienNhans_TourId",
                table: "BienNhans",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBNs_BienNhanId",
                table: "ChiTietBNs",
                column: "BienNhanId");
        }
    }
}
