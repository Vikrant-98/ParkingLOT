using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingReposLayer.Migrations
{
    public partial class fifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ChargePerHr",
                table: "ParkingInfo",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChargePerHr",
                table: "ParkingInfo",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
