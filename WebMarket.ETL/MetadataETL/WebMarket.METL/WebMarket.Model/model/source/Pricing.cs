// <copyright company="Recorded Books Inc" file="Pricing.cs">
// Copyright © 2016 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using Nest;

    [Serializable]
    public class Price
    {
        public decimal Retail { get; set; }
     
        public decimal List { get; set; }
     
        public decimal Discount { get; set; }
    }

    public class Pricing
    {
        public Price Price { get; set; }
     
        public string Currency { get; set; }
    }
}
