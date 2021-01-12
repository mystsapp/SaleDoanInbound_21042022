using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixKhachHang3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sgtcode",
                table: "KhachHangs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sgtcode",
                table: "KhachHangs",
                type: "varchar(17)",
                maxLength: 17,
                nullable: true);
        }
    }
}
