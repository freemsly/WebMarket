// <copyright company="Recorded Books Inc" file="ElasticOwnershipIndexMapper.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Linq;
using WebMarket.Model;

namespace WebMarket.ETL
{
    public static class ElasticOwnershipIndexMapper
    {
        public static ElasticOwnershipIndexCollection MapOwnership(this TitleIndex titleindex)
        {
            var item = new ElasticOwnershipIndexCollection
            {
                Isbn = titleindex.Isbn,
                ElasticOwnership = titleindex.Ownership.Select(t => new ElasticOwnershipIndex()
                {
                    TotalCopies = t.TotalCopies,
                    CirculationCopies = t.CirculationCopies,
                    HoldsCopies = t.HoldsCopies,
                    HoldsRatio = t.HoldsRatio,
                    ScopeId = t.ScopeId,
                    Subscriptions = t.Subscriptions,
                    Sop = t.Sop,
                    Expirations = t.Expirations,
                }).ToList()
            };

            return item;
        }
    }
}
