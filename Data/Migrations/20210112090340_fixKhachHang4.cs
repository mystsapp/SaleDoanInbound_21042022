using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixKhachHang4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "KhachHangs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sgtcode",
                table: "KhachHangs",
                type: "varchar(17)",
                maxLength: 17,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "KhachHangs");

            migrationBuilder.DropColumn(
                name: "Sgtcode",
                table: "KhachHangs");
        }
    }
}
