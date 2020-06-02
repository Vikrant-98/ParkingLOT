using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingReposLayer.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverCategory",
                table: "Entities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverCategory",
                table: "Entities",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
