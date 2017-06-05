// <copyright company="Recorded Books, Inc" file="DisplayableMapper.cs">
// Copyright © 2015 All Right Reserved
// </copyright>


using System.Globalization;
using System.Linq;
using WebMarket.Api.Model;

namespace WebMarket.Api.Search.Elastic
{
    using System;
    using System.Collections.Generic;

    public static class DisplayableMapper
    {
        private static readonly IDictionary<ItemTypeOption, Func<ElasticTitleIndex, Displayable, Displayable>> ItemTypeDictionary = new Dictionary<ItemTypeOption, Func<ElasticTitleIndex, Displayable, Displayable>>()
        {
            {ItemTypeOption.eAudio, MapToEAudio},
            {ItemTypeOption.eBook, MapToEBook},
            {ItemTypeOption.Cd, MapToCd},
            {ItemTypeOption.Dvd, MapToDvd},
            {ItemTypeOption.Mp3cd, MapToMp3},
            {ItemTypeOption.Playaway, MapToPlayAway},
            {ItemTypeOption.Text, MapToText},
            {ItemTypeOption.LargePrint, MapToLargePrint},
        };

        public static Displayable MapToDisplayable(this ElasticTitleIndex elasticProduct) 
        {
            var item = new Displayable();
            ItemTypeOption option;

            //remove spaces in text
            var parsedMediaType = elasticProduct.MediaType.Replace(" ","").ToLower();

            if (Enum.TryParse(parsedMediaType, true, out option))
            {
                item = ItemTypeDictionary[option].Invoke(elasticProduct, item);
            }

            return item;
        }



        //public static CustomerInterest MapCustomerInterest(this ElasticOwnershipIndex ownershipIndex)
        //{
        //    //map customer/library interest
        //    var customerInterest = new CustomerInterest();

        //    customerInterest.TotalCopies = ownershipIndex.TotalCopies;
        //    customerInterest.HoldsCopies = ownershipIndex.HoldsCopies;
        //    customerInterest.CirculationCopies = ownershipIndex.CirculationCopies;
        //    customerInterest.HoldsRatio = ownershipIndex.HoldsRatio;
        //    if (ownershipIndex.Subscriptions != null)
        //    {
        //        customerInterest.Subscriptions = ownershipIndex.Subscriptions.Select(t => t.Name).ToList();
        //    }

        //    return customerInterest;
        //}

        private static Displayable MapBookToDisplayable(ElasticTitleIndex elasticProduct, Book book)
        {
            if (book == null)
                book = new Book();

            
            book.Genre = elasticProduct.Genre;
            book.Publisher = elasticProduct.Publisher;
            book.Description = elasticProduct.Description;
            book.Authors = elasticProduct.Author;
            book.Series = elasticProduct.Series;
            book.SeriesNo = elasticProduct.SeriesNo;
            book.Language = elasticProduct.Language;
            book.Isbn = elasticProduct.Isbn;
            book.Title = elasticProduct.Title;
            book.Subtitle = elasticProduct.ItemSubtitle;

            book.Subtitle = elasticProduct.ItemSubtitle;
            book.PublishedDate = elasticProduct.PublishedOn;
            book.ListPrice = elasticProduct.ListPrice;
            book.DiscountPrice = elasticProduct.DiscountPrice;
            book.RetailPrice = elasticProduct.RetailPrice;
            book.Audience = elasticProduct.Audience;
            book.Awards = elasticProduct.Awards;
            book.Imprint = elasticProduct.Imprint;
            book.SourceItemId = elasticProduct.SourceItemId;
            book.ReleasedDate = Convert.ToDateTime(elasticProduct.ActivatedOn);
            book.Review = elasticProduct.Review;
            book.Rating = elasticProduct.Rating;
            book.IsMarcAllowed = elasticProduct.IsMarcAllowed;
            book.IsFiction = elasticProduct.IsFiction;

           
            book.MediaType = elasticProduct.MediaType;

            book.MediaTypeDescription = elasticProduct.MediaTypeDescription;
            book.MediaTypeBinding = elasticProduct.MediaTypeBinding;
            //book.Pricing = elasticProduct.Pricing;

            //map content advisory - only used in wfhowes
            //book.ContentAdvisory = elasticProduct.ContentAdvisory;

            //map item interest
            //book.ItemInterest = new ItemInterest();
            //book.ItemInterest.Subscription = elasticProduct.Subscriptions;
            //book.ItemInterest.UsageTerm = elasticProduct.UsageTerms;
            //book.ItemInterest.Group = elasticProduct.Group;
            //book.ItemInterest.SOP = elasticProduct.SOP;
            //book.ItemInterest.ProductLine = elasticProduct.ProductLine;
            //book.ItemInterest.IsComingSoon = elasticProduct.IsComingSoon;
            //if (elasticProduct.IsMarcAllowed)
            //{
            //    book.ItemInterest.MarcUrl = GenerateMarcUrl(elasticProduct.SourceItemId);
            //}

            var displayable = new Displayable();
            displayable.Item = book;
            //map to bundles
            //displayable.Bundle = elasticProduct.Bundle;
            return displayable;
        }

