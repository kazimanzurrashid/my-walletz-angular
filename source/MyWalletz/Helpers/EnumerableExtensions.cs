namespace MyWalletz.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class EnumerableExtensions
    {
        public static IEnumerable<SelectListItem> AsSelectListItems(
            this IEnumerable<KeyValuePair<string, string>> instance,
            object model = null)
        {
            return instance
                .Select(i =>
                    new SelectListItem
                    {
                        Text = i.Value,
                        Value = i.Key,
                        Selected = i.Key.Equals(model)
                    })
                .OrderBy(i => i.Text);
        }
    }
}