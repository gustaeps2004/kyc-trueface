using KYC.TrueFace.Core.Domain.Constants;

namespace KYC.TrueFace.Core.Application.Messaging.Response;

public sealed record ResponseError(
    string Message = ValidationErrors.GenericError)
{
    public static ResponseError Create() => new();
}