namespace MyWalletz.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    public class QueryTransactions : IValidatableObject
    {
        private const string DefaultSortProperty = "PostedAt";
        private const string DefaultSortOrder = "desc";

        private static readonly IDictionary<string, string> SortProperties =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "PostedAt", "PostedAt" },
                { "Category", "Category.Title" },
                { "Payee", "Payee" },
                { "Amount", "Amount" }
           };

        private static readonly IEnumerable<string> SortOrders
            = new[] { "asc", "desc" };

        public int Top { get; set; }

        public int Skip { get; set; }

        public string OrderBy { get; set; }

        public int AccountId { get; set; }

        public string GetOrderByClause()
        {
            if (string.IsNullOrWhiteSpace(OrderBy))
            {
                return DefaultSortProperty + " " + DefaultSortOrder;
            }

            var orderByPairs = OrderBy.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            var orderBy = SortProperties[orderByPairs[0]];

            if (orderByPairs.Length > 1)
            {
                orderBy += " " + orderByPairs[1];
            }

            return orderBy;
        }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext context)
        {
            if (this.AccountId < 1)
            {
                yield return new ValidationResult(
                    "Invalid account id.",
                    new[] { "AccountId" });
            }

            if (Top < 0)
            {
                yield return new ValidationResult(
                    "Top cannot be negative.",
                    new[] { "Top" });
            }

            if (Skip < 0)
            {
                yield return new ValidationResult(
                    "Skip cannot be negative.",
                    new[] { "Skip" });
            }

            if (string.IsNullOrWhiteSpace(OrderBy))
            {
                yield break;
            }

            var orderByPairs = OrderBy.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            if (!SortProperties.ContainsKey(orderByPairs[0]))
            {
                var sortPropertyErrorMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "Invalid sort property, only supports \"{0}\".",
                    string.Join(", ", SortProperties.Keys));

                yield return new ValidationResult(
                    sortPropertyErrorMessage,
                    new[] { "OrderBy" });
            }

            if (orderByPairs.Length < 2 ||
                SortOrders.Contains(
                    orderByPairs[1],
                    StringComparer.OrdinalIgnoreCase))
            {
                yield break;
            }

            var sortOrderErrorMessage = string.Format(
                CultureInfo.CurrentCulture,
                "Invalid sort order, only supports \"{0}\".",
                string.Join(", ", SortOrders));

            yield return new ValidationResult(
                sortOrderErrorMessage,
                new[] { "OrderBy" });
        }
    }
}