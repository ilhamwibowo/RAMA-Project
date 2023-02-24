using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RaceAlbums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Photo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "BigTags",
                table: "Photo",
                type: "integer[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlbumName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumId);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RaceName = table.Column<string>(type: "text", nullable: true),
                    RaceAlbumAlbumId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Races_Albums_RaceAlbumAlbumId",
                        column: x => x.RaceAlbumAlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId");
                });

            migrationBuilder.CreateTable(
                name: "RaceAttendance",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    BibNumber = table.Column<int>(type: "integer", nullable: false),
                    RunnerAccId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceAttendance", x => new { x.RaceId, x.BibNumber });
                    table.ForeignKey(
                        name: "FK_RaceAttendance_Accounts_RunnerAccId",
                        column: x => x.RunnerAccId,
                        principalTable: "Accounts",
                        principalColumn: "AccId");
                    table.ForeignKey(
                        name: "FK_RaceAttendance_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AlbumId",
                table: "Photo",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceAttendance_RunnerAccId",
                table: "RaceAttendance",
                column: "RunnerAccId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_RaceAlbumAlbumId",
                table: "Races",
                column: "RaceAlbumAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Albums_AlbumId",
                table: "Photo",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Albums_AlbumId",
                table: "Photo");

            migrationBuilder.DropTable(
                name: "RaceAttendance");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Photo_AlbumId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "BigTags",
                table: "Photo");
        }
    }
}
