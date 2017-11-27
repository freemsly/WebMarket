// <copyright company="Recorded Books Inc" file="BulkElasticParentChildTitleIndexMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Common;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    using System.Threading.Tasks;
    

    public class BulkElasticParentChildTitleIndexMDG 
    {
        public  BulkElasticGroupedTitleIndex Post(BulkElasticGroupedTitleIndex t)
        {
            const int batchCount = 400;
            ConsoleProcess.Start(t.GetType());
            CreateMapping();
            var i = 0;
            foreach (var elasticTitle in t.ElasticGroupedTitles.Batch(batchCount))
            {
                i++;
                ConsoleProcess.Restart();
                var rootDescriptor = GenerateElasticDescriptor(elasticTitle);
                ElasticSearch.Client.Bulk(rootDescriptor);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(@"iteration for {0} took - {1} ms", i * batchCount, ConsoleProcess.ElapsedMilliseconds());
            }

            //Parallel.ForEach(t.ElasticGroupedTitles.Batch(batchCount), elasticTitle => {
            //    i++;
            //    ConsoleProcess.Restart();
            //    var rootDescriptor = GenerateElasticDescriptor(elasticTitle);
            //    ElasticSearch.Client.Bulk(rootDescriptor);
            //    if (i%1000 != 0) return;
            //    Console.WriteLine(Environment.NewLine);
            //    Console.WriteLine(@"iteration for {0} took - {1} ms", i * batchCount, ConsoleProcess.ElapsedMilliseconds());
            //});

            ConsoleProcess.End(t.GetType());
            
            return t;
        }

        private static BulkDescriptor GenerateElasticDescriptor(IEnumerable<ElasticGroupedTitleIndex> elasticGroupedTitleIndexList)
        {
            var descriptor = new BulkDescriptor();
            foreach (var elasticGroupedTitleIndex in elasticGroupedTitleIndexList)
            {
                var elasticparentchildtitle = new ElasticParentChildTitleIndex()
                {
                    Id = elasticGroupedTitleIndex.Id,
                    Title = elasticGroupedTitleIndex.Title,
                    Audience = elasticGroupedTitleIndex.Audience,
                    Author = elasticGroupedTitleIndex.Author,
                    Genre = elasticGroupedTitleIndex.Genre,
                    AuthorSort = elasticGroupedTitleIndex.AuthorSort,
                    Description = elasticGroupedTitleIndex.Description,
                    Genres = elasticGroupedTitleIndex.Genres,
                    IsFiction = elasticGroupedTitleIndex.IsFiction,
                    ItemSubtitle = elasticGroupedTitleIndex.ItemSubtitle,
                    Language = elasticGroupedTitleIndex.Language,
                    Series = elasticGroupedTitleIndex.Series,
                    Series_Exact = elasticGroupedTitleIndex.Series_Exact
                };

                foreach (var item in elasticGroupedTitleIndex.NestedTitle.OrderBy(gr => gr.Group.Rank))
                {
                    if (item.HasImages)
                    {
                        elasticparentchildtitle.Images = item.Images;
                        break;
                    }
                    elasticparentchildtitle.Images = item.Images;
                }
                descriptor.Index<ElasticParentChildTitleIndex>(op => op.Document(elasticparentchildtitle));
                if (elasticGroupedTitleIndex.NestedTitle.Any())
                {
                    MapChildData(elasticGroupedTitleIndex, descriptor);
                }
                
            }
            return descriptor;
        }

        private static void MapChildData(ElasticGroupedTitleIndex elasticGroupedTitleIndex, BulkDescriptor descriptor)
        {
            foreach (var titledata in elasticGroupedTitleIndex.NestedTitle)
            {
                var data = new ElasticParentChildTitleData()
                {
                    Id = titledata.Isbn,
                    Isbn = titledata.Isbn,
                    Publisher = titledata.Publisher,
                    ListPrice = titledata.ListPrice,
                    DiscountPrice = titledata.DiscountPrice,
                    RetailPrice = titledata.RetailPrice,
                    MediaType = titledata.MediaType,
                    ActivatedOn = titledata.ActivatedOn,
                    Duration = titledata.Duration,
                    Group = titledata.Group,
                    HasDrm = titledata.HasDrm,
                    Imprint = titledata.Imprint,
                    Narrator = titledata.Narrator,
                    Narrators = titledata.Narrators,
                    PreviewFile = titledata.PreviewFile,
                    PublishedOn = titledata.PublishedOn,
                    SOP = titledata.SOP,
                    SalesRights = titledata.SalesRights,
                    Subscriptions = titledata.Subscriptions,
                    UsageTerms = titledata.UsageTerms,
                    Awards = titledata.Awards,
                    SourceItemId = titledata.SourceItemId,
                    StockLevel = titledata.StockLevel,
                    MediaCount = titledata.MediaCount,
                    Review = titledata.Review,
                    Rating = titledata.Rating,
                    IsMarcAllowed = titledata.IsMarcAllowed,
                    ProductLine = titledata.ProductLine,
                    IsComingSoon = titledata.IsComingSoon,
                    RecordingType = titledata.RecordingType,
                    MediaTypeDescription = titledata.MediaTypeDescription,
                    ContentAdvisory = titledata.ContentAdvisory,
                    SeriesNo = titledata.SeriesNo,
                    Publishers = titledata.Publishers,
                    MediaTypeBinding = titledata.MediaTypeBinding,
                    Pricing = titledata.Pricing,
                    Bundle = titledata.Bundle,
                    IsExclusive = titledata.IsExclusive
                };

                descriptor.Index<ElasticParentChildTitleData>(op =>op.Document(data).Parent(elasticGroupedTitleIndex.Id));
                MapInnerChildData(titledata, elasticGroupedTitleIndex.Id, descriptor);
            }
        }

        private static void MapInnerChildData(NestedTitle titledata, string rootId, BulkDescriptor descriptor)
        {
            foreach (Ownership ownership in titledata.Ownership)
            {
                var ownershipdata = new ElasticParentChildTitleDataOwnership()
                {
                    Id = titledata.Isbn + ownership.ScopeId, 
                    TotalCopies = ownership.TotalCopies, 
                    CirculationCopies = ownership.CirculationCopies, 
                    HoldsCopies = ownership.HoldsCopies, 
                    HoldsRatio = ownership.HoldsRatio,
                    ScopeId = ownership.ScopeId, 
                    //Subscriptions = ownership.Subscriptions.Select(t=>t.Name).ToList()
                    Sop = ownership.Sop,
                    Expirations = ownership.Expirations,
                };
                if (ownership.Subscriptions != null && ownership.Subscriptions.Any())
                {
                    ownershipdata.Subscriptions = ownership.Subscriptions;
                }
                descriptor.Index<ElasticParentChildTitleDataOwnership>(op => op.Document(ownershipdata).Parent(titledata.Isbn).Routing(rootId));
            }
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticParentChildTitleIndex>(m => m.AutoMap().AllField(a=>a.Enabled(false)).Properties(
                        p => p.Text(mu => mu.Name(n=>n.Title).Fields(f => f.Text(s=>s.Name(n=>n.Title.Suffix("sort")).Analyzer("titlesort").SearchAnalyzer("titlesort"))))));
            ElasticSearch.Client.Map<ElasticParentChildTitleData>(o => o.AutoMap().AllField(a => a.Enabled(false)).Parent<ElasticParentChildTitleIndex>());
            ElasticSearch.Client.Map<ElasticParentChildTitleDataOwnership>(o => o.AutoMap().AllField(a => a.Enabled(false)).Parent<ElasticParentChildTitleData>());
        }
    }

}
