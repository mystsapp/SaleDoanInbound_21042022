using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixCTBNTbl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ChiTietBNs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTTT",
                table: "ChiTietBNs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoTienCT",
                table: "ChiTietBNs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "BienNhans",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DienThoai",
                table: "BienNhans",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDung",
                table: "BienNhans",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoTien",
                table: "BienNhans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "HTTT",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "SoTienCT",
                table: "ChiTietBNs");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "DienThoai",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "NoiDung",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "SoTien",
                table: "BienNhans");
        }
    }
}
