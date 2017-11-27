// <copyright company="Recorded Books Inc" file="PurchaseOrderMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using Nest;
using WebMarket.Common;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    using System.Linq;
 

    public class BulkElasticMagazineIndexMDG 
    {
        public BulkElasticMagazineIndex Post(BulkElasticMagazineIndex t)
        {
            ConsoleProcess.Start(t.GetType());
            CreateMapping();
            foreach (var elasticMagazine in t.ElasticMagazine.Batch(5000))
            {
                ConsoleProcess.Increment();
                ElasticSearch.BulkIndex(elasticMagazine.ToList());
            }
            return t;
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticMagazineIndex>(m => m.AutoMap().AllField(a => a.Enabled(false)).Properties(
                p => p.Text(mu => mu.Name(n => n.Title).Fields(f => f.Text(s => s.Name(n => n.Title.Suffix("sort")).Analyzer("titlesort").SearchAnalyzer("titlesort"))))
            ));
        }
    }

}
