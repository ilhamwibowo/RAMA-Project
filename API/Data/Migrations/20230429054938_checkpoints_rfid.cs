using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class checkpointsrfid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckPoints",
                table: "CheckPoints");

            migrationBuilder.AddColumn<string>(
                name: "RFID",
                table: "CheckPoints",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckPoints",
                table: "CheckPoints",
                columns: new[] { "RFID", "RaceId", "ScannerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckPoints",
                table: "CheckPoints");

            migrationBuilder.DropColumn(
                name: "RFID",
                table: "CheckPoints");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckPoints",
                table: "CheckPoints",
                columns: new[] { "AccId", "RaceId", "ScannerId" });
        }
    }
}
