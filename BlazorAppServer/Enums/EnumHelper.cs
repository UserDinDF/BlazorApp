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

        public static string GetCategoryNameFromSlug(string slug, bool ru)
        {
            var category = CategoriesList.FirstOrDefault(c => c.Key == slug);

            if (category.Key != null)
            {
                category = CategoriesList.FirstOrDefault(c => c.Value == slug);
                if (category.Key != null)
                    return category.Key;
                else
                {
                    category = CategoriesList.FirstOrDefault(c => c.Key == slug);
                    return category.Value;
                }
            }
            else
            {
                if (ru)
                {
                    category = CategoriesList.FirstOrDefault(c => c.Value == slug);
                    return category.Key;
                }
               
                else
                {
                    return category.Value;
                }
            }
        }

        private static Dictionary<string, string> CategoriesList = Enum.GetValues(typeof(Categories))
        .Cast<Categories>()
        .ToDictionary(
            x => EnumHelper.GetDescription(x),
            x => EnumHelper.GetUrlSlug(x)
        );

    }
}
