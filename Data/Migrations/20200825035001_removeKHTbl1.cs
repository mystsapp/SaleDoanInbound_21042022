using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeKHTbl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhachHangs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMND = table.Column<int>(type: "int", nullable: false),
                    nvarchar250 = table.Column<string>(name: "nvarchar(250", type: "nvarchar(250)", maxLength: 250, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    varchar10 = table.Column<string>(name: "varchar(10", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHangs_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_TourId",
                table: "KhachHangs",
                column: "TourId");
        }
    }
}
