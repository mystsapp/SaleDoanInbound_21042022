using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixCTBNTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "ChiTietBNs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySua",
                table: "ChiTietBNs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "ChiTietBNs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NguoiSua",
                table: "ChiTietBNs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "ChiTietBNs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "NgaySua",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "NguoiSua",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "ChiTietBNs");
        }
    }
}
