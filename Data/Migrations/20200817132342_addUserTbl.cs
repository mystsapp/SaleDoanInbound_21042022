using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addUserTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_IdRole",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrKhuCNs_User_IdUser",
                table: "UsrKhuCNs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_IdRole",
                table: "Users",
                newName: "IX_Users_IdRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LoginModels",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Mact = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Hoten = table.Column<string>(nullable: true),
                    Dienthoai = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Maphong = table.Column<string>(nullable: true),
                    Macn = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    Trangthai = table.Column<bool>(nullable: false),
                    Ngaydoimk = table.Column<DateTime>(nullable: true),
                    Doimk = table.Column<bool>(nullable: false),
                    Macode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginModels", x => x.Username);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRole",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UsrKhuCNs_Users_IdUser",
                table: "UsrKhuCNs");

            migrationBuilder.DropTable(
                name: "LoginModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_IdRole",
                table: "User",
                newName: "IX_User_IdRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

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
    }
}
