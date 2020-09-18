using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixBNndhuytbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NDHuyTourId",
                table: "BienNhans");

            migrationBuilder.AddColumn<long>(
                name: "NDHuyBNId",
                table: "BienNhans",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NDHuyBNId",
                table: "BienNhans");

            migrationBuilder.AddColumn<long>(
                name: "NDHuyTourId",
                table: "BienNhans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
