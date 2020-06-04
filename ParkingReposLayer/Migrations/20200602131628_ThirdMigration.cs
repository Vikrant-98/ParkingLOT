using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingReposLayer.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParkingType",
                table: "Entities",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "ParkStatus",
                table: "Entities",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkStatus",
                table: "Entities");

            migrationBuilder.AlterColumn<string>(
                name: "ParkingType",
                table: "Entities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
