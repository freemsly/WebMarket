// <copyright company="Recorded Books, Inc" file="SearchProvider.cs">
// Copyright © 2016 All Rights Reserved
// </copyright>

using WebMarket.Api.Search.Contracts;
using WebMarket.Api.Model;

namespace WebMarket.Api.Search
{
    using System.Collections.Specialized;

    /// <summary>
    /// This class...
    /// </summary>
    public class SearchProvider : SearchProvider<Displayable>, ISearch<Displayable>
    {
        public SearchProvider(IQueryResolver<Displayable> resolver, IProfiler profiler)
        {
            Resolver = resolver;
            Profiler = profiler;
            //CacheProvider = cacheProvider;
        }

        public Query<Displayable> ExecuteSearch(NameValueCollection items)
        {
           // ClaimsPrincipalDto.AddMarker("kpi.TitleSearchProvider.begin");
            //var query = CacheProvider.GetOrSet(items.GetHashCodeKey(), () => ParseAndExecute(items), 60);
            var query =  ParseAndExecute(items);
           // ClaimsPrincipalDto.AddMarker("kpi.TitleSearchProvider.end");
            //Profiler.Publish<Displayable>(query);
            return query;
        }

        public Query<Displayable> ExecuteSearch(string url, NameValueCollection items)
        {
            // ClaimsPrincipalDto.AddMarker("kpi.TitleSearchProvider.begin");
            //var cacheKey = items.GetHashCodeKey();
            //var query = CacheProvider.GetOrSet(cacheKey, () => ParseAndExecute(url,items));
            //if (query.Items.Count == 0)
            //{
            //    CacheProvider.Remove(cacheKey);
            //}
            //ClaimsPrincipalDto.AddMarker("kpi.TitleSearchProvider.end");
            //Profiler.Publish<Displayable>(query);
            var query = ParseAndExecute(url, items);
            return query;
        }

        public Query<Displayable> ExecuteSearch(Query<Displayable> query)
        {
            base.ResolveQuery(query);
            //Profiler.Publish<Displayable>(query);
            return query;
        }

        private Query<Displayable> ParseAndExecute(NameValueCollection items)
        {
            var parser = new SearchQueryParser<Displayable>();
            Query<Displayable> query = parser.Parse(items);
            ExecuteQuery(query);
            return query;
        }

        private Query<Displayable> ParseAndExecute(string url, NameValueCollection items)
        {
            var parser = new SearchQueryParser<Displayable>();
            Query<Displayable> query = parser.Parse(url, items);
            ExecuteQuery(query);
            return query;
        }

    }
}
