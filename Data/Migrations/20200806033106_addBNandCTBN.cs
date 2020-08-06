using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addBNandCTBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "CTVATs",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descript",
                table: "CTVATs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BienNhans",
                columns: table => new
                {
                    varchar12 = table.Column<string>(name: "varchar(12", maxLength: 12, nullable: false),
                    varchar10 = table.Column<string>(name: "varchar(10", maxLength: 10, nullable: false),
                    NgayBN = table.Column<DateTime>(nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50", maxLength: 50, nullable: true),
                    SK = table.Column<int>(nullable: false),
                    nvarchar150 = table.Column<string>(name: "nvarchar(150", maxLength: 150, nullable: true),
                    varchar3 = table.Column<string>(name: "varchar(3", maxLength: 3, nullable: true),
                    TyGia = table.Column<decimal>(nullable: false),
                    NoiDungHuy = table.Column<long>(nullable: false),
                    KhachLe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienNhans", x => x.varchar12);
                    table.ForeignKey(
                        name: "FK_BienNhans_TourIBs_varchar(10",
                        column: x => x.varchar10,
                        principalTable: "TourIBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBNs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varchar12 = table.Column<string>(name: "varchar(12", maxLength: 12, nullable: true),
                    Descript = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBNs_BienNhans_varchar(12",
                        column: x => x.varchar12,
                        principalTable: "BienNhans",
                        principalColumn: "varchar(12",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BienNhans_varchar(10",
                table: "BienNhans",
                column: "varchar(10");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBNs_varchar(12",
                table: "ChiTietBNs",
                column: "varchar(12");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBNs");

            migrationBuilder.DropTable(
                name: "BienNhans");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "CTVATs",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Descript",
                table: "CTVATs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
