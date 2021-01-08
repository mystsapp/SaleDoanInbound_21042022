using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addDelDSKhachHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "KhachHangs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "Del",
                table: "KhachHangs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DienThoai",
                table: "KhachHangs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Del",
                table: "KhachHangs");

            migrationBuilder.DropColumn(
                name: "DienThoai",
                table: "KhachHangs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "KhachHangs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
