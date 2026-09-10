using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC.TrueFace.Core.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersReports",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeUser = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterText = table.Column<string>(type: "text", nullable: true),
                    FilterSituation = table.Column<int>(type: "integer", nullable: true),
                    FilterStartDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FilterEndDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    SituationMessage = table.Column<string>(type: "text", nullable: true),
                    SituationDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersReports", x => x.Code);
                    table.ForeignKey(
                        name: "FK_UsersReports_Users_CodeUser",
                        column: x => x.CodeUser,
                        principalTable: "Users",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersReports_CodeUser",
                table: "UsersReports",
                column: "CodeUser");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReports_Situation_InclusionDt",
                table: "UsersReports",
                columns: new[] { "Situation", "InclusionDt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersReports");
        }
    }
}
