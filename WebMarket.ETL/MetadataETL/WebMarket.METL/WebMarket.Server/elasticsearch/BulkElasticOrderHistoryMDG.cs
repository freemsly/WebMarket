// <copyright company="Recorded Books Inc" file="PurchaseOrderMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Common;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    using System.Linq;

    public class ElasticOrderHistoryMDG 
    {
        public BulkElasticOrderHistory Post(BulkElasticOrderHistory t)
        {
            ConsoleProcess.Start(t.GetType());
            CreateMapping();
            foreach (var elasticTitle in t.OrderHistories.Batch(5000))
            {
                ConsoleProcess.Increment();
                ElasticSearch.BulkIndex(elasticTitle.ToList());
            }
            return t;
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticOrderHistory>(m => m.AutoMap());
        }
    }

}
