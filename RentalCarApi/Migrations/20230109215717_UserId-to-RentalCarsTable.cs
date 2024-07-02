using Microsoft.EntityFrameworkCore.Migrations;

namespace Wieczorna_nauka_aplikacja_webowa.Migrations
{
    public partial class UserIdtoRentalCarsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "RentalCars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RentalCars");
        }
    }
}
