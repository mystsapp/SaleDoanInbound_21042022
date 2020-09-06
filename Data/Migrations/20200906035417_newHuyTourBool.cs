using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class newHuyTourBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HuyTour",
                table: "Tours",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HuyVAT",
                table: "Invoices",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "HTTT",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 12,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuyTour",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "HTTT",
                table: "Invoices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HuyVAT",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
