// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace WebMarket.Model
{
    using Nest;
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticsuggestivemagazineindex")]
    public class ElasticSuggestiveMagazineIndex
    {
        
        public Suggest Title { get; set; }
        
        public string Publisher { get; set; }
        
        public string Genre { get; set; }
    }

    [Serializable]
    public class BulkElasticSuggestiveMagazineIndex
    {
        private List<ElasticSuggestiveMagazineIndex> _elasticSuggestive = new List<ElasticSuggestiveMagazineIndex>();
        public List<ElasticSuggestiveMagazineIndex> ElasticSuggestive
        {
            get => _elasticSuggestive;
            set => _elasticSuggestive = value;
        }

    }

}
