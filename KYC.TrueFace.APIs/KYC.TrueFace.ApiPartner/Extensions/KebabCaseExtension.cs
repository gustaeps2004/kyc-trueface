using System.Text.RegularExpressions;

namespace KYC.TrueFace.ApiPartner.Extensions;

public class KebabCaseExtension : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) 
            return null;

        return Regex.Replace(value.ToString()!,
                            "([a-z])([A-Z])",
                            "$1-$2",
                            RegexOptions.CultureInvariant,
                            TimeSpan.FromMilliseconds(100))
                            .ToLowerInvariant();
    }
}       