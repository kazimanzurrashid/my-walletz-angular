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

    public class CategoriesController : ApplicationApiController
    {
        private const string DeleteErrorMessage = "Cannot delete category " +
            "\"{0}\" which has associated transactions.";

        public CategoriesController(
            IDataContext dataContext,
            Func<int> getUserId) : base(dataContext, getUserId)
        {
        }

        public HttpResponseMessage Get()
        {
            var model = DataContext.FindCategories(UserId);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        public HttpResponseMessage Get(int id)
        {
            var category = DataContext.Categories.Find(id);

            if ((category == null) || (category.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(
                HttpStatusCode.OK,
                category.AsModel());
        }

        public HttpResponseMessage Post(CreateCategory model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            var category = new Category().Merge(model);
            category.UserId = UserId;

            DataContext.Categories.Add(category);
            DataContext.SaveChanges();

            return Request.CreateResponse(
                HttpStatusCode.Created,
                category.AsModel());
        }

        public HttpResponseMessage Put(UpdateCategory model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            var category = DataContext.Categories.Find(model.Id);

            if ((category == null) || (category.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            category.Merge(model);
            DataContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(int id)
        {
            var category = DataContext.Categories.Find(id);

            if ((category == null) || (category.UserId != UserId))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (DataContext.Transactions.Any(t => t.CategoryId == id))
            {
                var errorMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    DeleteErrorMessage,
                    category.Title);

                ModelState.AddModelError(string.Empty, errorMessage);

                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            DataContext.Categories.Remove(category);
            DataContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}