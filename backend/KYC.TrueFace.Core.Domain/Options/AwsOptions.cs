namespace KYC.TrueFace.Core.Domain.Options;

public class AwsOptions
{
    public const string SectionName = "Aws";

    public string Region { get; init; } = string.Empty;

    public string AccessKeyId { get; init; } = string.Empty;

    public string SecretAccessKey { get; init; } = string.Empty;

    /// <summary>Optional endpoint override, for a local stub or LocalStack. Empty uses the real AWS endpoint.</summary>
    public string ServiceUrl { get; init; } = string.Empty;

    public RekognitionOptions Rekognition { get; init; } = new();

    // When no static keys are configured the SDK default credential chain is used
    // (environment variables, shared profile, EC2/ECS instance role).
    public bool HasStaticCredentials
        => !string.IsNullOrWhiteSpace(AccessKeyId)
           && !string.IsNullOrWhiteSpace(SecretAccessKey);
}
