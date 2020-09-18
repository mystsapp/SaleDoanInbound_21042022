using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixBNHuyTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDungHuy",
                table: "BienNhans");

            migrationBuilder.AddColumn<bool>(
                name: "HuyBN",
                table: "BienNhans",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NDHuyTourId",
                table: "BienNhans",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHuy",
                table: "BienNhans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HuyBN",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "NDHuyTourId",
                table: "BienNhans");

            migrationBuilder.DropColumn(
                name: "NgayHuy",
                table: "BienNhans");

            migrationBuilder.AddColumn<long>(
                name: "NoiDungHuy",
                table: "BienNhans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
