using System.ComponentModel;
using System.Reflection;

namespace KYC.TrueFace.Core.Domain.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();

        MemberInfo[] memberInfo = type.GetMember(value.ToString());

        if (memberInfo.Length > 0)
        {
            var attribute = memberInfo[0].GetCustomAttribute<DescriptionAttribute>();

            if (attribute != null)
                return attribute.Description;
        }

        return value.ToString();
    }
}