using System.ComponentModel;

namespace BlazorAppServer.Enums
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        public static string GetUrlSlug(Categories category)
        {
            var type = category.GetType();
            var memInfo = type.GetMember(category.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(UrlSlugAttribute), false);
            return (attributes.Length > 0) ? ((UrlSlugAttribute)attributes[0]).UrlSlug : string.Empty;
        }
    }
}
