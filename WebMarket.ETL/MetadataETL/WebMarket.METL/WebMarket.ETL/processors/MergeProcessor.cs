// <copyright company="Recorded Books Inc" file="MergeProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using System.Configuration;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;
using WebMarket.Server;
using WebMarket.Server.elasticsearch;

namespace WebMarket.ETL
{


    public class MergeProcessor : Processor<MediaTitle>
    {
        public IModelRequestService Service { get; set; }

        private BulkTitleIndex TitleIndexList { get; set; }
        private BulkElasticTitleIndex ElasticTitleIndexList { get; set; }
        private BulkElasticSuggestiveIndex ElasticSuggestiveIndexList { get; set; }
        private BulkElasticOwnershipIndexCollection ElasticOwnershipIndexList { get; set; }
        private BulkElasticGroupedTitleIndex BulkElasticGroupedTitleIndex { get; set; }

        public MergeProcessor()
        {
            TitleIndexList = new BulkTitleIndex();
            ElasticTitleIndexList = new BulkElasticTitleIndex();
            ElasticSuggestiveIndexList = new BulkElasticSuggestiveIndex();
            ElasticOwnershipIndexList = new BulkElasticOwnershipIndexCollection();
            BulkElasticGroupedTitleIndex = new BulkElasticGroupedTitleIndex();
        }

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            var titleindexbuilder = new TitleIndexBuilder(item);
            var titleIndex = titleindexbuilder.Build();
            TitleIndexList.Titles.Add(titleIndex);

            //var elasticIndexBuilder = new ElasticIndexBuilder(item);
            //var elasticTitle = elasticIndexBuilder.Build();
            var elasticTitle = titleIndex.Map();
            var elasticgroupedTitle = elasticTitle.MapGroup();

            ElasticSuggestiveIndexList.ElasticSuggestive.Add(elasticTitle.MapSuggestive());

            if (elasticTitle.Ownership.Count > 0)
            {
                ElasticOwnershipIndexList.ElasticOwnershipCollections.Add(titleIndex.MapOwnership());
            }
            ElasticTitleIndexList.ElasticTitles.Add(elasticTitle);
            BulkElasticGroupedTitleIndex.ElasticGroupedTitles.Add(elasticgroupedTitle);

            if (ElasticTitleIndexList.ElasticTitles.Count % 1000 == 0)
            {
                Console.WriteLine(@"merge process count - " + ElasticTitleIndexList.ElasticTitles.Count);
            }
        }


        private void BulkExecute<T>(T ttype) where T : class,new()
        {
            var response = Service.Post(ttype);
            if (!response.IsOkay)
            {
                //EventWriter.WriteError(response.Status, SeverityType.Error);
            }
        }

        protected override void Cleanup()
        {
            //if (ElasticTitleIndexList.ElasticTitles.Count > Constants.TotalProductsCount)
            //{
                
                //post to mongo
                //BulkExecute(TitleIndexList);
                var bulkTitleInde = new BulkTitleIndexMDG(EtlServiceProvider.ConnectionStrings.Mongo.Node, "metadata");
                bulkTitleInde.Index(TitleIndexList);

                ElasticSearchConfiguration.Host1 = EtlServiceProvider.ConnectionStrings.ElasticSearch.Node[0];
                ElasticSearchConfiguration.Host2 = EtlServiceProvider.ConnectionStrings.ElasticSearch.Node[1];
                ElasticSearchConfiguration.Host3 = EtlServiceProvider.ConnectionStrings.ElasticSearch.Node[2];
            
                //post to elastic search
                var elasticsearchMdg = new BulkElasticTitleIndexMDG();
                elasticsearchMdg.Post(ElasticTitleIndexList);

                var elasticsuggestivesearchMdg = new BulkElasticSuggestiveIndexMDG();
                elasticsuggestivesearchMdg.Post(ElasticSuggestiveIndexList);

                var elasticgroupedsearchMdg = new BulkElasticGroupedTitleIndexServer();
                elasticgroupedsearchMdg.Post(BulkElasticGroupedTitleIndex);

                var elasticownershipMdg = new BulkElasticOwnershipIndexCollectionIndexMDG();
                elasticownershipMdg.Post(ElasticOwnershipIndexList);





                //swap aliases of elastic search index once all other above action is done 
                //This is to avoid downtime during indexing
                elasticsearchMdg.SwapAliases();

                //clear the cache once indexing is done
                //var cacheProvider = new RB.API.Caching.Internal.RedisCache();
                //cacheProvider.Clear();
            //}
            //else
            //{
            //    //EventWriter.WriteError("ETL records were less than " + Constants.TotalProductsCount, SeverityType.Error); 
            //}
        }
    }
}

