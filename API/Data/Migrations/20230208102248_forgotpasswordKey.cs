using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class forgotpasswordKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ForgotPasswordHistory",
                table: "ForgotPasswordHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "ForgotPasswordHistory",
                newName: "ForgotPasswordHistories");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Email",
                table: "Accounts",
                newName: "IX_Accounts_Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ForgotPasswordHistories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForgotPasswordHistories",
                table: "ForgotPasswordHistories",
                columns: new[] { "Date", "Email" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ForgotPasswordHistories",
                table: "ForgotPasswordHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "ForgotPasswordHistories",
                newName: "ForgotPasswordHistory");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Email",
                table: "Account",
                newName: "IX_Account_Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ForgotPasswordHistory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForgotPasswordHistory",
                table: "ForgotPasswordHistory",
                column: "Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "AccId");
        }
    }
}
