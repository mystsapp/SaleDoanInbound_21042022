using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class delTongTienInBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TongTien",
                table: "BienNhans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TongTien",
                table: "BienNhans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
