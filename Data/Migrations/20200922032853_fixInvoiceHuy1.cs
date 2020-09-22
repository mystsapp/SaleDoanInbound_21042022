using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixInvoiceHuy1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuyBN",
                table: "Invoices");

            migrationBuilder.AddColumn<bool>(
                name: "HuyInvoice",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuyInvoice",
                table: "Invoices");

            migrationBuilder.AddColumn<bool>(
                name: "HuyBN",
                table: "Invoices",
                type: "bit",
                nullable: true);
        }
    }
}
