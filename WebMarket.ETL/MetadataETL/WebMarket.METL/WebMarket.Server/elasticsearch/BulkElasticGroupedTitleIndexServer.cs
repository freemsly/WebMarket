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
    public class BulkElasticGroupedTitleIndexServer
    {
        public  BulkElasticGroupedTitleIndex Post(BulkElasticGroupedTitleIndex t)
        {
            const int batchCount = 500;
            ConsoleProcess.Start(t.GetType());
            CreateMapping();
            var i = 0;
            foreach (var elasticTitle in t.ElasticGroupedTitles.Batch(batchCount))
            {
                i++;
                ConsoleProcess.Restart();
                ElasticSearch.BulkIndex(elasticTitle.ToList());
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(@"iteration for {0} took - {1} ms", i * batchCount, ConsoleProcess.ElapsedMilliseconds());
            }
            ConsoleProcess.End(t.GetType());
            return t;
        }


        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticGroupedTitleIndex>(m => m.AutoMap().AllField(a=>a.Enabled(false)).Properties(
                        p => p.Text(mu => mu.Name(n=>n.Title).Fields(f => f.Text(s=>s.Name(n=>n.Title.Suffix("sort")).Analyzer("titlesort").SearchAnalyzer("titlesort"))))));
           
        }
    }

}
