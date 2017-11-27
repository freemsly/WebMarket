// <copyright company="Recorded Books Inc" file="TitleIndexMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using System.Diagnostics;
using System.Linq;
using WebMarket.Common;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    using Nest;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BulkElasticOwnershipIndexCollectionIndexMDG 
    {
        public BulkElasticOwnershipIndexCollection Post(BulkElasticOwnershipIndexCollection t)
        {
           int batchCount = 400;
           ConsoleProcess.Start(t.GetType());           
           CreateMapping();
            Console.WriteLine(@"Ownership titles count = " + t.ElasticOwnershipCollections.Count);
            var i = 0;
            foreach (var elasticTitle in t.ElasticOwnershipCollections.Batch(batchCount))
            {
                i++;
                ConsoleProcess.Restart();
                var rootDescriptor = GenerateElasticDescriptor(elasticTitle);
                ElasticSearch.Client.Bulk(rootDescriptor);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(@"iteration for {0} took - {1} ms", i * batchCount, ConsoleProcess.ElapsedMilliseconds());
            }

            //Parallel.ForEach(t.ElasticOwnershipCollections.Batch(batchCount), elastictitle =>
            //{
            //    i++;
            //    ConsoleProcess.Restart();
            //    var rootDescriptor = GenerateElasticDescriptor(elastictitle);
            //    ElasticSearch.Client.Bulk(rootDescriptor);
            //    if (i % 1000 != 0) return;
            //    Console.WriteLine(Environment.NewLine);
            //    Console.WriteLine(@"iteration for {0} took - {1} ms", i * batchCount, ConsoleProcess.ElapsedMilliseconds());
            //});

            //foreach (var ownershipList in t.ElasticOwnershipCollections)
            //{
            //    ConsoleProcess.Increment();
            //    if (ownershipList.ElasticOwnership.Count > 0)
            //    {
            //        ElasticSearch.BulkIndex(ownershipList.ElasticOwnership, ownershipList.Isbn);
            //    }

            //    ConsoleProcess.ModOf5000();
            //}
            //ElasticSearch.Client.UpdateIndexSettings(us => us.RefreshInterval("1s"));
            ConsoleProcess.End(t.GetType());
            return t;
        }

        private BulkDescriptor GenerateElasticDescriptor(IEnumerable<ElasticOwnershipIndexCollection> t)
        {
            var descriptor = new BulkDescriptor();
            
            foreach (var ownershipIndex in t)
            {
                if (ownershipIndex.ElasticOwnership.Count > 0)
                {
                    foreach (var elasticData in ownershipIndex.ElasticOwnership.Select(ownership => new ElasticOwnershipIndex()
                    {
                        Id = ownershipIndex.Isbn + ownership.ScopeId,
                        TotalCopies = ownership.TotalCopies,
                        CirculationCopies = ownership.CirculationCopies,
                        HoldsCopies = ownership.HoldsCopies,
                        HoldsRatio = ownership.HoldsRatio,
                        ScopeId = ownership.ScopeId,
                        Subscriptions = ownership.Subscriptions,
                        Sop = ownership.Sop,
                        Expirations = ownership.Expirations,
                    }))
                    {
                        descriptor.Index<ElasticOwnershipIndex>(op => op.Document(elasticData).Parent(ownershipIndex.Isbn));
                    }
                }
            }
            return descriptor; 
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticOwnershipIndex> (m=>m.AutoMap().Parent<ElasticTitleIndex>());
        }
    }

}
