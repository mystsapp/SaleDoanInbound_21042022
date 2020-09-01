using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixInvoiceTbl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienNhans_TourIBs_TouIBId",
                table: "BienNhans");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Tours_TourId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_BienNhans_TouIBId",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "TourIBId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TouIBId",
                table: "BienNhans");

            migrationBuilder.AlterColumn<long>(
                name: "TourId",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "varchar(10",
                table: "BienNhans",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_BienNhans_varchar(10",
                table: "BienNhans",
                column: "varchar(10");

            migrationBuilder.AddForeignKey(
                name: "FK_BienNhans_Tours_varchar(10",
                table: "BienNhans",
                column: "varchar(10",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Tours_TourId",
                table: "Invoices",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienNhans_Tours_varchar(10",
                table: "BienNhans");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Tours_TourId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_BienNhans_varchar(10",
                table: "BienNhans");

            migrationBuilder.AlterColumn<long>(
                name: "TourId",
                table: "Invoices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TourIBId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "varchar(10",
                table: "BienNhans",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "TouIBId",
                table: "BienNhans",
                type: "varchar(10)",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Tours_TourId",
                table: "Invoices",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
