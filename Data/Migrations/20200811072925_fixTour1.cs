using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTour1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiNhanhDH",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DiemTQ",
                table: "Tours");

            migrationBuilder.AddColumn<int>(
                name: "ChiNhanhDHId",
                table: "Tours",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiNhanhDHId",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "ChiNhanhDH",
                table: "Tours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiemTQ",
                table: "Tours",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
