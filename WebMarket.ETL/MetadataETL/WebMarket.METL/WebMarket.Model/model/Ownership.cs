// <copyright company="Recorded Books, Inc" file="Ownership.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

using System.Collections.Generic;

namespace WebMarket.Model
{
    using System;

    [Serializable]
    public  class Ownership
    {
        //this is entity id
        public int ScopeId { get; set; }
        public int TotalCopies { get; set; }
        public int CirculationCopies { get; set; }
        public int HoldsCopies { get; set; }
        public List<SubscriptionOwnership> Subscriptions { get; set; }

        public List<SOP> Sop { get; set; }

        public List<Expiration> Expirations { get; set; }

        //will be removed in future once entityid is in platform
        public int PlatformTenantId { get; set; }

        public decimal HoldsRatio
        {
            get { return CalculateHoldsRatio(); }
        }

        public  decimal CalculateHoldsRatio()
        {
            return TotalCopies > 0 ? Decimal.Divide(HoldsCopies, TotalCopies) : 0;
        }
    }
}
