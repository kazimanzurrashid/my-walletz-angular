namespace MyWalletz.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainObjects;

    using AccountEntity = DomainObjects.Account;
    using AccountModel = DataTransferObjects.Account;

    using CategoryEntity = DomainObjects.Category;
    using CategoryModel = DataTransferObjects.Category;

    public static class Extensins
    {
        public static IEnumerable<AccountModel> FindAccounts(
            this IDataContext instance,
            int userId)
        {
            var result = instance.Accounts
                .Where(a => a.UserId == userId)
                .ToList()
                .OrderBy(a => a.Title)
                .Select(a => a.AsModel());

            return result;
        }

        public static IEnumerable<CategoryModel> FindCategories(
            this IDataContext instance,
            int userId)
        {
            var result = instance.Categories
                .Where(c => c.UserId == userId)
                .ToList()
                .OrderBy(c => c.Type)
                .ThenBy(c => c.Title)
                .Select(c => c.AsModel());

            return result;
        }
    }
}