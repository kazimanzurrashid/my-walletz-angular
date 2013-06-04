namespace MyWalletz
{
    using System.Web.Http;

    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonSettings = config.Formatters
                .JsonFormatter
                .SerializerSettings;

            jsonSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            jsonSettings.Converters.Add(new StringEnumConverter());

            config.Routes.MapHttpRoute(
                "TransactionAPI",
                "api/accounts/{accountId}/transactions",
                new { controller = "transactions" });

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}