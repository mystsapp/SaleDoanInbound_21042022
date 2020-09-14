using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addBNCTBNTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BienNhans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    TourId = table.Column<long>(nullable: false),
                    NgayBN = table.Column<DateTime>(nullable: false),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SK = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoiDungHuy = table.Column<long>(nullable: false),
                    KhachLe = table.Column<bool>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BienNhanId = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Descript = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBNs");

            migrationBuilder.DropTable(
                name: "BienNhans");
        }
    }
}
