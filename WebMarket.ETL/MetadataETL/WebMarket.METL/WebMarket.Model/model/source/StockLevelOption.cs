// <copyright company="Recorded Books Inc" file="StockLevelOption.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;

    [Serializable]
    public enum StockLevelOption
    {
        InProduction=0,
        InStock=1,
        LimitedAvailability=2,
    }

    
}
