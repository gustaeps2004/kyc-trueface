namespace KYC.TrueFace.Core.Application.Messaging.Response.Base;

public sealed record BaseResponse(string Message)
{
    public static BaseResponse Create(string message)
        => new (message);
}