using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class rfidcheckpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceAttendance_Accounts_RunnerAccId",
                table: "RaceAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceAttendance_Races_RaceId",
                table: "RaceAttendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceAttendance",
                table: "RaceAttendance");

            migrationBuilder.RenameTable(
                name: "RaceAttendance",
                newName: "RaceAttendances");

            migrationBuilder.RenameIndex(
                name: "IX_RaceAttendance_RunnerAccId",
                table: "RaceAttendances",
                newName: "IX_RaceAttendances_RunnerAccId");

            migrationBuilder.AddColumn<string>(
                name: "RFID",
                table: "RaceRegistrations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RFID",
                table: "RaceAttendances",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceAttendances",
                table: "RaceAttendances",
                columns: new[] { "RaceId", "BibNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_RaceAttendances_Accounts_RunnerAccId",
                table: "RaceAttendances",
                column: "RunnerAccId",
                principalTable: "Accounts",
                principalColumn: "AccId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceAttendances_Races_RaceId",
                table: "RaceAttendances",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "RaceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceAttendances_Accounts_RunnerAccId",
                table: "RaceAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceAttendances_Races_RaceId",
                table: "RaceAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceAttendances",
                table: "RaceAttendances");

            migrationBuilder.DropColumn(
                name: "RFID",
                table: "RaceRegistrations");

            migrationBuilder.DropColumn(
                name: "RFID",
                table: "RaceAttendances");

            migrationBuilder.RenameTable(
                name: "RaceAttendances",
                newName: "RaceAttendance");

            migrationBuilder.RenameIndex(
                name: "IX_RaceAttendances_RunnerAccId",
                table: "RaceAttendance",
                newName: "IX_RaceAttendance_RunnerAccId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceAttendance",
                table: "RaceAttendance",
                columns: new[] { "RaceId", "BibNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_RaceAttendance_Accounts_RunnerAccId",
                table: "RaceAttendance",
                column: "RunnerAccId",
                principalTable: "Accounts",
                principalColumn: "AccId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceAttendance_Races_RaceId",
                table: "RaceAttendance",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "RaceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
