using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addPhanKhuKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanKhuCNs",
                table: "PhanKhuCNs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanKhuCNs",
                table: "PhanKhuCNs",
                columns: new[] { "Id", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_PhanKhuCNs_RoleId",
                table: "PhanKhuCNs",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanKhuCNs",
                table: "PhanKhuCNs");

            migrationBuilder.DropIndex(
                name: "IX_PhanKhuCNs_RoleId",
                table: "PhanKhuCNs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanKhuCNs",
                table: "PhanKhuCNs",
                column: "RoleId");
        }
    }
}
