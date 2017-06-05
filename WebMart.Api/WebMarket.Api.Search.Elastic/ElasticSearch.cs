// <copyright company="Recorded Books, Inc" file="ElasticSearch.cs">
// Copyright © 2015 All Right Reserved
// </copyright>


using System;

namespace WebMarket.Api.Search.Elastic
{
    using Nest;

    public static class ElasticSearch
    {
        private static readonly IElasticClient Client = new ElasticClient(ElasticsearchConfiguration.Settings());

        public static ISearchResponse<T> Search<T>(SearchDescriptor<T> searchDescriptor) where T : class , new()
        {
            ISearchResponse<T> results = new SearchResponse<T>();
            try
            {
                results = Client.Search<T>(searchDescriptor);
            }
            catch (Exception ex)
            {
               
            }
            
            return results;
        }

        public  static ISuggestResponse<T> Suggestive<T>(Func<SuggestDescriptor<T>, SuggestDescriptor<T>> request) where T : class , new()
        {
            ISuggestResponse<T> results = new SuggestResponse<T>();
            try
            {
                results = Client.Suggest(request);
            }
            catch (Exception ex)
            {
               
            }
           
            return results;
            
        }

        public static IPingResponse Ping()
        {
            var results = Client.Ping();
            return results;
        }
    }
}
