namespace MyWalletz.Controllers
{
    using System;
    using System.Web.Mvc;

    using DataAccess;
    using Models;

    public class HomeController : Controller
    {
        private readonly IDataContext dataContext;
        private readonly Func<int> getUserId;

        private bool? authenticated;

        public HomeController(IDataContext dataContext, Func<int> getUserId)
        {
            this.dataContext = dataContext;
            this.getUserId = getUserId;
        }

        public bool IsAuthenticated
        {
            get
            {
                if (authenticated == null)
                {
                    authenticated = Request.IsAuthenticated;
                }

                return authenticated.GetValueOrDefault();
            }

            set
            {
                authenticated = value;
            }
        }

        public ActionResult Index()
        {
            var model = new Home();

            if (IsAuthenticated)
            {
                var userId = getUserId();

                model.Categories = dataContext.FindCategories(userId);
                model.Accounts = dataContext.FindAccounts(userId);
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            dataContext.Dispose();
            base.Dispose(disposing);
        }
    }
}