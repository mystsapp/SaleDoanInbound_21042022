using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addLogfileBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "varchar(3)",
                table: "BienNhans",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "BienNhans",
                type: "nvarchar(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "BienNhans");

            migrationBuilder.AlterColumn<string>(
                name: "varchar(3)",
                table: "BienNhans",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3);
        }
    }
}
