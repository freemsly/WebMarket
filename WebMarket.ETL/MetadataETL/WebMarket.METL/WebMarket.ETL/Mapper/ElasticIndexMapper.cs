// <copyright company="Recorded Books Inc" file="TitleIndexBuilder.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using System;
using System.Linq;
using WebMarket.Model;

namespace WebMarket.ETL
{

    public static class ElasticIndexMapper
    {
        public static ElasticTitleIndex Map(this TitleIndex titleindex)
        {
            var item = new ElasticTitleIndex
            {
                SalesRights = titleindex.SalesRights,
                Title = titleindex.Title,
                Author = titleindex.Author,
                AuthorSort = titleindex.AuthorSort,
                Audience = titleindex.Audience,
                ActivatedOn = titleindex.ActivatedOn,
                Duration = titleindex.Duration,
                Genre = titleindex.Genre,
                Genres = titleindex.Genres,
                HasDrm = titleindex.HasDrm,
                Images = titleindex.Images,
                Isbn = titleindex.Isbn,
                IsFiction = titleindex.IsFiction,
                ItemSubtitle = titleindex.ItemSubtitle,
                Language = titleindex.Language,
                MediaType = titleindex.MediaType,
                Narrator = titleindex.Narrator,
                NarratorSort = titleindex.NarratorSort,
                PreviewFile = titleindex.PreviewFile,
                PublishedOn = titleindex.PublishedOn,
                Publisher = titleindex.Publisher,
                Series = titleindex.Series,
                Series_Exact = titleindex.Series,
                Imprint = titleindex.Imprint,
                ListPrice = titleindex.ListPrice,
                Description = titleindex.Description,
                Id = titleindex.Isbn,
                Subscriptions = titleindex.Subscriptions,
                UsageTerms = titleindex.UsageTerms,
                SOP = titleindex.SOP,
                MediaCount = titleindex.MediaCount,
                StockLevel = titleindex.StockLevel,
                SourceItemId = titleindex.SourceItemId,
                Awards = titleindex.Awards,
                Review = titleindex.Review,
                Rating = titleindex.Rating,
                Group = titleindex.Group,
                Ownership = titleindex.Ownership,
                IsMarcAllowed = titleindex.IsMarcAllowed,
                ProductLine = titleindex.ProductLine,
                IsComingSoon = titleindex.IsComingSoon,
                RecordingType = titleindex.RecordingType,
                MediaTypeDescription = titleindex.MediaTypeDescription,
                ContentAdvisory = titleindex.ContentAdvisory,
                HasImages = titleindex.HasImages,
                Bundle = titleindex.Bundle,
                DiscountPrice = titleindex.DiscountPrice,
                FullTitle = titleindex.FullTitle,
                MediaTypeBinding = titleindex.MediaTypeBinding,
                PopularKeywords = titleindex.PopularKeywords,
                Pricing = titleindex.Pricing,
                Publishers = titleindex.Publishers,
                RetailPrice = titleindex.RetailPrice,
                SeriesNo = titleindex.SeriesNo,
                SystemCirculationCopies = titleindex.SystemCirculationCopies,
                SystemHoldsCopies = titleindex.SystemHoldsCopies,
                SystemTotalCopies = titleindex.SystemTotalCopies,
                IsExclusive = titleindex.IsExclusive
               
            };
            return item;
        }
        

    }
}
