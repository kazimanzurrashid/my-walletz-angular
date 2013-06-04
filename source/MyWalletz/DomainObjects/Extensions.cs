namespace MyWalletz.DomainObjects
{
    using System;

    using AccountEntity = Account;
    using AccountModel = DataTransferObjects.Account;

    using CategoryEntity = Category;
    using CategoryModel = DataTransferObjects.Category;

    using TransactionEntity = Transaction;
    using TransactionModel = DataTransferObjects.Transaction;

    public static class Extensions
    {
        public static AccountModel AsModel(this AccountEntity instance)
        {
            return new AccountModel
            {
                Id = instance.Id,
                Title = instance.Title,
                Notes = instance.Notes,
                Type = instance.Type,
                Balance = instance.Balance,
                Currency = instance.Currency
            };
        }

        public static CategoryModel AsModel(this CategoryEntity instance)
        {
            return new CategoryModel
            {
                Id = instance.Id,
                Title = instance.Title,
                Type = instance.Type
            };
        }

        public static TransactionModel AsModel(this TransactionEntity instance)
        {
            return new TransactionModel
            {
                Id = instance.Id,
                Payee = instance.Payee,
                Notes = instance.Notes,
                AccountId = instance.AccountId,
                CategoryId = instance.CategoryId,
                Amount = Math.Abs(instance.Amount),
                PostedAt = instance.PostedAt
            };
        }
    }
}