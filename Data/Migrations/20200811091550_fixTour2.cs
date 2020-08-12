using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTour2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaCN",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "ChiNhanhTaoId",
                table: "Tours",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiNhanhTaoId",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "MaCN",
                table: "Tours",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);
        }
    }
}
