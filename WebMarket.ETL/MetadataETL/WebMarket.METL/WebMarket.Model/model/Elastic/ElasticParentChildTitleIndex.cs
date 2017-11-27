// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2017 All Right Reserved
// </copyright>



namespace WebMarket.Model
{
    using Nest;
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticparentchildtitle")]
    public class ElasticParentChildTitleIndex
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
        
        public string FullTitle { get; set; }
        
        public string Description { get; set; }
        
        public List<ImageUrl> Images { get; set; }

    }

    [Serializable]
    [ElasticsearchType(Name = "elasticparentchildtitledata")]
    public class ElasticParentChildTitleData
    {
        public string Id { get; set; }
        
        public string SourceItemId { get; set; }
        
        public string Publisher { get; set; }
        
        public List<string> Publishers { get; set; }
        
        public string Isbn { get; set; }
        
        public DateTime? ActivatedOn { get; set; }
        
        public DateTime PublishedOn { get; set; }
        
        public bool HasDrm { get; set; }
        
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
    [ElasticsearchType(Name = "elasticparentchildtitledataownership")]
    public class ElasticParentChildTitleDataOwnership
    {
        
        public string Id { get; set; }
        
        public int ScopeId { get; set; }
        
        public int TotalCopies { get; set; }
        
        public int CirculationCopies { get; set; }
        
        public int HoldsCopies { get; set; }
        
        public decimal HoldsRatio { get; set; }
        
        public List<SubscriptionOwnership> Subscriptions { get; set; }
        
        public List<SOP> Sop { get; set; }
        
        public List<Expiration> Expirations { get; set; }
    }

    [Serializable]
    public class BulkElasticParentChildTitleIndex
    {
        private List<ElasticParentChildTitleIndex> _elasticParentChildTitles = new List<ElasticParentChildTitleIndex>();
        public List<ElasticParentChildTitleIndex> ElasticGroupedTitles
        {
            get { return _elasticParentChildTitles; }
            set { _elasticParentChildTitles = value; }
        }

    }
}
