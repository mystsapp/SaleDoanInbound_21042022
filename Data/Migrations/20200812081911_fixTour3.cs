using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTour3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoanhtTuDK",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NgayHuytTour",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "Tours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DienThoai",
                table: "Tours",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DoanhThuDK",
                table: "Tours",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Tours",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaKH",
                table: "Tours",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHuyTour",
                table: "Tours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TenKH",
                table: "Tours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DienThoai",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DoanhThuDK",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "MaKH",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NgayHuyTour",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TenKH",
                table: "Tours");

            migrationBuilder.AddColumn<decimal>(
                name: "DoanhtTuDK",
                table: "Tours",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHuytTour",
                table: "Tours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
