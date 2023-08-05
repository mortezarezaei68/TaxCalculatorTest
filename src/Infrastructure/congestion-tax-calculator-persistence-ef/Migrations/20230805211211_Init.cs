using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congestiontaxcalculatorpersistenceef.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtensiveRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    MaxTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxFreeDaysBeforeHolidayNumber = table.Column<int>(type: "int", nullable: false),
                    TaxFreeDaysAfterHolidayNumber = table.Column<int>(type: "int", nullable: false),
                    TaxFreeAfterOnePassInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensiveRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtensiveRules_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidayDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    HolidayDateTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayDates_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HolidayMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayMonths_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxFreeDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxFreeDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxFreeDays_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeTaxInCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    TaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FinishedTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTaxInCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTaxInCities_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExceptVehiclePerCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptVehiclePerCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExceptVehiclePerCities_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExceptVehiclePerCities_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExceptVehiclePerCities_CityId",
                table: "ExceptVehiclePerCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExceptVehiclePerCities_VehicleId",
                table: "ExceptVehiclePerCities",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtensiveRules_CityId",
                table: "ExtensiveRules",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDates_CityId",
                table: "HolidayDates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayMonths_CityId",
                table: "HolidayMonths",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxFreeDays_CityId",
                table: "TaxFreeDays",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTaxInCities_CityId",
                table: "TimeTaxInCities",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExceptVehiclePerCities");

            migrationBuilder.DropTable(
                name: "ExtensiveRules");

            migrationBuilder.DropTable(
                name: "HolidayDates");

            migrationBuilder.DropTable(
                name: "HolidayMonths");

            migrationBuilder.DropTable(
                name: "TaxFreeDays");

            migrationBuilder.DropTable(
                name: "TimeTaxInCities");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
