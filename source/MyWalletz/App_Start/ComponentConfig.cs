namespace MyWalletz.Infrastructure
{
    using System;
    using System.Collections.Generic;

    using WebMatrix.WebData;

    using Postal;

    using Controllers;
    using DataAccess;

    using ConfigMgr = System.Configuration.ConfigurationManager;
    using IMvcResolver = System.Web.Mvc.IDependencyResolver;
    using IWebApiResolver = System.Web.Http.Dependencies.IDependencyResolver;

    public class ComponentConfig
    {
        private static readonly Func<IDataContext> CreateDataContext = 
            () => new DataContext();

        private static readonly Func<IUrlSafeSecureDataSerializer>
            CreateUrlSafeSecureDataSerializer =
             () => new UrlSafeSecureDataSerializer(
                 ConfigMgr.AppSettings["urlSafeSecureData.algorithm"],
                 ConfigMgr.AppSettings["urlSafeSecureData.key"],
                 ConfigMgr.AppSettings["urlSafeSecureData.vector"]);

        private static readonly Func<IMailer> CreateMailer =
             () => new Mailer(
                 ConfigMgr.AppSettings["mailer.sender"],
                 new MailUrlResolver(),
                 new EmailService());

        private static readonly Func<ISeedDataLoader> CreateSeedDataLoader =
            () => new SeedDataLoader(() => CreateDataContext());
 
        private static readonly Func<int> GetUser =
            () => WebSecurity.CurrentUserId;

        public static void Register()
        {
            RegisterMvc();
            RegisterWebApi();
        }

        private static void RegisterMvc()
        {
            System.Web
                .Mvc
                .DependencyResolver
                .SetResolver(new DependencyResolver());
        }

        private static void RegisterWebApi()
        {
            System.Web
                .Http
                .GlobalConfiguration
                .Configuration
                .DependencyResolver = new DependencyResolver();
        }

        private sealed class DependencyResolver : IMvcResolver, IWebApiResolver
        {
            public void Dispose()
            {
            }

            public System.Web.Http.Dependencies.IDependencyScope BeginScope()
            {
                return new DependencyResolver();
            }

            public object GetService(Type serviceType)
            {
                if (typeof(HomeController) == serviceType)
                {
                    return new HomeController(
                        CreateDataContext(),
                        GetUser);
                }

                if (typeof(TransactionsController) == serviceType)
                {
                    return new TransactionsController(
                        CreateDataContext(),
                        GetUser);
                }

                if (typeof(AccountsController) == serviceType)
                {
                    return new AccountsController(
                        CreateDataContext(),
                        GetUser);
                }

                if (typeof(CategoriesController) == serviceType)
                {
                    return new CategoriesController(
                        CreateDataContext(),
                        GetUser);
                }

                if (typeof(SessionsController) == serviceType)
                {
                    return new SessionsController(
                        WebSecurity.Login,
                        WebSecurity.Logout);
                }

                if (typeof(UsersController) == serviceType)
                {
                    return new UsersController(
                        (userName, password, requireConfirmation) =>
                            WebSecurity.CreateUserAndAccount(
                                userName,
                                password,
                                requireConfirmationToken: requireConfirmation),
                        CreateMailer(),
                        CreateUrlSafeSecureDataSerializer(),
                        CreateSeedDataLoader());
                }

                if (typeof(SupportsController) == serviceType)
                {
                    return new SupportsController(
                        WebSecurity.ConfirmAccount,
                        WebSecurity.ResetPassword,
                        CreateUrlSafeSecureDataSerializer(),
                        CreateSeedDataLoader());
                }

                if (typeof(PasswordsController) == serviceType)
                {
                    return new PasswordsController(
                        userName =>
                            {
                                try
                                {
                                    return WebSecurity
                                        .GeneratePasswordResetToken(userName);
                                }
                                catch (InvalidOperationException)
                                {
                                }

                                return null;
                            },
                        WebSecurity.ChangePassword,
                        CreateMailer());
                }

                return null;
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                yield break;
            }
        }
    }
}