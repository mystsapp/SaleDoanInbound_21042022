using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class delTbl : Migration
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
                    varchar12 = table.Column<string>(name: "varchar(12)", type: "nvarchar(12)", maxLength: 12, nullable: false),
                    nvarchar150 = table.Column<string>(name: "nvarchar(150)", type: "nvarchar(150)", maxLength: 150, nullable: true),
                    KhachLe = table.Column<bool>(type: "bit", nullable: false),
                    varchar3 = table.Column<string>(name: "varchar(3)", type: "nvarchar(3)", maxLength: 3, nullable: false),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NgayBN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoiDungHuy = table.Column<long>(type: "bigint", nullable: false),
                    SK = table.Column<int>(type: "int", nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50)", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienNhans", x => x.varchar12);
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
                    varchar12 = table.Column<string>(name: "varchar(12", type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Descript = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBNs_BienNhans_varchar(12",
                        column: x => x.varchar12,
                        principalTable: "BienNhans",
                        principalColumn: "varchar(12)",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BienNhans_TourId",
                table: "BienNhans",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBNs_varchar(12",
                table: "ChiTietBNs",
                column: "varchar(12");
        }
    }
}
