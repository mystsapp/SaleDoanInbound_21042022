using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienNhans_TourIBs_varchar(10",
                table: "BienNhans");

            migrationBuilder.DropIndex(
                name: "IX_BienNhans_varchar(10",
                table: "BienNhans");

            migrationBuilder.AlterColumn<string>(
                name: "varchar(10",
                table: "BienNhans",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "TouIBId",
                table: "BienNhans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BienNhans_TouIBId",
                table: "BienNhans",
                column: "TouIBId");

            migrationBuilder.AddForeignKey(
                name: "FK_BienNhans_TourIBs_TouIBId",
                table: "BienNhans",
                column: "TouIBId",
                principalTable: "TourIBs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienNhans_TourIBs_TouIBId",
                table: "BienNhans");

            migrationBuilder.DropIndex(
                name: "IX_BienNhans_TouIBId",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "TouIBId",
                table: "BienNhans");

            migrationBuilder.AlterColumn<string>(
                name: "varchar(10",
                table: "BienNhans",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_BienNhans_varchar(10",
                table: "BienNhans",
                column: "varchar(10");

            migrationBuilder.AddForeignKey(
                name: "FK_BienNhans_TourIBs_varchar(10",
                table: "BienNhans",
                column: "varchar(10",
                principalTable: "TourIBs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
