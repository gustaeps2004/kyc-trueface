using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC.TrueFace.Core.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOnboardingFaceValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttemptCount",
                table: "Onboardings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Similarity",
                table: "Onboardings",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SituationMessage",
                table: "Onboardings",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Onboardings_Situation_InclusionDt",
                table: "Onboardings",
                columns: new[] { "Situation", "InclusionDt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Onboardings_Situation_InclusionDt",
                table: "Onboardings");

            migrationBuilder.DropColumn(
                name: "AttemptCount",
                table: "Onboardings");

            migrationBuilder.DropColumn(
                name: "Similarity",
                table: "Onboardings");

            migrationBuilder.DropColumn(
                name: "SituationMessage",
                table: "Onboardings");
        }
    }
}
