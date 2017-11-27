// <copyright company="Recorded Books Inc" file="ElasticGroupedIndexBuilder.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using System;
using System.Collections.Generic;
using WebMarket.Model;

namespace WebMarket.ETL
{

    public static class ElasticGroupedIndexMapper 
    {
        private static readonly IDictionary<string, ElasticGroupedTitleIndex> Elastictitleindexstore = new Dictionary<string, ElasticGroupedTitleIndex>();

        public static ElasticGroupedTitleIndex MapGroup(this ElasticTitleIndex elasticTitleIndex)
        {
            //if no groupid set the groupid to a guid value
            string key = elasticTitleIndex.Group != null && elasticTitleIndex.Group.Id > 0 ? elasticTitleIndex.Group.Id.ToString() : Guid.NewGuid().ToString();
            ElasticGroupedTitleIndex elasticgrouptitleindex;

            if (Elastictitleindexstore.TryGetValue(key, out elasticgrouptitleindex))
            {
                return AddNestedTitle(elasticTitleIndex, elasticgrouptitleindex);
            }
            var item = new ElasticGroupedTitleIndex
            {
                Id = key,
                IsFiction = elasticTitleIndex.IsFiction,
                Genre = elasticTitleIndex.Genre,
                Genres = elasticTitleIndex.Genres,
                Language = elasticTitleIndex.Language,
                Author = elasticTitleIndex.Author,
                AuthorSort = elasticTitleIndex.AuthorSort,
                ItemSubtitle = elasticTitleIndex.ItemSubtitle,
                Title = elasticTitleIndex.Title,
                Description = elasticTitleIndex.Description,
                Audience = elasticTitleIndex.Audience,
                Series = elasticTitleIndex.Series,
                Series_Exact = elasticTitleIndex.Series_Exact,
                NestedTitle = new List<NestedTitle>
                {
                    new NestedTitle()
                    {
                        Images = elasticTitleIndex.Images,
                        ActivatedOn = elasticTitleIndex.ActivatedOn,
                        HasDrm = elasticTitleIndex.HasDrm,
                        MediaType = elasticTitleIndex.MediaType,
                        Publisher = elasticTitleIndex.Publisher,
                        Isbn = elasticTitleIndex.Isbn,
                        ListPrice = elasticTitleIndex.ListPrice,
                        DiscountPrice = elasticTitleIndex.DiscountPrice,
                        RetailPrice = elasticTitleIndex.RetailPrice,
                        Subscriptions = elasticTitleIndex.Subscriptions,
                        Narrator = elasticTitleIndex.Narrator,
                        //Narratorsort = elasticTitleIndex.Narrators,
                        Duration = elasticTitleIndex.Duration,
                        PublishedOn = elasticTitleIndex.PublishedOn,
                        Imprint = elasticTitleIndex.Imprint,
                        PreviewFile = elasticTitleIndex.PreviewFile,
                        SOP = elasticTitleIndex.SOP,
                        SalesRights = elasticTitleIndex.SalesRights,
                        UsageTerms = elasticTitleIndex.UsageTerms,
                        Awards = elasticTitleIndex.Awards,
                        SourceItemId = elasticTitleIndex.SourceItemId,
                        Group = elasticTitleIndex.Group,
                        Ownership = elasticTitleIndex.Ownership,
                        MediaCount = elasticTitleIndex.MediaCount,
                        StockLevel = elasticTitleIndex.StockLevel,
                        Review = elasticTitleIndex.Review,
                        Rating = elasticTitleIndex.Rating,
                        IsMarcAllowed = elasticTitleIndex.IsMarcAllowed,
                        ProductLine = elasticTitleIndex.ProductLine,
                        IsComingSoon = elasticTitleIndex.IsComingSoon,
                        RecordingType = elasticTitleIndex.RecordingType,
                        MediaTypeDescription = elasticTitleIndex.MediaTypeDescription,
                        ContentAdvisory = elasticTitleIndex.ContentAdvisory,
                        HasImages = elasticTitleIndex.HasImages,
                        SeriesNo = elasticTitleIndex.SeriesNo,
                        Publishers = new List<string>() { elasticTitleIndex.Publisher },
                        MediaTypeBinding = elasticTitleIndex.MediaTypeBinding,
                        Pricing = elasticTitleIndex.Pricing,
                        Bundle = elasticTitleIndex.Bundle,
                        IsExclusive = elasticTitleIndex.IsExclusive
                    }
                }
            };

            Elastictitleindexstore.Add(key, item);
            return item;
        }

        private static ElasticGroupedTitleIndex AddNestedTitle(ElasticTitleIndex elasticTitleIndex,
            ElasticGroupedTitleIndex elasticgrouptitleindex)
        {
            elasticgrouptitleindex.NestedTitle.Add(new NestedTitle
            {
                SourceItemId = elasticTitleIndex.SourceItemId,
                Publisher = elasticTitleIndex.Publisher,
                Awards = elasticTitleIndex.Awards,
                ActivatedOn = elasticTitleIndex.ActivatedOn,
                HasDrm = elasticTitleIndex.HasDrm,
                MediaType = elasticTitleIndex.MediaType,
                Isbn = elasticTitleIndex.Isbn,
                ListPrice = elasticTitleIndex.ListPrice,
                DiscountPrice = elasticTitleIndex.DiscountPrice,
                RetailPrice = elasticTitleIndex.RetailPrice,
                Subscriptions = elasticTitleIndex.Subscriptions,
                Narrator = elasticTitleIndex.Narrator,
                //Narrators = elasticTitleIndex.Narrators,
                Duration = elasticTitleIndex.Duration,
                PublishedOn = elasticTitleIndex.PublishedOn,
                Imprint = elasticTitleIndex.Imprint,
                PreviewFile = elasticTitleIndex.PreviewFile,
                SOP = elasticTitleIndex.SOP,
                SalesRights = elasticTitleIndex.SalesRights,
                UsageTerms = elasticTitleIndex.UsageTerms,
                Ownership = elasticTitleIndex.Ownership,
                MediaCount = elasticTitleIndex.MediaCount,
                StockLevel = elasticTitleIndex.StockLevel,
                Review = elasticTitleIndex.Review,
                Rating = elasticTitleIndex.Rating,
                IsMarcAllowed = elasticTitleIndex.IsMarcAllowed,
                Group = elasticTitleIndex.Group,
                ProductLine = elasticTitleIndex.ProductLine,
                Images = elasticTitleIndex.Images,
                IsComingSoon = elasticTitleIndex.IsComingSoon,
                HasImages  = elasticTitleIndex.HasImages,
                RecordingType = elasticTitleIndex.RecordingType,
                MediaTypeDescription = elasticTitleIndex.MediaTypeDescription,
                ContentAdvisory = elasticTitleIndex.ContentAdvisory,
                SeriesNo = elasticTitleIndex.SeriesNo,
                Publishers = new List<string>() { elasticTitleIndex.Publisher },
                MediaTypeBinding = elasticTitleIndex.MediaTypeBinding,
                Pricing = elasticTitleIndex.Pricing,
                Bundle = elasticTitleIndex.Bundle,
                IsExclusive = elasticTitleIndex.IsExclusive
            });
            return elasticgrouptitleindex;
        }
    }
}
