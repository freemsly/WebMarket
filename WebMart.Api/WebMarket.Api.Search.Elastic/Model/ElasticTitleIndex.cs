// <copyright company="Recorded Books, Inc" file="ElasticTitle.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System;

namespace WebMarket.Api.Search.Elastic
{
    using Nest;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elastictitleindex")]
    public sealed class ElasticTitleIndex
    {
        
        
        public string Id { get; set; }

        
        public string SourceItemId { get; set; }

        
        public string Isbn { get; set; }

        
        public string Author { get; set; }

        
        public List<string> Authors { get; set; }

        
        public string Title { get; set; }

        
        public string Publisher { get; set; }

        
        public string Audience { get; set; }

        
        public string Language { get; set; }

        
        public string MediaType { get; set; }

        
        public string Genre { get; set; }

        
        public List<string> Genres { get; set; }

        
       //ublic List<ImageUrl> Images { get; set; }

        
        public DateTime? ActivatedOn { get; set; }

        
        public DateTime PublishedOn { get; set; }

        
        public bool HasDrm { get; set; }

        
        public bool IsFiction { get; set; }

        
        public string Narrator { get; set; }

        
        public List<string> Narrators { get; set; }

        
        public string Series { get; set; }

        
        public string SeriesNo { get; set; }

        
        public string Imprint { get; set; }

        
        public string PreviewFile { get; set; }

        
        public List<string> SalesRights { get; set; }

        
        public decimal? Duration { get; set; }

        
        public string ItemSubtitle { get; set; }

        
        public string FullTitle { get; set; }

        
        public string Description { get; set; }

        
        public decimal ListPrice { get; set; }

        
        public decimal RetailPrice { get; set; }

        
        public decimal DiscountPrice { get; set; }

        
        public bool HasAttachment { get; set; }

        
        public List<string> Subscriptions { get; set; }

        
        public List<string> UsageTerms { get; set; }

        
        public List<SOP> SOP { get; set; }

        
        public Group Group { get; set; }

        
        public int GroupId { get; set; }
        
        
        public int MediaCount { get; set; }
        
        
        public string StockLevel { get; set; }
        
        
        public string Awards { get; set; }
        
        
        public List<string> Review { get; set; }
        
        public int Rating { get; set; }
        
        public bool IsMarcAllowed { get; set; }

        
        public string ProductLine { get; set; }

        
        public bool IsComingSoon { get; set; }

        
        public string RecordingType { get; set; }

        
        public string MediaTypeDescription { get; set; }

        
        public ContentAdvisory ContentAdvisory { get; set; }

        
        public string MediaTypeBinding { get; set; }

        
        public List<Pricing> Pricing { get; set; }

        
        public Bundle Bundle { get; set; }
    }

    [Serializable]
    public sealed class Ownership
    {
        public int LibraryId { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int CirculationCopies { get; set; }
        public int HoldsCopies { get; set; }
        public decimal HoldsRatio { get; set; }
    }

    public class Bundle
    {
        
        public string Id { get; set; }
        
        public string ItemNumber { get; set; }
        
        public decimal Price { get; set; }
        
        public string Name { get; set; }
        
        public List<BundleItem> BundleItems { get; set; }
    }

    
    public class BundleItem
    {
       
        public string Isbn { get; set; }
       
        public string MediaType { get; set; }
       
        public decimal Price { get; set; }
    }

    public class Pricing
    {
        
        public Price Price { get; set; }
        
        public string Currency { get; set; }
    }

    
    public class Price
    {
    
        public decimal Retail { get; set; }
    
        public decimal List { get; set; }
    
        public decimal Discount { get; set; }
    }

    public class ContentAdvisory
    {
     
        public string Sex { get; set; }
     
        public string Language { get; set; }
     
        public string Violence { get; set; }
    }

    public class Group
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public int Rank { get; set; }

        
        public List<GroupItem> GroupItems { get; set; }
    }

    
    public class GroupItem
    {
    
        public string MediaType { get; set; }

    
        public string Isbn { get; set; }

    
        public string MediaTypeDescription { get; set; }
    }


    public class SOP
    {
        
        public string ClassId { get; set; }

        
        public string Name { get; set; }

        
        public int Year { get; set; }

        
        public string Type { get; set; }

        
        public string StockLevel { get; set; }
    }

}
