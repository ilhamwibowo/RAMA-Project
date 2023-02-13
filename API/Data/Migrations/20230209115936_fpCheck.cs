using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class fpCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OTP",
                table: "ForgotPasswordHistories",
                newName: "Otp");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "ForgotPasswordHistories",
                newName: "RequestCount");

            migrationBuilder.AddColumn<int>(
                name: "CheckCount",
                table: "ForgotPasswordHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckCount",
                table: "ForgotPasswordHistories");

            migrationBuilder.RenameColumn(
                name: "Otp",
                table: "ForgotPasswordHistories",
                newName: "OTP");

            migrationBuilder.RenameColumn(
                name: "RequestCount",
                table: "ForgotPasswordHistories",
                newName: "Count");
        }
    }
}
