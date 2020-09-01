using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixInvoiceTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_TourIBs_TourIBId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_TourIBId",
                table: "Invoices");

            migrationBuilder.AlterColumn<long>(
                name: "TourIBId",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TourId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TourId",
                table: "Invoices",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Tours_TourId",
                table: "Invoices",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Tours_TourId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_TourId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Invoices");

            migrationBuilder.AlterColumn<string>(
                name: "TourIBId",
                table: "Invoices",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TourIBId",
                table: "Invoices",
                column: "TourIBId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_TourIBs_TourIBId",
                table: "Invoices",
                column: "TourIBId",
                principalTable: "TourIBs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
