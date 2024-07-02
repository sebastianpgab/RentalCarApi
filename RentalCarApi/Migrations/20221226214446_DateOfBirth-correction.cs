using Microsoft.EntityFrameworkCore.Migrations;

namespace Wieczorna_nauka_aplikacja_webowa.Migrations
{
    public partial class DateOfBirthcorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBrith",
                table: "Users",
                newName: "DateOfBirth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "DateOfBrith");
        }
    }
}
