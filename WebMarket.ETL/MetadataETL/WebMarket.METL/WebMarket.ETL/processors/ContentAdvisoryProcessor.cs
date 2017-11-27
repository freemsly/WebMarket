// <copyright company="Recorded Books Inc" file="ContentAdvisoryProcessor.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class ContentAdvisoryProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, IEnumerable<ContentAdvisory>> _sourceData = new Dictionary<string, IEnumerable<ContentAdvisory>>();
        public IDictionary<string, IEnumerable<ContentAdvisory>> SourceData
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
            item.Model.ContentAdvisory = new List<ContentAdvisory>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                item.Model.ContentAdvisory.AddRange(SourceData[item.Model.ISBN]);
            }
        }
    }

}
