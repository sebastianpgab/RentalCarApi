using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wieczorna_nauka_aplikacja_webowa.Migrations
{
    public partial class add_CreatedById_to_Vehicile_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RentalCars");

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Vehicles",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Vehicles");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "RentalCars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
