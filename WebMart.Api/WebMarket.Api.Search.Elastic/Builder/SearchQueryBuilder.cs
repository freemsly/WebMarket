// <copyright company="Recorded Books, Inc" file="SearchQueryBuilder.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System;
using System.Collections.Generic;
using WebMarket.Api.Model;
using WebMarket.Common;
using Q = WebMarket.Api.Search.Contracts;

namespace WebMarket.Api.Search.Elastic
{
    using Nest;
    using System.Linq;

    public sealed class SearchQueryBuilder : QueryBuilder<ElasticTitleIndex>
    {

        public static int ScopeId { get; set; }
        public SearchQueryBuilder(Q.Query<Displayable> query)
        {
            ScopeId = query.ScopeId;
        }
        private readonly IDictionary<string, Func<KeyValuePair<string, List<string>>, QueryContainer>> _filterMap = new Dictionary
            <string, Func<KeyValuePair<string, List<string>>, QueryContainer>>()
        {
            {SearchConstants.MediaType, MapFilterParam},
            {SearchConstants.DigitalRightsManagement, MapFilterParam},
            {SearchConstants.Genre, MapGenreParam},
            {SearchConstants.Language, MapFilterParam},
            {SearchConstants.Audience, MapFilterParam},
            {SearchConstants.Publisher, MapPublisherParam},
            {SearchConstants.Sop, MapSopParam},
            {SearchConstants.Subscription, MapSubscriptionParam},
            {SearchConstants.GroupId, MapGroupParam},
            {SearchConstants.UsageTerm, MapUsageTermParam},
            {SearchConstants.Isbn, MapIsbnParam},
            {SearchConstants.IsComingSoon, MapFilterBoolParam},
            {SearchConstants.FictionNonfiction, MapFilterBoolParam},
            {SearchConstants.PublishedDate, MapPublishedDateParam},
            {SearchConstants.ReleaseDate, MapReleaseDateParam},
            {SearchConstants.Duration, MapDurationParam},
            {SearchConstants.MediaCount, MapMediaCountParam},
            {SearchConstants.CountryCode, MapSalesRightsParam},
            {SearchConstants.HoldsRatio, MapChildRangeFilterParam},
            {SearchConstants.Ownership, MapOwnershipParam},
            {SearchConstants.ListPrice, MapPriceParam},
            {SearchConstants.Facets.ContentAdvisoryLanguage, MapContentAdvisoryParam},
            {SearchConstants.Facets.ContentAdvisorySex, MapContentAdvisoryParam},
            {SearchConstants.Facets.ContentAdvisoryViolence, MapContentAdvisoryParam},
        };


        private readonly IDictionary<string, Func<KeyValuePair<string, string>, QueryContainer>> _queryMap = new Dictionary
            <string, Func<KeyValuePair<string, string>, QueryContainer>>()
        {
            {SearchConstants.Title, MapQueryParam},
            {SearchConstants.Keywords, MapQueryKeywordsParam},
            
            {SearchConstants.Author, MapAuthorQueryParam},
            {SearchConstants.Narrator, MapQueryParam},
            {SearchConstants.Imprint, MapQueryParam},
            {SearchConstants.Series, MapQueryParam},
        };


        private readonly IDictionary<string, string> _sortDictionary = new Dictionary<string, string>()
        {
            {SearchConstants.Title, "title.sort"},
            {SearchConstants.Author, "authorsort"},
            {SearchConstants.Narrator, "narrator.sort"},
            {SearchConstants.ReleaseDate, "activatedon.sort"},
            {SearchConstants.PublishedDate, "publishedon"},
            {SearchConstants.ListPrice, "listprice"},
            {SearchConstants.SystemCirculationCopies, "systemcirculationcopies"},
            {SearchConstants.SystemHoldsCopies, "systemholdscopies"},
            {SearchConstants.SystemTotalCopies, "systemtotalcopies"},
        };

        private static readonly IDictionary<string, string> ElasticFieldMapDictionary = new Dictionary<string, string>()
        {
            {SearchConstants.Facets.ContentAdvisoryLanguage, "contentadvisory.language"},
            {SearchConstants.Facets.ContentAdvisorySex, "contentadvisory.sex"},
            {SearchConstants.Facets.ContentAdvisoryViolence, "contentadvisory.sex"},
            
        };

