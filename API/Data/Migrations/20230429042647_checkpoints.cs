using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class checkpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Races_RaceId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Location_StartLocationId",
                table: "Races");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Location_RaceId",
                table: "Locations",
                newName: "IX_Locations_RaceId");

            migrationBuilder.AddColumn<string>(
                name: "ScannerId",
                table: "Locations",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CheckPoint",
                columns: table => new
                {
                    AccId = table.Column<Guid>(type: "uuid", nullable: false),
                    RaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScannerId = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RaceAttendanceBibNumber = table.Column<string>(type: "text", nullable: true),
                    RaceAttendanceRaceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckPoint", x => new { x.AccId, x.RaceId, x.ScannerId });
                    table.ForeignKey(
                        name: "FK_CheckPoint_RaceAttendances_RaceAttendanceRaceId_RaceAttenda~",
                        columns: x => new { x.RaceAttendanceRaceId, x.RaceAttendanceBibNumber },
                        principalTable: "RaceAttendances",
                        principalColumns: new[] { "RaceId", "BibNumber" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoint_RaceAttendanceRaceId_RaceAttendanceBibNumber",
                table: "CheckPoint",
                columns: new[] { "RaceAttendanceRaceId", "RaceAttendanceBibNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Races_RaceId",
                table: "Locations",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Locations_StartLocationId",
                table: "Races",
                column: "StartLocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Races_RaceId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Races_Locations_StartLocationId",
                table: "Races");

            migrationBuilder.DropTable(
                name: "CheckPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ScannerId",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_RaceId",
                table: "Location",
                newName: "IX_Location_RaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Races_RaceId",
                table: "Location",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Location_StartLocationId",
                table: "Races",
                column: "StartLocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}
