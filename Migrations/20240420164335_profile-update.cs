using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextApp.Migrations
{
    /// <inheritdoc />
    public partial class profileupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "Profiles",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<Guid[]>(
                name: "Contacts",
                table: "Profiles",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]");

            migrationBuilder.AlterColumn<Guid[]>(
                name: "Blocks",
                table: "Profiles",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "Profiles",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "Contacts",
                table: "Profiles",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0],
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid[]>(
                name: "Blocks",
                table: "Profiles",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0],
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]",
                oldNullable: true);
        }
    }
}
