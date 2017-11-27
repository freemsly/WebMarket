// <copyright company="Recorded Books Inc" file="SOPProcessor.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class SOPProcessor : Processor<MediaTitle>
    {
        private Dictionary<string, IEnumerable<SOPMetadata>> _sourceData = new Dictionary<string, IEnumerable<SOPMetadata>>();
        public Dictionary<string, IEnumerable<SOPMetadata>> SourceData
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
            item.Model.SOP = new List<SOPMetadata>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                foreach (var sop in SourceData[item.Model.ISBN])
                {
                    item.Model.SOP.Add(sop);
                }
            }
        }
    }

}
