using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class photos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfilePhotoPhotoId",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ProfilePhotoPhotoId",
                table: "Accounts",
                column: "ProfilePhotoPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Photo_ProfilePhotoPhotoId",
                table: "Accounts",
                column: "ProfilePhotoPhotoId",
                principalTable: "Photo",
                principalColumn: "PhotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Photo_ProfilePhotoPhotoId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ProfilePhotoPhotoId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoPhotoId",
                table: "Accounts");
        }
    }
}
