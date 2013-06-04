namespace MyWalletz.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using DataAccess;
    using DomainObjects;
    using Helpers;
    using Models;

    public class TransactionsController : ApplicationApiController
    {
        public TransactionsController(
            IDataContext dataContext,
            Func<int> getUserId) : base(dataContext, getUserId)
        {
        }

        public HttpResponseMessage Get([FromUri] QueryTransactions model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            var count = DataContext.Transactions
                .Count(t =>
                    t.AccountId == model.AccountId &&
                    t.UserId == UserId);

            var data = DataContext.Transactions
                .Include(t => t.Category)
                .Where(t =>
                    t.AccountId == model.AccountId &&
                    t.UserId == UserId)
                .OrderBy(model.GetOrderByClause())
                .Skip(model.Skip)
                .Take(model.Top)
                .ToList()
                .Select(t => t.AsModel())
                .ToList();

            return Request.CreateResponse(
                HttpStatusCode.OK,
                new { data, count });
        }

        public HttpResponseMessage Get(int accountId, int id)
        {
            var transaction = DataContext.Transactions.Find(id);

            if ((transaction == null) ||
                (transaction.AccountId != accountId) ||
                (transaction.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(
                HttpStatusCode.OK,
                transaction.AsModel());
        }

        public HttpResponseMessage Post(CreateTransaction model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    ModelState);
            }

            var account = DataContext.Accounts.Find(model.AccountId);

            if ((account == null) || (account.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var category = DataContext.Categories.Find(model.CategoryId);

            if ((category == null) || (category.UserId != UserId))
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Specified category does not exist.");

                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            Transaction transaction;

            switch (category.Type)
            {
                case CategoryType.Expense:
                    transaction = account.Withdraw(model.Amount);
                    break;
                case CategoryType.Income:
                    transaction = account.Deposit(model.Amount);
                    break;
                default:
                    throw new InvalidOperationException(
                        "Invalid category type.");
            }

            transaction.Merge(model, new[] { "Payee", "Notes", "PostedAt" });
            transaction.Category = category;

            DataContext.Transactions.Add(transaction);
            DataContext.SaveChanges();

            return Request.CreateResponse(
                HttpStatusCode.Created,
                transaction.AsModel());
        }
    }
}