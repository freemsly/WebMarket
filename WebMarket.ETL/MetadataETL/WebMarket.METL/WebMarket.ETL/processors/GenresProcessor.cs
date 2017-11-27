// <copyright company="Recorded Books Inc" file="GenresProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;

    public class GenresProcessor : Processor<MediaTitle>
    {
        private Dictionary<string, string> _sourceData  = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _capitalizeMap = new Dictionary<string, string> {
            {"lgbt-interest", "LGBT Interest" } };
        private readonly Dictionary<string, string> _pluralPossesiveMap = new Dictionary<string, string>() {
            {"womens-fiction","women's-fiction" }
        };


        public Dictionary<string, string> SourceData
        {
            get { return _sourceData; }

            set
            {
                if (_sourceData != null && _sourceData != value)
                {
                    _sourceData = value;
                }
            }
        } 

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            HashSet<string> hs = new HashSet<string>();
            int i = 1;
            if (SourceData.ContainsKey(item.Model.TitleId))
            {
                string[] bisacgenres = SourceData[item.Model.TitleId].Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string bisacgenre in bisacgenres)
                {
                    if (hs.Add(bisacgenre.Trim()))
                    {
                        item.TokenProperties.Add(new Genre()
                        {
                            Order = i++,
                            Text =  String.Intern(ParseAndResolve(bisacgenre)),
                            Facet = String.Intern(Constants.Facets.Genre),
                            Token = String.Intern(bisacgenre)
                        });
                    }
                }
            }
            else
            {
                item.TokenProperties.Add(new Genre()
                {
                    Order = i++,
                    Text = String.Intern("General Fiction"),
                    Facet = String.Intern(Constants.Facets.Genre),
                    Token = String.Intern("general-fiction")
                });
            }
        }

        private string ParseAndResolve(string genreToken)
        {
            var parsedGenre = MapPluralPossesiveString(genreToken);
            string capitalize;
            return CapitalizeAcronym(genreToken, out capitalize) ? capitalize : Resolve(parsedGenre);
        }

        private string MapPluralPossesiveString(string genreToken)
        {
            var parsedGenre = genreToken;
            if (_pluralPossesiveMap.ContainsKey(genreToken.ToLower().Trim()))
            {
                parsedGenre = _pluralPossesiveMap[genreToken.ToLower().Trim()];
            }
            return parsedGenre;
        }

        private bool CapitalizeAcronym(string genreToken, out string capitalize)
        {
            if (_capitalizeMap.ContainsKey(genreToken.ToLower().Trim()))
            {
                {
                    capitalize = _capitalizeMap[genreToken.ToLower().Trim()];
                    return true;
                }
            }
            capitalize = null;
            return false;
        }

        private string Resolve(string genreToken)
        {
            return ToTitleCase(genreToken.Replace('-', ' '));
        }
        
    }
}

