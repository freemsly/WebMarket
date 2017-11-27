// <copyright company="Recorded Books Inc" file="PriceProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class PriceProcessor : Processor<MediaTitle>
    {
        public Dictionary<string, IEnumerable<Pricing>> SourceData;

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                item.SimpleProperties.Add(new TypedItem(Constants.Facets.Price, SourceData[item.Model.ISBN]));
            }
        }
    }
}
