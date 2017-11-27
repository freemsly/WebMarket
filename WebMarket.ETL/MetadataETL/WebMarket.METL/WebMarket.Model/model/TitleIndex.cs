// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2014 All Right Reserved
// </copyright>



namespace WebMarket.Model
{
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TitleIndex
    {
        [BsonId]
        public string Id { get; set; }

        public string Isbn { get; set; }
        public string Author { get; set; }
        public string AuthorSort { get; set; }
        public string Title { get; set; }
        public string ItemSubtitle { get; set; }
        public string FullTitle { get; set; }
        public string Publisher { get; set; }
        public List<string> Publishers { get; set; }
        public string Audience { get; set; }
        public string Language { get; set; }
        public string MediaType { get; set; }
        public string Genre { get; set; }
        public List<string> Genres { get; set; }
        public bool IsActive { get; set; }
        public List<ImageUrl> Images { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool HasDrm { get; set; }
        public bool HasImages { get; set; }
        public bool IsFiction { get; set; }
        public string Imprint { get; set; }
        public string Narrator { get; set; }
        public string NarratorSort { get; set; }
        public string Series { get; set; }
        public int SeriesNo { get; set; }
        public string PreviewFile { get; set; }
        public decimal ListPrice { get; set; }
        public decimal RetailPrice { get; set; }
        
        public decimal DiscountPrice { get; set; }
        public decimal? Duration { get; set; }
        public string FileSize { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public List<string> UsageTerms { get; set; }
        public Group Group { get; set; }
        public List<string> SalesRights { get; set; }
        public List<string> Subscriptions { get; set; }

        public List<SOP> SOP { get; set; }
        public List<Ownership> Ownership { get; set; }
        

        public int MediaCount { get; set; }
        public StockLevelOption StockLevelOption { get; set; }
        public string SourceStatus { get; set; }
        public string SourceItemId { get; set; }

        public string Awards { get; set; }
        public List<string> Review { get; set; }
        public int Rating { get; set; }
        public bool IsMarcAllowed { get; set; }
        public string ProductLine { get; set; }
        public bool IsComingSoon { get; set; }
        public string RecordingType { get; set; }
        public string MediaTypeDescription { get; set; }

        public List<string> PopularKeywords { get; set; }

        //used only for wfhowes
        public ContentAdvisory ContentAdvisory { get; set; }
        public string StockLevel { get; set; }

        public string MediaTypeBinding { get; set; }

        // we are adding global ownershipdata at elastictitleindex level since we require this parameters for sorting and Elastic search does not yet support for child sorting.
        
        public int SystemTotalCopies { get; set; }
        
        public int SystemHoldsCopies { get; set; }
        
        public int SystemCirculationCopies { get; set; }

        
        public List<Pricing> Pricing { get; set; }

        
        public Bundle Bundle { get; set; }
        public bool IsExclusive { get; set; }


    }


    [Serializable]
    public class BulkTitleIndex
    {
        private List<TitleIndex> _titles = new List<TitleIndex>(500000);
        public List<TitleIndex> Titles
        {
            get { return _titles; }
            set { _titles = value; }
        }
    }

    
}
