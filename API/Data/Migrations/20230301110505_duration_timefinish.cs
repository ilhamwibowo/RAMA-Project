using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class durationtimefinish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "position",
                table: "RaceAttendance",
                newName: "Position");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "RaceAttendance",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "RaceAttendance",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "RaceAttendance");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "RaceAttendance");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "RaceAttendance",
                newName: "position");
        }
    }
}
