// <copyright company="Recorded Books Inc" file="SubscriptionProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class SubscriptionProcessor : Processor<MediaTitle>
    {
        private Dictionary<string, IEnumerable<SubscriptionOwnership>> _sourceData = new Dictionary<string, IEnumerable<SubscriptionOwnership>>();
        public Dictionary<string, IEnumerable<SubscriptionOwnership>> SourceData
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
            item.Model.Subscription = new List<SubscriptionOwnership>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                foreach (var subscription in SourceData[item.Model.ISBN])
                {
                    item.Model.Subscription.Add(subscription);   
                }
            }
        }
    }

}
