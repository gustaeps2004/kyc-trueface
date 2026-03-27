namespace KYC.TrueFace.ApiPartner.Messaging.Base;

public static class ResponseBase
{
    public static ResponseBaseValues GenericError()
        => new("Internal error.", 
            StatusCodes.Status500InternalServerError.ToString());

    public static ResponseBaseValues SetError(Exception ex)
        => new(ex.Message,
            StatusCodes.Status400BadRequest.ToString());
}

public class ResponseBaseValues(string message, string code)
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
}