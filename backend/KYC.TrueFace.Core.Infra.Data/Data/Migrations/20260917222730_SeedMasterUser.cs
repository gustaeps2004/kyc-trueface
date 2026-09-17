using System;
using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC.TrueFace.Core.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMasterUser : Migration
    {
        private static readonly Guid PartnerCode = Guid.Parse("c1951e02-5a1d-4d06-a677-34f6cc2509bf");
        private static readonly Guid UserCode = Guid.Parse("2778a157-4a79-4962-a321-cee75cdbcd0f");
        private static readonly Guid UserAccessCode = Guid.Parse("d791f5aa-e1d3-481c-b761-a8f40e10a810");

        // Placeholder values — edit before/after applying this migration in a real environment.
        private const string PartnerIdNumber = "15174473733414";
        private const string PartnerName = "GUSTAEPS LTDA";
        private const string PartnerEmail = "gustech@gmail.com";
        private const string UserName = "Gustavo";
        private const string UserIdNumber = "21955473838";
        private const string UserEmail = "gustavo_santo@estudante.sesisenai.org.br";

        // Username follows the app's login convention: "<email>_onb" (see PasswordHelper.GetSuffix).
        private const string UserAccessUsername = "gustavo_santo@estudante.sesisenai.org.br_onb";

        // Argon2id hash for a randomly generated password (plaintext given separately, not stored here).
        // Format/params match Argon2PasswordHasher with the current empty Pepper (k=0).
        private const string UserAccessPasswordHash =
            "$argon2id$v=19$m=19456,t=2,p=1,k=0$BokQGAz6RKOc3VwRhcvIMA==$In0lnPHUUGnyn4jUlVNK61tjE6dCGTyq4YR6nOOkQLg=";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow;

            migrationBuilder.InsertData(
                table: "Partners",
                columns: ["Code", "IdNumber", "Name", "Email", "Situation", "InclusionDt"],
                values: [PartnerCode, PartnerIdNumber, PartnerName, PartnerEmail, 1, now]);

            migrationBuilder.InsertData(
                table: "Users",
                columns: ["Code", "CodePartner", "Name", "IdNumber", "BirthDate", "MotherName", "Email", "Permission", "Situation", "InclusionDt"],
                values: [UserCode, PartnerCode, UserName, UserIdNumber, new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), null, UserEmail, 3, 1, now]);

            migrationBuilder.InsertData(
                table: "UsersAccess",
                columns: ["Code", "Username", "Password", "Situation", "Role", "Claim", "InclusionDt", "ResetPasswordTokenHash", "ResetPasswordTokenExpiresAt", "AccessFailedCount", "LockoutEndsAt"],
                values:
                [
                    UserAccessCode,
                    UserAccessUsername,
                    UserAccessPasswordHash,
                    1,
                    "MASTER",
                    $$"""{"user_code":"{{UserCode}}","user_code_partner":"{{PartnerCode}}","user_name":"{{UserName}}"}""",
                    now,
                    null,
                    null,
                    0,
                    null
                ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "UsersAccess", keyColumn: "Code", keyValue: UserAccessCode);
            migrationBuilder.DeleteData(table: "Users", keyColumn: "Code", keyValue: UserCode);
            migrationBuilder.DeleteData(table: "Partners", keyColumn: "Code", keyValue: PartnerCode);
        }
    }
}
