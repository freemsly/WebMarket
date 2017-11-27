// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2014 All Right Reserved
// </copyright>




namespace WebMarket.Model
{
    using Nest;
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticsuggestiveindex")]
    public class ElasticSuggestiveIndex
    {
        
        public Suggest Title { get; set; }
        
        public string Author { get; set; }
        
        public string Narrator { get; set; }
        
        public string Publisher { get; set; }
        
        public IEnumerable<string> Genre { get; set; }
        
        public string Imprints { get; set; }
        
        public string Series { get; set; }
        
        public List<string> UsageTerms { get; set; }
        
        public List<string> Groups { get; set; }
        
        public List<string> Subscriptions { get; set; }
        
        public List<string> Sop { get; set; }
        
        public List<string> Keywords { get; set; }
    }

    [Serializable]
    public class BulkElasticSuggestiveIndex
    {
        private List<ElasticSuggestiveIndex> _elasticSuggestive = new List<ElasticSuggestiveIndex>(500000);
        public List<ElasticSuggestiveIndex> ElasticSuggestive
        {
            get { return _elasticSuggestive; }
            set { _elasticSuggestive = value; }
        }

    }

    public class Suggest
    {
        public IEnumerable<string> Input { get; set; }
        public string Output { get; set; }
        public object Payload { get; set; }
        public int? Weight { get; set; }
    }

}
