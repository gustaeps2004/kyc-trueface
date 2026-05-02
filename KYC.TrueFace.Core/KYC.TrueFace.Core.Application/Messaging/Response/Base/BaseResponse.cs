namespace KYC.TrueFace.Core.Application.Messaging.Response.Base;

public class BaseResponse(string message)
{
    public string Message { get; set; } = message;
}