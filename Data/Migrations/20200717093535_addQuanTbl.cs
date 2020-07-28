using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addQuanTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NguoiQH",
                table: "DMKhachHangs",
                newName: "NguoiLH");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySua",
                table: "DMKhachHangs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Quans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaQuocGia = table.Column<int>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quans");

            migrationBuilder.RenameColumn(
                name: "NguoiLH",
                table: "DMKhachHangs",
                newName: "NguoiQH");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySua",
                table: "DMKhachHangs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
