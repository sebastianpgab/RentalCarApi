using Microsoft.EntityFrameworkCore.Migrations;

namespace Wieczorna_nauka_aplikacja_webowa.Migrations
{
    public partial class RentalCarUserIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "RentalCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById1",
                table: "RentalCars",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalCars_CreatedById1",
                table: "RentalCars",
                column: "CreatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalCars_Users_CreatedById1",
                table: "RentalCars",
                column: "CreatedById1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalCars_Users_CreatedById1",
                table: "RentalCars");

            migrationBuilder.DropIndex(
                name: "IX_RentalCars_CreatedById1",
                table: "RentalCars");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RentalCars");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "RentalCars");
        }
    }
}