        private static QueryContainer MapQueryKeywordsParam(KeyValuePair<string, string> query)
        {
            var queryContainer = Nest.Query<ElasticTitleIndex>.Bool(b=>b
                .Must(m => m.MultiMatch(t => t.Fields(f => f.Field(fi=>fi.Title).Field(fi=>fi.ItemSubtitle).Field(fi => fi.Authors)
							.Field(fi => fi.Author).Field(fi => fi.Genre).Field(fi => fi.Series).Field(fi => fi.Isbn).Field(fi => fi.Narrators)
							.Field(fi => fi.Narrator).Field(fi => fi.SourceItemId))
							.Query(query.Value).Type(TextQueryType.CrossFields).Boost(2.0).MinimumShouldMatch("75%")))
                .Should(s=>s.Match(m=>m.Field(f=>f.Title.Suffix("sort")).Query(query.Value).Boost(5))
                            || s.MatchPhrase(m => m.Field(f => f.Title.Suffix("sort")).Query(query.Value).Boost(4))
                            || s.MatchPhrasePrefix(m => m.Field(f => f.Title.Suffix("sort")).Query(query.Value).Boost(10))));
            
            return queryContainer;
        }

        private static QueryContainer MapContentAdvisoryParam(KeyValuePair<string, List<string>> facets)
        {
            var queryContainer = Query<ElasticTitleIndex>.Terms(t=>t.Field(ElasticFieldMapDictionary[facets.Key]).Terms(facets.Value));
            return queryContainer;
        }

        private static QueryContainer MapQueryParam(KeyValuePair<string, string> query)
        {
            QueryContainer queryContainer = Query<ElasticTitleIndex>.Match(t => t.Field(query.Key).Query(query.Value).Operator(Operator.And));
            return queryContainer;
        }

        private static QueryContainer MapAuthorQueryParam(KeyValuePair<string, string> query)
        {
            var authorList = query.Value.Split('|');
            QueryContainer queryContainer = null;
            foreach (var author in authorList)
            {
                queryContainer |= Nest.Query<ElasticTitleIndex>.Match(t => t.Field(query.Key).Query(author).Operator(Operator.And));
            }
            //QueryContainer queryContainer = Nest.Query<ElasticTitleIndex>.Match(t => t.OnField(query.Key).Query(query.Value).Operator(Operator.And));
            return queryContainer;
        }

        private static QueryContainer MapOwnershipParam(KeyValuePair<string, List<string>> facets)
        {
            QueryContainer QueryContainer;
            if (facets.Value.First() == SearchConstants.Facets.Owned)
            {
                //QueryContainer = Query<ElasticTitleIndex>.Bool(bo=>bo.Must(mus=>mus.HasChild<ElasticOwnershipIndex>(hc => hc.Query(f => 
                //    f.Bool(b => b.Must(mu => mu.Term(te => te.ScopeId, ScopeId),ra=>ra.Range(r=>r.Field(of=>of.TotalCopies).GreaterThan(0))))))));
            }
            else
            {
                //QueryContainer = Query<ElasticTitleIndex>.Bool(b => b.Must(mus => mus.Bool(bo=>bo.MustNot(mnot=>mnot.HasChild<ElasticOwnershipIndex>(hc => hc.Query(f =>
                //    f.Bool(boo => boo.Must(mu => mu.Term(te => te.ScopeId, ScopeId), ra => ra.Range(r => r.Field(of => of.TotalCopies).GreaterThan(0))))))))));
            }
            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapIsbnParam(KeyValuePair<string, List<string>> facets)
        {
            //QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("isbn", facets.Value, TermsExecution.Or);
            //return QueryContainer;
            return null;
        }

        private static QueryContainer MapSopParam(KeyValuePair<string, List<string>> facets)
        {
            QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t=>t.Field("sop.name").Terms(facets.Value));
            return QueryContainer;
        }

        private static QueryContainer MapSubscriptionParam(KeyValuePair<string, List<string>> facets)
        {
	        QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t => t.Field("subscriptions").Terms(facets.Value));
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("subscriptions", facets.Value, TermsExecution.Or);
            return QueryContainer;
        }

