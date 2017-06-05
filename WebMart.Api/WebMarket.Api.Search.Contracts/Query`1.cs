using System;
using System.Runtime.Serialization;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebMarket.Api.Search.Contracts
{
    [DataContract]
    public class Query<T> where T : class, new()
    {
        [DataMember(Name = "scopeid")]
        public int ScopeId { get; set; }
        [DataMember(Name = "page-index")]
        public int PageIndex { get; set; }
        [DataMember(Name = "page-size")]
        public int PageSize { get; set; }
        [DataMember(Name = "page-count")]
        public int PageCount
        {
            get
            {
                int numberOfPages = 0;
                if (ResultSetCount > 0 && PageSize > 0)
                {
                    numberOfPages = (int)Math.Ceiling(ResultSetCount / (double)PageSize);
                }
                return numberOfPages;
            }
        }

        [DataMember(Name = "total-count")]
        public int ResultSetCount { get; set; }
        [DataMember(Name = "sort-by")]
        public string SortBy { get; set; }
        [DataMember(Name = "sort-order")]
        public string SortOrder { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Criterion { get; set; }

        [IgnoreDataMember]
        public ConcurrentDictionary<string, List<string>> Facets { get; set; }

        [IgnoreDataMember]
        public string DataQueryToken { get; set; }

        [IgnoreDataMember]
        public string SessionId { get; set; }

        [IgnoreDataMember]
        public string Source { get; set; }

        [IgnoreDataMember]
        public string Zone { get; set; }

        [IgnoreDataMember]
        public bool IsNamedQuery => !string.IsNullOrEmpty(DataQueryToken);

        [DataMember]
        public List<T> Items { get; set; }
        [DataMember]
        public ConcurrentDictionary<string, List<FacetFilter>> Filters { get; set; }

        public Query()
        {
            PageSize = 50;
            PageIndex = 0;
            Facets = new ConcurrentDictionary<string, List<string>>();
            Criterion = new Dictionary<string, string>();
            Items = new List<T>();
            Filters = new ConcurrentDictionary<string, List<FacetFilter>>();
        }
    }

    public class FacetFilter
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
    }
}
