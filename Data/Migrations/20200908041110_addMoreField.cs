using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addMoreField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "Invoices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenKhach",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DLHH",
                table: "CTVATs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DS",
                table: "CTVATs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TenDanhMuc",
                table: "CTVATs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TiengAnh",
                table: "CTVATs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TenKhach",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DLHH",
                table: "CTVATs");

            migrationBuilder.DropColumn(
                name: "DS",
                table: "CTVATs");

            migrationBuilder.DropColumn(
                name: "TenDanhMuc",
                table: "CTVATs");

            migrationBuilder.DropColumn(
                name: "TiengAnh",
                table: "CTVATs");
        }
    }
}
