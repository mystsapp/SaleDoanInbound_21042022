using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class MrSonDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourIBs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Order = table.Column<string>(nullable: true),
                    SGTCode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: false),
                    Arr = table.Column<DateTime>(nullable: false),
                    Dep = table.Column<DateTime>(nullable: false),
                    Pax = table.Column<int>(nullable: false),
                    Ref = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Deposit = table.Column<int>(nullable: false),
                    Note = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NoiDungHuyId = table.Column<long>(nullable: false),
                    CompanyId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourIBs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Replace = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Arr = table.Column<DateTime>(nullable: false),
                    Dep = table.Column<DateTime>(nullable: false),
                    Pax = table.Column<int>(nullable: false),
                    SGL = table.Column<int>(nullable: false),
                    DBL = table.Column<int>(nullable: false),
                    TPL = table.Column<int>(nullable: false),
                    MOFP = table.Column<string>(nullable: true),
                    DOFP = table.Column<DateTime>(nullable: false),
                    TourIBId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    STT = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Bill = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NgayVAT = table.Column<DateTime>(nullable: false),
                    Ref = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    MsThue = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    HopDong = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    HuyVAT = table.Column<DateTime>(nullable: false),
                    SttMoi = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    BillMoi = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NgayVATMoi = table.Column<DateTime>(nullable: false),
                    KyHieuHD = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MauSoHD = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    KeyHDDT = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    Lock = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_TourIBs_TourIBId",
                        column: x => x.TourIBId,
                        principalTable: "TourIBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CTVATs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Descript = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    ServiceFee = table.Column<decimal>(nullable: false),
                    VAT = table.Column<decimal>(nullable: false),
                    Percent = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTVATs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTVATs_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTVATs_InvoiceId",
                table: "CTVATs",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TourIBId",
                table: "Invoices",
                column: "TourIBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTVATs");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "TourIBs");
        }
    }
}
