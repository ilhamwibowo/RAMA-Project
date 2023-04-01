using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumnrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDateRegistration",
                table: "Races",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Races",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RaceDesc",
                table: "Races",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RaceThumbnailPhotoId",
                table: "Races",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDateRegistration",
                table: "Races",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Races_RaceThumbnailPhotoId",
                table: "Races",
                column: "RaceThumbnailPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Photo_RaceThumbnailPhotoId",
                table: "Races",
                column: "RaceThumbnailPhotoId",
                principalTable: "Photo",
                principalColumn: "PhotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Photo_RaceThumbnailPhotoId",
                table: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Races_RaceThumbnailPhotoId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "EndDateRegistration",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "RaceDesc",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "RaceThumbnailPhotoId",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "StartDateRegistration",
                table: "Races");
        }
    }
}
