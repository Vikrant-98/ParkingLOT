using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingReposLayer.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    ParkingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParkingSlotNo = table.Column<int>(nullable: false),
                    DriverCategory = table.Column<string>(maxLength: 50, nullable: false),
                    VehicalNo = table.Column<string>(nullable: false),
                    VehicalBrand = table.Column<string>(nullable: false),
                    VehicalColor = table.Column<string>(nullable: true),
                    ChargePerHr = table.Column<int>(nullable: false),
                    EntryTime = table.Column<DateTime>(nullable: false),
                    ExitTime = table.Column<DateTime>(nullable: false),
                    ParkingType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.ParkingID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    MailID = table.Column<string>(nullable: false),
                    DriverCategory = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
