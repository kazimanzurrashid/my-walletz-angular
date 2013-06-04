namespace MyWalletz.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    public static class ViewDataContainerExtensions
    {
        public static IEnumerable<SelectListItem> SelectListItems<TEnum>(
            this IViewDataContainer instance)
             where TEnum : IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(
                    e =>
                    new SelectListItem
                    {
                        Text = GetText(e),
                        Value = e.ToString(CultureInfo.CurrentCulture)
                    })
                .OrderBy(i => i.Text)
                .ToList();
        }

        private static string GetText<TEnum>(TEnum value)
             where TEnum : IComparable, IFormattable, IConvertible
        {
            var name = value.ToString(CultureInfo.CurrentCulture);

            var attribute = value.GetType()
                .GetField(name)
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                return name;
            }

            return string.IsNullOrWhiteSpace(attribute.Description) ?
                name :
                attribute.Description;
        }
    }
}