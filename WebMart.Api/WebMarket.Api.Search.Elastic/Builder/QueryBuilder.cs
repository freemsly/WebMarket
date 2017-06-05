// <copyright company="Recorded Books, Inc" file="ElasticQueryBuilder.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System.Linq;

namespace WebMarket.Api.Search.Elastic
{
    using Nest;
    using Q = WebMarket.Api.Search.Contracts;

	public abstract class QueryBuilder<T> where T : class, new()
    {
       
        public virtual QueryContainer GetDefaultQuery()
        {
            QueryContainer queryContainer = new MatchAllQuery();
            return queryContainer;
        }

        public virtual SearchDescriptor<T> GetQueryFor<U>(Q.Query<U> query) where U: class,new() 
        {

			//var filterContainer = new FilterContainer();

			//if ( query.Facets!= null && query.Facets.Count > 0)
			//{
			//    filterContainer = query.Facets.Aggregate(filterContainer,
			//        (current, facet) => current & Filter<T>.Term(facet.Key, facet.Value));
			//}

			QueryContainer queryContainer = new MatchAllQuery();
            if (query.Criterion != null && query.Criterion.Count > 0)
            {
                queryContainer = query.Criterion.Aggregate(queryContainer, (current, field) => current & new TermQuery
                {
                    Field = field.Key,
                    Value = field.Value
                });
            }
            var searchDescriptor = new SearchDescriptor<T>()
                .Query(q=>queryContainer)
                .Size(query.PageSize)
                .From(query.PageIndex);
            return searchDescriptor;
        }
    }

}
