// <copyright company="Recorded Books Inc" file="PopularKeywordsProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{

    public class PopularKeywordsProcessor : Processor<MediaTitle>
    {
        private HashSet<string> _sourceData = new HashSet<string>();
        public HashSet<string> SourceData
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
            var keywords = SourceData.Where(text => item.Model.Title.ToLower().StartsWith(text.ToLower()) && item.Model.Title.ToLower().Contains(text.ToLower())).ToList();

            item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.Keywords), keywords));
        }
    }
}
