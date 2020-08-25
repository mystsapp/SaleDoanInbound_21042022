using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addKHTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<int>(nullable: false),
                    varchar10 = table.Column<string>(name: "varchar(10", maxLength: 10, nullable: true),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50", maxLength: 50, nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    GioiTinh = table.Column<bool>(nullable: false),
                    CMND = table.Column<int>(nullable: false),
                    nvarchar250 = table.Column<string>(name: "nvarchar(250", maxLength: 250, nullable: true),
                    TourId = table.Column<long>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhachHangs");
        }
    }
}
