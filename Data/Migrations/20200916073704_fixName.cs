using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenDanhMuc",
                table: "CTVATs",
                newName: "TenKhoanMuc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenKhoanMuc",
                table: "CTVATs",
                newName: "TenDanhMuc");
        }
    }
}