        //private static string GenerateMarcUrl(string sourceItemId)
        //{
        //    return ApiConfig.MarcUrl + sourceItemId;
        //}

        private static Displayable MapToEAudio(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var eAudioItem = new eAudio
            {
                Narrators = elasticProduct.Narrator,
                Duration = Convert.ToDecimal(elasticProduct.Duration),
                PreviewFile = elasticProduct.PreviewFile,
                RecordingType = elasticProduct.RecordingType,
            };
            Book book = (Book)eAudioItem;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToEBook(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var eBookItem = new eBook{ HasAttachment = elasticProduct.HasAttachment};
            Book book = (Book)eBookItem;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToCd(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var cdItem = new Cd
            {
                StockLevel = elasticProduct.StockLevel, 
                MediaCount = elasticProduct.MediaCount, 
                Narrators = elasticProduct.Narrator,
                RecordingType = elasticProduct.RecordingType,
            };
            Book book = (Book)cdItem;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToDvd(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var dvdItem = new Dvd
            {
                StockLevel = elasticProduct.StockLevel, 
                MediaCount = elasticProduct.MediaCount,
                Narrators = elasticProduct.Narrator,
                RecordingType = elasticProduct.RecordingType,
            };
            Book book = (Book)dvdItem;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToMp3(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var mp3 = new Mp3
            {
                Narrators = elasticProduct.Narrator,
                StockLevel = elasticProduct.StockLevel,
                MediaCount = elasticProduct.MediaCount,
            };
            Book book = (Book)mp3;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToPlayAway(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var playaway = new Playaway { Narrators = elasticProduct.Narrator };
            Book book = (Book)playaway;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToText(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var text = new Text();
            Book book = (Book)text;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        private static Displayable MapToLargePrint(ElasticTitleIndex elasticProduct, Displayable displayable)
        {
            var largePrint = new LargePrint {MediaCount = elasticProduct.MediaCount};
            Book book = (Book)largePrint;
            displayable = MapBookToDisplayable(elasticProduct, book);
            return displayable;
        }

        //public static Displayable MapToMagazine(this ElasticMagazineIndex elasticMagazine)
        //{
        //    var displayable = new Displayable
        //    {
        //        Item = new eMagazine
        //        {
        //            Audience = elasticMagazine.Audience,
        //            CheckoutOn = DateTime.Now.ToString(CultureInfo.InvariantCulture),
        //            Country = elasticMagazine.Country,
        //            CoverDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
        //            Description = elasticMagazine.Description,
        //            Genre = elasticMagazine.Genre,
        //            ImageUrl = elasticMagazine.ImageUrl,
        //            IssueId = elasticMagazine.IssueId,
        //            MagazineId = elasticMagazine.MagazineId,
        //            IssueTitle = elasticMagazine.IssueTitle,
        //            Language = elasticMagazine.Language,
        //            PublishedOn = elasticMagazine.PublishedOn.ToString(),
        //            Publisher = elasticMagazine.Publisher,
        //            RBZID = elasticMagazine.Id,
        //            Title = elasticMagazine.Title,
        //            Token = elasticMagazine.Token,
        //            CapLimit = elasticMagazine.CapLimit,
        //            Frequency = elasticMagazine.Frequency,
        //            Price = elasticMagazine.Price,
        //            Rating = elasticMagazine.Rating,
        //            Issn = elasticMagazine.Issn
        //        }
        //    };
        //    return displayable;
        //}

    }
}
