using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addDatCocTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatCocs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDatCoc = table.Column<DateTime>(nullable: true),
                    IdTour = table.Column<decimal>(nullable: false),
                    SoBienNhan = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
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
                    LoaiTien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TyGia = table.Column<decimal>(nullable: false),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatCocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatCocs_Tours_IdTour",
                        column: x => x.IdTour,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatCocs_IdTour",
                table: "DatCocs",
                column: "IdTour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatCocs");
        }
    }
}
