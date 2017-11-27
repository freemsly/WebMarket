// <copyright company="Recorded Books Inc" file="SeriesProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;

    public class SeriesProcessor : Processor<MediaTitle>
    {
        public SortedDictionary<int, FacetMap> FacetMaps { get; set; }

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            if (!String.IsNullOrEmpty(item.Model.Series))
            {
                item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.Series), item.Model.Series));
            }
        }
    }
}

