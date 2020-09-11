using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addLogFileInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "Invoices",
                type: "nvarchar(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "Invoices");
        }
    }
}
