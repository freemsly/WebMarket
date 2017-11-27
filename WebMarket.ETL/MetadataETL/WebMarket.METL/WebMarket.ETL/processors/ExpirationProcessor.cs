// <copyright company="Recorded Books Inc" file="CirculationProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class ExpirationProcessor : Processor<MediaTitle>
    {
        private IDictionary<string, IEnumerable<Expiration>> _sourceData = new Dictionary<string, IEnumerable<Expiration>>();
        public IDictionary<string, IEnumerable<Expiration>> SourceData
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
            item.Model.Expirations = new List<Expiration>();
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                foreach (var expiration in SourceData[item.Model.ISBN])
                {
                    item.Model.Expirations.Add(new Expiration()
                    {
                        TenantId = expiration.TenantId,
                        ExpiryOn = expiration.ExpiryOn,
                        Isbn = expiration.Isbn,
                    });
                }
            }
        }
    }

}
