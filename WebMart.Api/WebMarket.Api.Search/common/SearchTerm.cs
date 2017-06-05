// <copyright company="Recorded Books, Inc" file="SearchTerm.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

using WebMarket.Common;

namespace WebMarket.Api.Search
{

    public static class SearchTerm
    {
        public static string[] FacetList =
        {
            SearchConstants.MediaType,
            SearchConstants.DigitalRightsManagement,
            SearchConstants.Genre,
            SearchConstants.Audience,
            SearchConstants.Language,
            SearchConstants.UsageTerm,
            SearchConstants.Subscription,
            SearchConstants.GroupId,
            SearchConstants.Sop,
            SearchConstants.LibraryId,
            SearchConstants.CountryCode,
            SearchConstants.Isbn,
            SearchConstants.SourceItemId,
            SearchConstants.IsComingSoon,
            SearchConstants.FictionNonfiction,
            SearchConstants.Duration,
            SearchConstants.PublishedDate,
            SearchConstants.ReleaseDate,
            SearchConstants.MediaCount,
            SearchConstants.RecordingType,
            SearchConstants.Publisher,
            //SearchConstants.Imprint,
            SearchConstants.HoldsRatio,
            SearchConstants.ListPrice,
            SearchConstants.Bundle,
            SearchConstants.Facets.ContentAdvisoryLanguage,
            SearchConstants.Facets.ContentAdvisorySex,
            SearchConstants.Facets.ContentAdvisoryViolence,
        };

        public static string[] CaseSensitiveFacetList =
        {
            SearchConstants.SourceItemId,
        };

        public static string[] QueryList =
        {
            SearchConstants.Title,
            SearchConstants.SubTitle,
            SearchConstants.Author,
            
            SearchConstants.Imprint,
            SearchConstants.Keywords,
            SearchConstants.Series,
            SearchConstants.Narrator,
        };

        public static string[] SortByList =
        {
            SearchConstants.Title,
            SearchConstants.Author,
            SearchConstants.Publisher,
            SearchConstants.PublishedDate,
            SearchConstants.ReleaseDate,
            SearchConstants.Narrator,
            SearchConstants.ListPrice,

            SearchConstants.SystemCirculationCopies,
            SearchConstants.SystemHoldsCopies,
            SearchConstants.SystemTotalCopies,
        };

        public static string[] SuggestiveList =
        {
            SearchConstants.Title,
            SearchConstants.Author,
            SearchConstants.Publisher,
            SearchConstants.Imprint,
            SearchConstants.Series,
            SearchConstants.Genre,
            SearchConstants.Audience,
            SearchConstants.Language,
            SearchConstants.Narrator,
            SearchConstants.UsageTerm,
            SearchConstants.Subscription,
            SearchConstants.Sop,
        };
    }
}

