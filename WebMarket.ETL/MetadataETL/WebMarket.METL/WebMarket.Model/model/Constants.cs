// <copyright company="Recorded Books Inc" file="Constants.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;

    public static class Constants
    {

        public const string Index = "index";
        public const string Position = "position";
        public const string Count = "count";
        public const string Comparison = "comparison";

        public const string S3Folder = "s3";
        public const string ImageUrlSmall = "url.image.small";
        public const string ImageUrlMedium = "url.image.medium";
        public const string ImageUrlLarge = "url.image.large";
        public const string ImageUrlKey = "api.url.images";
        public const string Activation = "title.activation";
        public const string IsInactive = "state.inactive";
        public const string ImageUrls = "image.urls";
        public const string Release = "released-date";
        public const string Duration = "duration";
        public const string PreviewFile = "preview.file";
        public const string AudioFileSize = "audio.filesize";
        public const string ShortDescription = "short.description";
        public const string Description = "description";
        public const string ListPrice = "listprice";

        public static class Facets
        {
            public const string Title = "title";
            public const string TitleSummary = "title-summary";
            public const string Subtitle = "subtitle";
            public const string Fulltitle = "fulltitle";
            public const string Genre = "genre";
            public const string Author = "author";
            public const string Narrator = "narrator";
            public const string Audience = "audience";
            public const string MediaType = "media-type";
            public const string SourceId = "id";
            public const string TitleId = "title-id";
            public const string Illustrator = "illustrator";
            public const string Translator = "translator";
            public const string Id = "id";
            public const string Publisher = "publisher";
            public const string Language = "language";
            public const string Imprint = "imprint";
            public const string DigitalRights = "digital-rights";
            public const string FictionNonFiction = "fnf";
            public const string IsFiction = "is-fiction";
            public const string TermsConditions = "terms";
            public const string Series = "series";
            public const string Isbn = "isbn";
            public const string PublishedDate = "published-date";
            public const string Status = "status";
            public const string Bisac = "bisac";
            public const string Ownership = "ownership";
            public const string SalesRights = "sales-rights";
            public const string Comparison = "comparision";
            public const string HasDrm = "has-drm";
            public const string SortTitle = "sort-title";
            public const string SortAuthor = "sort-author";
            public const string TitleKeywords = "keywords-title";
            public const string CategoryKeywords = "keywords-category";
            public const string Keywords = "";
            public const string ReleaseDate = "released-on";
            public const string MetlProcess = "metl-process";
            public const string HasAttachments = "has.attachments";
            public const string IsComingSoon = "iscomingsoon";
            public const string ContentAdvisory = "contentadvisory";
            public const string HasImages = "has-images";
            public const string HasMarc = "has-marc";
            public const string MarcFile = "marcfilename";
            public const string Price = "price";
            public const string Bundles = "bundles";
            public const string IsExclusive = "isexclusive";
        }

        public static class Settings
        {
            public const string IsbnImageRootKey = "images.isbn.root";
            public const string IsbnImageDefaultKey = "images.isbn.default";
            public const string Environment = "environment";
            public const string TotalProductsCount = "totalproductscount";

            public static class Defaults
            {
                public static readonly string IsbnImageRootUrlDefault = "http://images.oneclickdigital.com/";
                public static readonly string IsbnImageDefaultUrlDefault = "http://images.oneclickdigital.com/default/RB_image_128x192.jpg";
                public const string Environment = "na";
                public const int TotalProductsCount = 300000;
            }
        }


        public static readonly string IsbnImageRootUrl;
        public static readonly string IsbnImageDefaultUrl;
        public static readonly string Environment;
        public static readonly int TotalProductsCount;

        #region static constructor

        static Constants()
        {
            try
            {

                //string isbnImageRootCandidate = ConfigurationManager.AppSettings[Constants.Settings.IsbnImageRootKey];
                string isbnImageRootCandidate = String.Empty;
                if (!String.IsNullOrWhiteSpace(isbnImageRootCandidate))
                {
                    IsbnImageRootUrl = isbnImageRootCandidate;
                }
                else
                {
                    IsbnImageRootUrl = Constants.Settings.Defaults.IsbnImageRootUrlDefault;
                }

                //string isbnImageDefaultCandidate = ConfigurationManager.AppSettings[Constants.Settings.IsbnImageDefaultKey];
                string isbnImageDefaultCandidate = String.Empty;
                if (!String.IsNullOrWhiteSpace(isbnImageDefaultCandidate))
                {
                    IsbnImageDefaultUrl = isbnImageDefaultCandidate;
                }
                else
                {
                    IsbnImageDefaultUrl = Constants.Settings.Defaults.IsbnImageDefaultUrlDefault;
                }

                //string environment = ConfigurationManager.AppSettings[Constants.Settings.Environment];
                string environment = string.Empty;
                if (!String.IsNullOrWhiteSpace(environment))
                {
                    Environment = environment;
                }
                else
                {
                    Environment = Constants.Settings.Defaults.Environment;
                }

                //var totalproductscount = ConfigurationManager.AppSettings[Constants.Settings.TotalProductsCount];
                var totalproductscount = string.Empty;
                if (!String.IsNullOrWhiteSpace(totalproductscount))
                {
                    TotalProductsCount = Convert.ToInt32(totalproductscount);
                }
                else
                {
                    TotalProductsCount = Constants.Settings.Defaults.TotalProductsCount;
                }

            }
            catch
            {
                IsbnImageRootUrl = Constants.Settings.Defaults.IsbnImageRootUrlDefault;
                IsbnImageDefaultUrl = Constants.Settings.Defaults.IsbnImageDefaultUrlDefault;

            }
        }

        #endregion


    }
}
