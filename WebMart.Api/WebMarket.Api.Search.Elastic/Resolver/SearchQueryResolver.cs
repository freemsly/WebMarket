// <copyright company="Recorded Books, Inc" file="SearchQueryResolver.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest;
using WebMarket.Api.Model;
using WebMarket.Common;
using WebMarket.Api.Search.Contracts;
using Q = WebMarket.Api.Search.Contracts;

namespace WebMarket.Api.Search.Elastic.Resolver
{
	public sealed class SearchQueryResolver : SearchResolver<Displayable>
    {
        public override void Resolve(Q.Query<Displayable> query)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.SearchQueryResolver.begin");

            //if (!string.IsNullOrEmpty(query.DataQueryToken))
            //{
            //    //NamedQueryFactory.Resolve(query);
            //    return;
            //}

            var builder = new SearchQueryBuilder(query);
            var queryContainer = builder.GetQueryFor<Displayable>(query);

            var results = ElasticSearch.Search<ElasticTitleIndex>(queryContainer);
            //ClaimsPrincipalDto.Add("kpi.query", Encoding.UTF8.GetString(results.RequestInformation.Request));
            query.Items = new List<Displayable>();
            if (results.IsValid && results.Hits.Any())
            {
                foreach (var displayable in results.Documents.Select(item => item.MapToDisplayable()))
                {
                    query.Items.Add(displayable);
                }
                ExtractFilter(query, results);
                query.ResultSetCount = int.Parse(results.Total.ToString(CultureInfo.InvariantCulture));
            }
           // ClaimsPrincipalDto.AddMarker("kpi.SearchQueryResolver.end");
        }

        private static void ExtractFilter(Q.Query<Displayable> query, ISearchResponse<ElasticTitleIndex> results)
        {
            query.Filters = new ConcurrentDictionary<string, List<FacetFilter>>();
            lock (query.Filters)
            {
                if (query.ScopeId > 0)
                {
                    query.Filters.TryAdd(SearchConstants.Ownership,
                        GetOwnershipFacetFilter(results.Aggregations[SearchConstants.Ownership] as SingleBucketAggregate,
                            query.Facets));
                }
                query.Filters.TryAdd(SearchConstants.ListPrice,
                  GetListPriceFacetFilter(results.Aggregations[SearchConstants.ListPrice] as SingleBucketAggregate, query.Facets,
                      SearchConstants.MediaType));
                query.Filters.TryAdd(SearchConstants.MediaType,
                    GetFacetFilter(results.Aggregations[SearchConstants.MediaType] as SingleBucketAggregate, query.Facets,
                        SearchConstants.MediaType));
                query.Filters.TryAdd(SearchConstants.Genre,
                    GetFacetFilter(results.Aggregations[SearchConstants.Genre] as SingleBucketAggregate, query.Facets,
                        SearchConstants.Genre));
                query.Filters.TryAdd(SearchConstants.Audience,
                    GetFacetFilter(results.Aggregations[SearchConstants.Audience] as SingleBucketAggregate, query.Facets,
                        SearchConstants.Audience));
                query.Filters.TryAdd(SearchConstants.Language,
                    GetFacetFilter(results.Aggregations[SearchConstants.Language] as SingleBucketAggregate, query.Facets,
                        SearchConstants.Language));
                query.Filters.TryAdd(SearchConstants.UsageTerm,
                    GetFacetFilter(results.Aggregations[SearchConstants.UsageTerm] as SingleBucketAggregate, query.Facets,
                        SearchConstants.UsageTerm));
                query.Filters.TryAdd(SearchConstants.Facets.ContentAdvisorySex,
                    GetFacetFilter(results.Aggregations[SearchConstants.ContentAdvisory] as SingleBucketAggregate,
                        query.Facets,
                        "sex"));
                query.Filters.TryAdd(SearchConstants.Facets.ContentAdvisoryLanguage,
                    GetFacetFilter(results.Aggregations[SearchConstants.ContentAdvisory] as SingleBucketAggregate,
                        query.Facets,
                        "language"));
                query.Filters.TryAdd(SearchConstants.Facets.ContentAdvisoryViolence,
                    GetFacetFilter(results.Aggregations[SearchConstants.ContentAdvisory] as SingleBucketAggregate,
                        query.Facets,
                        "violence"));
            }
        }


        private static List<FacetFilter> GetFacetFilter(SingleBucketAggregate source, IEnumerable<KeyValuePair<string, List<string>>> facetList, string key)
        {
            return source.Terms(key).Buckets.Select(item => item).Select(facetitem => new FacetFilter()
            {
                Count = int.Parse(facetitem.DocCount.ToString()), 
                Value = facetitem.Key, 
                IsSelected = IsFacetTermExists(facetList, facetitem.Key)
            }).ToList();
        }

        private static List<FacetFilter> GetListPriceFacetFilter(SingleBucketAggregate source, IEnumerable<KeyValuePair<string, List<string>>> facetList, string key)
        {
            var facetFilters = new List<FacetFilter>();
            foreach (var aggregation in source.Aggregations)
            {
                var priceList = (aggregation.Value).Meta.ToList();
                for (int i=0;i< priceList.Count;i++)
                {
                    var priceItem = priceList[i];
                    var facet = new FacetFilter();
                    //facet.Count = int.Parse(priceItem.DocCount.ToString());
                    facet.Value = priceItem.Key;
                    facet.IsSelected = IsFacetTermExists(facetList, priceItem.Key);
                    facetFilters.Add(facet);
                }
                
            }
            return facetFilters;
        }

        private static List<FacetFilter> GetOwnershipFacetFilter(SingleBucketAggregate source, IEnumerable<KeyValuePair<string, List<string>>> facetList)
        {
                var facetFilters = new List<FacetFilter>();
                foreach (var aggregation in source.Aggregations)
                {
                    var facet = new FacetFilter();
                    facet.Count = int.Parse((aggregation.Value as SingleBucketAggregate).DocCount.ToString(CultureInfo.InvariantCulture));
                    facet.Value = aggregation.Key;
                    facet.IsSelected = IsFacetTermExists(facetList, aggregation.Key);
                    facetFilters.Add(facet);
                }
                return facetFilters;
        }


        private static bool IsFacetTermExists(IEnumerable<KeyValuePair<string, List<string>>> facetList, string facetItem)
        {
            return facetList.Select(keyValuePair => keyValuePair.Value.Contains(facetItem)).FirstOrDefault();
        }

       
    }
}
