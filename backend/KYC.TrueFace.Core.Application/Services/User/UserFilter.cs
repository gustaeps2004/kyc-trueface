using KYC.TrueFace.Core.Domain.Extensions;

namespace KYC.TrueFace.Core.Application.Services.User;

public static class UserFilter
{
    public static bool MatchesText(
        string name,
        string idNumber,
        string email,
        string? filter)
        => string.IsNullOrEmpty(filter)
           || name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)
           || idNumber.Contains(filter.JustNumbers())
           || email.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
}
