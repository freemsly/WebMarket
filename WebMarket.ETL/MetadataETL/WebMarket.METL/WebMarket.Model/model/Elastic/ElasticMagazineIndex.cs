// <copyright company="Recorded Books, Inc" file="ElasticTitleIndex.cs">
// Copyright © 2015 All Right Reserved
// </copyright>



namespace WebMarket.Model
{
    using Nest;
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticmagazineindex")]
    public class ElasticMagazineIndex
    {
        public string MediaType { get; set; }

        
        public string Publisher { get; set; }

        
        public string Country { get; set; } 

        
        public string Audience { get; set; } 

        
        public string Description { get; set; }

        
        public string Id { get; set; } 

        
        public string Title { get; set; } 

        
        public string Token { get; set; } 

        
        public string Genre { get; set; } 

        
        public string Language { get; set; }

        
        public string MagazineId { get; set; }

        
        public string IssueId { get; set; }

        
        public DateTime PublishedOn { get; set; }

        
        public DateTime CoverDate { get; set; } 

        
        public string ImageUrl { get; set; }
        
        public DateTime CheckoutOn { get; set; }

        
        public string IssueTitle { get; set; }

        
        public string Rating { get; set; }

        
        public string Frequency { get; set; }

        
        public int CapLimit { get; set; }

        
        public decimal Price { get; set; }

        
        public string Issn { get; set; }

        
        public int TermsAndConditionIdentifier { get; set; }

        
        public int CheckoutCount { get; set; }
    }

    [Serializable]
    public class BulkElasticMagazineIndex
    {
        private List<ElasticMagazineIndex> _elasticMagazine = new List<ElasticMagazineIndex>(50000);
        public List<ElasticMagazineIndex> ElasticMagazine
        {
            get { return _elasticMagazine; }
            set { _elasticMagazine = value; }
        }
    }
}
