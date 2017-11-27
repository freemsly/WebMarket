// <copyright company="Recorded Books, Inc" file="Subscription.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

using System;
using Nest;

namespace WebMarket.Model
{
    [Serializable]
    public class SOP
    {
        public string Id { get; set; }
     
        public string Name { get; set; }
        
     
        public int Year { get; set; }
     
        public string Group { get; set; }
     
        public string Format { get; set; }
     
        public string StockLevel { get; set; }
    }

    public class SOPMetadata : SOP
    {
     
        public string Isbn { get; set; }
     
        public int ScopeId { get; set; }
    }

}
