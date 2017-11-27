// <copyright company="Recorded Books Inc" file="LanguageProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;

    public class LanguageProcessor : Processor<MediaTitle>
    {
        public List<FacetMap> FacetMaps { get; set; }

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            if (!String.IsNullOrEmpty(item.Model.Language))
            {
                item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.Language), item.Model.Language));
            }
        }
    }
}
