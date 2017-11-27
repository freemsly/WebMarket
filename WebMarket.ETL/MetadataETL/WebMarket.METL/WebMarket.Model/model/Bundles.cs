// <copyright company="Recorded Books, Inc" file="Bundles.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebMarket.Model
{
    using System;

    [Serializable]
    public class Bundle 
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("itemnumber")]
        public string ItemNumber { get; set; }
        [XmlAttribute("price")]
        public decimal Price { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        
    }
}
