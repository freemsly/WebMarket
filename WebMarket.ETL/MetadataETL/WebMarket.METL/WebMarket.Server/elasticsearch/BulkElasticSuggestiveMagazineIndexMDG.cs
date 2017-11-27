// <copyright company="Recorded Books Inc" file="TitleIndexMDG.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using System;
using System.Diagnostics;
using System.Linq;
using WebMarket.Model;

namespace WebMarket.Server.elasticsearch
{
    public class BulkElasticSuggestiveMagazineIndexMDG 
    {
        public  BulkElasticSuggestiveMagazineIndex Post(BulkElasticSuggestiveMagazineIndex t)
        {
            const int batchCount = 3000;
            var timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            var i = 0;
            Console.WriteLine(@"Elastic suggestive magazine indexing started - ");

            CreateMapping();
            
            foreach (var elasticTitle in t.ElasticSuggestive.Batch(batchCount))
            {
                i++;
                timerStopwatch.Restart();
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(@"indexed records - " + i * batchCount);
                ElasticSearch.BulkIndex(elasticTitle.ToList());
                Console.WriteLine(@"iteration {0} took - {1} ms", i, timerStopwatch.ElapsedMilliseconds);
            }
            Console.WriteLine(@"Elastic suggestive magazine indexing finished at - " + timerStopwatch.ElapsedMilliseconds);
            timerStopwatch.Stop();
            return t;
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticSuggestiveMagazineIndex>(m => m.AllField(a=>a.Enabled(false))
                .Properties(p => p.Completion(c => c.
                    Name(s => s.Title)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.
                    Name(s => s.Genre)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.
                    Name(s => s.Publisher)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(25)))
                );
        }
    }

}
