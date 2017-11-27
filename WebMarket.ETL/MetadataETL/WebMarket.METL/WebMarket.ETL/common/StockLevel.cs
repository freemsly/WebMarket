// <copyright company="Recorded Books, Inc" file="StockLevel.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

using WebMarket.Model;

namespace WebMarket.ETL
{
    public static class StockLevel
    {
        public static StockLevelOption Calculate(int stockLevel)
        {
            if (stockLevel > 0)
            {
                 return stockLevel >= 5 ? StockLevelOption.InStock : StockLevelOption.LimitedAvailability;
            }
            return StockLevelOption.InProduction;
        }
    }
}
