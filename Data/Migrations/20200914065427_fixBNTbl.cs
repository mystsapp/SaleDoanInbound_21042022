using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixBNTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienNhans_Tours_varchar(10",
                table: "BienNhans");

            migrationBuilder.RenameColumn(
                name: "varchar(10",
                table: "BienNhans",
                newName: "TourId");

            migrationBuilder.RenameColumn(
                name: "nvarchar(50",
                table: "BienNhans",
                newName: "nvarchar(50)");

            migrationBuilder.RenameColumn(
                name: "varchar(3",
                table: "BienNhans",
                newName: "varchar(3)");

            migrationBuilder.RenameColumn(
                name: "nvarchar(150",
                table: "BienNhans",
                newName: "nvarchar(150)");

            migrationBuilder.RenameColumn(
                name: "varchar(12",
                table: "BienNhans",
                newName: "varchar(12)");

            migrationBuilder.RenameIndex(
                name: "IX_BienNhans_varchar(10",
                table: "BienNhans",
                newName: "IX_BienNhans_TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_BienNhans_Tours_TourId",
                table: "BienNhans",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienNhans_Tours_TourId",
                table: "BienNhans");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "BienNhans",
                newName: "varchar(10");

            migrationBuilder.RenameColumn(
                name: "nvarchar(50)",
                table: "BienNhans",
                newName: "nvarchar(50");

            migrationBuilder.RenameColumn(
                name: "varchar(3)",
                table: "BienNhans",
                newName: "varchar(3");

            migrationBuilder.RenameColumn(
                name: "nvarchar(150)",
                table: "BienNhans",
                newName: "nvarchar(150");

            migrationBuilder.RenameColumn(
                name: "varchar(12)",
                table: "BienNhans",
                newName: "varchar(12");

            migrationBuilder.RenameIndex(
                name: "IX_BienNhans_TourId",
                table: "BienNhans",
                newName: "IX_BienNhans_varchar(10");

            migrationBuilder.AddForeignKey(
                name: "FK_BienNhans_Tours_varchar(10",
                table: "BienNhans",
                column: "varchar(10",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
