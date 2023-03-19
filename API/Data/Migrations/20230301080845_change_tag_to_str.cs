using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class changetagtostr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigTags",
                table: "Photo");

            migrationBuilder.AddColumn<List<string>>(
                name: "BibTags",
                table: "Photo",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BibTags",
                table: "Photo");

            migrationBuilder.AddColumn<List<int>>(
                name: "BigTags",
                table: "Photo",
                type: "integer[]",
                nullable: true);
        }
    }
}
