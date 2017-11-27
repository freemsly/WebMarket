// <copyright company="Recorded Books Inc" file="ReviewProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class ReviewProcessor : Processor<MediaTitle>
    {
        private Dictionary<string, IEnumerable<ReviewProcessorLoader.Review>> _sourceData =
            new Dictionary<string, IEnumerable<ReviewProcessorLoader.Review>>();

        public Dictionary<string, IEnumerable<ReviewProcessorLoader.Review>> SourceData
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
            item.Model.Review = new List<string>();
            if (!SourceData.ContainsKey(item.Model.ISBN)) return;
            foreach (var review in SourceData[item.Model.ISBN])
            {
                item.Model.Review.Add(review.Data);
            }
        }
    }

}
