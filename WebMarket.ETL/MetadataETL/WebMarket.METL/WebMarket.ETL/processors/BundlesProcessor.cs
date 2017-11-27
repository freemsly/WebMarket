// <copyright company="Recorded Books Inc" file="BundlesProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class BundlesProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, BundlesProcessorLoader.IsbnBundles> _sourceData = new Dictionary<string, BundlesProcessorLoader.IsbnBundles>();
        public IDictionary<string, BundlesProcessorLoader.IsbnBundles> SourceData
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
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                var data = SourceData[item.Model.ISBN];
                var bundleData = new Bundle()
                {
                    Price = data.Price,
                    ItemNumber = data.ItemNumber,
                    Name = data.Name,
                    Id = data.Id,
                };
               item.SimpleProperties.Add(new TypedItem(Constants.Facets.Bundles, bundleData));
            }
        }
    }

}
