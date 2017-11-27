// <copyright company="Recorded Books Inc" file="TitleIndexMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using System.Diagnostics;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    using System.Linq;

    public class BulkElasticSuggestiveIndexMDG 
    {
        public BulkElasticSuggestiveIndex Post(BulkElasticSuggestiveIndex t)
        {
            const int batchCount = 2000;
            var timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            var i = 0;
            Console.WriteLine(@"Elastic suggestive indexing started - ");

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
            Console.WriteLine(@"Elastic suggestive indexing finished at - " + timerStopwatch.ElapsedMilliseconds);
            timerStopwatch.Stop();
            return t;
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.Map<ElasticSuggestiveIndex>(m => m.AllField(a => a.Enabled(false))
                .Properties(p => p.Completion(c => c.Name(s => s.Title)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.Name(s => s.Genre)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.Name(s => s.Series)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(25)))
                .Properties(p => p.Completion(c => c.Name(s => s.Imprints)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(25)))
                .Properties(p => p.Completion(c => c.Name(s => s.Subscriptions)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.Name(s => s.Groups)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.Name(s => s.UsageTerms)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.Name(s => s.Sop)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
                .Properties(p => p.Completion(c => c.Name(s => s.Keywords)
                    .Analyzer("simple")
                    .SearchAnalyzer("simple")
                    .MaxInputLength(50)))
            );
        }
    }

}
