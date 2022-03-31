using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addHTTT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HTTT",
                table: "BienNhans",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HTTT",
                table: "BienNhans");
        }
    }
}
