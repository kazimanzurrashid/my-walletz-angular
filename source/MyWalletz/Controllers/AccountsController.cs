namespace MyWalletz.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using DataAccess;
    using DomainObjects;
    using Helpers;
    using Models;

    public class AccountsController : ApplicationApiController
    {
        private const string DeleteErrorMessage = "Cannot delete account " +
            "\"{0}\" which has associated transactions.";

        public AccountsController(
            IDataContext dataContext,
            Func<int> getUserId) : base(dataContext, getUserId)
        {
        }

        public HttpResponseMessage Get()
        {
            var model = DataContext.FindAccounts(UserId);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        public HttpResponseMessage Get(int id)
        {
            var account = DataContext.Accounts.Find(id);

            if ((account == null) || (account.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, account.AsModel());
        }

        public HttpResponseMessage Post(CreateAccount model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    ModelState);
            }

            var account = new Account()
                .Merge(model, excludedProperties: new[] { "Balance" });

            account.UserId = UserId;

            if (model.Balance > 0)
            {
                var transaction = account.Deposit(model.Balance);
                transaction.PostedAt = model.CreatedAt;
                transaction.Notes = "Initial balance.";
                DataContext.Transactions.Add(transaction);
            }

            DataContext.Accounts.Add(account);
            DataContext.SaveChanges();

            return Request.CreateResponse(
                HttpStatusCode.Created,
                account.AsModel());
        }

        public HttpResponseMessage Put(UpdateAccount model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            var account = DataContext.Accounts.Find(model.Id);

            if ((account == null) || (account.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            account.Merge(model);
            DataContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(int id)
        {
            var account = DataContext.Accounts.Find(id);

            if ((account == null) || (account.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (DataContext.Transactions.Any(t => t.AccountId == id))
            {
                var errorMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    DeleteErrorMessage,
                    account.Title);

                ModelState.AddModelError(string.Empty, errorMessage);

                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            DataContext.Accounts.Remove(account);
            DataContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}