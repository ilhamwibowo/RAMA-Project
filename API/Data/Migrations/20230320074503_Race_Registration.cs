using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RaceRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RaceRegistrations",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "integer", nullable: false),
                    AccId = table.Column<int>(type: "integer", nullable: false),
                    RegistedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationFee = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TakenKitAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountAccId = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_RaceRegistrations_AccountAccId",
                table: "RaceRegistrations",
                column: "AccountAccId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceRegistrations");
        }
    }
}
