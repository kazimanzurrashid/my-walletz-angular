[assembly: WebActivator.PreApplicationStartMethod(typeof(MyWalletz.Infrastructure.StarterKitConfig), "PreStart")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(MyWalletz.Infrastructure.StarterKitConfig), "PostStart")]

namespace MyWalletz.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using WebMatrix.WebData;

    using DataAccess;

    public class StarterKitConfig
    {
        public static void PreStart()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void PostStart()
        {
            RegisterBundles(BundleTable.Bundles);
            RegisterMembership();
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/angular-ui.css")
                .Include("~/Content/application.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/json2")
                .Include("~/Scripts/json2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-resource.js")
                .Include("~/Scripts/ui.*"));

            bundles.Add(new ScriptBundle("~/bundles/application")
                .IncludeDirectory("~/Scripts/application", "*.js", true));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute(
                "PasswordsRpcApi",
                "api/passwords/{action}",
                new { controller = "passwords" });

            routes.MapRoute(
                "user-confirmation",
                "users/confirm/{token}",
                new
                {
                    controller = "Supports",
                    action = "ConfirmUser"
                });

            routes.MapRoute(
                "password-reset",
                "passwords/reset/{token}",
                new
                {
                    controller = "Supports",
                    action = "ResetPassword"
                });
        }

        public static void RegisterMembership()
        {
            Database.SetInitializer<DataContext>(null);

            try
            {
                using (var context = new DataContext())
                {
                    if (!context.Database.Exists())
                    {
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }
                }

                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "mw_Users", "Id", "Email", true);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", e);
            }
        }
    }
}