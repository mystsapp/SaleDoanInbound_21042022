using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_DMKhachHang_IdKH",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_LoaiTours_IdLoaiTour",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_IdKH",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_IdLoaiTour",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "BatDau",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "IdKH",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "IdLoaiTour",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "KetThuc",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NguyenNhanHuyThau",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "ChiNhanhDH",
                table: "Tours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Tours",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "Tours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KhachLe",
                table: "Tours",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LoaiTien",
                table: "Tours",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoaiTourId",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NDHuyTourId",
                table: "Tours",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDen",
                table: "Tours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDi",
                table: "Tours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKhoa",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiKhoa",
                table: "Tours",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SKTreEm",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TyGia",
                table: "Tours",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiNhanhDH",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "KhachLe",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "LoaiTien",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "LoaiTourId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NDHuyTourId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NgayDen",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NgayDi",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NgayKhoa",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NguoiKhoa",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "SKTreEm",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TyGia",
                table: "Tours");

            migrationBuilder.AddColumn<DateTime>(
                name: "BatDau",
                table: "Tours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "IdKH",
                table: "Tours",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "IdLoaiTour",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "KetThuc",
                table: "Tours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NguyenNhanHuyThau",
                table: "Tours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_IdKH",
                table: "Tours",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_IdLoaiTour",
                table: "Tours",
                column: "IdLoaiTour");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_DMKhachHang_IdKH",
                table: "Tours",
                column: "IdKH",
                principalTable: "DMKhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_LoaiTours_IdLoaiTour",
                table: "Tours",
                column: "IdLoaiTour",
                principalTable: "LoaiTours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
