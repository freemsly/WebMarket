// <copyright company="Recorded Books Inc" file="TitleIndexMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using System.Diagnostics;
using Nest;
using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    using System.Linq;

    public class BulkElasticTitleIndexMDG 
    {
        public  BulkElasticTitleIndex Post(BulkElasticTitleIndex t)
        {

            const int batchCount = 2000;
            var timerStopwatch = new Stopwatch();
            timerStopwatch.Start();
            var i = 0;
            Console.WriteLine(@"Elastic search indexing started - ");

            CreateMapping();

            foreach (var elasticTitle in t.ElasticTitles.Batch(batchCount))
            {
                i++;
                timerStopwatch.Restart();
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(@"indexed records - " + i * batchCount);
                ElasticSearch.BulkIndex(elasticTitle.ToList());
                Console.WriteLine(@"iteration {0} took - {1} ms", i, timerStopwatch.ElapsedMilliseconds);
            }
            Console.WriteLine(@"Elastic search indexing finished at - " + timerStopwatch.ElapsedMilliseconds);
            timerStopwatch.Stop();
            return t;
        }


        public  void SwapAliases()
        {
            Console.WriteLine(@"Swapping Aliases");
            ElasticSearch.SwapAlias();
        }

        private static void CreateMapping()
        {
            ElasticSearch.Client.CreateIndex(ElasticSearchConfiguration.IndexName, t => t
                .Settings(se => se.RefreshInterval(-1)
                    .Analysis(x =>
                        {
                            var descriptor = x
                                .Tokenizers(p => p.Keyword("sorttokeniser", k => k.BufferSize(200)))
                                .CharFilters(cf => cf.PatternReplace("charfilters", pr => pr.Pattern("[^\\w]"))
                                    .PatternReplace("charignorefilters",
                                        pr => pr.Pattern(@"(?i)(?=(\w|^))(a|the)(?!(\w|$))").Replacement("")))
                                .TokenFilters(tf => tf
                                    .PatternReplace("patternreplacefilters", pr => pr.Pattern("[^\\w]"))
                                    .Stemmer("possessiveenglishstemmer", s => s.Language("possessive_english")))
                                .Analyzers(i => i.Custom("titlesort", ts => ts.CharFilters(new[] {"charignorefilters"})
                                        .Tokenizer("sorttokeniser").Filters(new[]
                                            {new LowercaseTokenFilter().Type, "patternreplacefilters"}))
                                    .Custom("sort",
                                        ts => ts.CharFilters(new[] {"charfilters"}).Tokenizer("sorttokeniser")
                                            .Filters(new[] {new LowercaseTokenFilter().Type}))
                                    .Custom("rbstandardanalyser", ts => ts.Tokenizer(new StandardAnalyzer().Type)
                                        .Filters(new[] {new LowercaseTokenFilter().Type, "possessiveenglishstemmer"}))
                                );
                            return descriptor;
                        }
                    )).Mappings(m => m.Map<ElasticTitleIndex>(e => e.AutoMap().AllField(a => a.Enabled(false))
                    .Properties(
                        p => p.Text(te => te.Name(n => n.Title).Fields(
                            f => f.Text(s =>
                                s.Name(n => n.Title.Suffix("sort")).SearchAnalyzer("titlesort").Analyzer("sort"))
                        ))))));
        }
    }

}
