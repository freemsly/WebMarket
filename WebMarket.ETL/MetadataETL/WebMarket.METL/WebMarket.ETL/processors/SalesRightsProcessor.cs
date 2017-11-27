// <copyright company="Recorded Books Inc" file="SalesRightsProcessor.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class SalesRightsProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, IEnumerable<string>> _sourceData = new Dictionary<string, IEnumerable<string>>();
        public IDictionary<string, IEnumerable<string>> SourceData
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
            item.Model.SalesRights = new List<string>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                foreach (var salesRight in SourceData[item.Model.ISBN])
                {
                    item.Model.SalesRights.Add(salesRight);
                }
            }
        }
    }

}
