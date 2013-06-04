namespace MyWalletz.Controllers
{
    using System;
    using System.Web.Http;

    using DataAccess;

    [Authorize]
    public abstract class ApplicationApiController : ApiController
    {
        protected ApplicationApiController(
            IDataContext dataContext,
            Func<int> getUserId)
        {
            this.DataContext = dataContext;
            this.UserId = getUserId();
        }

        protected IDataContext DataContext { get; private set; }

        protected int UserId { get; private set; }

        protected override void Dispose(bool disposing)
        {
            DataContext.Dispose();
            base.Dispose(disposing);
        }
    }
}