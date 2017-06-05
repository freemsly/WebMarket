
using System.Collections.Specialized;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Api.Infrastructure.Common;
using System.Net;
using System.Net.Http;
using WebMarket.Api.Search;
using WebMarket.Api.Search.Contracts;
using WebMarket.Api.Search.Elastic.Resolver;
using WebMarket.Common;

namespace WebMarket.Api.Infrastructure.Controllers.Search
{


    /// <summary>
    /// 
    /// </summary>
    public sealed class SearchController : Controller
    {
        /// <summary>
        ///  General Search API / All search
        /// </summary>
        /// <param name="version">1</param>
        /// <returns></returns>
        /// <remarks> 
        /// <p>Here is the sample query string request</p>
        /// <p>http://{url}/v1/search?title=dead&amp;page-index=0&amp;page-size=50</p>
        /// <p>http://{url}/v1/search?title=the&amp;author=robin&amp;page-index=0&amp;page-size=50</p>
        /// <p>http://{url}/v1/search?keywords=the&amp;page-index=0&amp;page-size=50</p>
        /// </remarks>
        [HttpGet]
        public HttpResponseMessage Get(int version)
        {
            ClaimsPrincipalDto.AddMarker("kpi.search.begin");

            var searchProvider = new SearchProvider(new SearchQueryResolver(), new ApiProfiler());
            var inputParameters = ProcureParameters(Request);
            var result = searchProvider.ExecuteSearch(inputParameters);

            ClaimsPrincipalDto.AddMarker("kpi.search.end");
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        /// <summary>
        /// Search API returning magazine results
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [ActionName("magazine")]
        public HttpResponseMessage GetMagazineTitle(int version)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.search.magazine.begin");

            //var inputParameters = ProcureParameters(Request);
            //var searchProvider = new SearchProvider(new MagazineQueryResolver(), new ApiProfiler(), new NoCache());
            //var result = searchProvider.ExecuteSearch(inputParameters);

            //ClaimsPrincipalDto.AddMarker("kpi.search.magazine.end");
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            return null;
        }

        /// <summary>
        /// Search API returning grouped results
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [ActionName("grouped")]
        public HttpResponseMessage GetGroupedTitle(int version)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.groupedsearch.begin");

            //var searchProvider = new GSearchProvider(new GSearchQueryResolver(), new ApiProfiler(), new NoCache());
            //var result = searchProvider.ExecuteSearch(ProcureParameters(Request));

            //ClaimsPrincipalDto.AddMarker("kpi.search.end");
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            return null;
        }

        /// <summary>
        ///  Get the customer interest for a ISBN
        /// </summary>
        /// <param name="isbn">9780385366564</param>
        /// <param name="customerId">7035151</param>
        /// <returns>Total Copies Total Holds &amp; Total Circulations</returns>
        /// <remarks> CustomerAccountLibrary Interest  </remarks>
        [ActionName("customerinterest")]
        public HttpResponseMessage GetCustomerInterest(string isbn, int customerId)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.search.customerinterest.begin");

            //var searchProvider = new CustomerInterestProvider(new CustomerInterestResolver(), new ApiProfiler(), new NoCache());
            //var result = searchProvider.ExecuteSearch(isbn, customerId);

            //ClaimsPrincipalDto.AddMarker("kpi.search.customerinterest.end");
            //return Request.CreateResponse(HttpStatusCode.OK, result.Items);
            return null;
        }

        /// <summary>
        ///  Get the orderhistory for a ISBN
        /// </summary>
        /// <param name="isbn">9780385366564</param>
        /// <param name="customerId">7035151</param>
        /// <returns>Order history for given isbn and entityid</returns>
        /// <remarks> orderhistory  </remarks>
        [ActionName("orderhistory")]
        public HttpResponseMessage GetOrderHistory(string isbn, int customerId)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.search.orderhistory.begin");

            //var orderHistoryProvider = new OrderHistoryProvider(new OrderHistoryResolver(), new ApiProfiler(), new NoCache());
            //var result = orderHistoryProvider.ExecuteSearch(isbn, customerId);

            //ClaimsPrincipalDto.AddMarker("kpi.search.orderhistory.end");

            //return Request.CreateResponse(HttpStatusCode.OK, result.Items);
            return null;
        }

        /// <summary>
        ///  Get the customer interest for a ISBN
        /// </summary>
        /// <param name="isbn">9780385366564</param>
        /// <returns>Total Copies Total Holds &amp; Total Circulations</returns>
        /// <remarks> CustomerAccountLibrary Interest  </remarks>
        [ActionName("iteminterest")]
        public HttpResponseMessage GetItemInterest(string isbn)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.search.begin");

            //var searchProvider = new ItemInterestProvider(new ItemInterestResolver(), new ApiProfiler(), new NoCache());
            //var result = searchProvider.ExecuteSearch(isbn);

            //ClaimsPrincipalDto.AddMarker("kpi.search.end");
            //return Request.CreateResponse(HttpStatusCode.OK, result.Items);
            return null;
        }

        private static NameValueCollection ProcureParameters(HttpRequest request)
        {
            var nvc = new NameValueCollection();
            foreach (var item in request.Query)
            {
                nvc.Add(item.Key, item.Value.First());
            }
            return nvc;
        }
    }
}
