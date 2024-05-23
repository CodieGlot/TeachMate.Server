using System.ComponentModel;
using System.Resources;

namespace TeachMate.Domain;
public static class ApiDescriptionExtensions
{
    private static readonly ResourceManager ResourceManager = new ResourceManager("TeachMate.Domain.Resources.core", typeof(ApiDescriptionExtensions).Assembly);

    public static string ToApiDescription(this PushNotificationType notificationType, params object[] args)
    {
        var enumType = typeof(PushNotificationType);
        var memberInfo = enumType.GetMember(notificationType.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
        {
            var description = ((DescriptionAttribute)attributes[0]).Description;
            var messageTemplate = ResourceManager.GetString(description);

            if (messageTemplate != null)
            {
                return string.Format(messageTemplate, args);
            }
        }

        return string.Empty;
    }
}
