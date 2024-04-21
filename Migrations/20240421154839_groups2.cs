using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextApp.Migrations
{
    /// <inheritdoc />
    public partial class groups2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "Message",
                newName: "GroupId");

            migrationBuilder.AddColumn<Guid[]>(
                name: "Groups",
                table: "Profiles",
                type: "uuid[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Groups",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Message",
                newName: "Receiver");
        }
    }
}
