using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixKHTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nuocs");

            migrationBuilder.DropColumn(
                name: "NganhNghe",
                table: "DMKhachHangs");

            migrationBuilder.DropColumn(
                name: "QuocGia",
                table: "DMKhachHangs");

            migrationBuilder.DropColumn(
                name: "ThanhPho",
                table: "DMKhachHangs");

            migrationBuilder.AlterColumn<string>(
                name: "TenLoaiTour",
                table: "LoaiTours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenKhu",
                table: "KhuVucs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenNganhNghe",
                table: "DMNganhNghes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenGiaoDich",
                table: "DMKhachHangs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaNganhNghe",
                table: "DMKhachHangs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaQuocGia",
                table: "DMKhachHangs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaThanhPho",
                table: "DMKhachHangs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "CacNoiDungHuyTours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNganhNghe",
                table: "DMKhachHangs");

            migrationBuilder.DropColumn(
                name: "MaQuocGia",
                table: "DMKhachHangs");

            migrationBuilder.DropColumn(
                name: "MaThanhPho",
                table: "DMKhachHangs");

            migrationBuilder.AlterColumn<string>(
                name: "TenLoaiTour",
                table: "LoaiTours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "TenKhu",
                table: "KhuVucs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "TenNganhNghe",
                table: "DMNganhNghes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "TenGiaoDich",
                table: "DMKhachHangs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "NganhNghe",
                table: "DMKhachHangs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuocGia",
                table: "DMKhachHangs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThanhPho",
                table: "DMKhachHangs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "CacNoiDungHuyTours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateTable(
                name: "Nuocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKhuVuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Nguoisua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PhamVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenNuoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nuocs", x => x.Id);
                });
        }
    }
}
