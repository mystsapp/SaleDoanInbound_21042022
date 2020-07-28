using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixNuocKV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nuocs_KhuVucs_IdKhuVuc",
                table: "Nuocs");

            migrationBuilder.DropIndex(
                name: "IX_Nuocs_IdKhuVuc",
                table: "Nuocs");

            migrationBuilder.AlterColumn<string>(
                name: "IdKhuVuc",
                table: "Nuocs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DatCocLogs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDatCoc = table.Column<long>(nullable: false),
                    NgayDatCoc = table.Column<DateTime>(nullable: false),
                    IdTour = table.Column<decimal>(nullable: false),
                    SoBienNhan = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    NguoiLamBN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DaiLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SoTien = table.Column<decimal>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChungTuGoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenMay = table.Column<string>(type: "nvarchar(3000)", maxLength: 300, nullable: true),
                    LoaiTienn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TyGia = table.Column<decimal>(nullable: false),
                    NguoiThaoTac = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThoiGianThaoTac = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatCocLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DatCocLogs_DatCocs_IdDatCoc",
                        column: x => x.IdDatCoc,
                        principalTable: "DatCocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatCocLogs_IdDatCoc",
                table: "DatCocLogs",
                column: "IdDatCoc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatCocLogs");

            migrationBuilder.AlterColumn<int>(
                name: "IdKhuVuc",
                table: "Nuocs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nuocs_IdKhuVuc",
                table: "Nuocs",
                column: "IdKhuVuc");

            migrationBuilder.AddForeignKey(
                name: "FK_Nuocs_KhuVucs_IdKhuVuc",
                table: "Nuocs",
                column: "IdKhuVuc",
                principalTable: "KhuVucs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
