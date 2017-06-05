// <copyright company="Recorded Books, Inc" file="SearchQueryParser.cs">
// Copyright © 2016 All Right Reserved
// </copyright>


using WebMarket.Common;

namespace WebMarket.Api.Search
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using WebMarket.Api.Search.Contracts;

    public sealed class SearchQueryParser<T> : QueryParser<T> where T:class ,new()
    {
        private static Query<T> _query;
        //private readonly IDictionary<string, IEnumerable<Facet>> _facetDictionary; 
        //private readonly CacheProvider _cache = new RedisCache();
        public SearchQueryParser()
        {
            _query = new Query<T> { Source = "Unknown" };
            //_facetDictionary = _cache.GetOrSet("facets", LoadFacets, 280);
            //_facetDictionary =  LoadFacets;
        }

        private readonly IDictionary<string, Action<KeyValuePair<string, string>>> _metadataDictionary = new Dictionary<string, Action<KeyValuePair<string, string>>>()
        {
            {SearchConstants.ScopeId, MapScopeId},
            {SearchConstants.Ownership, MapOwnership},
            {SearchConstants.Metadata.PageSize, MapPageSize},
            {SearchConstants.Metadata.PageIndex, MapPageIndex},
            {SearchConstants.Metadata.SortBy, MapSortBy},
            {SearchConstants.Metadata.SortOrder, MapSortOrder}
        };

        public override Query<T> Parse(NameValueCollection formFields)
        {
            //ClaimsPrincipalDto.AddMarker("kpi.TitleQueryParser.formFields.begin");
            _query.Facets = new ConcurrentDictionary<string, List<string>>();
            if (formFields != null)
            {
                foreach (KeyValuePair<string, string> field in formFields.AllKeys.SelectMany(formFields.GetValues,(k,v)=> new KeyValuePair<string,string>(k,v)))
                {
                    if (_metadataDictionary.ContainsKey(field.Key))
                    {
                        _metadataDictionary[field.Key].Invoke(field);
                    }
                    else if (SearchTerm.FacetList.Contains(field.Key) && !string.IsNullOrEmpty(field.Value))
                    {
                        AssignFacet(field);
                    }
                    else if (SearchTerm.QueryList.Contains(field.Key) && !string.IsNullOrEmpty(field.Value))
                    {
                        AssignCriterion(field);
                    }
                    else if (field.Key == SearchConstants.NamedQuery)
                    {
                        _query.DataQueryToken = field.Value;
                    }
                }
            }

            //ClaimsPrincipalDto.AddMarker("kpi.TitleQueryParser.formFields.end");
            return _query;
        }

        private static void AssignCriterion(KeyValuePair<string, string> field)
        {
            if (_query.Criterion.ContainsKey(field.Key))
            {
                //_query.Criterion[field.Key] = field.Value.ToDecode();
                _query.Criterion[field.Key] = field.Value;
            }
            else
            {
                //_query.Criterion.Add(field.Key, field.Value.ToDecode());
                _query.Criterion.Add(field.Key, field.Value);
            }
        }

        private void AssignFacet(KeyValuePair<string, string> field)
        {
            //var facetValue = ParseFacetAndSet(field);
            //var facetValue = ParseFacetAndSet(field);
            //if (SearchTerm.CaseSensitiveFacetList.Contains(field.Key))
            //{
            //    //facetValue = facetValue.ToList().ConvertAll(t => t.ToLower());
            //    facetValue = facetValue.ToList();
            //}
            //lock (_query.Facets)
            //{
            //    if (_query.Facets.ContainsKey(field.Key))
            //    {
            //        _query.Facets[field.Key].AddRange(facetValue);
            //    }
            //    else
            //    {
            //        _query.Facets.TryAdd(field.Key, facetValue.ToList());
            //    }
            //}
        }

       

        #region Private Map functions

        private IEnumerable<string> ParseFacetAndSet(KeyValuePair<string, string> field)
        {
            //var facetValue = field.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim().ToDecode()).ToList();
            //if (!_facetDictionary.ContainsKey(field.Key))
            //{
            //    return facetValue;
            //}
            //for (var i = 0; i < facetValue.Count; i++)
            //{
            //    var facet =_facetDictionary[field.Key].Where(t => String.Equals(t.Text, facetValue[i], StringComparison.CurrentCultureIgnoreCase));
            //    if (facet.Any())
            //    {
            //        facetValue[i] = facet.First().Text;
            //    }
            //}
            //return facetValue;
            return null;
        }

        private static void MapScopeId(KeyValuePair<string, string> field)
        {
            _query.ScopeId = Convert.ToInt32(field.Value);
        }

        private static void MapPageIndex(KeyValuePair<string, string> field)
        {
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(field.Value) && int.TryParse(field.Value, out pageIndex))
            {
                _query.PageIndex = pageIndex;
            }
        }

        private static void MapSortBy(KeyValuePair<string, string> field)
        {
            if (SearchTerm.SortByList.Contains(field.Value))
            {
                _query.SortBy = field.Value;
            }
        }

        private static void MapSortOrder(KeyValuePair<string, string> field)
        {
            _query.SortOrder = field.Value.ToLower() == "desc" || field.Value.ToLower() == "asc" ? field.Value.ToLower() : "asc";
        }

        private static void MapPageSize(KeyValuePair<string, string> field)
        {
            int pageSize;
            if (!string.IsNullOrEmpty(field.Value) && int.TryParse(field.Value, out pageSize))
            {
                _query.PageSize = pageSize;
            }
        }

        private static void MapOwnership(KeyValuePair<string, string> field)
        {
            if (_query.Facets.ContainsKey(field.Key))
            {
                if (field.Value == SearchConstants.Facets.Owned ||
                    field.Value == SearchConstants.Facets.UnOwned)
                {
                    _query.Facets[field.Key].AddRange(new List<string>() { field.Value });
                }
            }
            else
            {
                _query.Facets.TryAdd(field.Key, new List<string>() { field.Value });
            }
        }

        //private IDictionary<string, IEnumerable<Facet>> LoadFacets()
        //{
        //    IDictionary<string, IEnumerable<Facet>> facetDictionary = new Dictionary<string, IEnumerable<Facet>>();
        //    var facetProvider = new FacetProvider(new FacetResolver(),new RedisCache());

        //    facetDictionary.Add(SearchConstants.Genre, facetProvider.ExecuteSearch(SearchConstants.Genre).Items);
        //    facetDictionary.Add(SearchConstants.Language, facetProvider.ExecuteSearch(SearchConstants.Language).Items);
        //    facetDictionary.Add(SearchConstants.Audience, facetProvider.ExecuteSearch(SearchConstants.Audience).Items);
        //    facetDictionary.Add(SearchConstants.MediaType, facetProvider.ExecuteSearch(SearchConstants.MediaType).Items);
        //    return facetDictionary;
        //}

        #endregion
       
    }

    
}

