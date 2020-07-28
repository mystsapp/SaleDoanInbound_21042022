using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class newTenTP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenDiemDen",
                table: "ThanhPhos",
                newName: "TenThanhPho");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenThanhPho",
                table: "ThanhPhos",
                newName: "TenDiemDen");
        }
    }
}
