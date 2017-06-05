// <copyright company="Recorded Books, Inc" file="ApiConfigRegistrar.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace WebMarket.Api.Infrastructure.Common
{
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    /// 
    /// </summary>
    public static class ApiConfigRegistrar
    {

        public static IRouter BuildRouter(IApplicationBuilder app)
        {
            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");

            });

            var routeBuilder = new RouteBuilder(app);
            routeBuilder.DefaultHandler = trackPackageRouteHandler;
            //routeBuilder.MapRoute("Track Package Route", "package/{operation:regex(^(track|create|detonate)$)}/{id:int}");
            //routeBuilder.MapGet("hello/{name}", context => {var name = context.GetRouteValue("name");return context.Response.WriteAsync($"Hi, {name}!");});
            RegisterSearch(routeBuilder);

            return routeBuilder.Build();
        }



        private const string SearchUrl = "v{version}/search";
        private static void RegisterSearch(RouteBuilder builder)
        {
            builder.MapRoute(name: "Search", template: SearchUrl, defaults: new {controller = "Search", action = "Get"});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        //public static void Register( config)
        //{
        //    var xmlFormatter = config.Formatters.FirstOrDefault(x => x.GetType().Equals(typeof(System.Net.Http.Formatting.XmlMediaTypeFormatter)));
        //    if (xmlFormatter != null)
        //    {
        //        config.Formatters.Remove(xmlFormatter);
        //    }


        //    RegisterSearch(config);
        //    RegisterMagazineSearch(config);
        //    RegisterGroupedSearch(config);
        //    RegisterCustomerInterest(config);
        //    RegisterOrderHistory(config);
        //    RegisterItemInterest(config);
        //    RegisterSuggestive(config);
        //    RegisterFacet(config);
        //    RegisterPing(config);
        //    RegisterTitle(config);
        //    RegisterTitleItemInterest(config);
        //    RegisterTitleCustomerInterest(config);

        //    RegisterMarcFile(config);
        //    RegisterMarcFileCustom(config);
        //    RegisterMarcFileCustomFTP(config);
        //    RegisterMarcUploadFile(config);

        //    //WFHowes
        //    //RegisterMarcWFHUploadFile(config);
        //}

        //private static string CustomerCartUrl = "v{version}/customer/{customerId}/carts/{cartId}";
        //private static void RegisterCustomerCart(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Cart",
        //        routeTemplate: CustomerCartUrl,
        //        defaults: new { controller = "Cart", customerId = RouteParameter.Optional, cartId = RouteParameter.Optional }

        //        );
        //}

        //private static string CustomerMarcUrl = "v{version}/customer/{customerId}/marcs/{tempId}";
        //private static void RegisterCustomerMarc(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Marc",
        //        routeTemplate: CustomerMarcUrl,
        //        defaults: new { controller = "Marc", tempId = RouteParameter.Optional }

        //        );
        //}

        //private static string CustomerOrdersUrl = "v{version}/customer/{customerId}/orders";
        //private static void RegisterCustomerOrders(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Orders",
        //        routeTemplate: CustomerOrdersUrl,
        //        defaults: new { controller = "Orders", tempId = RouteParameter.Optional }

        //        );
        //}

        //private static string CartInfoUrl = "v{version}/customer/{customerId}/cartinfo/{cartId}/";
        //private static void RegisterCartInfo(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "CartInfo",
        //        routeTemplate: CartInfoUrl,
        //        defaults: new { controller = "CartInfo", lineId = RouteParameter.Optional }

        //        );
        //}
        //private static string ProdStockUrl = "v{version}/customer/{customerId}/prodstock/";
        //private static void RegisterProdStock(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "ProdStock",
        //        routeTemplate: ProdStockUrl,
        //        defaults: new { controller = "ProdStock", lineId = RouteParameter.Optional }

        //        );
        //}


        //private const string SearchUrl = "v{version}/search";
        //private static void RegisterSearch(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Search",
        //        routeTemplate: SearchUrl,
        //        defaults: new { controller = "Search", action="Get"}

        //        );
        //}

        //private const string SearchMagazineUrl = "v{version}/search/magazine";
        //private static void RegisterMagazineSearch(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Magazine",
        //        routeTemplate: SearchMagazineUrl,
        //        defaults: new { controller = "Search", action = "magazine" }
        //        );
        //}

        //private const string SearchGroupedUrl = "v{version}/search/grouped";
        //private static void RegisterGroupedSearch(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Grouped",
        //        routeTemplate: SearchGroupedUrl,
        //        defaults: new { controller = "Search" , action="grouped"}
        //        );
        //}

        //private const string ItemInterestUrl = "v{version}/search/{isbn}/iteminterest";
        //private static void RegisterItemInterest(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "ItemInterest",
        //        routeTemplate: ItemInterestUrl,
        //        defaults: new { controller = "Search", action = "iteminterest", isbn="" }

        //        );
        //}

        //private const string OwnershipUrl = "v{version}/search/{isbn}/customerinterest/{customerId}";
        //private static void RegisterCustomerInterest(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "CustomerInterest",
        //        routeTemplate: OwnershipUrl,
        //        defaults: new { controller = "Search", action = "customerinterest", isbn = "", customerId = 0 }

        //        );
        //}

        //private const string OrderHistoryUrl = "v{version}/search/{isbn}/orderhistory/{customerId}";
        //private static void RegisterOrderHistory(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "OrderHistory",
        //        routeTemplate: OrderHistoryUrl,
        //        defaults: new { controller = "Search", action = "orderhistory", isbn = "", customerId = 0 }

        //        );
        //}

        //private const string SuggestiveUrl = "v{version}/suggestive/{token}/{value}";
        //private const string SuggestiveAllUrl = "v{version}/suggestive/{value}";
        //private const string MagazineSuggestiveUrl = "v{version}/suggestive/magazine/{value}";
        //private static void RegisterSuggestive(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //       name: "SuggestiveAll",
        //       routeTemplate: SuggestiveAllUrl,
        //       defaults: new { controller = "Suggestive" , action = "Get" , value=""}

        //       );
        //    config.Routes.MapHttpRoute(
        //        name: "MagazineSuggestive",
        //        routeTemplate: MagazineSuggestiveUrl,
        //        defaults: new { controller = "Suggestive", action = "magazine", value="" }

        //    );
        //    config.Routes.MapHttpRoute(
        //        name: "Suggestive",
        //        routeTemplate: SuggestiveUrl,
        //        defaults: new { controller = "Suggestive", action = "Get" , token="", value=""}

        //        );
        //}

        
       

        
        //private const string FacetUrl = "v{version}/facet/{token}";

        //private static void RegisterFacet(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Facet",
        //        routeTemplate: FacetUrl,
        //        defaults: new {controller = "Facet"}
        //        );
        //}

        //private const string PingUrl = "v{version}/ping";

        //private static void RegisterPing(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Ping",
        //        routeTemplate: PingUrl,
        //        defaults: new { controller = "Ping" }
        //        );
        //}

        //private const string TitleUrl = "v{version}/title/{isbn}";

        //private static void RegisterTitle(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Title",
        //        routeTemplate: TitleUrl,
        //        defaults: new { controller = "Title" }
        //        );
        //}

        //private const string TitleItemInterestUrl = "v{version}/title/{isbn}/iteminterest";
        //private static void RegisterTitleItemInterest(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "TitleItemInterest",
        //        routeTemplate: TitleItemInterestUrl,
        //        defaults: new { controller = "Title", action = "iteminterest" }

        //        );
        //}

        //private const string TitleOwnershipUrl = "v{version}/title/{isbn}/customerinterest/{customerId}";
        //private static void RegisterTitleCustomerInterest(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "TitleCustomerInterest",
        //        routeTemplate: TitleOwnershipUrl,
        //        defaults: new { controller = "Title", isbn = "", customerId = 0, action="customerinterest" }

        //        );
        //}
        
        

        //private const string MarcFileUrl = "v{version}/marcfile/{productNumbers}";
        //private static void RegisterMarcFile(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "marcfile",
        //        routeTemplate: MarcFileUrl,
        //        defaults: new { controller = "MarcFile", productNumber = "" }
        //        );
        //}

        //private const string MarcCustomFileUrl = "v{version}/marccustom";
        //private static void RegisterMarcFileCustom(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "marccustom",
        //        routeTemplate: MarcCustomFileUrl,
        //        defaults: new { controller = "MarcCustomFile", objData= "" }
        //        );
        //}

        //private const string MarcCustomFileFTPUrl = "v{version}/marccustomftp";
        //private static void RegisterMarcFileCustomFTP(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "marccustomftp",
        //        routeTemplate: MarcCustomFileFTPUrl,
        //        defaults: new { controller = "MarcFTP", requestMarcFTPDetails = "" }
        //        );
        //}

        //private const string MarcUploadFileUrl = "v{version}/marcUpload";
        //private static void RegisterMarcUploadFile(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "marcUploadFile",
        //        routeTemplate: MarcUploadFileUrl,
        //        defaults: new { controller = "MarcUploadFile", requestMarcFTPDetails = "" }
        //        );
        //}

        //private const string MarcWFHUploadFileUrl = "v{version}/marcWFHUpload";
        //private static void RegisterMarcWFHUploadFile(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "marcWFHUploadFile",
        //        routeTemplate: MarcWFHUploadFileUrl,
        //        defaults: new { controller = "MarcWFHUploadFile"}
        //        );
        //}



    }
}
