// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2017 All Right Reserved
// </copyright>



namespace WebMarket.Model
{
    using Nest;
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticgroupedtitleindex")]
    public class ElasticGroupedTitleIndex
    {
        public string Id { get; set; }

     
        public string Author { get; set; }
        
        public string AuthorSort { get; set; }

        
        public string Title { get; set; }

        
        public string Audience { get; set; }
        
        public string Language { get; set; }

        
        public string Genre { get; set; }

        
        public List<string> Genres { get; set; }



        
        public bool IsFiction { get; set; }
        
        public string Series { get; set; }

        
        public string Series_Exact { get; set; }

        
        public string ItemSubtitle { get; set; }

        
        public string Description { get; set; }

        
        public List<NestedTitle> NestedTitle { get; set; }
        
    }

    [Serializable]
    public class NestedTitle
    {
        
        public string SourceItemId { get; set; }

        
        public string Isbn { get; set; }

        
        public List<ImageUrl> Images { get; set; }

        
        public string Publisher { get; set; }

        
        public List<string> Publishers { get; set; }

        
        public DateTime? ActivatedOn { get; set; }

        
        public DateTime PublishedOn { get; set; }

        
        public bool HasDrm { get; set; }

        
        public bool HasImages { get; set; }

        
        public string MediaType { get; set; }

        
        public string Narrator { get; set; }

        
        public List<string> Narrators { get; set; }
        
        
        public string Imprint { get; set; }

        
        public string PreviewFile { get; set; }

        
        public List<string> SalesRights { get; set; }

        
        public decimal? Duration { get; set; }

        
        public decimal ListPrice { get; set; }

        
        public decimal RetailPrice { get; set; }

        
        public decimal DiscountPrice { get; set; }

        
        public List<string> Subscriptions { get; set; }

        
        public List<string> UsageTerms { get; set; }

        
        public List<SOP> SOP { get; set; }


        
        public Group Group { get; set; }

        
        public List<Ownership> Ownership { get; set; }

        
        public string Awards { get; set; }

        
        public int MediaCount { get; set; }
        
        public string StockLevel { get; set; }
        
        public List<string> Review { get; set; }
        
        public int Rating { get; set; }
        
        public bool IsMarcAllowed { get; set; }
        
        public string ProductLine { get; set; }
        
        public bool IsComingSoon { get; set; }
        
        public string RecordingType { get; set; }
        
        public string MediaTypeDescription { get; set; }

        
        public ContentAdvisory ContentAdvisory { get; set; }

        
        public int SeriesNo { get; set; }

        
        public string MediaTypeBinding { get; set; }
        
        public List<Pricing> Pricing { get; set; }
        
        public Bundle Bundle { get; set; }
        
        public bool IsExclusive { get; set; }
    }

    [Serializable]
    public class BulkElasticGroupedTitleIndex
    {
        private List<ElasticGroupedTitleIndex> _elasticGroupedTitles = new List<ElasticGroupedTitleIndex>();
        public List<ElasticGroupedTitleIndex> ElasticGroupedTitles
        {
            get { return _elasticGroupedTitles; }
            set { _elasticGroupedTitles = value; }
        }

    }
}
