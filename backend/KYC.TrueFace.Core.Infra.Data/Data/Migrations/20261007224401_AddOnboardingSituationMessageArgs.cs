using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC.TrueFace.Core.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOnboardingSituationMessageArgs : Migration
    {
        // Messages recorded in English before they became translation keys. Pattern captures, in
        // order, the values now stored as Args (null: nothing to convert, the record is rewritten
        // by the worker); Text is the format() template that rebuilds the old message on Down.
        private static readonly (string Key, string Pattern, string[] Args, string Text)[] LegacyMessages =
        [
            ("onboarding.result.facesMatched",
                @"^Faces matched with ([0-9.]+)% similarity \(auto-approve from ([0-9.]+)%\)\.$",
                ["similarity", "approveThreshold"],
                "Faces matched with %s%% similarity (auto-approve from %s%%)."),
            ("onboarding.result.manualReviewRequired",
                @"^Similarity of ([0-9.]+)% is between ([0-9.]+)% and ([0-9.]+)% - manual review required\.$",
                ["similarity", "reviewThreshold", "approveThreshold"],
                "Similarity of %s%% is between %s%% and %s%% - manual review required."),
            ("onboarding.result.facesNotMatched",
                @"^Faces did not match: ([0-9.]+)% similarity, below the ([0-9.]+)% minimum\.$",
                ["similarity", "reviewThreshold"],
                "Faces did not match: %s%% similarity, below the %s%% minimum."),
            ("onboarding.result.noFaceFound",
                @"^Rekognition found no comparable face in the selfie\.$",
                [],
                "Rekognition found no comparable face in the selfie."),
            ("onboarding.result.imagesUnreadable",
                @"^Rekognition could not read the images:",
                [],
                "Rekognition could not read the images."),
            ("onboarding.result.imageFormatUnsupported",
                @"^Unsupported image format:",
                [],
                "Unsupported image format."),
            ("onboarding.result.imageTooLarge",
                @"^Image too large for Rekognition:",
                [],
                "Image too large for Rekognition."),
            ("onboarding.result.maxAttemptsExceeded",
                @"^Max attempts \(([0-9]+)\) exceeded\. Last failure - ",
                ["maxAttempts"],
                "Max attempts (%s) exceeded."),
            ("onboarding.result.processingFailed",
                null,
                [],
                "Face comparison failed."),
        ];

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SituationMessageArgs",
                table: "Onboardings",
                type: "jsonb",
                nullable: true);

            foreach (var (key, pattern, args, _) in LegacyMessages.Where(m => m.Pattern is not null))
            {
                var argsJson = args.Length == 0
                    ? "NULL"
                    : "jsonb_build_object(" + string.Join(", ", args.Select((arg, i) =>
                        $@"'{arg}', (regexp_match(""SituationMessage"", '{pattern}'))[{i + 1}]")) + ")";

                // SET reads the old row, so the captures come from the English text being replaced.
                migrationBuilder.Sql($@"
                    UPDATE ""Onboardings""
                    SET ""SituationMessage"" = '{key}',
                        ""SituationMessageArgs"" = {argsJson}
                    WHERE ""SituationMessage"" ~ '{pattern}';");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var (key, _, args, text) in LegacyMessages)
            {
                var formatArgs = string.Concat(args.Select(arg => $@", ""SituationMessageArgs"" ->> '{arg}'"));

                migrationBuilder.Sql($@"
                    UPDATE ""Onboardings""
                    SET ""SituationMessage"" = format('{text}'{formatArgs})
                    WHERE ""SituationMessage"" = '{key}';");
            }

            migrationBuilder.DropColumn(
                name: "SituationMessageArgs",
                table: "Onboardings");
        }
    }
}
