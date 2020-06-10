using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingReposLayer.Migrations
{
    public partial class forthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Entities",
                table: "Entities");

            migrationBuilder.RenameTable(
                name: "Entities",
                newName: "ParkingInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingInfo",
                table: "ParkingInfo",
                column: "ParkingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingInfo",
                table: "ParkingInfo");

            migrationBuilder.RenameTable(
                name: "ParkingInfo",
                newName: "Entities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entities",
                table: "Entities",
                column: "ParkingID");
        }
    }
}
