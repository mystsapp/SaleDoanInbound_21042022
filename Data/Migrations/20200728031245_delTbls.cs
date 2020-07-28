using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class delTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiNhanhs_PhanKhuCNs_IdPhanKhuCN",
                table: "ChiNhanhs");

            migrationBuilder.DropForeignKey(
                name: "FK_DMDaiLies_ChiNhanhs_IdChiNhanh",
                table: "DMDaiLies");

            migrationBuilder.DropForeignKey(
                name: "FK_TourLogs_DMKhachHangs_IdKH",
                table: "TourLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_DMKhachHangs_IdKH",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRole",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrKhuCNs_Users_IdUser",
                table: "UsrKhuCNs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DMKhachHangs",
                table: "DMKhachHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiNhanhs",
                table: "ChiNhanhs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "DMKhachHangs",
                newName: "DMKhachHang");

            migrationBuilder.RenameTable(
                name: "ChiNhanhs",
                newName: "ChiNhanh");

            migrationBuilder.RenameIndex(
                name: "IX_Users_IdRole",
                table: "User",
                newName: "IX_User_IdRole");

            migrationBuilder.RenameIndex(
                name: "IX_ChiNhanhs_IdPhanKhuCN",
                table: "ChiNhanh",
                newName: "IX_ChiNhanh_IdPhanKhuCN");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DMKhachHang",
                table: "DMKhachHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiNhanh",
                table: "ChiNhanh",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiNhanh_PhanKhuCNs_IdPhanKhuCN",
                table: "ChiNhanh",
                column: "IdPhanKhuCN",
                principalTable: "PhanKhuCNs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DMDaiLies_ChiNhanh_IdChiNhanh",
                table: "DMDaiLies",
                column: "IdChiNhanh",
                principalTable: "ChiNhanh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourLogs_DMKhachHang_IdKH",
                table: "TourLogs",
                column: "IdKH",
                principalTable: "DMKhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_DMKhachHang_IdKH",
                table: "Tours",
                column: "IdKH",
                principalTable: "DMKhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_IdRole",
                table: "User",
                column: "IdRole",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrKhuCNs_User_IdUser",
                table: "UsrKhuCNs",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiNhanh_PhanKhuCNs_IdPhanKhuCN",
                table: "ChiNhanh");

            migrationBuilder.DropForeignKey(
                name: "FK_DMDaiLies_ChiNhanh_IdChiNhanh",
                table: "DMDaiLies");

            migrationBuilder.DropForeignKey(
                name: "FK_TourLogs_DMKhachHang_IdKH",
                table: "TourLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_DMKhachHang_IdKH",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_IdRole",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrKhuCNs_User_IdUser",
                table: "UsrKhuCNs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DMKhachHang",
                table: "DMKhachHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiNhanh",
                table: "ChiNhanh");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "DMKhachHang",
                newName: "DMKhachHangs");

            migrationBuilder.RenameTable(
                name: "ChiNhanh",
                newName: "ChiNhanhs");

            migrationBuilder.RenameIndex(
                name: "IX_User_IdRole",
                table: "Users",
                newName: "IX_Users_IdRole");

            migrationBuilder.RenameIndex(
                name: "IX_ChiNhanh_IdPhanKhuCN",
                table: "ChiNhanhs",
                newName: "IX_ChiNhanhs_IdPhanKhuCN");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DMKhachHangs",
                table: "DMKhachHangs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiNhanhs",
                table: "ChiNhanhs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiNhanhs_PhanKhuCNs_IdPhanKhuCN",
                table: "ChiNhanhs",
                column: "IdPhanKhuCN",
                principalTable: "PhanKhuCNs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DMDaiLies_ChiNhanhs_IdChiNhanh",
                table: "DMDaiLies",
                column: "IdChiNhanh",
                principalTable: "ChiNhanhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourLogs_DMKhachHangs_IdKH",
                table: "TourLogs",
                column: "IdKH",
                principalTable: "DMKhachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_DMKhachHangs_IdKH",
                table: "Tours",
                column: "IdKH",
                principalTable: "DMKhachHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_IdRole",
                table: "Users",
                column: "IdRole",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsrKhuCNs_Users_IdUser",
                table: "UsrKhuCNs",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
