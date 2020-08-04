using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class intToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Invoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
