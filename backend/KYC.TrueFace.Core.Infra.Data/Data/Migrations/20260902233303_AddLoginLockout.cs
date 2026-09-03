using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC.TrueFace.Core.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginLockout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "UsersAccess",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutEndsAt",
                table: "UsersAccess",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "UsersAccess");

            migrationBuilder.DropColumn(
                name: "LockoutEndsAt",
                table: "UsersAccess");
        }
    }
}
