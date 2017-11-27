// <copyright company="Recorded Books, Inc" file="ElasticSearch.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace WebMarket.Server.elasticsearch
{
    public static class ElasticSearch 
    {
        public static readonly ElasticClient Client;
        static ElasticSearch()
        {
            Client = ElasticSearchConfiguration.GetClient();
        }

        public static IIndexResponse Index<T>(T data) where T: class, new ()
        {
            var result = Client.Index(data);
            return result;
        }

        public static IBulkResponse BulkIndex<T>(IEnumerable<T> elasticIndexes, string parentId = null) where T : class, new()
        {
            var result = BulkInsert(elasticIndexes, parentId);
            return result;
        }

        private static IBulkResponse BulkInsert<T>(IEnumerable<T> elasticIndexes, string parentId = null) where T : class, new()
        {
            var descriptor = new BulkDescriptor();
            foreach (var i in elasticIndexes)
            {
                descriptor.Index<T>(op => op.Document(i).Parent(parentId));
            }
            var bulkresult = Client.Bulk(descriptor);
            return bulkresult;
        }

        public static Task<IBulkResponse> BulkInsertAsync<T>(IEnumerable<T> elasticIndexes, string parentId = null) where T : class, new()
        {
            var descriptor = new BulkDescriptor();
            foreach (var i in elasticIndexes)
            {
                descriptor.Index<T>(op => op.Document(i).Parent(parentId));
            }
            var bulkresult = Client.BulkAsync(descriptor);
            return bulkresult;
        }

        public static void SwapAlias()
        {
            var indexExists = Client.IndexExists(ElasticSearchConfiguration.LiveIndexAlias).Exists;
            Client.Alias(aliases =>
            {
                if (indexExists)
                    aliases.Add(a => a.Alias(ElasticSearchConfiguration.OldIndexAlias).Index(ElasticSearchConfiguration.LiveIndexAlias));

                return aliases
                .Remove(a => a.Alias(ElasticSearchConfiguration.LiveIndexAlias).Index("*"))
                .Add(a => a.Alias(ElasticSearchConfiguration.LiveIndexAlias).Index(ElasticSearchConfiguration.IndexName));
            });

            var oldIndices = Client.GetIndicesPointingToAlias(ElasticSearchConfiguration.OldIndexAlias);

            foreach (var oldIndex in oldIndices)
                Client.DeleteIndex(oldIndex);
        }
        
    }
}
