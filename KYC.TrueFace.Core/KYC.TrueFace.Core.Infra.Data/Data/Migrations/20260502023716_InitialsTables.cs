using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC.TrueFace.Core.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    IdNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UsersAccess",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Claim = table.Column<string>(type: "text", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAccess", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Onboardings",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    CodePartner = table.Column<Guid>(type: "uuid", nullable: false),
                    SituationDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    PathDocument = table.Column<string>(type: "text", nullable: false),
                    PathSelfie = table.Column<string>(type: "text", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboardings", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Onboardings_Partners_CodePartner",
                        column: x => x.CodePartner,
                        principalTable: "Partners",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnersCredentials",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    CodePartner = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    ClientSecret = table.Column<string>(type: "text", nullable: false),
                    GrantType = table.Column<string>(type: "text", nullable: false),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnersCredentials", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PartnersCredentials_Partners_CodePartner",
                        column: x => x.CodePartner,
                        principalTable: "Partners",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    CodePartner = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdNumber = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MotherName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Permission = table.Column<int>(type: "integer", nullable: false),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Users_Partners_CodePartner",
                        column: x => x.CodePartner,
                        principalTable: "Partners",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersAccessLogs",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeUserAccess = table.Column<Guid>(type: "uuid", nullable: false),
                    SituationDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Flow = table.Column<int>(type: "integer", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAccessLogs", x => x.Code);
                    table.ForeignKey(
                        name: "FK_UsersAccessLogs_UsersAccess_CodeUserAccess",
                        column: x => x.CodeUserAccess,
                        principalTable: "UsersAccess",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnboardingsResults",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeOnboarding = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeUser = table.Column<Guid>(type: "uuid", nullable: false),
                    Observation = table.Column<string>(type: "text", nullable: false),
                    InclusionDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnboardingsResults", x => x.Code);
                    table.ForeignKey(
                        name: "FK_OnboardingsResults_Onboardings_CodeOnboarding",
                        column: x => x.CodeOnboarding,
                        principalTable: "Onboardings",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Onboardings_CodePartner",
                table: "Onboardings",
                column: "CodePartner");

            migrationBuilder.CreateIndex(
                name: "IX_OnboardingsResults_CodeOnboarding",
                table: "OnboardingsResults",
                column: "CodeOnboarding");

            migrationBuilder.CreateIndex(
                name: "IX_PartnersCredentials_CodePartner",
                table: "PartnersCredentials",
                column: "CodePartner");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CodePartner",
                table: "Users",
                column: "CodePartner");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAccessLogs_CodeUserAccess",
                table: "UsersAccessLogs",
                column: "CodeUserAccess");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnboardingsResults");

            migrationBuilder.DropTable(
                name: "PartnersCredentials");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UsersAccessLogs");

            migrationBuilder.DropTable(
                name: "Onboardings");

            migrationBuilder.DropTable(
                name: "UsersAccess");

            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
