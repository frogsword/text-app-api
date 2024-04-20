using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextApp.Migrations
{
    /// <inheritdoc />
    public partial class removemessageprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Profiles_ProfileUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ProfileUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ProfileUserId",
                table: "Message");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileUserId",
                table: "Message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ProfileUserId",
                table: "Message",
                column: "ProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Profiles_ProfileUserId",
                table: "Message",
                column: "ProfileUserId",
                principalTable: "Profiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