        private static QueryContainer MapGroupParam(KeyValuePair<string, List<string>> facets)
        {
	        QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t => t.Field("group.id").Terms(facets.Value));
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("group.id", facets.Value, TermsExecution.Or);
            return QueryContainer;
        }

        private static QueryContainer MapUsageTermParam(KeyValuePair<string, List<string>> facets)
        {
	        QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t => t.Field("usageterms").Terms(facets.Value));
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("usageterms", facets.Value, TermsExecution.Or);
            return QueryContainer;
        }

        private static QueryContainer MapGenreParam(KeyValuePair<string, List<string>> facets)
        {
	        QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t => t.Field("genres").Terms(facets.Value));
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("genres", facets.Value,TermsExecution.Or);
            return QueryContainer;
        }
        private static QueryContainer MapPublisherParam(KeyValuePair<string, List<string>> facets)
        {
	        QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t => t.Field("publishers").Terms(facets.Value));
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("publishers", facets.Value, TermsExecution.Or);
            return QueryContainer;
        }


        private static QueryContainer MapFilterParam(KeyValuePair<string, List<string>> facets)
        {
	        QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(t => t.Field(facets.Key).Terms(facets.Value));
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms(facets.Key, facets.Value, TermsExecution.Or);
            return QueryContainer;
        }

        private static QueryContainer MapChildRangeFilterParam(KeyValuePair<string, List<string>> facets)
        {
            //QueryContainer QueryContainer = Query<ElasticTitleIndex>.Bool(bo => bo.Must(mus => mus.HasChild<ElasticOwnershipIndex>(hc => hc.Query(f =>
            //       f.Bool(b => b.Must(ra => ra.Term(te=>te.ScopeId,ScopeId),ra=> ra.Range(r => r.OnField(of => of.HoldsRatio).GreaterOrEquals(facets.Value.FirstOrDefault())), ra => ra.Range(ro=>ro.OnField(of=>of.TotalCopies).Greater(0))))))));

            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapFilterBoolParam(KeyValuePair<string, List<string>> facets)
        {

            //QueryContainer QueryContainer = Filter<ElasticTitleIndex>.Term(facets.Key, facets.Value.First().ToLower() == "true");
            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapDurationParam(KeyValuePair<string, List<string>> facets)
        {
            //QueryContainer QueryContainer = Query<ElasticTitleIndex>.Range(t=>t.LowerOrEquals(facets.Value.First()).OnField(facets.Key));
            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapReleaseDateParam(KeyValuePair<string, List<string>> facets)
        {
            //QueryContainer QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("activatedon").GreaterOrEquals(facets.Value.First()).LowerOrEquals(facets.Value.Last()));
            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapPriceParam(KeyValuePair<string, List<string>> facets)
        {
            QueryContainer QueryContainer;
            //if (facets.Value.Count > 1)
            //{
            //    var minValue = facets.Value.First();
            //    var maxValue = facets.Value.Last();
            //    if (minValue != "-1" &&  maxValue != "-1")
            //    { 
            //        QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("pricing.price.list").GreaterOrEquals(minValue).LowerOrEquals(maxValue));
            //    }
            //    else if (minValue == "-1" && maxValue != "-1")
            //    {
            //        QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("pricing.price.list").LowerOrEquals(maxValue));
            //    }
            //    else if (minValue != "-1" && maxValue == "-1")
            //    {
            //        QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("pricing.price.list").GreaterOrEquals(minValue));
            //    }
            //    else
            //    {
            //        QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("pricing.price.list").GreaterOrEquals(0));
            //    }
            //}
            //else
            //{
            //    QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("pricing.price.list").LowerOrEquals(facets.Value.First()));
            //}
            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapPublishedDateParam(KeyValuePair<string, List<string>> facets)
        {
            //QueryContainer QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField("publishedon").GreaterOrEquals(facets.Value.First()).LowerOrEquals(facets.Value.Last()));
            //return QueryContainer;
	        return null;
        }

        private static QueryContainer MapMediaCountParam(KeyValuePair<string, List<string>> facets)
        {
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Range(t => t.OnField(facets.Key).GreaterOrEquals(facets.Value.First()));
			//return QueryContainer;
			return null;
		}

        private static QueryContainer MapSalesRightsParam(KeyValuePair<string, List<string>> facets)
        {
			//QueryContainer QueryContainer = Query<ElasticTitleIndex>.Terms("salesrights", facets.Value);
			//return QueryContainer;
			return null;
		}


        public override SearchDescriptor<ElasticTitleIndex> GetQueryFor<TU>(Q.Query<TU> query)
        {
            QueryContainer queryContainer= null;
            if (!query.Criterion.Any())
            {
                queryContainer = GetDefaultQuery();
            }
            else
            {
                foreach (KeyValuePair<string, string> term in query.Criterion)
                {
                    if (_queryMap.ContainsKey(term.Key))
                    {
                        queryContainer &= _queryMap[term.Key](term);
                    }
                    else
                    {
                        queryContainer &= new MatchQuery()
                        {
                            Field = term.Key,
                            Query = term.Value
                        };
                    }
                }
            }

            //var queryContainer = new QueryContainer();
            //if (query.Facets != null && query.Facets.Count > 0)
            //{
            //    foreach (KeyValuePair<string, List<string>> facet in query.Facets)
            //    {
            //        if (_filterMap.ContainsKey(facet.Key))
            //        {
            //            QueryContainer &= _filterMap[facet.Key](facet);   
            //        }
            //        else
            //        {
            //            QueryContainer &= Query<ElasticTitleIndex>.Terms(t=>t.Field(facet.Key).Terms(facet.Value));
            //        }
            //    }
            //}


            var searchDescriptor = new SearchDescriptor<ElasticTitleIndex>()
                .Query(q => queryContainer)
                .Size(query.PageSize)
                .From(query.PageIndex*query.PageSize).ExecuteOnPrimary().RequestCache(false)
                .Aggregations(
                    a => 
                      a
                        .Filter(SearchConstants.ListPrice, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Histogram(SearchConstants.ListPrice, l => l.Field("pricing.price.list").Interval(50).MinimumDocumentCount(0).ExtendedBounds(0, 1000).OrderDescending("_key"))))
                        .Filter(SearchConstants.Language, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Terms(SearchConstants.Language, l => l.Field(f => f.Language).MinimumDocumentCount(0).OrderAscending("_term").Size(30))))
                        .Filter(SearchConstants.UsageTerm, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Terms(SearchConstants.UsageTerm, l => l.Field(f => f.UsageTerms).MinimumDocumentCount(0).OrderAscending("_term").Size(30))))
                        .Filter(SearchConstants.MediaType, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Terms(SearchConstants.MediaType, l => l.Field(f => f.MediaType).MinimumDocumentCount(0).OrderAscending("_term"))))
                        .Filter(SearchConstants.Genre, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Terms(SearchConstants.Genre, l => l.Field(f => f.Genres).MinimumDocumentCount(0).OrderAscending("_term").Size(50))))
                        .Filter(SearchConstants.Audience, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Terms(SearchConstants.Audience, l => l.Field(f => f.Audience).MinimumDocumentCount(0).OrderAscending("_term").Size(30))))
                        .Filter(SearchConstants.ContentAdvisory, fc => fc.Filter(f => queryContainer).Aggregations(agg => agg.Terms("sex", l => l.Field(ElasticFieldMapDictionary[SearchConstants.Facets.ContentAdvisorySex]).MinimumDocumentCount(0).OrderAscending("_term"))
                                                 .Terms("language", l => l.Field(ElasticFieldMapDictionary[SearchConstants.Facets.ContentAdvisoryLanguage]).MinimumDocumentCount(0).OrderAscending("_term"))
                                                 .Terms("violence", l => l.Field(ElasticFieldMapDictionary[SearchConstants.Facets.ContentAdvisoryViolence]).MinimumDocumentCount(0).OrderAscending("_term"))))
                        .Filter(SearchConstants.Ownership , fc => fc.Filter(f => queryContainer)
                            //.Aggregations(agg => 
                            //    agg
                            //    .Filter(SearchConstants.Facets.Owned, f => f
                            //        .Filter(fi =>  fi.Bool(bo=>bo.Must(m=>m.HasChild<ElasticOwnershipIndex>(hc=>hc.Query(fil=>fil.Bool(b=>b.Must(mu=>mu.Term(t=>t.ScopeId, query.ScopeId),ra=>ra.Range(r=>r.OnField(of=>of.TotalCopies).Greater(0)) ))))))
                            //        ))
                            //    .Filter(SearchConstants.Facets.UnOwned, f => f
                            //        .Filter(fi =>  fi.Bool(bo=>bo.MustNot(m=>m.HasChild<ElasticOwnershipIndex>(hc=>hc.Query(fil=>fil.Bool(b=>b.Must(mu=>mu.Term(t=>t.ScopeId, query.ScopeId),ra=>ra.Range(r=>r.OnField(of=>of.TotalCopies).Greater(0)) ))))))
                            //        ))
                            //)
							))
                        ;
            //add sort order
            if (!string.IsNullOrEmpty(query.SortBy) && _sortDictionary.ContainsKey(query.SortBy))
            {
                searchDescriptor.Sort(s =>s.Field(f=>f.Field(_sortDictionary[query.SortBy]).MissingLast().Order(query.SortOrder.ToLower() == "desc" ? SortOrder.Descending : SortOrder.Ascending)));
            }
            return searchDescriptor;
        }
    }
}
