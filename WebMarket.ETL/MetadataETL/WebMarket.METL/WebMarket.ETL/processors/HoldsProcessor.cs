// <copyright company="Recorded Books Inc" file="PublisherProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class HoldsProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, IEnumerable<HoldsProcessorLoader.IsbnHolds>> _sourceData = new Dictionary<string, IEnumerable<HoldsProcessorLoader.IsbnHolds>>();
        public IDictionary<string, IEnumerable<HoldsProcessorLoader.IsbnHolds>> SourceData
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
            item.Model.Holds = new List<Holds>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                foreach (var holds in SourceData[item.Model.ISBN])
                {
                    item.Model.Holds.Add( new Holds()
                    {
                        ScopeId = holds.ScopeId,
                        Count = holds.Count,
                    });
                }
            }
        }
    }

}
