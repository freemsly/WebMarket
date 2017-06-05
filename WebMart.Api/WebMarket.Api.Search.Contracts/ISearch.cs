

namespace WebMarket.Api.Search.Contracts
{
    using System.Collections.Specialized;

    public interface ISearch<T> where T : class, new()
    {
        Query<T> ExecuteSearch(NameValueCollection queryString);
        Query<T> ExecuteSearch(string url, NameValueCollection queryString);
        Query<T> ExecuteSearch(Query<T> query);        
    }
}
