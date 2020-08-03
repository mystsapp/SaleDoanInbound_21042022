using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTourIB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDungHuyId",
                table: "TourIBs");

            migrationBuilder.AddColumn<long>(
                name: "NoiDungHuy",
                table: "TourIBs",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDungHuy",
                table: "TourIBs");

            migrationBuilder.AddColumn<long>(
                name: "NoiDungHuyId",
                table: "TourIBs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
