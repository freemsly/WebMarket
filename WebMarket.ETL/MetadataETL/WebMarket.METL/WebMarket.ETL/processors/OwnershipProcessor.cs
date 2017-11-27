// <copyright company="Recorded Books Inc" file="OwnershipProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public class OwnershipProcessor : Processor<MediaTitle>
    {

        private IDictionary<string, IEnumerable<OwnershipProcessorLoader.IsbnOwnership>> _SourceData =
            new Dictionary<string, IEnumerable<OwnershipProcessorLoader.IsbnOwnership>>();

        public IDictionary<string, IEnumerable<OwnershipProcessorLoader.IsbnOwnership>> SourceData
        {
            get { return _SourceData; }
            set
            {
                if (_SourceData != value)
                {
                    _SourceData = value;
                }
            }
        }

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            if (SourceData.ContainsKey(item.Model.ISBN))
            {
                item.Model.Ownership = new List<Ownership>();
                foreach (var ownership in SourceData[item.Model.ISBN])
                {
                    item.Model.Ownership.Add( new Ownership()
                    {
                         CirculationCopies = ownership.CirculationCopies,
                         HoldsCopies = ownership.HoldsCopies,
                         ScopeId = ownership.ScopeId,
                         TotalCopies = ownership.TotalCopies,
                         Subscriptions = ownership.Subscriptions,
                         PlatformTenantId = ownership.PlatformTenantId,
                    });
                }
            }
        }
    }
}
