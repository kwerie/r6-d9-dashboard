using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RenameExpiresInToExpiresAtAndChangeTypeToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresIn",
                table: "discord_login_sessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "discord_login_sessions",
                type: "datetime(6)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "discord_login_sessions");

            migrationBuilder.AddColumn<int>(
                name: "ExpiresIn",
                table: "discord_login_sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
