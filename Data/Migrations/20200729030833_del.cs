using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class del : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTVATs");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "TourIBs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourIBs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Arr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Dep = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deposit = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NoiDungHuyId = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pax = table.Column<int>(type: "int", nullable: false),
                    Ref = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SGTCode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: false)
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
                    Arr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bill = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    BillMoi = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    DBL = table.Column<int>(type: "int", nullable: false),
                    DOFP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dep = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HopDong = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    HuyVAT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KeyHDDT = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    KyHieuHD = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Lock = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MOFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MauSoHD = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    MsThue = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    NgayVAT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayVATMoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pax = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Ref = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Replace = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    SGL = table.Column<int>(type: "int", nullable: false),
                    STT = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SttMoi = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TPL = table.Column<int>(type: "int", nullable: false),
                    TourIBId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Type = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descript = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
    }
}
