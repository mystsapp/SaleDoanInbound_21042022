using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixCacNDHuyTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayXoa",
                table: "CacNoiDungHuyTours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NguoiXoa",
                table: "CacNoiDungHuyTours",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Xoa",
                table: "CacNoiDungHuyTours",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayXoa",
                table: "CacNoiDungHuyTours");

            migrationBuilder.DropColumn(
                name: "NguoiXoa",
                table: "CacNoiDungHuyTours");

            migrationBuilder.DropColumn(
                name: "Xoa",
                table: "CacNoiDungHuyTours");
        }
    }
}
