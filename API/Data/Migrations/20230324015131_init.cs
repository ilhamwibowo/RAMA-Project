using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    AlbumName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumId);
                });

            migrationBuilder.CreateTable(
                name: "ForgotPasswordHistories",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Otp = table.Column<string>(type: "text", nullable: true),
                    RequestCount = table.Column<int>(type: "integer", nullable: false),
                    CheckCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPasswordHistories", x => new { x.Date, x.Email });
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Url = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    BibTags = table.Column<List<string>>(type: "text[]", nullable: true),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_Photo_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KTP = table.Column<string>(type: "text", nullable: true),
                    NoHP = table.Column<string>(name: "No_HP", type: "text", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePhotoPhotoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccId);
                    table.ForeignKey(
                        name: "FK_Accounts_Photo_ProfilePhotoPhotoId",
                        column: x => x.ProfilePhotoPhotoId,
                        principalTable: "Photo",
                        principalColumn: "PhotoId");
                });

            migrationBuilder.CreateTable(
                name: "RaceRegistrations",
                columns: table => new
                {
                    RaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationFee = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TakenKitAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountAccId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceRegistrations", x => new { x.RaceId, x.AccId, x.RegistedAt });
                    table.ForeignKey(
                        name: "FK_RaceRegistrations_Accounts_AccountAccId",
                        column: x => x.AccountAccId,
                        principalTable: "Accounts",
                        principalColumn: "AccId");
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Longitude = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<string>(type: "text", nullable: true),
                    RaceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    RaceName = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartLocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Distance = table.Column<int>(type: "integer", nullable: false),
                    RegistrationFee = table.Column<int>(type: "integer", nullable: false),
                    RaceAlbumAlbumId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Races_Albums_RaceAlbumAlbumId",
                        column: x => x.RaceAlbumAlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId");
                    table.ForeignKey(
                        name: "FK_Races_Location_StartLocationId",
                        column: x => x.StartLocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RaceAttendance",
                columns: table => new
                {
                    RaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    BibNumber = table.Column<string>(type: "text", nullable: false),
                    RunnerAccId = table.Column<Guid>(type: "uuid", nullable: true),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ProfilePhotoPhotoId",
                table: "Accounts",
                column: "ProfilePhotoPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_RaceId",
                table: "Location",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AlbumId",
                table: "Photo",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceAttendance_RunnerAccId",
                table: "RaceAttendance",
                column: "RunnerAccId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceRegistrations_AccountAccId",
                table: "RaceRegistrations",
                column: "AccountAccId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_RaceAlbumAlbumId",
                table: "Races",
                column: "RaceAlbumAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_StartLocationId",
                table: "Races",
                column: "StartLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Races_RaceId",
                table: "Location",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Races_RaceId",
                table: "Location");

            migrationBuilder.DropTable(
                name: "ForgotPasswordHistories");

            migrationBuilder.DropTable(
                name: "RaceAttendance");

            migrationBuilder.DropTable(
                name: "RaceRegistrations");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
