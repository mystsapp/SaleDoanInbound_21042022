using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixInvoiceHuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HuyBN",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHuy",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuyBN",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "NgayHuy",
                table: "Invoices");
        }
    }
}
