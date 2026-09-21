using Amazon;
using Amazon.Rekognition;
using Amazon.Runtime;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class AwsConfig
{
    public static void ConfigureAws(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(AwsOptions.SectionName);

        services.Configure<AwsOptions>(section);

        var awsOptions = section.Get<AwsOptions>() ?? new AwsOptions();

        services.AddSingleton<IAmazonRekognition>(_ => CreateRekognitionClient(awsOptions));
    }

    private static IAmazonRekognition CreateRekognitionClient(AwsOptions options)
    {
        var config = new AmazonRekognitionConfig
        {
            Timeout = TimeSpan.FromSeconds(options.Rekognition.TimeoutSeconds),
            MaxErrorRetry = options.Rekognition.MaxErrorRetry
        };

        // Left unset, the SDK resolves the region from AWS_REGION or the shared profile.
        if (!string.IsNullOrWhiteSpace(options.Region))
            config.RegionEndpoint = RegionEndpoint.GetBySystemName(options.Region);

        if (!string.IsNullOrWhiteSpace(options.ServiceUrl))
        {
            config.ServiceURL = options.ServiceUrl;
            config.AuthenticationRegion = options.Region;
        }

        return options.HasStaticCredentials
            ? new AmazonRekognitionClient(
                    new BasicAWSCredentials(options.AccessKeyId, options.SecretAccessKey),
                    config)
            : new AmazonRekognitionClient(config);
    }
}
