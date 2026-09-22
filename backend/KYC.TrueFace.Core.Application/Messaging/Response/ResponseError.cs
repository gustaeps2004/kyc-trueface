using KYC.TrueFace.Core.Domain.Constants;

namespace KYC.TrueFace.Core.Application.Messaging.Response;

public sealed record ResponseError(string Message)
{
    public static ResponseError Create(string message) => new(message);
}