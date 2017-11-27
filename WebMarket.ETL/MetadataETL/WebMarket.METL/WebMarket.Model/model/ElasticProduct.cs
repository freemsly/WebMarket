// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2017 All Right Reserved
// </copyright>




namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;
    using Nest;

    //[Serializable]
    //[ElasticType(Name = "elasticproductindex")]
    //public class ElasticProduct
    //{
    //    [ElasticProperty(Name = "id", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Id { get; set; }

    //    [ElasticProperty(Name = "isbn",Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Isbn { get; set; }

    //    [ElasticProperty(Name = "author", AddSortField = true)]
    //    public string Author { get; set; }

    //    [ElasticProperty(Name = "authors", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<string> Authors { get; set; }

    //    [ElasticProperty(Name = "title", AddSortField = true)]
    //    public string Title { get; set; }

    //    [ElasticProperty(Name = "publisher", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Publisher { get; set; }

    //    [ElasticProperty(Name = "audience", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Audience { get; set; }

    //    [ElasticProperty(Name = "language", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Language { get; set; }

    //    [ElasticProperty(Name = "mediatype", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string MediaType { get; set; }

    //    [ElasticProperty(Name = "genre", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Genre { get; set; }

    //    [ElasticProperty(Name = "genres", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<string> Genres { get; set; }

    //    [ElasticProperty(Name = "images", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<ImageUrl> Images { get; set; }

    //    [ElasticProperty(Name = "activatedon", Type = FieldType.Date, Index = FieldIndexOption.NotAnalyzed, AddSortField = true)]
    //    public DateTime? ActivatedOn { get; set; }

    //    [ElasticProperty(Name = "publishedon", Type = FieldType.Date, Index = FieldIndexOption.NotAnalyzed)]
    //    public DateTime PublishedOn { get; set; }

    //    [ElasticProperty(Name = "hasdrm", Type = FieldType.Boolean, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool HasDrm { get; set; }

    //    [ElasticProperty(Name = "isfiction", Type = FieldType.Boolean, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsFiction { get; set; }

    //    [ElasticProperty(Name = "narrator", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed, AddSortField = true)]
    //    public string Narrator { get; set; }

    //    [ElasticProperty(Name = "narrators", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<string> Narrators { get; set; }

    //    [ElasticProperty(Name = "series", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Series { get; set; }

    //    [ElasticProperty(Name = "imprint", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Imprint { get; set; }

    //    [ElasticProperty(Name = "previewfile", Type = FieldType.String, Index = FieldIndexOption.NotAnalyzed)]
    //    public string PreviewFile { get; set; }

    //    [ElasticProperty(Name = "salesrights", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<string> SalesRights { get; set; }

    //    [ElasticProperty(Name = "duration", Type = FieldType.Float, Index = FieldIndexOption.NotAnalyzed)]
    //    public decimal? Duration { get; set; }

    //    [ElasticProperty(Name = "subtitle", Type = FieldType.String, Index = FieldIndexOption.Analyzed)]
    //    public string ItemSubtitle { get; set; }

    //    [ElasticProperty(Name = "description", Type = FieldType.String, Index = FieldIndexOption.Analyzed)]
    //    public string Description { get; set; }


    //    [ElasticProperty(Name = "subscriptions", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<string> Subscriptions { get; set; }

    //    [ElasticProperty(Name = "usageterms", Index = FieldIndexOption.NotAnalyzed )]
    //    public List<string> UsageTerms { get; set; }

    //    [ElasticProperty(Name = "sop",Index = FieldIndexOption.NotAnalyzed)]
    //    public List<SOP> SOP { get; set; }

    //    [ElasticProperty(Name = "ownership", Type=FieldType.Nested, Index = FieldIndexOption.NotAnalyzed)]
    //    public List<Ownership> Ownership { get; set; }

    //    [ElasticProperty(Name = "group", Index = FieldIndexOption.NotAnalyzed)]
    //    public List<string> Group { get; set; }



    //    //Suggestive(Autocomplete) fields
    //    [ElasticProperty(Name = "title_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Title_Suggestive { get; set; }
    //    [ElasticProperty(Name = "author_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Author_Suggestive { get; set; }
    //    [ElasticProperty(Name = "narrator_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Narrator_Suggestive { get; set; }
    //    [ElasticProperty(Name = "publisher_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Publisher_Suggestive { get; set; }
    //    [ElasticProperty(Name = "genre_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Genre_Suggestive { get; set; }
    //    [ElasticProperty(Name = "imprints_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Imprints_Suggestive { get; set; }
    //    [ElasticProperty(Name = "series_suggestive", Type = FieldType.Completion, IndexAnalyzer = "simple", SearchAnalyzer = "simple", Index = FieldIndexOption.NotAnalyzed)]
    //    public string Series_Suggestive { get; set; }
    //}

    //[Serializable]
    //public class BulkElasticTitleIndex
    //{
    //    private List<ElasticProduct> _elasticTitles = new List<ElasticProduct>(500000);
    //    public List<ElasticProduct> ElasticTitles
    //    {
    //        get { return _elasticTitles; }
    //        set { _elasticTitles = value; }
    //    }

    //}
}
