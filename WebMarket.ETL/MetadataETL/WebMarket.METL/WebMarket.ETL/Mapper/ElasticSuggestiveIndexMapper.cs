// <copyright company="Recorded Books Inc" file="ElasticSuggestiveIndexMapper.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Model;

namespace WebMarket.ETL
{
    public static class ElasticSuggestiveIndexMapper
    {
        private static readonly HashSet<string> DuplicatePublisherHashSet = new HashSet<string>();
        private static readonly HashSet<string> DuplicateKeywordHashSet = new HashSet<string>();
        public static ElasticSuggestiveIndex MapSuggestive(this ElasticTitleIndex titleIndex)
        {
            var item = new ElasticSuggestiveIndex
            {
                Title = new Suggest()
                {
                    Input = ParseTitle(titleIndex.Title),
                    Output = titleIndex.Title
                },
                Author = titleIndex.Author,
                Genre = titleIndex.Genres,
                Imprints = titleIndex.Imprint,
                Narrator = titleIndex.Narrator,
                Publisher = DuplicatePublisherHashSet.Add(titleIndex.Publisher) ? titleIndex.Publisher : string.Empty,
                Series = titleIndex.Series,
                UsageTerms = titleIndex.UsageTerms,
                Subscriptions = titleIndex.Subscriptions,
                Sop = titleIndex.SOP.Select(t => t.Name).ToList(),
                Keywords = ParseKeywords(titleIndex.PopularKeywords)
            };

            return item;
        }

        private static List<string> ParseKeywords(List<string> keywords)
        {
            var parsedKeywords = new List<string>();
            if (keywords == null || keywords.Count <= 0) return parsedKeywords;
            if (DuplicateKeywordHashSet.Contains(keywords.First(), StringComparer.CurrentCultureIgnoreCase))
                return parsedKeywords;
            DuplicateKeywordHashSet.Add(keywords.First());
            parsedKeywords = keywords;
            return parsedKeywords;
        }

        private static List<string> ParseTitle(string title)
        {
            var titles = new List<string>();
            var parsedTitle = RemoveStopWordsFromStart(title);
            if (!title.Equals(parsedTitle, StringComparison.CurrentCultureIgnoreCase))
            {
                titles.Add(parsedTitle);
            }
            titles.Add(title);
            return titles;
        }

        private static string RemoveStopWordsFromStart(string stringToClean)
        {
            var stopwords = new[] {"the ", "a ", "this "};
            foreach (var stopword in stopwords)
            {
                if (stringToClean.StartsWith(stopword, StringComparison.CurrentCultureIgnoreCase))
                {
                    stringToClean = stringToClean.Remove(0, stopword.Length).TrimStart();
                    break;
                }
            }
            return stringToClean;
        }
    }
}
