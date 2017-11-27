// <copyright company="Recorded Books Inc" file="CirculationProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class CirculationProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, IEnumerable<CirculationProcessorLoader.IsbnCirculation>> _sourceData = new Dictionary<string, IEnumerable<CirculationProcessorLoader.IsbnCirculation>>();
        public IDictionary<string, IEnumerable<CirculationProcessorLoader.IsbnCirculation>> SourceData
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
            item.Model.Circulations = new List<Circulation>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                foreach (var circulation in SourceData[item.Model.ISBN])
                {
                    item.Model.Circulations.Add(new Circulation()
                    {
                        ScopeId = circulation.ScopeId,
                        Count = circulation.Count,
                    });
                }
            }
        }
    }

}
