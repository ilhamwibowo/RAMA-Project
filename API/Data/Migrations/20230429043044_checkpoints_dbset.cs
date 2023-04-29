using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class checkpointsdbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckPoint_RaceAttendances_RaceAttendanceRaceId_RaceAttenda~",
                table: "CheckPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckPoint",
                table: "CheckPoint");

            migrationBuilder.RenameTable(
                name: "CheckPoint",
                newName: "CheckPoints");

            migrationBuilder.RenameIndex(
                name: "IX_CheckPoint_RaceAttendanceRaceId_RaceAttendanceBibNumber",
                table: "CheckPoints",
                newName: "IX_CheckPoints_RaceAttendanceRaceId_RaceAttendanceBibNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckPoints",
                table: "CheckPoints",
                columns: new[] { "AccId", "RaceId", "ScannerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CheckPoints_RaceAttendances_RaceAttendanceRaceId_RaceAttend~",
                table: "CheckPoints",
                columns: new[] { "RaceAttendanceRaceId", "RaceAttendanceBibNumber" },
                principalTable: "RaceAttendances",
                principalColumns: new[] { "RaceId", "BibNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckPoints_RaceAttendances_RaceAttendanceRaceId_RaceAttend~",
                table: "CheckPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckPoints",
                table: "CheckPoints");

            migrationBuilder.RenameTable(
                name: "CheckPoints",
                newName: "CheckPoint");

            migrationBuilder.RenameIndex(
                name: "IX_CheckPoints_RaceAttendanceRaceId_RaceAttendanceBibNumber",
                table: "CheckPoint",
                newName: "IX_CheckPoint_RaceAttendanceRaceId_RaceAttendanceBibNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckPoint",
                table: "CheckPoint",
                columns: new[] { "AccId", "RaceId", "ScannerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CheckPoint_RaceAttendances_RaceAttendanceRaceId_RaceAttenda~",
                table: "CheckPoint",
                columns: new[] { "RaceAttendanceRaceId", "RaceAttendanceBibNumber" },
                principalTable: "RaceAttendances",
                principalColumns: new[] { "RaceId", "BibNumber" });
        }
    }
}
