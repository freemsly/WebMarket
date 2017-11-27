// <copyright company="Recorded Books Inc" file="DescriptionProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class DescriptionProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, string> _sourceData = new Dictionary<string, string>();
        public IDictionary<string, string> SourceData
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
            if (SourceData.ContainsKey(item.Model.TitleId))
            {
                item.Model.Description = SourceData[item.Model.TitleId];
            }
        }
    }

}
