using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class quanToDiemDen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quans");

            migrationBuilder.CreateTable(
                name: "DiemDens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiemDen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaQuocGia = table.Column<int>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDens", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiemDens");

            migrationBuilder.CreateTable(
                name: "Quans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaQuocGia = table.Column<int>(type: "int", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    TenQuan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quans", x => x.Id);
                });
        }
    }
}
